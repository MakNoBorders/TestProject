using UnityEngine;
using UnityEditor;


public class WaitWindow : EditorWindow
{
    public static EditorWindow waitWindow;
    

    public static void ShowWindow()
    {
      
        waitWindow = GetWindowWithRect(typeof(WaitWindow), new Rect(0, 0, 300, 500), true, "");
    }

    public static void Closewindow()
    {       
        waitWindow.Close();
    }
    private void OnEnable()
    {
     
    }
    private void OnGUI()
    {
       
        {
            var centeredStyle = GUI.skin.GetStyle("Header");
            centeredStyle.alignment = TextAnchor.MiddleCenter;
            centeredStyle.fontSize = 17;
            centeredStyle.fontStyle = FontStyle.Bold;
            centeredStyle.normal.textColor = new Color(0, 0, 0);            
            if(EditorScriptAssetBundles.delete)
            GUILayout.Label("Deleting..." + "\n" , centeredStyle);
            else
            GUILayout.Label("Please Wait..." + "\n" + "Your assets are being uploaded.", centeredStyle);
        }
     

    }



}
