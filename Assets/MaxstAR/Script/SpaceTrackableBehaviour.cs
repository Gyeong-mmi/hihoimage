/*==============================================================================
Copyright 2017 Maxst, Inc. All Rights Reserved.
==============================================================================*/

using System;
using UnityEngine;
using System.IO;

namespace maxstAR
{
    public class SpaceTrackableBehaviour : AbstractSpaceTrackableBehaviour
    {
        private IntPtr meshPtr;

        [SerializeField]
        private string objFilePath;
        public string ObjFilePath
        {
            get
            {
                return objFilePath;
            }
            set
            {
                objFilePath = value;
            }
        }

        [SerializeField]
        private UnityEngine.Object objObject;
        public UnityEngine.Object ObjObject
        {
            get
            {
                return objObject;
            }
            set
            {
                objObject = value;
            }
        }

#if false
        [SerializeField]
        private string plyFilePath;
        public string PlyFilePath
        {
            get
            {
                return plyFilePath;
            }
            set
            {
                plyFilePath = value;
                //OnTrackerDataFileChanged(plyFilePath);
            }
        }

        [SerializeField]
        private UnityEngine.Object plyObject;
        public UnityEngine.Object PlyObject
        {
            get
            {
                return plyObject;
            }
            set
            {
                plyObject = value;
            }
        }

        [SerializeField]
        private string texturePath;
        public string TexturePath
        {
            get
            {
                return texturePath;
            }
            set
            {
                texturePath = value;
                //OnTrackerDataFileChanged(texturePath);
            }
        }

        [SerializeField]
        private UnityEngine.Object textureObject = null;
        public UnityEngine.Object TextureObject
        {
            get
            {
                return textureObject;
            }
            set
            {
                textureObject = value;
            }
        }
#endif

        public override void OnTrackSuccess(string id, string name, Matrix4x4 poseMatrix)
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable renderers
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = true;
            }

            // Enable colliders
            foreach (Collider component in colliderComponents)
            {
                component.enabled = true;
            }

            transform.position = MatrixUtils.PositionFromMatrix(poseMatrix);
            transform.rotation = MatrixUtils.QuaternionFromMatrix(poseMatrix);
            transform.localScale = MatrixUtils.ScaleFromMatrix(poseMatrix);
        }

        public override void OnTrackFail()
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable renderer
            foreach (Renderer component in rendererComponents)
            {
                component.enabled = false;
            }

            // Disable collider
            foreach (Collider component in colliderComponents)
            {
                component.enabled = false;
            }
        }
#if UNITY_EDITOR
        public void LoadObj()
        {
            string objPath = Path.Combine(Application.streamingAssetsPath, objFilePath);
            string error;
            {
                //file path
                if (!File.Exists(objPath))
                {
                    error = "File doesn't exist.";
                }
                else
                {
                    GameObject loadedObject = null;
                    if (loadedObject != null)
                    {
                        Destroy(loadedObject);
                    }

                    FileInfo objInfo = new FileInfo(objPath);
                    string path = objInfo.DirectoryName + "/";
                    string filename = objInfo != null ? Path.GetFileName(objInfo.Name) : "WavefrontObject.obj";
                    string filenameWithOutExtension = objInfo != null ? Path.GetFileNameWithoutExtension(objInfo.Name) : "WavefrontObject";
                    loadedObject = new GameObject(filenameWithOutExtension);
                    loadedObject.transform.localScale = new Vector3(1f, 1f, -1f);
                    ObjectLoader loader = loadedObject.AddComponent<ObjectLoader>();
                    loader.Load(path, filename);

                    loadedObject.transform.parent = transform;
                    error = string.Empty;
                }
            }

            if (!string.IsNullOrWhiteSpace(error))
            {
                GUI.color = Color.red;
                GUI.Box(new Rect(0, 64, 256 + 64, 32), error);
                GUI.color = Color.white;
            }

            if (error != string.Empty)
            {
                Debug.Log($"error: {error}");
            }
        }

#endif
    }
}