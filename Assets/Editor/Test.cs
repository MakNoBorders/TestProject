using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Test : EditorWindow
{
    public static GameObject myObject;
    public static Test editorWindow;

    [MenuItem("Test/Test")]
    public static void ShowWindow()
    {
        editorWindow = GetWindow<Test>("Testing");
    }


    private void OnGUI()
    {
        myObject = (GameObject)EditorGUILayout.ObjectField("AvatarPrefab", myObject, typeof(GameObject));
        
        if (GUILayout.Button("Test"))
        {
            //CloseWindow();
            //FileUtil.MoveFileOrDirectory("Assets"+"/"+myObject.name+".prefab", "Cube.prefab");
            //File.Move("Assets/" + myObject.name + ".prefab", Application.streamingAssetsPath + "/Cube.prefab");
            Debug.LogError(AssetDatabase.GetAssetPath(myObject));


        }
    }
}
