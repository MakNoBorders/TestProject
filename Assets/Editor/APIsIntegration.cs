using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using SimpleJSON;
using System.Threading.Tasks;
using UnityEditor;

public class APIsIntegration : MonoBehaviour
{
    public static string token;
    private string url = "https://angeluim-metaverse.s3-ap-southeast-1.amazonaws.com/unitydata/Environment/EventEnv/Auditorium/android";
    private static string andPath;
    private static string stdPath;
    private static string iosPath;
    private static string modelPath;
    private static string thumbnailPath;
   
    public EditorScriptAssetBundles instance;

    public static void CallUploadAll(string thumbnail, string androidFile, string IOSFile, string standaloneFile, string modelFile)
    {
        WWWForm form = new WWWForm();

        form.AddField("thumbnail", thumbnail);
        form.AddField("android_file", androidFile);
        form.AddField("ios_file", IOSFile);
        form.AddField("standalone_file", standaloneFile);
        form.AddField("model_file", modelFile);

        byte[] image = File.ReadAllBytes(thumbnail);
        form.AddBinaryData("thumbnail", image);

        byte[] ios = File.ReadAllBytes(IOSFile);
        form.AddBinaryData("ios_file", ios, EditorScriptAssetBundles.avatarName +".ios");

        byte[] std = File.ReadAllBytes(standaloneFile);
        form.AddBinaryData("standalone_file", std, EditorScriptAssetBundles.avatarName + ".standalone");

        byte[] and = File.ReadAllBytes(androidFile);
        form.AddBinaryData("android_file", and, EditorScriptAssetBundles.avatarName + ".android");

        byte[] model = File.ReadAllBytes(modelFile);
        form.AddBinaryData("model_file", model, EditorScriptAssetBundles.avatarName + ".fbx");

        using (UnityWebRequest UploadRequest = UnityWebRequest.Post("http://3.16.76.210:8000/xanaEvent/uploadAvatar", form))
        {
            UploadRequest.SetRequestHeader("token", "piyush55");
            UploadRequest.SetRequestHeader("avatar_name", EditorScriptAssetBundles.avatarName);
            UploadRequest.SetRequestHeader("Authorization", "JWT " + token);
            //UploadRequest.SetRequestHeader("update", "true");
            //UploadRequest.SetRequestHeader("user_limit", "200");
            //UploadRequest.SetRequestHeader("event_listing", "public");
            //UploadRequest.timeout = 120;
            UploadRequest.SendWebRequest();
            while (!UploadRequest.isDone)
            {
               
            }
            if (UploadRequest.isNetworkError || UploadRequest.isHttpError)
            {
                JSONObject jsonObject = (JSONObject)JSON.Parse(UploadRequest.downloadHandler.text);
                string data = jsonObject["data"];
                EditorUtility.DisplayDialog("Upload Status", data, "Close");
            }
            else
            {
                if (UploadRequest.isDone)
                {
                    JSONObject jsonObject = (JSONObject)JSON.Parse(UploadRequest.downloadHandler.text);
                    string data = jsonObject["data"];
                    WaitWindow.Closewindow();
                    EditorUtility.DisplayDialog("Upload Status", data, "Close");
                }
            }
        }

    }

    public static void CallUploadAnd(string thumbnail, string androidFile, string IOSFile, string standaloneFile, string modelFile)
    {
          
        WWWForm form = new WWWForm();

        form.AddField("thumbnail", thumbnail);
        form.AddField("android_file", androidFile);
        form.AddField("model_file", modelFile);

        byte[] image = File.ReadAllBytes(thumbnail);
        form.AddBinaryData("thumbnail", image);
        byte[] and = File.ReadAllBytes(androidFile);
        form.AddBinaryData("android_file", and, EditorScriptAssetBundles.avatarName + ".android");
        byte[] model = File.ReadAllBytes(modelFile);
        form.AddBinaryData("model_file", model, EditorScriptAssetBundles.avatarName + ".fbx");


        using (UnityWebRequest UploadRequest = UnityWebRequest.Post("http://3.16.76.210:8000/xanaEvent/uploadAvatar", form))
        {
            UploadRequest.SetRequestHeader("token", "piyush55");
            UploadRequest.SetRequestHeader("avatar_name", EditorScriptAssetBundles.avatarName);
            UploadRequest.SetRequestHeader("Authorization", "JWT " + token);
            //UploadRequest.SetRequestHeader("user_limit", "200");
            //UploadRequest.SetRequestHeader("event_listing", "public");
            //UploadRequest.timeout = 120;
            UploadRequest.SendWebRequest();
            while(!UploadRequest.isDone)
            {
                
            }
            if (UploadRequest.isNetworkError || UploadRequest.isHttpError)
            {
                JSONObject jsonObject = (JSONObject)JSON.Parse(UploadRequest.downloadHandler.text);
                string data = jsonObject["data"];
                EditorUtility.DisplayDialog("Upload Status", data, "Close");
            }
            else
            {
                if (UploadRequest.isDone)
                {
                    JSONObject jsonObject = (JSONObject)JSON.Parse(UploadRequest.downloadHandler.text);
                    string data = jsonObject["data"];
                    WaitWindow.Closewindow();
                    EditorUtility.DisplayDialog("Upload Status", data, "Close");                       
                }
            }
        }

    }

