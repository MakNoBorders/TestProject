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

public class CharacterLoadAnimation : MonoBehaviour
{
    protected RuntimeAnimatorController animator;
    protected AnimatorOverrideController animatorOverrideController;
    GameObject spawnCharacterObject;
    public Camera sceneCamera;
    public Camera renderCamera;
    bool single = true;
    public GameObject selectedGameAvatar;
    public static AudioClip backgroundClipAudio;
    public GameObject characterLoadAnimationScript;
    public GameObject loadingPenal;
    public GameObject recorderScriptComponent;
    public GameObject characterBgChangeRawImage1;
    public GameObject characterBgChangeRawImage2;
    public GameObject characterBgChangeRawImage3;
    public GameObject[] disableObjectArray;
    public GameObject characterFrame;
    public Sprite characterDefaultFrameSprite;
    private static float uploadVideoProgress;
    public GameObject UploadingPenal;
    public RawImage characterWithFramePreview;
    [Header("API links")]
    public static string LOGIN_API_ANGELIUM = "https://api-xana.angelium.net/api/jwt/api-token-auth-session/";
    public Text uploadingProgressUpadateText;
    public GameObject uploadVideoBtn;
    public GameObject animationListPenal;
    public GameObject characterPoseAnimationPenal;
    private bool apiUpdateParamValue=false;
    public GameObject assetBundleLoadCanvasObject;
    private Texture characterBackgroundChangeTexture;
    public GameObject saveVideoPenal;
   public AudioSource inGameSceneAudioSource;
    public GameObject  exitBtn;
    public GameObject backgroundChangeBtn;
    public static bool saveVideoBtnClick=false;
    public string animClipName;

