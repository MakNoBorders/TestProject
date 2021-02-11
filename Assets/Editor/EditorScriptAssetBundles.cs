using UnityEngine;
using UnityEditor;
using System;

public class EditorScriptAssetBundles : EditorWindow
{
    public static GameObject myObject;
    public static string avatarName = "hassan177";
    public static string email;
    public static string password;
    private GameObject spawnPoint;
    public string[] options = new string[] { "Android", "StandaloneWindows", "IOS", "All" };
    public int index = 0;
    public static int platform;
    private string assetPath;
    public static EditorScriptAssetBundles editorWindow;
    public static float value;
    public static bool delete = false;
   

    [MenuItem("Tools/AssetBundle")]
    public static void ShowWindow()
    {
        editorWindow =  GetWindow<EditorScriptAssetBundles>("AvatarSDK");
    }

    public static void CloseWindow()
    {
        editorWindow.Close();
    }


    private void OnGUI()
    {
       
        myObject = (GameObject)EditorGUILayout.ObjectField("AvatarPrefab", myObject, typeof(GameObject));
        GUILayout.Space(5);
        avatarName = EditorGUILayout.TextField("AvatarName", avatarName);
        GUILayout.Space(10);
        EditorGUILayout.LabelField ("BuildTarget");
        index = EditorGUILayout.Popup(index, options);
        platform = index;
        GUILayout.Space(15);
        if (GUILayout.Button("Generate"))
        {
            GetYourAsset();
        }
        GUILayout.Space(2.5f);
        void GetYourAsset()
        {
            {
            
                spawnPoint = null;
                foreach (Transform child in myObject.transform)
                {
                    if (child.name == "SpawnPoint")
                    {                       
                        spawnPoint = child.gameObject;
                    }

                }
                
                {
                    Debug.Log("Making bundle");
                    switch (index)
                    {
                        case 0:
                           
                            assetPath = AssetDatabase.GetAssetPath(myObject.transform);
                            AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(avatarName + ".android", "");
                            BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
                            break;
                        case 1:
                          
                            assetPath = AssetDatabase.GetAssetPath(myObject.transform);
                            AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(avatarName + ".standalone", "");
                            BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
                            break;
                        case 2:
                            
                            assetPath = AssetDatabase.GetAssetPath(myObject.transform);
                            AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(avatarName + ".ios", "");
                            BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.iOS);
                            break;
                        case 3:
                            assetPath = AssetDatabase.GetAssetPath(myObject.transform);
                            AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(avatarName + ".ios", "");
                            BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.iOS);                                                     
                            assetPath = AssetDatabase.GetAssetPath(myObject.transform);
                            AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(avatarName + ".standalone", "");
                            BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows);                           
                            assetPath = AssetDatabase.GetAssetPath(myObject.transform);
                            AssetImporter.GetAtPath(assetPath).SetAssetBundleNameAndVariant(avatarName + ".android", "");
                            BuildPipeline.BuildAssetBundles("Assets/StreamingAssets", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
                            break;
                            
                    }
                }
            }

        }


        if (GUILayout.Button("Upload"))
        {
            CloseWindow();
            LoginEditorScript.ShowWindow();
          
        }
        GUILayout.Space(2.5f);
        
        if (GUILayout.Button("Delete"))
        {
            CloseWindow();
            LoginEditorScript.ShowWindow();
            delete = true;
          
        }


    }


}