    public static void CallUploadStd(string thumbnail, string androidFile, string IOSFile, string standaloneFile, string modelFile)
    {
        WWWForm form = new WWWForm();

        form.AddField("thumbnail", thumbnail);    
        form.AddField("standalone_file", standaloneFile);
        form.AddField("model_file", modelFile);

        byte[] image = File.ReadAllBytes(thumbnail);
        form.AddBinaryData("thumbnail", image);
        byte[] std = File.ReadAllBytes(standaloneFile);
        form.AddBinaryData("standalone_file", std, EditorScriptAssetBundles.avatarName + ".standalone");
        byte[] model = File.ReadAllBytes(modelFile);
        form.AddBinaryData("model_file", model, EditorScriptAssetBundles.avatarName + ".fbx");

        using (UnityWebRequest UploadRequest = UnityWebRequest.Post("http://3.16.76.210:8000/xanaEvent/uploadAvatar", form))
        {
            UploadRequest.SetRequestHeader("token", "piyush55");
            UploadRequest.SetRequestHeader("avatar_name", EditorScriptAssetBundles.avatarName);
            UploadRequest.SetRequestHeader("Authorization", "JWT " + token);
            //UploadRequest.SetRequestHeader("user_limit", "200");
            //UploadRequest.SetRequestHeader("event_listing", "public");
            //UploadRequest.timeout = 120;
            UploadRequest.SendWebRequest();
            while (!UploadRequest.isDone)
            {
               
            }
            if (UploadRequest.isNetworkError || UploadRequest.isHttpError)
            {
                JSONObject jsonObject = (JSONObject)JSON.Parse(UploadRequest.downloadHandler.text);
                string data = jsonObject["data"];
                EditorUtility.DisplayDialog("Upload Status", data, "Close");
            }
            else
            {
                if (UploadRequest.isDone)
                {
                    JSONObject jsonObject = (JSONObject)JSON.Parse(UploadRequest.downloadHandler.text);
                    string data = jsonObject["data"];
                    WaitWindow.Closewindow();
                    EditorUtility.DisplayDialog("Upload Status", data, "Close");
                }
            }
        }


        }

    public static void CallUploadIos(string thumbnail, string androidFile, string IOSFile, string standaloneFile, string modelFile)
    {
        WWWForm form = new WWWForm();

        form.AddField("thumbnail", thumbnail);
        form.AddField("ios_file", IOSFile);
        form.AddField("model_file", modelFile);

        byte[] image = File.ReadAllBytes(thumbnail);
        form.AddBinaryData("thumbnail", image);
        byte[] ios = File.ReadAllBytes(IOSFile);
        form.AddBinaryData("ios_file", ios, EditorScriptAssetBundles.avatarName + ".ios");
        byte[] model = File.ReadAllBytes(modelFile);
        form.AddBinaryData("model_file", model, EditorScriptAssetBundles.avatarName + ".fbx");

        using (UnityWebRequest UploadRequest = UnityWebRequest.Post("http://3.16.76.210:8000/xanaEvent/uploadAvatar", form))
        {
            UploadRequest.SetRequestHeader("token", "piyush55");
            UploadRequest.SetRequestHeader("avatar_name", EditorScriptAssetBundles.avatarName);
            UploadRequest.SetRequestHeader("Authorization", "JWT " + token);
            //UploadRequest.SetRequestHeader("user_limit", "200");
            //UploadRequest.SetRequestHeader("event_listing", "public");
            //UploadRequest.timeout = 120;
            UploadRequest.SendWebRequest();
            while (!UploadRequest.isDone)
            {
               
            }
            if (UploadRequest.isNetworkError || UploadRequest.isHttpError)
            {
                JSONObject jsonObject = (JSONObject)JSON.Parse(UploadRequest.downloadHandler.text);
                string data = jsonObject["data"];
                EditorUtility.DisplayDialog("Upload Status", data, "Close");
            }
            else
            {
                if (UploadRequest.isDone)
                {
                    JSONObject jsonObject = (JSONObject)JSON.Parse(UploadRequest.downloadHandler.text);
                    string data = jsonObject["data"];
                    WaitWindow.Closewindow();
                    EditorUtility.DisplayDialog("Upload Status", data, "Close");
                }
            }
        }

    }

