/*==============================================================================
Copyright 2017 Maxst, Inc. All Rights Reserved.
==============================================================================*/

using UnityEngine;
using System.Collections.Generic;
using maxstAR;
using System.IO;
using System.Collections;
using NRKernal;

public class ImageFusionTrackerSampleForNreal : ARBehaviour
{
	private NRCollectYUV nrCollectYUV;
	private AndroidEngine androidEngine;

	private Dictionary<string, ImageTrackableBehaviour> imageTrackablesMap =
		new Dictionary<string, ImageTrackableBehaviour>();

    public GameObject guideView;

    void Awake()
    {
		androidEngine = new AndroidEngine();
		Init();

        AndroidRuntimePermissions.Permission[] result = AndroidRuntimePermissions.RequestPermissions("android.permission.WRITE_EXTERNAL_STORAGE", "android.permission.CAMERA");
        if (result[0] == AndroidRuntimePermissions.Permission.Granted && result[1] == AndroidRuntimePermissions.Permission.Granted)
            Debug.Log("We have all the permissions!");
        else
            Debug.Log("Some permission(s) are not granted...");


		nrCollectYUV = new NRCollectYUV();
	}

	void Start()
	{
        QualitySettings.vSyncCount = 0;

		imageTrackablesMap.Clear();
		ImageTrackableBehaviour[] imageTrackables = FindObjectsOfType<ImageTrackableBehaviour>();
		foreach (var trackable in imageTrackables)
		{
			imageTrackablesMap.Add(trackable.TrackableName, trackable);
			Debug.Log("Trackable add: " + trackable.TrackableName);
		}

		nrCollectYUV.PlayNReal();
		StartCoroutine(StartEngine());
	}

	private void AddTrackerData()
	{
		foreach (var trackable in imageTrackablesMap)
		{
			if (trackable.Value.TrackerDataFileName.Length == 0)
			{
				continue;
			}

			if (trackable.Value.StorageType == StorageType.AbsolutePath)
			{
				TrackerManager.GetInstance().AddTrackerData(trackable.Value.TrackerDataFileName);
				TrackerManager.GetInstance().LoadTrackerData();
			}
			else if (trackable.Value.StorageType == StorageType.StreamingAssets)
			{
				if (Application.platform == RuntimePlatform.Android)
				{
					StartCoroutine(MaxstARUtil.ExtractAssets(trackable.Value.TrackerDataFileName, (filePah) =>
					{
						TrackerManager.GetInstance().AddTrackerData(filePah, false);
						TrackerManager.GetInstance().LoadTrackerData();
					}));
				}
				else
				{
					TrackerManager.GetInstance().AddTrackerData(Application.streamingAssetsPath + "/" + trackable.Value.TrackerDataFileName);
					TrackerManager.GetInstance().LoadTrackerData();
				}
			}
		}
	}

	private void DisableAllTrackables()
	{
		foreach (var trackable in imageTrackablesMap)
		{
			trackable.Value.OnTrackFail();
		}
	}

	void Update()
	{
		DisableAllTrackables();

		nrCollectYUV.UpdateFrame();

		maxstAR.TrackingState state = TrackerManager.GetInstance().UpdateTrackingState(1);

		if (state == null)
		{
			return;
		}

		TrackingResult trackingResult = state.GetTrackingResult();

		for(int i = 0; i < trackingResult.GetCount(); i++)
		{
			Trackable trackable = trackingResult.GetTrackable(i);
			imageTrackablesMap[trackable.GetName()].OnTrackSuccess(trackable.GetId(), trackable.GetName(), trackable.GetNRealPose());
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
		nrCollectYUV.StopNReal();
		imageTrackablesMap.Clear();
		TrackerManager.GetInstance().SetTrackingOption(TrackerManager.TrackingOption.NORMAL_TRACKING);
		TrackerManager.GetInstance().StopTracker();
		TrackerManager.GetInstance().DestroyTracker();
	}

	IEnumerator StartEngine()
	{
		while (!nrCollectYUV.isReady)
		{
			yield return new WaitForEndOfFrame();
		}

		TrackerManager.GetInstance().StartTracker(TrackerManager.TRACKER_TYPE_IMAGE_FUSION);
		AddTrackerData();
	}
}