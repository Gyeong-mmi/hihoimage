/*==============================================================================
Copyright 2017 Maxst, Inc. All Rights Reserved.
==============================================================================*/

using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using maxstAR;

public class MarkerFusionTrackerSampleForNreal : ARBehaviour
{
    private NRCollectYUV nrCollectYUV;
    private AndroidEngine androidEngine;

    private Dictionary<int, MarkerTrackerBehaviour> markerTrackableMap =
        new Dictionary<int, MarkerTrackerBehaviour>();

    public GameObject guideView;

    void Awake()
    {
        androidEngine = new AndroidEngine();
        Init();
		nrCollectYUV = new NRCollectYUV();

		AndroidRuntimePermissions.Permission[] result = AndroidRuntimePermissions.RequestPermissions("android.permission.WRITE_EXTERNAL_STORAGE", "android.permission.CAMERA");
        if (result[0] == AndroidRuntimePermissions.Permission.Granted && result[1] == AndroidRuntimePermissions.Permission.Granted)
            Debug.Log("We have all the permissions!");
        else
            Debug.Log("Some permission(s) are not granted...");
    }

	void Start()
	{
        QualitySettings.vSyncCount = 0;

        MarkerTrackerBehaviour[] markerTrackables = FindObjectsOfType<MarkerTrackerBehaviour>();

		foreach (var trackable in markerTrackables)
		{
            trackable.SetMarkerTrackerFileName(trackable.MarkerID, trackable.MarkerSize);
			markerTrackableMap.Add(trackable.MarkerID, trackable);
			Debug.Log("Trackable id: " + trackable.MarkerID);
            Debug.Log(trackable.TrackerDataFileName);
		}

		nrCollectYUV.PlayNReal();
		StartCoroutine(StartEngine());
	}

	private void AddTrackerData()
	{
		foreach (var trackable in markerTrackableMap)
		{
			if (trackable.Value.TrackerDataFileName.Length == 0)
			{
				continue;
			}

            TrackerManager.GetInstance().AddTrackerData(trackable.Value.TrackerDataFileName);

            if (trackable.Value.MarkerSize > 0)
            {
                string realSizeData = "{\"marker_fusion\":\"set_scale\",\"id\":\"" + trackable.Value.MarkerID + "\", \"scale\":" + trackable.Value.MarkerSize + "}";
                TrackerManager.GetInstance().AddTrackerData(realSizeData);
            }
        }

		TrackerManager.GetInstance().LoadTrackerData();
	}

	private void DisableAllTrackables()
	{
		foreach (var trackable in markerTrackableMap)
		{
			trackable.Value.OnTrackFail();
		}
	}

	void Update()
	{
		DisableAllTrackables();

		nrCollectYUV.UpdateFrame();

		TrackingState state = TrackerManager.GetInstance().UpdateTrackingState(1);

        if (state == null)
        {
            return;
        }

		TrackingResult trackingResult = state.GetTrackingResult();

        string recognizedID = null;
		for (int i = 0; i < trackingResult.GetCount(); i++)
		{
			Trackable trackable = trackingResult.GetTrackable(i);
            int markerId = -1;
            if (int.TryParse(trackable.GetName(), out markerId)) {
                if (markerTrackableMap.ContainsKey(markerId))
                {
                    markerTrackableMap[markerId].OnTrackSuccess(trackable.GetId(), trackable.GetName(), trackable.GetNRealPose());

                    recognizedID += trackable.GetId().ToString() + ", ";
                }
            }
		}
		Debug.Log("Recognized Marker id : " + recognizedID);
	}

    void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			nrCollectYUV.StopNReal();
			TrackerManager.GetInstance().StopTracker();
		}
		else
		{
			nrCollectYUV.PlayNReal();
			StartCoroutine(StartEngine());
		}
	}

	void OnDestroy()
	{
		markerTrackableMap.Clear();
		TrackerManager.GetInstance().StopTracker();
		TrackerManager.GetInstance().DestroyTracker();
	}

	IEnumerator StartEngine()
	{
		while (!nrCollectYUV.isReady)
		{
			yield return new WaitForEndOfFrame();
		}

		TrackerManager.GetInstance().StartTracker(TrackerManager.TRACKER_TYPE_MARKER_FUSION);
		AddTrackerData();
	}
}
