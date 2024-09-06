/****************************************************************************
* Copyright 2019 Nreal Techonology Limited. All rights reserved.
*                                                                                                                                                          
* This file is part of NRSDK.                                                                                                          
*                                                                                                                                                           
* https://www.nreal.ai/        
* 
*****************************************************************************/

using NRKernal.Record;
using System;
using System.IO;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace NRKernal.NRExamples
{
#if UNITY_ANDROID && !UNITY_EDITOR
    using GalleryDataProvider = NativeGalleryDataProvider;
#else
    using GalleryDataProvider = MockGalleryDataProvider;
#endif
    /// <summary> A video capture 2 local example. </summary>
    [HelpURL("https://developer.nreal.ai/develop/unity/video-capture")]
    public class VideoCapture2LocalExample : MonoBehaviour
    {
        [SerializeField] private Button m_PlayButton;
        [SerializeField] private NRPreviewer m_Previewer;
        [SerializeField] private Slider m_SliderMic;
        [SerializeField] private Text m_TextMic;
        [SerializeField] private Slider m_SliderApp;
        [SerializeField] private Text m_TextApp;

        public BlendMode blendMode = BlendMode.Blend;
        public ResolutionLevel resolutionLevel;
        public LayerMask cullingMask = -1;
        public NRVideoCapture.AudioState audioState = NRVideoCapture.AudioState.ApplicationAudio;
        public bool useGreenBackGround = false;

        public enum ResolutionLevel
        {
            High,
            Middle,
            Low,
        }

        /// <summary> Save the video to Application.persistentDataPath. </summary>
        /// <value> The full pathname of the video save file. </value>
        public string VideoSavePath
        {
            get
            {
                string timeStamp = Time.time.ToString().Replace(".", "").Replace(":", "");
                string filename = string.Format("Nreal_Record_{0}.mp4", timeStamp);
                return Path.Combine(Application.persistentDataPath, filename);
            }
        }

        /// <summary> The video capture. </summary>
        NRVideoCapture m_VideoCapture = null;

        /// <summary> Starts this object. </summary>
        void Start()
        {
            CreateVideoCaptureTest();
            this.StartVideoCapture();
        }

        /// <summary> Tests create video capture. </summary>
        void CreateVideoCaptureTest()
        {
            NRVideoCapture.CreateAsync(false, delegate (NRVideoCapture videoCapture)
            {
                NRDebugger.Info("Created VideoCapture Instance!");
                if (videoCapture != null)
                {
                    m_VideoCapture = videoCapture;
                }
                else
                {
                    NRDebugger.Error("Failed to create VideoCapture Instance!");
                }
            });
        }


        public void OnClickPlay()
        {
            if (m_VideoCapture.IsRecording)
            {
                //this.StopVideoCapture();
            }
            else
            {
                //this.StartVideoCapture();
            }
        }

        void RefreshUIState()
        {
            bool flag = m_VideoCapture.IsRecording;
            m_PlayButton.GetComponent<Image>().color = flag ? Color.red : Color.green;
        }

        /// <summary> Starts video capture. </summary>
        public void StartVideoCapture()
        {
            if (m_VideoCapture != null)
            {
                CameraParameters cameraParameters = new CameraParameters();
                Resolution cameraResolution = NRVideoCapture.SupportedResolutions.OrderByDescending((res) => res.width * res.height).First();
                cameraParameters.hologramOpacity = 0.0f;
                cameraParameters.frameRate = cameraResolution.refreshRate;
                cameraParameters.cameraResolutionWidth = cameraResolution.width;
                cameraParameters.cameraResolutionHeight = cameraResolution.height;
                cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;
                // Set the blend mode.
                cameraParameters.blendMode = BlendMode.Blend;
                // Set audio state, audio record needs the permission of "android.permission.RECORD_AUDIO",
                // Add it to your "AndroidManifest.xml" file in "Assets/Plugin".
                cameraParameters.audioState = NRVideoCapture.AudioState.MicAudio;


                m_VideoCapture.StartVideoModeAsync(cameraParameters, OnStartedVideoCaptureMode);
            }
        }

        /// <summary> Stops video capture. </summary>
        public void StopVideoCapture()
        {
            if (m_VideoCapture == null)
            {
                return;
            }

            NRDebugger.Info("Stop Video Capture!");
            m_VideoCapture.StopRecordingAsync(OnStoppedRecordingVideo);
            m_Previewer.SetData(m_VideoCapture.PreviewTexture, false);
        }

        /// <summary> Executes the 'started video capture mode' action. </summary>
        /// <param name="result"> The result.</param>
        void OnStartedVideoCaptureMode(NRVideoCapture.VideoCaptureResult result)
        {
            if (!result.success)
            {
                NRDebugger.Info("Started Video Capture Mode faild!");
                return;
            }

            NRDebugger.Info("Started Video Capture Mode!");
            m_VideoCapture.StartRecordingAsync(VideoSavePath, OnStartedRecordingVideo);
            // Set preview texture.
            m_Previewer.SetData(m_VideoCapture.PreviewTexture, true);
        }

        /// <summary> Executes the 'started recording video' action. </summary>
        /// <param name="result"> The result.</param>
        void OnStartedRecordingVideo(NRVideoCapture.VideoCaptureResult result)
        {
            if (!result.success)
            {
                NRDebugger.Info("Started Recording Video Faild!");
                return;
            }

            NRDebugger.Info("Started Recording Video!");
            m_VideoCapture.GetContext().GetBehaviour().SetBackGroundColor(Color.green);
            RefreshUIState();
        }

        /// <summary> Executes the 'stopped recording video' action. </summary>
        /// <param name="result"> The result.</param>
        void OnStoppedRecordingVideo(NRVideoCapture.VideoCaptureResult result)
        {
            if (!result.success)
            {
                NRDebugger.Info("Stopped Recording Video Faild!");
                return;
            }

            NRDebugger.Info("Stopped Recording Video!");
            m_VideoCapture.StopVideoModeAsync(OnStoppedVideoCaptureMode);
        }

        /// <summary> Executes the 'stopped video capture mode' action. </summary>
        /// <param name="result"> The result.</param>
        void OnStoppedVideoCaptureMode(NRVideoCapture.VideoCaptureResult result)
        {
            NRDebugger.Info("Stopped Video Capture Mode!");
        }
    }
}
