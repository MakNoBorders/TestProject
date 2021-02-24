using NatSuite.Examples;
using NatSuite.Sharing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AB : MonoBehaviour
{
    protected RuntimeAnimatorController animator;
    protected AnimatorOverrideController animatorOverrideController;
    GameObject main;
    public Camera sceneCamera;
    public Camera renderCamera;
    bool single = true;
    public GameObject currentAvatar;
    public static AudioClip clipAudio;
    public static AudioSource BundleAudio;
    public GameObject classObject;
    public GameObject loadingPenal;
    public GameObject ScriptComponent;
    public GameObject rawText1;
    public GameObject rawText2;
    public GameObject rawText3;
    public GameObject[] toClose;
    public GameObject Frame;
    public Sprite defaultSprite;
    private static float progress;
    public GameObject UploadingPenal;
    public RawImage preview;
    [Header("API links")]
    public static string LOGIN_API_ANGELIUM = "https://api-xana.angelium.net/api/jwt/api-token-auth-session/";
    public Text ProgressUpadateText;
    public GameObject DoneBtn;
    public GameObject VideoPenal;
    public GameObject PoseAnimationPenal;
    private bool updateValue=false;
    public GameObject CanvasObject;
    private Texture texture1;
    public GameObject SavePenal;
   public AudioSource CameraObject;
    public GameObject  closeBtn;
    public GameObject backgroundBtn;
    public static bool SaveBtnClick=false;
    public string animName;

    private void Start()
    {
        DoneBtn.GetComponent<Button>().interactable = false;
        LoginAngelium("kc143", "qwerty123");
    }

    public void LoginAngelium(string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);
        StartCoroutine(LoginCallback(form));
    }

    IEnumerator LoginCallback(WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(LOGIN_API_ANGELIUM, form))
        {
            www.timeout = 30;
            yield return www.SendWebRequest();
            if (www.isHttpError || www.isNetworkError)
            {

            }
            else
            {

                if (www != null)
                {
                    if (!string.IsNullOrEmpty(www.downloadHandler.text))
                    {
                        LoginDataModel loginData = Gods.DeserializeJSON<LoginDataModel>(www.downloadHandler.text);

                        if (!string.IsNullOrEmpty(loginData.token) || !string.IsNullOrEmpty(loginData.message))
                        {
                            PlayerPrefs.SetString(ConstantsGod.AUTH_TOKEN, loginData.token);
                            Debug.LogError("token==" + PlayerPrefs.GetString(ConstantsGod.AUTH_TOKEN));
                            UserDeatilsDataValue();
                            
                           
                        }

                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
        }
    }

    public void UserDeatilsDataValue()
    {
        //StartCoroutine(LoadingManager.Instance.ICheckInternetConnectivity((isConnected) =>
        //{
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
        }
        else
        {
            UserDataPass data = new UserDataPass();
            data.token = ConstantsGod.DEFAULT_TOKEN;
            string jsonstringtrial = Gods.SerializeJSON<UserDataPass>(data);
            StartCoroutine(IUserDetailsData(ConstantsGod.USERDETAILS, jsonstringtrial));
        }
        //}));
    }

    private IEnumerator IUserDetailsData(string uri, string json)
    {
        UnityWebRequest uwr = null;
        try
        {
            uwr = new UnityWebRequest(uri, "POST");
            uwr.SetRequestHeader("Authorization", "JWT " + PlayerPrefs.GetString(ConstantsGod.AUTH_TOKEN));
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "application/json");
            uwr.timeout = 120;
        }
        catch
        {

        }

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {

        }
        else
        {
            try
            {
                Debug.LogError("Response UserDetails Data" + uwr.downloadHandler.text.ToString().Trim());
                updateValue = true;
            }
            catch (Exception )
            {

            }
        }
    }
    public void Load(string url)
    {

        if (single)
        {

            foreach (GameObject objects in toClose)
            {
                objects.SetActive(false);
            }
            

            StartCoroutine(GetAssetBundleFromServerUrl(url));


            single = false;
        }
        else
        {
            // Destroy(main);
            sceneCamera.gameObject.SetActive(true);
            single = true;
        }
    }

    IEnumerator GetAssetBundleFromServerUrl(string BundleURL)
    {
        loadingPenal.SetActive(true);

        using (WWW www = new WWW(BundleURL))
        {
            yield return www;
            if (www.error != null)
            {
                loadingPenal.SetActive(false);
                throw new Exception("WWW download had an error:" + www.error);
            }
            else
            {
                AssetBundle assetBundle = www.assetBundle;

                if (assetBundle != null)
                {
                    GameObject[] animation = assetBundle.LoadAllAssets<GameObject>();
                    foreach (var go in animation)
                    {
                        Debug.LogError(go.name);
                        if (go.name.Equals("Animation"))
                        {
                           // ScriptComponent.GetComponent<ReplayCam>().microphoneSource.GetComponent<AudioSource>().mute = true;
                            CanvasObject.transform.GetChild(0).GetComponent<RawImage>().texture = null;
                            //toClose[5].GetComponent<Button>().interactable = false;
                            toClose[4].gameObject.SetActive(true);
                            toClose[5].gameObject.SetActive(true);
                            CanvasObject.SetActive(true);
                            loadingPenal.SetActive(false);
                            main = go.transform.gameObject;
                            //main = Instantiate(go);
                            //currentAvatar.SetActive(true);
                            animator = main.transform.GetComponent<Animator>().runtimeAnimatorController;
                          
                            clipAudio = main.transform.GetChild(0).GetComponent<AudioSource>().clip;
                           // ReplayCam.microphoneSource = main.transform.GetChild(0).GetComponent<AudioSource>();
                            //main.transform.GetChild(0).GetComponent<AudioSource>().Play();
                            classObject.transform.GetComponent<AudioSource>().clip = clipAudio;
                            classObject.transform.GetComponent<AudioSource>().Play();
                            CameraObject.clip = clipAudio;
                           // CameraObject.mute = true;
                            CameraObject.Play();
                            //AudioSource.PlayClipAtPoint(clipAudio, transform.position);
                            Vector3 temp = new Vector3(0f, -1.94f, 3.43f);
                          
                            currentAvatar.transform.position = temp;
                            currentAvatar.GetComponent<Animator>().runtimeAnimatorController = animator;
                           string animationname=currentAvatar.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).ToString();
                            Debug.Log("anim name==="+animationname);
                            //animator.Play("your animation name", -1, 0f);
                            ScriptComponent.GetComponent<ReplayCam>().StartRecording();
                            ReplayCam.stopSave = false;
                            RuntimeAnimatorController ac = currentAvatar.GetComponent<Animator>().runtimeAnimatorController;
                            float time = ac.animationClips[0].length;
                            StartCoroutine(stopClipForUpload(time));

                        }
                    }
                    GetAllCameras();
                    assetBundle.Unload(false);
                }

            }


        }
    }

    public void VideoIndex(int index)
    {
        if (index == 0)
        {
            animName = "breakdance";
        }
        else if (index==1)
        {
            animName = "sitting";
        }
        else if (index == 2)
        {
            animName = "waving";
        }
    }
    public void bgChange(int index)
    {
        texture1 = null;
        if (CanvasObject.activeSelf)
        {

            if (index == 0)
            {
                ScriptComponent.GetComponent<ReplayCam>().microphoneSource.GetComponent<AudioSource>().mute = true;
                if (!ReplayCam.stopSave)
                {
                    CameraObject.Stop();
                    ScriptComponent.GetComponent<ReplayCam>().StopRecordingForSave();
                }
                if (Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
                {
                    Directory.Delete(Application.persistentDataPath + "/UploadVideo",true);
                }
                SaveBtnClick = false;
                DoneBtn.GetComponent<Button>().interactable = false;
                StopAllCoroutines();
                CameraObject.clip = clipAudio;
               // CameraObject.mute = true;
                CameraObject.Play();
                toClose[5].GetComponent<Button>().interactable = true;
                texture1 = rawText1.GetComponent<RawImage>().texture;
                CanvasObject.transform.GetChild(0).GetComponent<RawImage>().texture = texture1;
                currentAvatar.GetComponent<Animator>().Play(animName, -1, 0f);
                ScriptComponent.GetComponent<ReplayCam>().StartRecording();
                ReplayCam.stopSave = false;
                RuntimeAnimatorController ac = currentAvatar.GetComponent<Animator>().runtimeAnimatorController;
                float time = ac.animationClips[0].length;
                StartCoroutine(stopClipForUpload(time));
            }
            else if (index == 1)
            {
                ScriptComponent.GetComponent<ReplayCam>().microphoneSource.GetComponent<AudioSource>().mute = true;
                if (!ReplayCam.stopSave)
                {
                    CameraObject.Stop();
                    ScriptComponent.GetComponent<ReplayCam>().StopRecordingForSave();
                }
                if (Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
                {
                    Directory.Delete(Application.persistentDataPath + "/UploadVideo", true);
                }
                SaveBtnClick = false;
                DoneBtn.GetComponent<Button>().interactable = false;
                StopAllCoroutines();
               CameraObject.clip = clipAudio;
                //CameraObject.mute = true;
                CameraObject.Play();
                toClose[5].GetComponent<Button>().interactable = true;
                texture1 = rawText2.GetComponent<RawImage>().texture;
                CanvasObject.transform.GetChild(0).GetComponent<RawImage>().texture = texture1;
                currentAvatar.GetComponent<Animator>().Play(animName, -1, 0f);
                ScriptComponent.GetComponent<ReplayCam>().StartRecording();
                ReplayCam.stopSave = false;
                RuntimeAnimatorController ac = currentAvatar.GetComponent<Animator>().runtimeAnimatorController;
                float time = ac.animationClips[0].length;
                StartCoroutine(stopClipForUpload(time));
            }
            else if (index == 2)
            {
                ScriptComponent.GetComponent<ReplayCam>().microphoneSource.GetComponent<AudioSource>().mute = true;
                if (!ReplayCam.stopSave)
                {
                    CameraObject.Stop();
                    ScriptComponent.GetComponent<ReplayCam>().StopRecordingForSave();
                }
                if (Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
                {
                    Directory.Delete(Application.persistentDataPath + "/UploadVideo", true);
                }
                SaveBtnClick = false;
                DoneBtn.GetComponent<Button>().interactable = false;
                StopAllCoroutines();
                CameraObject.clip = clipAudio;
                //CameraObject.mute = true;
                CameraObject.Play();
                toClose[5].GetComponent<Button>().interactable = true;
                texture1 = rawText3.GetComponent<RawImage>().texture;
                CanvasObject.transform.GetChild(0).GetComponent<RawImage>().texture = texture1;
                currentAvatar.GetComponent<Animator>().Play(animName, -1, 0f);
                ScriptComponent.GetComponent<ReplayCam>().StartRecording();
                ReplayCam.stopSave = false;
                RuntimeAnimatorController ac = currentAvatar.GetComponent<Animator>().runtimeAnimatorController;
                float time = ac.animationClips[0].length;
                StartCoroutine(stopClipForUpload(time));
            }
        }
    }
    public void uploadVideoPenal()
    {

    }

    public void saveVideo()
    {
        if (!ReplayCam.stopSave)
        {
            ScriptComponent.GetComponent<ReplayCam>().StopRecordingForSave();
        }
        
        //ReplayCam.audioInput?.Dispose();
        StopAllCoroutines();
        SaveBtnClick = true;
        closeBtn.SetActive(false);
        toClose[5].SetActive(false);
        backgroundBtn.SetActive(false);
        DoneBtn.SetActive(false);
        SavePenal.SetActive(true);
        currentAvatar.GetComponent<Animator>().Play(animName, -1, 0f);
        ScriptComponent.GetComponent<ReplayCam>().StartRecording();
        RuntimeAnimatorController ac = currentAvatar.GetComponent<Animator>().runtimeAnimatorController;
        float time = ac.animationClips[0].length;
        StartCoroutine(stopClip(time));
    }

    IEnumerator stopClipForUpload(float count)
    {
        yield return new WaitForSeconds(count);
        ScriptComponent.GetComponent<ReplayCam>().StopRecordingForSave();
        DoneBtn.GetComponent<Button>().interactable = true;
    }

    IEnumerator stopClip(float count)
    {
        yield return new WaitForSeconds(count);
        ScriptComponent.GetComponent<ReplayCam>().StopRecording();
        //SavePenal.SetActive(false);
        //toClose[5].GetComponent<Button>().interactable = false;

        closeBtn.SetActive(true);
        toClose[5].SetActive(true);
        backgroundBtn.SetActive(true);
        DoneBtn.SetActive(true);
        DoneBtn.GetComponent<Button>().interactable=true;
        SavePenal.SetActive(false);
        toClose[5].GetComponent<Button>().interactable = false;

        if (Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
        {
            Directory.Delete(Application.persistentDataPath + "/UploadVideo", true);
            currentAvatar.GetComponent<Animator>().Play(animName, -1, 0f);
            ScriptComponent.GetComponent<ReplayCam>().StartRecording();
            RuntimeAnimatorController ac = currentAvatar.GetComponent<Animator>().runtimeAnimatorController;
            float time = ac.animationClips[0].length;
            StartCoroutine(stopClipForUpload(time));
        }
        else
        {
            currentAvatar.GetComponent<Animator>().Play(animName, -1, 0f);
            ScriptComponent.GetComponent<ReplayCam>().StartRecording();
            RuntimeAnimatorController ac = currentAvatar.GetComponent<Animator>().runtimeAnimatorController;
            float time = ac.animationClips[0].length;
            StartCoroutine(stopClipForUpload(time));
        }


    }
    void GetAllCameras()
    {
        Camera[] cam = Camera.allCameras;
        foreach (Camera c in cam)
        {
            if (!c.gameObject.tag.Equals("MainCamera"))
            {
                c.gameObject.SetActive(false);
            }

        }
    }

    //public void resetAll()
    //{
    //    Debug.Log("Call");
    //    StopAllCoroutines();
    //    ScriptComponent.GetComponent<ReplayCam>().microphoneSource.GetComponent<AudioSource>().Stop();
    //    // Destroy(main.gameObject);
    //    sceneCamera.gameObject.SetActive(true);
    //    renderCamera.gameObject.SetActive(true);
    //    single = true;
    //}

    public void resetAll()
    {
        StopAllCoroutines();
        currentAvatar.GetComponent<Animator>().Play(animName, -1, 0f);
        UploadingPenal.SetActive(true);
        closeBtn.SetActive(false);
        backgroundBtn.SetActive(false);
        DoneBtn.SetActive(false);
        toClose[5].SetActive(false);
        TakeScreenshot(500, 500);
    }

    public void closeScreen()
    {
        ScriptComponent.GetComponent<ReplayCam>().microphoneSource.GetComponent<AudioSource>().Stop();
        CameraObject.Stop();
        if (Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
        {
            Directory.Delete(Application.persistentDataPath + "/UploadVideo", true);
        }
        Vector3 temp = new Vector3(0f, -1.39f, 3.43f);
        currentAvatar.transform.position = temp;
        StopAllCoroutines();
        if (Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
        {
            Directory.Delete(Application.persistentDataPath + "/UploadVideo", true);
        }
        CanvasObject.SetActive(false);
        DoneBtn.GetComponent<Button>().interactable = false;
        VideoPenal.SetActive(true);
        PoseAnimationPenal.SetActive(false);
        PoseAnimationPenal.GetComponent<Image>().enabled = true;
        Debug.Log("Call");
        UploadingPenal.SetActive(false);

       

       // Destroy(main.gameObject);
        sceneCamera.gameObject.SetActive(true);
        renderCamera.gameObject.SetActive(true);
        single = true;
        VideoDataValue();
    }

    IEnumerator resetAllAnim()
    {
       
        yield return null;
    }
    public void TakeScreenshot(int width, int height)
    {
        Texture2D tempText = new Texture2D(width, height, TextureFormat.RGBA32, false);
        Rect rect = new Rect(0, 0, width, height);
        //tempText.ReadPixels(rect, 0, 0, false);
        tempText.Apply();
        preview.texture = tempText;

        StartCoroutine(UploadVideoFile());

    }

    public void ClearBG()
    {
        Frame.gameObject.GetComponent<Image>().sprite = defaultSprite;
    }

    public void VideoDataValue()
    {
        //StartCoroutine(LoadingManager.Instance.ICheckInternetConnectivity((isConnected) =>
        //{
        if (Application.internetReachability == NetworkReachability.NotReachable)
        { 
        }
        else
        {
            VideoDataPass data = new VideoDataPass();
            data.type = "isMine";
            data.token =ConstantsGod.DEFAULT_TOKEN;
            data.limit = "500";
            data.page = "1";
          
            string jsonstringtrial = Gods.SerializeJSON<VideoDataPass>(data);
            StartCoroutine(IVideoFeedData(ConstantsGod.GETVIDEOFEED, jsonstringtrial));
        }
        //}));
    }

    private IEnumerator IVideoFeedData(string uri, string json)
    {
        UnityWebRequest uwr = null;
        try
        {
            uwr = new UnityWebRequest(uri, "POST");
            uwr.SetRequestHeader("Authorization", "JWT "+PlayerPrefs.GetString(ConstantsGod.AUTH_TOKEN));
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "application/json");
            uwr.timeout = 120;
        }
        catch
        {

        }

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {

        }
        else
        {
            try
            {
                Debug.LogError("Response video Data" + uwr.downloadHandler.text.ToString().Trim());
                GetVideoFeed bean = Gods.DeserializeJSON<GetVideoFeed>(uwr.downloadHandler.text.ToString().Trim());

                if (bean.success)
                {
                    if (bean.data.Count > 0)
                    {
                        for (int i = 0; i < bean.data.Count; i++)
                        {
                            for(int j=0;j< bean.data[i].post_file.Count; j++)
                            {
                                StartCoroutine(GetFeedVideo(bean.data[i].post_file[j].file));
                            }
                           
                        }

                    }
                    else
                    {

                    }
                }
                else
                {

                }

            }
            catch (Exception e)
            {

            }
        }
    }



    IEnumerator GetFeedVideo(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            if (!Directory.Exists(Application.persistentDataPath + "/DownloadVideo"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/" + "DownloadVideo");
            }

            File.WriteAllBytes(Application.persistentDataPath + "/DownloadVideo/"+ Path.GetFileName(url), www.downloadHandler.data);

           StopAllCoroutines();
        }
    }



    IEnumerator UploadVideoFile()
    {

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {


        }
        else
        {
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(ConstantsGod.UPLOADVIDEOPATH))/*textureImageThumbnail != null || isPC*/)
            {
                Debug.LogError("Value of==="+ PlayerPrefs.GetString(ConstantsGod.UPLOADVIDEOPATH));
                WWWForm form = new WWWForm();

                byte[] videoData = File.ReadAllBytes(Application.persistentDataPath+ "/UploadVideo/" + PlayerPrefs.GetString(ConstantsGod.UPLOADVIDEOPATH));/*VideoPathString*/
                form.AddBinaryData("post_file", videoData, PlayerPrefs.GetString(ConstantsGod.UPLOADVIDEOPATH), "video/mp4");

                using (UnityWebRequest www = UnityWebRequest.Post(ConstantsGod.UPLOADVIDEO, form))
                {
                    www.SetRequestHeader("Authorization", "JWT "+ PlayerPrefs.GetString(ConstantsGod.AUTH_TOKEN));
                    www.SetRequestHeader("post_name", PlayerPrefs.GetString(ConstantsGod.UPLOADVIDEOPATH));
                    www.SetRequestHeader("token",ConstantsGod.DEFAULT_TOKEN);
                    www.SetRequestHeader("update", "true");
                    Debug.LogError("update value==="+ updateValue.ToString());
                  
                    var oper = www.SendWebRequest();
                    while (!oper.isDone)
                    {
                        
                        if (www.isNetworkError || www.isHttpError)
                        {
                            yield break;
                        }
                        progress = www.uploadProgress*100 ;
                        ProgressUpadateText.text = progress.ToString("F0") + "%";
                        Debug.Log("TEST5"+ www.uploadProgress * 100);

                        yield return null;
                    }

                    //Debug.Log("VideoPath==="+ Application.persistentDataPath + "/" + PlayerPrefs.GetString(ConstantsGod.VIDEOPATH));
                    //Debug.Log("VideoPathFileName==="+PlayerPrefs.GetString(ConstantsGod.VIDEOPATH));
                    try
                    {
                        Debug.LogError("Response===" + www.downloadHandler.text);
                        UploadVideo UploadData = Gods.DeserializeJSON<UploadVideo>(www.downloadHandler.text);

                        if (UploadData.success)
                        {
                            CameraObject.GetComponent<AudioSource>().clip = null;
                           // Destroy(main.gameObject);
                            sceneCamera.gameObject.SetActive(true);
                            renderCamera.gameObject.SetActive(true);
                            single = true;
                            updateValue = false;
                            UploadingPenal.SetActive(false);
                            ScriptComponent.GetComponent<ReplayCam>().microphoneSource.GetComponent<AudioSource>().Stop();
                            VideoPenal.SetActive(true);
                            ProgressUpadateText.text = string.Empty;



                            closeBtn.SetActive(true);
                            backgroundBtn.SetActive(true);
                            DoneBtn.SetActive(true);
                            toClose[5].SetActive(false);

                            DoneBtn.GetComponent<Button>().interactable = false;
                            PoseAnimationPenal.SetActive(false);
                            PoseAnimationPenal.GetComponent<Image>().enabled = true;

                            StopAllCoroutines();
                            VideoDataValue();

                            if (Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
                            {
                                Directory.Delete(Application.persistentDataPath + "/UploadVideo", true);
                            }
                        }
                        else
                        {
                            UploadingPenal.SetActive(false);

                            //if (Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
                            //{
                            //    Directory.Delete(Application.persistentDataPath + "/UploadVideo", true);
                            //}
                            closeBtn.SetActive(true);
                            backgroundBtn.SetActive(true);
                            DoneBtn.SetActive(true);
                            toClose[5].SetActive(true);
                            toClose[5].GetComponent<Button>().interactable = true;
                            DoneBtn.GetComponent<Button>().interactable = true;
                            VideoDataValue();
                        }
                    }
                    catch(Exception e)
                    {
                        closeBtn.SetActive(true);
                        backgroundBtn.SetActive(true);
                        DoneBtn.SetActive(true);
                        toClose[5].SetActive(true);
                        toClose[5].GetComponent<Button>().interactable = true;
                        DoneBtn.GetComponent<Button>().interactable = true;
                    }
                   
                   
                   

                   
                }
            }
            else
            {
                if (File.Exists(Application.persistentDataPath + "/SaveVideo/" + PlayerPrefs.GetString(ConstantsGod.VIDEOPATH)))
                {
                    WWWForm form = new WWWForm();

                    byte[] videoData = File.ReadAllBytes(Application.persistentDataPath + "/SaveVideo/" + PlayerPrefs.GetString(ConstantsGod.VIDEOPATH));/*VideoPathString*/
                    form.AddBinaryData("post_file", videoData, PlayerPrefs.GetString(ConstantsGod.VIDEOPATH), "video/mp4");

                    using (UnityWebRequest www = UnityWebRequest.Post(ConstantsGod.UPLOADVIDEO, form))
                    {
                        www.SetRequestHeader("Authorization", "JWT " + PlayerPrefs.GetString(ConstantsGod.AUTH_TOKEN));
                        www.SetRequestHeader("post_name", PlayerPrefs.GetString(ConstantsGod.VIDEOPATH));
                        www.SetRequestHeader("token", ConstantsGod.DEFAULT_TOKEN);
                        www.SetRequestHeader("update", "true");
                        Debug.LogError("update value===" + updateValue.ToString());

                        var oper = www.SendWebRequest();
                        while (!oper.isDone)
                        {

                            if (www.isNetworkError || www.isHttpError)
                            {
                                yield break;
                            }
                            progress = www.uploadProgress * 100;
                            ProgressUpadateText.text = progress.ToString("F0") + "%";
                            Debug.Log("TEST5" + www.uploadProgress * 100);

                            yield return null;
                        }

                        //Debug.Log("VideoPath==="+ Application.persistentDataPath + "/" + PlayerPrefs.GetString(ConstantsGod.VIDEOPATH));
                        //Debug.Log("VideoPathFileName==="+PlayerPrefs.GetString(ConstantsGod.VIDEOPATH));

                        try
                        {
                            Debug.LogError("Response===" + www.downloadHandler.text);

                            UploadVideo UploadData = Gods.DeserializeJSON<UploadVideo>(www.downloadHandler.text);
                            if (UploadData.success)
                            {
                                CameraObject.GetComponent<AudioSource>().clip = null;
                               // Destroy(main.gameObject);

                                updateValue = false;
                                UploadingPenal.SetActive(false);
                                ScriptComponent.GetComponent<ReplayCam>().microphoneSource.GetComponent<AudioSource>().Stop();
                                VideoPenal.SetActive(true);
                                ProgressUpadateText.text = string.Empty;

                                //Destroy(main.gameObject);
                                sceneCamera.gameObject.SetActive(true);
                                renderCamera.gameObject.SetActive(true);
                                single = true;

                                closeBtn.SetActive(true);
                                backgroundBtn.SetActive(true);
                                DoneBtn.SetActive(true);
                                toClose[5].SetActive(false);

                                DoneBtn.GetComponent<Button>().interactable = false;
                                PoseAnimationPenal.SetActive(false);
                                PoseAnimationPenal.GetComponent<Image>().enabled = true;
                                StopAllCoroutines();
                                Vector3 temp = new Vector3(0f, -1.39f, 3.43f);
                                currentAvatar.transform.position = temp;
                                VideoDataValue();

                                if (Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
                                {
                                    Directory.Delete(Application.persistentDataPath + "/UploadVideo", true);
                                }
                            }
                            else
                            {
                                UploadingPenal.SetActive(false);
                                //if (Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
                                //{
                                //    Directory.Delete(Application.persistentDataPath + "/UploadVideo", true);
                                //}
                                closeBtn.SetActive(true);
                                backgroundBtn.SetActive(true);
                                DoneBtn.SetActive(true);
                                toClose[5].SetActive(true);
                                toClose[5].GetComponent<Button>().interactable = true;
                                DoneBtn.GetComponent<Button>().interactable = true;
                                VideoDataValue();
                            }
                        }
                        catch(Exception e)
                        {
                            closeBtn.SetActive(true);
                            backgroundBtn.SetActive(true);
                            DoneBtn.SetActive(true);
                            toClose[5].SetActive(true);
                            toClose[5].GetComponent<Button>().interactable = true;
                            DoneBtn.GetComponent<Button>().interactable = true;
                        }
                    }
                }
            }


        }
       
    }


    //GetVideoFeed Data Pass Value

    public class VideoDataPass
    {
        public string token;
        public string page;
        public string limit;
        public string type;
    } 
    public class UserDataPass
    {
        public string token;
    }




///Model Class for Serialization& De-serialization Login Data

[Serializable]
public class LoginDataModel
{
    public string token;
    public string message;
}

///Model Class for Serialization& De-serialization Upload Video
[Serializable]
public class UploadVideo
    {
        public bool success { get; set; }
        public string data { get; set; }
    }

///Model Class for Serialization& De-serialization Download Video
[Serializable]
public class VideoFile
    {
        public string file { get; set; }
        public string upload_timestamp { get; set; }
    }
[Serializable]
public class FeedData
    {
        public string _id { get; set; }
        public string username_db { get; set; }
        public List<VideoFile> post_file { get; set; }
    }
[Serializable]
public class GetVideoFeed
    {
        public bool success { get; set; }
        public List<FeedData> data { get; set; }
    }
}