    private void Start()
    {
        uploadVideoBtn.GetComponent<Button>().interactable = false;
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
                apiUpdateParamValue = true;
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

            foreach (GameObject objects in disableObjectArray)
            {
                objects.SetActive(false);
            }
            

            StartCoroutine(GetAssetBundleFromServerUrl(url));


            single = false;
        }
        else
        {
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
                            assetBundleLoadCanvasObject.transform.GetChild(0).GetComponent<RawImage>().texture = null;
                            disableObjectArray[4].gameObject.SetActive(true);
                            disableObjectArray[5].gameObject.SetActive(true);
                            assetBundleLoadCanvasObject.SetActive(true);
                            loadingPenal.SetActive(false);
                            spawnCharacterObject = go.transform.gameObject;
                            animator = spawnCharacterObject.transform.GetComponent<Animator>().runtimeAnimatorController;
                            backgroundClipAudio = spawnCharacterObject.transform.GetChild(0).GetComponent<AudioSource>().clip;
                            characterLoadAnimationScript.transform.GetComponent<AudioSource>().clip = backgroundClipAudio;
                            characterLoadAnimationScript.transform.GetComponent<AudioSource>().Play();
                            inGameSceneAudioSource.clip = backgroundClipAudio;
                            inGameSceneAudioSource.Play();
                            Vector3 temp = new Vector3(0f, -1.94f, 3.43f);
                            selectedGameAvatar.transform.position = temp;
                            selectedGameAvatar.GetComponent<Animator>().runtimeAnimatorController = animator;
                           string animationname=selectedGameAvatar.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).ToString();
                            Debug.Log("anim name==="+animationname);
                            recorderScriptComponent.GetComponent<ReplayCam>().StartRecording();
                            ReplayCam.stopSave = false;
                            RuntimeAnimatorController ac = selectedGameAvatar.GetComponent<Animator>().runtimeAnimatorController;
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
            animClipName = "breakdance";
        }
        else if (index==1)
        {
            animClipName = "sitting";
        }
        else if (index == 2)
        {
            animClipName = "waving";
        }
    }
    public void bgChange(int index)
    {
        characterBackgroundChangeTexture = null;
        if (assetBundleLoadCanvasObject.activeSelf)
        {
            if (index == 0)
            {
                recorderScriptComponent.GetComponent<ReplayCam>().microphoneSource.GetComponent<AudioSource>().mute = true;
                if (!ReplayCam.stopSave)
                {
                    inGameSceneAudioSource.Stop();
                    recorderScriptComponent.GetComponent<ReplayCam>().StopRecordingForSave();
                }
                if (Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
                {
                    Directory.Delete(Application.persistentDataPath + "/UploadVideo",true);
                }
                saveVideoBtnClick = false;
                uploadVideoBtn.GetComponent<Button>().interactable = false;
                StopAllCoroutines();
                inGameSceneAudioSource.clip = backgroundClipAudio;
                inGameSceneAudioSource.Play();
                disableObjectArray[5].GetComponent<Button>().interactable = true;
                characterBackgroundChangeTexture = characterBgChangeRawImage1.GetComponent<RawImage>().texture;
                assetBundleLoadCanvasObject.transform.GetChild(0).GetComponent<RawImage>().texture = characterBackgroundChangeTexture;
                selectedGameAvatar.GetComponent<Animator>().Play(animClipName, -1, 0f);
                recorderScriptComponent.GetComponent<ReplayCam>().StartRecording();
                ReplayCam.stopSave = false;
                RuntimeAnimatorController ac = selectedGameAvatar.GetComponent<Animator>().runtimeAnimatorController;
                float time = ac.animationClips[0].length;
                StartCoroutine(stopClipForUpload(time));
            }
            else if (index == 1)
            {
                recorderScriptComponent.GetComponent<ReplayCam>().microphoneSource.GetComponent<AudioSource>().mute = true;
                if (!ReplayCam.stopSave)
                {
                    inGameSceneAudioSource.Stop();
                    recorderScriptComponent.GetComponent<ReplayCam>().StopRecordingForSave();
                }
                if (Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
                {
                    Directory.Delete(Application.persistentDataPath + "/UploadVideo", true);
                }
                saveVideoBtnClick = false;
                uploadVideoBtn.GetComponent<Button>().interactable = false;
                StopAllCoroutines();
               inGameSceneAudioSource.clip = backgroundClipAudio;;
                inGameSceneAudioSource.Play();
                disableObjectArray[5].GetComponent<Button>().interactable = true;
                characterBackgroundChangeTexture = characterBgChangeRawImage2.GetComponent<RawImage>().texture;
                assetBundleLoadCanvasObject.transform.GetChild(0).GetComponent<RawImage>().texture = characterBackgroundChangeTexture;
                selectedGameAvatar.GetComponent<Animator>().Play(animClipName, -1, 0f);
                recorderScriptComponent.GetComponent<ReplayCam>().StartRecording();
                ReplayCam.stopSave = false;
                RuntimeAnimatorController ac = selectedGameAvatar.GetComponent<Animator>().runtimeAnimatorController;
                float time = ac.animationClips[0].length;
                StartCoroutine(stopClipForUpload(time));
            }
            else if (index == 2)
            {
                recorderScriptComponent.GetComponent<ReplayCam>().microphoneSource.GetComponent<AudioSource>().mute = true;
                if (!ReplayCam.stopSave)
                {
                    inGameSceneAudioSource.Stop();
                    recorderScriptComponent.GetComponent<ReplayCam>().StopRecordingForSave();
                }
                if (Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
                {
                    Directory.Delete(Application.persistentDataPath + "/UploadVideo", true);
                }
                saveVideoBtnClick = false;
                uploadVideoBtn.GetComponent<Button>().interactable = false;
                StopAllCoroutines();
                inGameSceneAudioSource.clip = backgroundClipAudio;
                inGameSceneAudioSource.Play();
                disableObjectArray[5].GetComponent<Button>().interactable = true;
                characterBackgroundChangeTexture = characterBgChangeRawImage3.GetComponent<RawImage>().texture;
                assetBundleLoadCanvasObject.transform.GetChild(0).GetComponent<RawImage>().texture = characterBackgroundChangeTexture;
                selectedGameAvatar.GetComponent<Animator>().Play(animClipName, -1, 0f);
                recorderScriptComponent.GetComponent<ReplayCam>().StartRecording();
                ReplayCam.stopSave = false;
                RuntimeAnimatorController ac = selectedGameAvatar.GetComponent<Animator>().runtimeAnimatorController;
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
            recorderScriptComponent.GetComponent<ReplayCam>().StopRecordingForSave();
        }
        StopAllCoroutines();
        saveVideoBtnClick = true;
        exitBtn.SetActive(false);
        disableObjectArray[5].SetActive(false);
        backgroundChangeBtn.SetActive(false);
        uploadVideoBtn.SetActive(false);
        saveVideoPenal.SetActive(true);
        selectedGameAvatar.GetComponent<Animator>().Play(animClipName, -1, 0f);
        recorderScriptComponent.GetComponent<ReplayCam>().StartRecording();
        RuntimeAnimatorController ac = selectedGameAvatar.GetComponent<Animator>().runtimeAnimatorController;
        float time = ac.animationClips[0].length;
        StartCoroutine(stopClip(time));
    }

    IEnumerator stopClipForUpload(float count)
    {
        yield return new WaitForSeconds(count);
        recorderScriptComponent.GetComponent<ReplayCam>().StopRecordingForSave();
        uploadVideoBtn.GetComponent<Button>().interactable = true;
    }

    IEnumerator stopClip(float count)
    {
        yield return new WaitForSeconds(count);
        recorderScriptComponent.GetComponent<ReplayCam>().StopRecording();
        exitBtn.SetActive(true);
        disableObjectArray[5].SetActive(true);
        backgroundChangeBtn.SetActive(true);
        uploadVideoBtn.SetActive(true);
        uploadVideoBtn.GetComponent<Button>().interactable=true;
        saveVideoPenal.SetActive(false);
        disableObjectArray[5].GetComponent<Button>().interactable = false;

        if (Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
        {
            Directory.Delete(Application.persistentDataPath + "/UploadVideo", true);
            selectedGameAvatar.GetComponent<Animator>().Play(animClipName, -1, 0f);
            recorderScriptComponent.GetComponent<ReplayCam>().StartRecording();
            RuntimeAnimatorController ac = selectedGameAvatar.GetComponent<Animator>().runtimeAnimatorController;
            float time = ac.animationClips[0].length;
            StartCoroutine(stopClipForUpload(time));
        }
        else
        {
            selectedGameAvatar.GetComponent<Animator>().Play(animClipName, -1, 0f);
            recorderScriptComponent.GetComponent<ReplayCam>().StartRecording();
            RuntimeAnimatorController ac = selectedGameAvatar.GetComponent<Animator>().runtimeAnimatorController;
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

    public void resetAll()
    {
        StopAllCoroutines();
        selectedGameAvatar.GetComponent<Animator>().Play(animClipName, -1, 0f);
        UploadingPenal.SetActive(true);
        exitBtn.SetActive(false);
        backgroundChangeBtn.SetActive(false);
        uploadVideoBtn.SetActive(false);
        disableObjectArray[5].SetActive(false);
        TakeScreenshot(500, 500);
    }

    public void closeScreen()
    {
        recorderScriptComponent.GetComponent<ReplayCam>().microphoneSource.GetComponent<AudioSource>().Stop();
        inGameSceneAudioSource.Stop();
        if (Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
        {
            Directory.Delete(Application.persistentDataPath + "/UploadVideo", true);
        }
        Vector3 temp = new Vector3(0f, -1.39f, 3.43f);
        selectedGameAvatar.transform.position = temp;
        StopAllCoroutines();
        if (Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
        {
            Directory.Delete(Application.persistentDataPath + "/UploadVideo", true);
        }
        assetBundleLoadCanvasObject.SetActive(false);
        uploadVideoBtn.GetComponent<Button>().interactable = false;
        animationListPenal.SetActive(true);
        characterPoseAnimationPenal.SetActive(false);
        characterPoseAnimationPenal.GetComponent<Image>().enabled = true;
        Debug.Log("Call");
        UploadingPenal.SetActive(false);
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
        tempText.Apply();
        characterWithFramePreview.texture = tempText;
        StartCoroutine(UploadVideoFile());
    }

    public void ClearBG()
    {
        characterFrame.gameObject.GetComponent<Image>().sprite = characterDefaultFrameSprite;
    }

    public void VideoDataValue()
    {
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
                    Debug.LogError("update value==="+ apiUpdateParamValue.ToString());
                  
                    var oper = www.SendWebRequest();
                    while (!oper.isDone)
                    {
                        
                        if (www.isNetworkError || www.isHttpError)
                        {
                            yield break;
                        }
                        uploadVideoProgress = www.uploadProgress*100 ;
                        uploadingProgressUpadateText.text = uploadVideoProgress.ToString("F0") + "%";
                        Debug.Log("TEST5"+ www.uploadProgress * 100);

                        yield return null;
                    }
                    try
                    {
                        Debug.LogError("Response===" + www.downloadHandler.text);
                        UploadVideo UploadData = Gods.DeserializeJSON<UploadVideo>(www.downloadHandler.text);

                        if (UploadData.success)
                        {
                            inGameSceneAudioSource.GetComponent<AudioSource>().clip = null;
                            sceneCamera.gameObject.SetActive(true);
                            renderCamera.gameObject.SetActive(true);
                            single = true;
                            apiUpdateParamValue = false;
                            UploadingPenal.SetActive(false);
                            recorderScriptComponent.GetComponent<ReplayCam>().microphoneSource.GetComponent<AudioSource>().Stop();
                            animationListPenal.SetActive(true);
                            uploadingProgressUpadateText.text = string.Empty;
                            exitBtn.SetActive(true);
                            backgroundChangeBtn.SetActive(true);
                            uploadVideoBtn.SetActive(true);
                            disableObjectArray[5].SetActive(false);
                            uploadVideoBtn.GetComponent<Button>().interactable = false;
                            characterPoseAnimationPenal.SetActive(false);
                            characterPoseAnimationPenal.GetComponent<Image>().enabled = true;
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
                            exitBtn.SetActive(true);
                            backgroundChangeBtn.SetActive(true);
                            uploadVideoBtn.SetActive(true);
                            disableObjectArray[5].SetActive(true);
                            disableObjectArray[5].GetComponent<Button>().interactable = true;
                            uploadVideoBtn.GetComponent<Button>().interactable = true;
                            VideoDataValue();
                        }
                    }
                    catch(Exception e)
                    {
                        exitBtn.SetActive(true);
                        backgroundChangeBtn.SetActive(true);
                        uploadVideoBtn.SetActive(true);
                        disableObjectArray[5].SetActive(true);
                        disableObjectArray[5].GetComponent<Button>().interactable = true;
                        uploadVideoBtn.GetComponent<Button>().interactable = true;
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
                        var oper = www.SendWebRequest();
                        while (!oper.isDone)
                        {
                            if (www.isNetworkError || www.isHttpError)
                            {
                                yield break;
                            }
                            uploadVideoProgress = www.uploadProgress * 100;
                            uploadingProgressUpadateText.text = uploadVideoProgress.ToString("F0") + "%";
                            Debug.Log("TEST5" + www.uploadProgress * 100);

                            yield return null;
                        }
                        try
                        {
                            Debug.LogError("Response===" + www.downloadHandler.text);

                            UploadVideo UploadData = Gods.DeserializeJSON<UploadVideo>(www.downloadHandler.text);
                            if (UploadData.success)
                            {
                                inGameSceneAudioSource.GetComponent<AudioSource>().clip = null;
                                apiUpdateParamValue = false;
                                UploadingPenal.SetActive(false);
                                recorderScriptComponent.GetComponent<ReplayCam>().microphoneSource.GetComponent<AudioSource>().Stop();
                                animationListPenal.SetActive(true);
                                uploadingProgressUpadateText.text = string.Empty;;
                                sceneCamera.gameObject.SetActive(true);
                                renderCamera.gameObject.SetActive(true);
                                single = true;
                                exitBtn.SetActive(true);
                                backgroundChangeBtn.SetActive(true);
                                uploadVideoBtn.SetActive(true);
                                disableObjectArray[5].SetActive(false);
                                uploadVideoBtn.GetComponent<Button>().interactable = false;
                                characterPoseAnimationPenal.SetActive(false);
                                characterPoseAnimationPenal.GetComponent<Image>().enabled = true;
                                StopAllCoroutines();
                                Vector3 temp = new Vector3(0f, -1.39f, 3.43f);
                                selectedGameAvatar.transform.position = temp;
                                VideoDataValue();

                                if (Directory.Exists(Application.persistentDataPath + "/UploadVideo"))
                                {
                                    Directory.Delete(Application.persistentDataPath + "/UploadVideo", true);
                                }
                            }
                            else
                            {
                                UploadingPenal.SetActive(false);
                                exitBtn.SetActive(true);
                                backgroundChangeBtn.SetActive(true);
                                uploadVideoBtn.SetActive(true);
                                disableObjectArray[5].SetActive(true);
                                disableObjectArray[5].GetComponent<Button>().interactable = true;
                                uploadVideoBtn.GetComponent<Button>().interactable = true;
                                VideoDataValue();
                            }
                        }
                        catch(Exception e)
                        {
                            exitBtn.SetActive(true);
                            backgroundChangeBtn.SetActive(true);
                            uploadVideoBtn.SetActive(true);
                            disableObjectArray[5].SetActive(true);
                            disableObjectArray[5].GetComponent<Button>().interactable = true;
                            uploadVideoBtn.GetComponent<Button>().interactable = true;
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
