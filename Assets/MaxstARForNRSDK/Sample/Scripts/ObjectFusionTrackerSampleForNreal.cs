/*==============================================================================
Copyright 2017 Maxst, Inc. All Rights Reserved.
==============================================================================*/

using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO;
using maxstAR;
using System.Collections;

public class ObjectFusionTrackerSampleForNreal : ARBehaviour
{
    private NRCollectYUV nrCollectYUV;
    private AndroidEngine androidEngine;

    private Dictionary<string, ObjectTrackableBehaviour> objectTrackablesMap =
	new Dictionary<string, ObjectTrackableBehaviour>();

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

        objectTrackablesMap.Clear();
		ObjectTrackableBehaviour[] objectTrackables = FindObjectsOfType<ObjectTrackableBehaviour>();
		foreach (var trackable in objectTrackables)
		{
			objectTrackablesMap.Add(trackable.TrackableName, trackable);
			Debug.Log("Trackable add: " + trackable.TrackableName);
		}

        nrCollectYUV.PlayNReal();
        StartCoroutine(StartEngine());
    }

    private void AddTrackerData()
    {
        foreach (var trackable in objectTrackablesMap)
        {
            if (trackable.Value.TrackerDataFileName.Length == 0)
            {
                continue;
            }

            string realSizeData = null;

            if (trackable.Value.RealSize > 0)
            {
                realSizeData = "{\"object_fusion\":\"set_length\",\"object_name\":\"" + trackable.Value.TrackableName + "\", \"length\":" + trackable.Value.RealSize + "}";
            }

            if (trackable.Value.StorageType == StorageType.AbsolutePath)
            {
                TrackerManager.GetInstance().AddTrackerData(trackable.Value.TrackerDataFileName);
                if(realSizeData != null)
                {
                    TrackerManager.GetInstance().AddTrackerData(realSizeData);
                }
               
                TrackerManager.GetInstance().LoadTrackerData();
            }
            else if (trackable.Value.StorageType == StorageType.StreamingAssets)
            {
                if (Application.platform == RuntimePlatform.Android)
                {
                    StartCoroutine(MaxstARUtil.ExtractAssets(trackable.Value.TrackerDataFileName, (filePah) =>
                    {
                        TrackerManager.GetInstance().AddTrackerData(filePah, false);
                        if (realSizeData != null)
                        {
                            TrackerManager.GetInstance().AddTrackerData(realSizeData);
                        }
                        TrackerManager.GetInstance().LoadTrackerData();
                    }));
                }
                else
                {
                    TrackerManager.GetInstance().AddTrackerData(Application.streamingAssetsPath + "/" + trackable.Value.TrackerDataFileName);
                    if (realSizeData != null)
                    {
                        TrackerManager.GetInstance().AddTrackerData(realSizeData);
                    }
                    TrackerManager.GetInstance().LoadTrackerData();
                }
            }
        }
    }


	private void DisableAllTrackables()
	{
		foreach (var trackable in objectTrackablesMap)
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

        GuideInfo guideInfo = TrackerManager.GetInstance().GetGuideInfo();
        TagAnchor[] anchors = guideInfo.GetTagAnchors();

        int trackingCount = trackingResult.GetCount();

        for (int i = 0; i < trackingResult.GetCount(); i++)
		{
			Trackable trackable = trackingResult.GetTrackable(i);

			if (!objectTrackablesMap.ContainsKey(trackable.GetName()))
			{
				return;
			}

			objectTrackablesMap[trackable.GetName()].OnTrackSuccess(trackable.GetId(), trackable.GetName(),
																   trackable.GetNRealPose());
		}
	}

	void OnApplicationPause(bool pause)
	{
        if (pause)
        {
            TrackerManager.GetInstance().StopTracker();
            nrCollectYUV.StopNReal();
        }
        else
        {
            nrCollectYUV.PlayNReal();
            StartCoroutine(StartEngine());
        }
    }

	void OnDestroy()
	{
		objectTrackablesMap.Clear();
		TrackerManager.GetInstance().StopTracker();
		TrackerManager.GetInstance().DestroyTracker();
	}

    IEnumerator StartEngine()
    {
        while (!nrCollectYUV.isReady)
        {
            yield return new WaitForEndOfFrame();
        }

        TrackerManager.GetInstance().StartTracker(TrackerManager.TRACKER_TYPE_OBJECT_FUSION);
        AddTrackerData();
    }
}