/*==============================================================================
Copyright 2017 Maxst, Inc. All Rights Reserved.roomTrackablesMap
==============================================================================*/

using UnityEngine;
using System.Collections.Generic;
using System.Text;
using NRKernal;
using maxstAR;
using System.IO;
using System.Collections;

public class SpaceTrackerSampleForNreal : ARBehaviour
{
    private NRCollectYUV nrCollectYUV;
    private AndroidEngine androidEngine;

    private Dictionary<string, SpaceTrackableBehaviour> spaceTrackablesMap = new Dictionary<string, SpaceTrackableBehaviour>();


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

        spaceTrackablesMap.Clear();
        SpaceTrackableBehaviour[] RoomTrackables = FindObjectsOfType<SpaceTrackableBehaviour>();
        foreach (var trackable in RoomTrackables)
        {
            spaceTrackablesMap.Add(trackable.TrackableName, trackable);
            Debug.Log("Trackable add: " + trackable.TrackableName);
        }

        nrCollectYUV.PlayNReal();
        StartCoroutine(StartEngine());
    }

    private void AddTrackerData()
    {
        foreach (var trackable in spaceTrackablesMap)
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
                    Debug.Log(Application.streamingAssetsPath + "/" + trackable.Value.TrackerDataFileName);
                    TrackerManager.GetInstance().AddTrackerData(Application.streamingAssetsPath + "/" + trackable.Value.TrackerDataFileName);
                    TrackerManager.GetInstance().LoadTrackerData();
                }
            }
        }
    }

    private void DisableAllTrackables()
    {
        foreach (var trackable in spaceTrackablesMap)
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

        for (int i = 0; i < trackingResult.GetCount(); i++)
        {
            Trackable trackable = trackingResult.GetTrackable(i);

            if (!spaceTrackablesMap.ContainsKey(trackable.GetName()))
            {
                return;
            }

            SpaceTrackableBehaviour spaceTrackableBehaviour = spaceTrackablesMap[trackable.GetName()];
            spaceTrackablesMap[trackable.GetName()].OnTrackSuccess(trackable.GetId(), trackable.GetName(), trackable.GetSpaceNRealPose());
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
        spaceTrackablesMap.Clear();
        TrackerManager.GetInstance().StopTracker();
        TrackerManager.GetInstance().DestroyTracker();
    }

    IEnumerator StartEngine()
    {
        while(!nrCollectYUV.isReady)
        {
            yield return new WaitForEndOfFrame();
        }

        TrackerManager.GetInstance().StartTracker(TrackerManager.TRACKER_TYPE_SPACE);
        AddTrackerData();
    }
}