    public static void StartUpload()
    {

        thumbnailPath = Application.streamingAssetsPath + "/" + "image.png";
        modelPath = Application.streamingAssetsPath + "/" + "model.fbx";
        if (File.Exists(Application.streamingAssetsPath + "/" + EditorScriptAssetBundles.avatarName + ".android"))
            andPath = Application.streamingAssetsPath + "/" + EditorScriptAssetBundles.avatarName + ".android";
        if (File.Exists(Application.streamingAssetsPath + "/" + EditorScriptAssetBundles.avatarName + ".ios"))
            iosPath = Application.streamingAssetsPath + "/" + EditorScriptAssetBundles.avatarName + ".ios";
        if (File.Exists(Application.streamingAssetsPath + "/" + EditorScriptAssetBundles.avatarName + ".standalone"))
            stdPath = Application.streamingAssetsPath + "/" + EditorScriptAssetBundles.avatarName + ".standalone";
        switch (EditorScriptAssetBundles.platform)
        {
            case 0:
                CallUploadAnd(thumbnailPath, andPath, iosPath, stdPath, modelPath);
                break;
            case 1:
                CallUploadStd(thumbnailPath, andPath, iosPath, stdPath, modelPath);
                break;
            case 2:
                CallUploadIos(thumbnailPath, andPath, iosPath, stdPath, modelPath);
                break;
            case 3:
                CallUploadAll(thumbnailPath, andPath, iosPath, stdPath, modelPath);
                break;
        }


    }

    public static void CallDeleteEnv()
    {
     
        WWWForm form = new WWWForm();

        form.AddField("token", "piyush55");
        form.AddField("avatar_name", EditorScriptAssetBundles.avatarName);
              

        using (UnityWebRequest UploadRequest = UnityWebRequest.Post("http://3.16.76.210:8000/xanaEvent/deleteAvatar", form))
        {
        
           
            UploadRequest.SetRequestHeader("Authorization", "JWT " + token);
            //UploadRequest.timeout = 120;
            UploadRequest.SendWebRequest();
            while (!UploadRequest.isDone)
            {
              
            }
            if (UploadRequest.isNetworkError || UploadRequest.isHttpError)
            {
                JSONObject jsonObject = (JSONObject)JSON.Parse(UploadRequest.downloadHandler.text);
                string data = jsonObject["data"];
                EditorUtility.DisplayDialog("Upload Status", data, "Close");
            }
            else
            {
                if (UploadRequest.isDone)
                {
                    WaitWindow.Closewindow();
                    EditorScriptAssetBundles.delete = false;
                    JSONObject jsonObject = (JSONObject)JSON.Parse(UploadRequest.downloadHandler.text);
                    string data = jsonObject["data"];
                    EditorUtility.DisplayDialog("Upload Status", data, "Close");
                }
            }
        }

    }

    public void CallDownloadAPI(string EnvName)

    {
        WWWForm form = new WWWForm();
        form.AddField("environment_name", EnvName);
        UnityWebRequest SendRequest = UnityWebRequest.Post("Download_API", form);
        SendRequest.SetRequestHeader("Authorization", "JWT " + token);
        SendRequest.timeout = 120;
        SendRequest.SendWebRequest();


    }

    public void StartDownload()
    {
        string EnvName = EditorScriptAssetBundles.avatarName; 
        CallDownloadAPI(EnvName);
    }

    public IEnumerator DownloadFile(string url)
    {

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
         
            var operation = www.SendWebRequest();
            while (!operation.isDone)
            {
                Debug.Log(www.downloadProgress);
                yield return null;
            }
            if (www.isHttpError || www.isNetworkError)
            {
                Debug.LogError("Network Error");
            }
            else
            {
                byte[] fileData = www.downloadHandler.data;
                string filePath = Application.persistentDataPath + "/" + EditorScriptAssetBundles.avatarName;
                File.WriteAllBytes(filePath, fileData);
                if (operation.isDone)
                {
                    Debug.Log("done");
                }

            }

        }
    }

    public static void CallGetToken()
    {
        WWWForm form = new WWWForm();

        form.AddField("email", LoginEditorScript.email);
        form.AddField("password", LoginEditorScript.password);


        using (UnityWebRequest UploadRequest = UnityWebRequest.Post("https://api-xana.angelium.net/api/jwt/api-token-auth-session/", form))
        {
           

            UploadRequest.timeout = 120;
            UploadRequest.SendWebRequest();
            while (!UploadRequest.isDone)
            {

            }
            if (UploadRequest.isNetworkError || UploadRequest.isHttpError)
            {
                Debug.LogError(UploadRequest.downloadHandler.text);
            }
            else
            {
                if (UploadRequest.isDone)
                {
                    JSONObject jsonObject = (JSONObject)JSON.Parse(UploadRequest.downloadHandler.text);
                    token = jsonObject["token"];
                    LoginEditorScript.Closewindow();
                    if (EditorScriptAssetBundles.delete)
                    {
                        WaitWindow.ShowWindow();
                        DelayUseAsyncDelete();
                       
                    }
                    else
                    {
                       WaitWindow.ShowWindow();
                       
                        UploadAsset();
                    }
                }

            }
        }

    }

    public static void UploadAsset()
    {
        var fileInfo = new System.IO.FileInfo("Assets/StreamingAssets/" + EditorScriptAssetBundles.avatarName + ".standalone");
        double length = 0;
        length = ((double)(fileInfo.Length / 1024) / 1024);
        if (length > 5)
        {
            Debug.LogError("Can't Upload, File size is larger than 5mbs");
        }
        else
        {
            DelayUseAsync();
        }
    }

    async static void DelayUseAsync()
    {
        await Task.Delay(1000);     
        StartUpload();
     
    }

    async static void DelayUseAsyncDelete()
    {
        await Task.Delay(1000);
        CallDeleteEnv();
      

    }

}

