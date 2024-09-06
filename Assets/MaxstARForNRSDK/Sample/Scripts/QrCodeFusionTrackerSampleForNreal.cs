/*==============================================================================
Copyright 2017 Maxst, Inc. All Rights Reserved.
==============================================================================*/

using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO;
using maxstAR;
using System.Collections;

public class QrCodeFusionTrackerSampleForNreal : ARBehaviour
{
    private NRCollectYUV nrCollectYUV;
    private AndroidEngine androidEngine;

    private string defaultSearchingWords = "[DEFUALT]";
    private Dictionary<string, List<QrCodeTrackableBehaviour>> QrCodeTrackablesMap =
        new Dictionary<string, List<QrCodeTrackableBehaviour>>();

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

        QrCodeTrackablesMap.Clear();
        QrCodeTrackableBehaviour[] QrCodeTrackables = FindObjectsOfType<QrCodeTrackableBehaviour>();

        if (QrCodeTrackables.Length > 0)
        {
            if (QrCodeTrackables[0].QrCodeSearchingWords.Length < 1)
            {
                List<QrCodeTrackableBehaviour> qrCodeList = new List<QrCodeTrackableBehaviour>();

                qrCodeList.Add(QrCodeTrackables[0]);
                QrCodeTrackablesMap.Add(defaultSearchingWords, qrCodeList);
            }
        }

        foreach (var trackable in QrCodeTrackables)
        {
            string key = trackable.QrCodeSearchingWords;

            if (key.Length < 1) key = defaultSearchingWords;

            if (QrCodeTrackablesMap.ContainsKey(key))
            {
                bool isNew = true;

                foreach (var QrCodeTrackableList in QrCodeTrackablesMap[key])
                {
                    if (trackable.name.Equals(QrCodeTrackableList.name))
                    {
                        isNew = false;
                        break;
                    }
                }

                if (isNew) QrCodeTrackablesMap[defaultSearchingWords].Add(trackable);
            }
            else
            {
                List<QrCodeTrackableBehaviour> qrCodeList = new List<QrCodeTrackableBehaviour>();

                qrCodeList.Add(trackable);
                QrCodeTrackablesMap.Add(key, qrCodeList);
            }

            Debug.Log("Trackable add: " + trackable.QrCodeSearchingWords);
        }

        nrCollectYUV.PlayNReal();
        StartCoroutine(StartEngine());
    }

	private void AddTrackerData()
    {
        QrCodeTrackableBehaviour[] QrCodeTrackables = FindObjectsOfType<QrCodeTrackableBehaviour>();

        if (QrCodeTrackables.Length > 0)
        {
            foreach(var trackable in QrCodeTrackables)
            {
                if (trackable.QrCodeRealSize > 0)
                {
                    string realSizeData = "{\"qr_fusion\":\"set_scale\",\"content\":\"" + trackable.QrCodeSearchingWords + "\", \"scale\":" + trackable.QrCodeRealSize + "}";
                    TrackerManager.GetInstance().AddTrackerData(realSizeData);
                }
            }
        }
        TrackerManager.GetInstance().LoadTrackerData();
    }

	private void DisableAllTrackables()
    {
        foreach (var key in QrCodeTrackablesMap.Keys)
        {
            foreach (var trackable in QrCodeTrackablesMap[key])
            {
                trackable.OnTrackFail();
            }
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

        int trackingCount = trackingResult.GetCount();

        for (int i = 0; i < trackingResult.GetCount(); i++)
		{
			Trackable trackable = trackingResult.GetTrackable(i);

            bool isNotFound = true;

            foreach (var key in QrCodeTrackablesMap.Keys)
            {
                if (key.Length < 1) continue;

                if (trackable.GetName().Contains(key))
                {
                    foreach (var qrCodeTrackable in QrCodeTrackablesMap[key])
                    {
                        qrCodeTrackable.OnTrackSuccess("", trackable.GetName(), trackable.GetNRealPose());
                    }
                    Debug.Log("Trackable add: " + trackable.GetName());

                    isNotFound = false;
                    break;
                }
            }

            if (isNotFound && QrCodeTrackablesMap.ContainsKey(defaultSearchingWords))
            {
                foreach (var qrCodeTrackable in QrCodeTrackablesMap[defaultSearchingWords])
                {
                    qrCodeTrackable.OnTrackSuccess("", trackable.GetName(), trackable.GetNRealPose());
                }
            }
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
        QrCodeTrackablesMap.Clear();
		TrackerManager.GetInstance().StopTracker();
		TrackerManager.GetInstance().DestroyTracker();
	}

    IEnumerator StartEngine()
    {
        while (!nrCollectYUV.isReady)
        {
            yield return new WaitForEndOfFrame();
        }

        TrackerManager.GetInstance().StartTracker(TrackerManager.TRACKER_TYPE_QR_FUSION);
        AddTrackerData();
    }
}