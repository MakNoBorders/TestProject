using UnityEngine;
using UnityEditor;
using System.Threading.Tasks;

public class LoginEditorScript : EditorWindow
{

    public static string password = "hassan177";
    public static string email = "hassan";
    public static LoginEditorScript loginWindow;

    
    public static void ShowWindow()
    {
        loginWindow = GetWindow<LoginEditorScript>("UserLogin");
    }

    public static void Closewindow()
    {
        loginWindow.Close();
    }

    private void OnGUI()
    {
        email = EditorGUILayout.TextField("Email", email);
        password = EditorGUILayout.TextField("Password", password);

        if (GUILayout.Button("Login"))
        {         
            APIsIntegration.CallGetToken();            
        }

    }
  


}
