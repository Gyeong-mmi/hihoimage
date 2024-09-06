/*==============================================================================
Copyright 2017 Maxst, Inc. All Rights Reserved.
==============================================================================*/

using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace maxstAR
{
    [CustomEditor(typeof(SpaceTrackableBehaviour))]
    public class SpaceTrackableEditor : Editor
    {
        private SpaceTrackableBehaviour trackableBehaviour = null;

        public void OnEnable()
        {
            if (PrefabUtility.GetPrefabType(target) == PrefabType.Prefab)
            {
                return;
            }
        }

        public override void OnInspectorGUI()
        {
            if (PrefabUtility.GetPrefabType(target) == PrefabType.Prefab)
            {
                return;
            }

            bool isDirty = false;

            trackableBehaviour = (SpaceTrackableBehaviour)target;

            EditorGUILayout.Separator();

            StorageType oldType = trackableBehaviour.StorageType;
            StorageType newType = (StorageType)EditorGUILayout.EnumPopup("Storage type", trackableBehaviour.StorageType);

            if (oldType != newType)
            {
                trackableBehaviour.StorageType = newType;
                isDirty = true;
            }

            // mmap
            if (trackableBehaviour.StorageType == StorageType.StreamingAssets)
            {
                EditorGUILayout.Separator();
                EditorGUILayout.HelpBox("Drag&drop a *.mmap file from your project view here, and click Load Button.", MessageType.Info);
                EditorGUILayout.Separator();

                UnityEngine.Object oldDataObject = trackableBehaviour.TrackerDataFileObject;
                UnityEngine.Object newDataObject = EditorGUILayout.ObjectField(trackableBehaviour.TrackerDataFileObject, typeof(UnityEngine.Object), true);

                EditorGUILayout.Separator();
                EditorGUILayout.Separator();

                if (oldDataObject != newDataObject)
                {
                    if (newDataObject == null)
                    {
                        isDirty = true;
                        trackableBehaviour.TrackerDataFileObject = null;
                        trackableBehaviour.TrackerDataFileName = "";
                    }
                    else
                    {
                        string trackerDataFileName = AssetDatabase.GetAssetPath(newDataObject);
                        if (!trackerDataFileName.EndsWith(".mmap"))
                        {
                            Debug.Log("trackerDataFileName: " + trackerDataFileName);
                            Debug.LogError("It's not proper tracker data file!!. File's extension should be .mmap");
                        }
                        else
                        {
                            trackableBehaviour.TrackerDataFileObject = newDataObject;
                            trackableBehaviour.TrackerDataFileName = trackerDataFileName.Replace("Assets/StreamingAssets/", "");
                            isDirty = true;
                        }
                    }
                }
            }

#if false
            // mesh
            if (trackableBehaviour.StorageType == StorageType.StreamingAssets)
            {
                EditorGUILayout.Separator();
                EditorGUILayout.HelpBox("Drag&drop a *.ply file with tracking data from your project view here, and click Load Button.", MessageType.Info);
                EditorGUILayout.Separator();
                UnityEngine.Object oldDataObject = trackableBehaviour.PlyObject;
                UnityEngine.Object newDataObject = EditorGUILayout.ObjectField(trackableBehaviour.PlyObject, typeof(UnityEngine.Object), true);

                if (oldDataObject != newDataObject)
                {
                    if (newDataObject == null)
                    {
                        isDirty = true;
                        trackableBehaviour.PlyObject = null;
                        trackableBehaviour.PlyFilePath = "";
                    }
                    else
                    {
                        string dataPath = AssetDatabase.GetAssetPath(newDataObject);
                        if (!dataPath.EndsWith(".ply"))
                        {
                            Debug.Log("ply file: " + dataPath);
                            Debug.LogError("It's not proper tracker data file!!. File's extension should be .ply");
                        }
                        else
                        {
                            trackableBehaviour.PlyObject = newDataObject;
                            trackableBehaviour.PlyFilePath = dataPath.Replace("Assets/StreamingAssets/", "");
                            isDirty = true;
                        }
                    }
                }
            }

            // texture
            if (trackableBehaviour.StorageType == StorageType.StreamingAssets)
            {
                EditorGUILayout.Separator();
                EditorGUILayout.HelpBox("Drag&drop a *.png file with tracking data from your project view here, and click Load Button.", MessageType.Info);
                EditorGUILayout.Separator();
                UnityEngine.Object oldDataObject = trackableBehaviour.TextureObject;
                UnityEngine.Object newDataObject = EditorGUILayout.ObjectField(trackableBehaviour.TextureObject, typeof(UnityEngine.Object), true);

                if (oldDataObject != newDataObject)
                {
                    if (newDataObject == null)
                    {
                        isDirty = true;
                        trackableBehaviour.TextureObject = null;
                        trackableBehaviour.TexturePath = "";
                    }
                    else
                    {
                        string dataPath = AssetDatabase.GetAssetPath(newDataObject);
                        if (!dataPath.EndsWith(".png"))
                        {
                            Debug.Log("texture file: " + dataPath);
                            Debug.LogError("It's not proper tracker data file!!. File's extension should be .png");
                        }
                        else
                        {
                            trackableBehaviour.TextureObject = newDataObject;
                            trackableBehaviour.TexturePath = dataPath.Replace("Assets/StreamingAssets/", "");
                            isDirty = true;
                        }
                    }
                }

                EditorGUILayout.Separator();
            }
#endif

            // obj
            if (trackableBehaviour.StorageType == StorageType.StreamingAssets)
            {
                EditorGUILayout.Separator();
                EditorGUILayout.HelpBox("Drag&drop a *.obj file with tracking data from your project view here, and click Load Button.", MessageType.Info);
                EditorGUILayout.Separator();
                UnityEngine.Object oldDataObject = trackableBehaviour.ObjObject;
                UnityEngine.Object newDataObject = EditorGUILayout.ObjectField(trackableBehaviour.ObjObject, typeof(UnityEngine.Object), true);

                if (oldDataObject != newDataObject)
                {
                    if (newDataObject == null)
                    {
                        isDirty = true;
                        trackableBehaviour.ObjObject = null;
                        trackableBehaviour.ObjFilePath = "";
                    }
                    else
                    {
                        string dataPath = AssetDatabase.GetAssetPath(newDataObject);
                        if (!dataPath.EndsWith(".obj"))
                        {
                            Debug.Log("obj file: " + dataPath);
                            Debug.LogError("It's not proper tracker data file!!. File's extension should be .obj");
                        }
                        else
                        {
                            trackableBehaviour.ObjObject = newDataObject;
                            trackableBehaviour.ObjFilePath = dataPath.Replace("Assets/StreamingAssets/", "");
                            isDirty = true;
                        }
                    }
                }
            }

            // load
            GUILayout.FlexibleSpace();
            GUIContent content = new GUIContent("Load");

            if (GUILayout.Button(content, GUILayout.Width(100)))
            {
                LoadObj();
            }

            // set dirty
            if (GUI.changed && isDirty)
            {
                EditorUtility.SetDirty(trackableBehaviour);
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                SceneManager.Instance.SceneUpdated();
            }
        }
#if false
        private void LoadPly()
        {
            if (trackableBehaviour != null)
            {
#if UNITY_EDITOR
                trackableBehaviour.Load();
#endif
            }
        }
#endif

        private void LoadObj()
        {
            if (trackableBehaviour != null)
            {
#if UNITY_EDITOR
                trackableBehaviour.LoadObj();
#endif
            }
        }
    }
}