/*==============================================================================
Copyright 2017 Maxst, Inc. All Rights Reserved.
==============================================================================*/

using UnityEngine;
using System.Collections;

public class HomeSceneManagerForNreal : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void OnImageFusionTrackerForNrealClick()
    {
        SceneStackManager.Instance.LoadScene("HomeForNreal", "ImageFusionTrackerForNreal");
    }

    public void OnMarkerFusionTrackerForNrealClick()
    {
        SceneStackManager.Instance.LoadScene("HomeForNreal", "MarkerFusionTrackerForNreal");
    }

    public void OnQRCodeFusionTrackerForNrealClick()
    {
        SceneStackManager.Instance.LoadScene("HomeForNreal", "QRCodeFusionTrackerForNreal");
    }

    public void OnObjectFusionTrackerForNrealClick()
    {
        SceneStackManager.Instance.LoadScene("HomeForNreal", "ObjectFusionTrackerForNreal");
    }

    public void OnSpaceTrackerForNrealClick()
    {
        SceneStackManager.Instance.LoadScene("HomeForNreal", "SpaceTrackerForNreal");
    }
}