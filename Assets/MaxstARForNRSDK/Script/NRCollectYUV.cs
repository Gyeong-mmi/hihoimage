using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NRKernal;
using System;
using System.IO;
using UnityEngine.UI;
using maxstAR;

public class NRCollectYUV : NRRGBCamTextureYUV
{
    private Matrix4x4 _rgbEyeToHeadPose;
    private bool isFirst = true;

    private float[] localPose = new float[16];
    private float[] tempPose = new float[16];
    private ulong timestamp = 0;
    private float[] tempIntrinsic = new float[4];
    public bool isReady = false;

    public NRCollectYUV()
    {
    }

    public void PlayNReal()
    {
        this.Play();
    }

    public void StopNReal()
    {
        this.Pause();
        NRSessionManager.Instance.NRHMDPoseTracker.ResetWorldMatrix();
        CameraDevice.GetInstance().Stop();
        this.isFirst = true;
        this.isReady = false;
    }

    protected override void OnRawDataUpdate(FrameRawData frame)
    {
        base.OnRawDataUpdate(frame);

        if (!NRSessionManager.Instance.IsInitialized)
        {
            Debug.Log("UpdateFrame NRSessionManager.Instance.IsInitialized false ");
            return;
        }

        if(!isReady)
        {
            isReady = true;
        }

        if (isFirst)
        {
            int imageWidth = Width;
            int imageHeight = Height;
            int scale = 1;

            if (imageWidth % 640 == 0)
                scale = imageWidth / 640;
            else if (imageWidth % 720 == 0)
                scale = imageWidth / 720;
            else
                scale = 1;

            NativeMat3f intrinsic = NRFrame.GetRGBCameraIntrinsicMatrix();
            float fx = intrinsic.column0.X / scale;
            float fy = intrinsic.column1.Y / scale;
            float px = intrinsic.column2.X / scale;
            float py = intrinsic.column2.Y / scale;

            int resized_imageWidth = (int)imageWidth / scale;
            int resized_imageHeight = (int)imageHeight / scale;

            CameraDevice.GetInstance().SetExternalCamera(true);
            CameraDevice.GetInstance().SetCalibrationDatas(resized_imageWidth, resized_imageHeight, fx, fy, px, py);
            CameraDevice.GetInstance().SetCameraSize(resized_imageWidth, resized_imageHeight);

            isFirst = false;
        }

        timestamp = frame.timeStamp;

        Pose head_pose = Pose.identity;

        bool result =  NRFrame.GetHeadPoseByTime(ref head_pose, timestamp);

        Pose eyeToHeadRgbPose = NRFrame.EyePoseFromHead.RGBEyePose;
        this._rgbEyeToHeadPose = ConvertPoseToMatrix4x4(eyeToHeadRgbPose);

        if (result)
        {
            Matrix4x4 Mwh = ConvertPoseToMatrix4x4(head_pose);
            Matrix4x4 Mhe = this._rgbEyeToHeadPose;

            Matrix4x4 Mrl = GetLeft2RightHandedMatrix();

            Matrix4x4 Mwel = Mwh * Mhe;
            Matrix4x4 Mwer = Mrl * Mwel * Mrl;

            localPose[0] = Mwer.m00;
            localPose[1] = Mwer.m10;
            localPose[2] = Mwer.m20;
            localPose[3] = Mwer.m30;

            localPose[4] = Mwer.m01;
            localPose[5] = Mwer.m11;
            localPose[6] = Mwer.m21;
            localPose[7] = Mwer.m31;

            localPose[8] = Mwer.m02;
            localPose[9] = Mwer.m12;
            localPose[10] = Mwer.m22;
            localPose[11] = Mwer.m32;

            localPose[12] = Mwer.m03;
            localPose[13] = Mwer.m13;
            localPose[14] = Mwer.m23;
            localPose[15] = Mwer.m33;

            
            NativeMat3f intrinsic = NRFrame.GetRGBCameraIntrinsicMatrix();
            float fx = intrinsic.column0.X;
            float fy = intrinsic.column1.Y;
            float px = intrinsic.column2.X;
            float py = intrinsic.column2.Y;
            tempIntrinsic[0] = fx;
            tempIntrinsic[1] = fy;
            tempIntrinsic[2] = px;
            tempIntrinsic[3] = py;

            CameraDevice.GetInstance().SetNewFrameAndPoseAndIntrinsicAndTimestamp(frame.data, frame.data.Length, Width, Height, ColorFormat.YUV420, localPose, tempIntrinsic, timestamp);

        }
    }

    public bool UpdateFrame()
    {
        if (!NRSessionManager.Instance.IsInitialized)
        {
            Debug.Log("UpdateFrame NRSessionManager.Instance.IsInitialized false ");
            return false;
        }

        if (!NRFrame.isHeadPoseReady)
        {
            return false;
        }

        LostTrackingReason lostTrackingReason = LostTrackingReason.NONE;
        Pose head_pose = Pose.identity;
        bool result = NRFrame.GetFramePresentHeadPose(ref head_pose, ref lostTrackingReason, ref timestamp);

        Pose eyeToHeadRgbPose = NRFrame.EyePoseFromHead.RGBEyePose;
        this._rgbEyeToHeadPose = ConvertPoseToMatrix4x4(eyeToHeadRgbPose);
        if (result && lostTrackingReason == LostTrackingReason.NONE)
        {
            Matrix4x4 Mwh = ConvertPoseToMatrix4x4(head_pose);
            Matrix4x4 Mhe = this._rgbEyeToHeadPose;

            Matrix4x4 Mrl = GetLeft2RightHandedMatrix();

            Matrix4x4 Mwel = Mwh * Mhe;
            Matrix4x4 Mwer = Mrl * Mwel * Mrl;


            tempPose[0] = Mwer.m00;
            tempPose[1] = Mwer.m10;
            tempPose[2] = Mwer.m20;
            tempPose[3] = Mwer.m30;

            tempPose[4] = Mwer.m01;
            tempPose[5] = Mwer.m11;
            tempPose[6] = Mwer.m21;
            tempPose[7] = Mwer.m31;

            tempPose[8] = Mwer.m02;
            tempPose[9] = Mwer.m12;
            tempPose[10] = Mwer.m22;
            tempPose[11] = Mwer.m32;

            tempPose[12] = Mwer.m03;
            tempPose[13] = Mwer.m13;
            tempPose[14] = Mwer.m23;
            tempPose[15] = Mwer.m33;

            CameraDevice.GetInstance().SetNewFusionCameraFrameAndPoseAndTimestamp(null, 0, 0, 0, ColorFormat.YUV420, tempPose, timestamp);
        }
        return true;
    }

    private static Matrix4x4 GetLeft2RightHandedMatrix()
    {
        Matrix4x4 Mc = Matrix4x4.identity;
        Mc.m22 = -Mc.m22;
        return Mc;
    }

    public static Matrix4x4 ConvertPoseToMatrix4x4(Pose pose)
    {
        Matrix4x4 result = Matrix4x4.Rotate(pose.rotation);
        result.m03 = pose.position.x;
        result.m13 = pose.position.y;
        result.m23 = pose.position.z;

        return result;
    }
}