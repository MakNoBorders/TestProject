using NatSuite.Examples;
using NatSuite.Sharing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AB : MonoBehaviour
{
    protected RuntimeAnimatorController animator;
    protected AnimatorOverrideController animatorOverrideController;
    GameObject main;
    public Camera sceneCamera;
    bool single = true;
    public GameObject currentAvatar;
    public static AudioClip clipAudio;
    public GameObject startBtn;
    public GameObject stopBtn;
    public GameObject classObject;
    public GameObject animPenal;
    public GameObject loadingPenal;
    //public GameObject applayButton;
    public GameObject ScriptComponent;
    public GameObject bgPenal;
    public GameObject rawText1;
    public GameObject rawText2;
    public GameObject rawText3;
    private bool statecall=false;
    private Texture texture1;
    //public GameObject rightPenal;
    //public GameObject bottomPenal;
    private bool open=false;
    public GameObject bgImage;

    public void Load(string url)
    {
        if (single)
        {
#if ANDROID
            //InitAnim();

           // animPenal.SetActive(false);
          
            StartCoroutine(GetAssetBundleFromServerUrl(url));
#endif

#if WINDOWS
           // animPenal.SetActive(false);
           
            //InitAnim();
            StartCoroutine(GetAssetBundleFromServerUrl(url));
#endif

#if IOS
         //   animPenal.SetActive(false);
           
            //InitAnim();
            StartCoroutine(GetAssetBundleFromServerUrl(url));
#endif

            single = false;
        }
        else
        {
            startBtn.SetActive(false);
            stopBtn.SetActive(false);
            Destroy(main);
            sceneCamera.gameObject.SetActive(true);
            single = true;
        }
    }

    void InitAnim()
    {

        AssetBundle assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/android" + "/xbot.unity3d");
        if (assetBundle == null)
        {
            return;
        }
        GameObject[] animation = assetBundle.LoadAllAssets<GameObject>();
        foreach (var go in animation)
        {
            Debug.LogError(go.name);
            if (go.name.Equals("xbot"))
            {
              //  main = Instantiate(go);
                main = go;
            }
            //animator = go.transform.GetChild(0).GetComponent<Animator>().runtimeAnimatorController;
            //clipAudio = go.transform.GetChild(1).GetComponent<AudioSource>().clip;

            //classObject.transform.GetComponent<AudioSource>().clip = clipAudio;
            //classObject.transform.GetComponent<AudioSource>().Play();
            //currentAvatar.GetComponent<Animator>().runtimeAnimatorController = animator;
            main.transform.GetChild(1).GetComponent<AudioSource>().enabled = false;
            //startBtn.SetActive(true);
            //stopBtn.SetActive(true);
           // currentAvatar.GetComponent<AudioSource>().clip = clipAudio;
           // bgPenal.SetActive(true);
           // BackButton.SetActive(false);
            currentAvatar.SetActive(true);
          
            animator = main.transform.GetComponent<Animator>().runtimeAnimatorController;
            main.transform.GetChild(0).gameObject.SetActive(false);
            clipAudio = main.transform.GetChild(1).GetComponent<AudioSource>().clip;
            classObject.transform.GetComponent<AudioSource>().clip = clipAudio;
            classObject.transform.GetComponent<AudioSource>().Play();
            currentAvatar.GetComponent<Animator>().runtimeAnimatorController = animator;
            ScriptComponent.GetComponent<ReplayCam>().StartRecording();
          //  NextButton.SetActive(true);
        }

        GetAllCameras();

        //  assetBundle.Unload(false);

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
                        if (go.name.Equals("Animation_1"))
                        {
                            loadingPenal.SetActive(false);
                            // BackButton.SetActive(true);
                            main = Instantiate(go);
                          //  bgPenal.SetActive(true);
                           // BackButton.SetActive(false);
                            currentAvatar.SetActive(true);
                            
                            animator = main.transform.GetComponent<Animator>().runtimeAnimatorController;
                            main.transform.GetChild(0).gameObject.SetActive(false);
                            main.transform.GetChild(2).transform.gameObject.SetActive(false);
                            clipAudio = main.transform.GetChild(1).GetComponent<AudioSource>().clip;
                            classObject.transform.GetComponent<AudioSource>().clip = clipAudio;
                            classObject.transform.GetComponent<AudioSource>().Play();
                            currentAvatar.GetComponent<Animator>().runtimeAnimatorController = animator;
                            ScriptComponent.GetComponent<ReplayCam>().StartRecording();
                            RuntimeAnimatorController ac = currentAvatar.GetComponent<Animator>().runtimeAnimatorController;
                            float time = ac.animationClips[0].length;
                           
                            StartCoroutine(stopClip(time));
                            //bottomPenal.SetActive(true);
                            //rightPenal.SetActive(true);

                            // applayButton.SetActive(true);
                        }
                        else if (go.name.Equals("Animation_2"))
                        {
                            loadingPenal.SetActive(false);
                            // BackButton.SetActive(true);
                            main = Instantiate(go);
                           // bgPenal.SetActive(true);
                           // BackButton.SetActive(false);
                            currentAvatar.SetActive(true);
                           
                            animator = main.transform.GetComponent<Animator>().runtimeAnimatorController;
                            main.transform.GetChild(0).gameObject.SetActive(false);
                            main.transform.GetChild(2).transform.gameObject.SetActive(false);
                            clipAudio = main.transform.GetChild(1).GetComponent<AudioSource>().clip;
                            classObject.transform.GetComponent<AudioSource>().clip = clipAudio;
                            classObject.transform.GetComponent<AudioSource>().Play();
                            currentAvatar.GetComponent<Animator>().runtimeAnimatorController = animator;
                            ScriptComponent.GetComponent<ReplayCam>().StartRecording();
                            RuntimeAnimatorController ac = currentAvatar.GetComponent<Animator>().runtimeAnimatorController;
                            float time = ac.animationClips[0].length;
                            
                            StartCoroutine(stopClip(time));

                            //bottomPenal.SetActive(true);
                            // rightPenal.SetActive(true);
                            //  applayButton.SetActive(true);
                        }
                        else if (go.name.Equals("Animation_3"))
                        {
                            loadingPenal.SetActive(false);
                            //  BackButton.SetActive(true);
                            main = Instantiate(go);
                           // bgPenal.SetActive(true);
                           // BackButton.SetActive(false);
                            currentAvatar.SetActive(true);
                         
                            animator = main.transform.GetComponent<Animator>().runtimeAnimatorController;
                            main.transform.GetChild(0).gameObject.SetActive(false);
                            main.transform.GetChild(2).transform.gameObject.SetActive(false);
                            clipAudio = main.transform.GetChild(1).GetComponent<AudioSource>().clip;
                            classObject.transform.GetComponent<AudioSource>().clip = clipAudio;
                            classObject.transform.GetComponent<AudioSource>().Play();
                            currentAvatar.GetComponent<Animator>().runtimeAnimatorController = animator;
                            ScriptComponent.GetComponent<ReplayCam>().StartRecording();
                            RuntimeAnimatorController ac = currentAvatar.GetComponent<Animator>().runtimeAnimatorController;
                            float time = ac.animationClips[0].length;
                           
                            StartCoroutine(stopClip(time));
                            //bottomPenal.SetActive(true);
                           //rightPenal.SetActive(true);
                           
                        }
                       
                        // currentAvatar.GetComponent<AudioSource>().clip = clipAudio;
                    }
                }
                GetAllCameras();
                assetBundle.Unload(false);
            }
             
           

        }
    }
    IEnumerator stopClip(float count)
    {
        yield return new WaitForSeconds(count);
        ScriptComponent.GetComponent<ReplayCam>().StopRecording();
    }

    public void bgPenalOpenClose()
    {
        if (open)
        {
            open = false;
            bgPenal.SetActive(false);
        }
        else
        {
            open = true;
            bgPenal.SetActive(true);
        }
    }

    public void bgChange(int index)
    {
        texture1 = null;
        if (index == 1)
        {
             texture1= rawText1.GetComponent<RawImage>().texture;
            // main.transform.GetChild(2).GetComponent<Canvas>().transform.GetChild(0).GetComponent<RawImage>().texture = texture1;
            bgImage.transform.GetComponent<RawImage>().texture = texture1;
        }
        else if (index == 2)
        {

            texture1 = rawText2.GetComponent<RawImage>().texture;
            bgImage.transform.GetComponent<RawImage>().texture = texture1;
        }
        else if (index == 3)
        {
            texture1 = rawText3.GetComponent<RawImage>().texture;
            bgImage.transform.GetComponent<RawImage>().texture = texture1;
        }
    }

    public void applyClick()
    {
        bgPenal.SetActive(true);
       // BackButton.SetActive(false);
        currentAvatar.SetActive(true);
       
        //bottomPenal.SetActive(false);
        //rightPenal.SetActive(false);
        animator = main.transform.GetComponent<Animator>().runtimeAnimatorController;
        main.transform.GetChild(0).gameObject.SetActive(false);
        clipAudio = main.transform.GetChild(1).GetComponent<AudioSource>().clip;
        classObject.transform.GetComponent<AudioSource>().clip = clipAudio;
        classObject.transform.GetComponent<AudioSource>().Play();
        currentAvatar.GetComponent<Animator>().runtimeAnimatorController = animator;
        ScriptComponent.GetComponent<ReplayCam>().StartRecording();
        //NextButton.SetActive(true);
      //  statecall = true;
        //startBtn.SetActive(true);
        //stopBtn.SetActive(true);
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
    //void Load()
    //{
    //    AssetBundle assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/animations");
    //    if (assetBundle == null)
    //    {
    //        return;
    //    }


    //    AnimationClip[] animation = assetBundle.LoadAllAssets<AnimationClip>();
    //    //foreach (var anim in animationClip)
    //    //{
    //    //    animationClip.Add(anim);
    //    //}
    //    Debug.LogError("Animation " + animation[0].name);
    //    animatorOverrideController["New State"] = animation[0];
    //    assetBundle.Unload(false);

    //}
    //void LoadAnimator()
    //{
    //    AssetBundle assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/animations");
    //    if (assetBundle == null)
    //    {
    //        return;
    //    }
    //    RuntimeAnimatorController[] animationClip = assetBundle.LoadAllAssets<RuntimeAnimatorController>();
    //    foreach (var anim in animationClip)
    //    {
    //        if (anim != null)
    //        {
    //            Debug.LogError(anim.name);
    //            animators.Add(anim);
    //        }
    //    }
    //    assetBundle.Unload(false);

    //}


     void Update()
    {
        ////if(statecall)

        ////{
        ////    if (currentAvatar.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idle"))
        ////    {
        ////        statecall = false;
        ////        Debug.Log("state info===" + currentAvatar.GetComponent<Animator>().GetCurrentAnimatorStateInfo(1));
        ////    }
        ////}

    }

    public void Back()
    {
        Destroy(main);
        //bottomPenal.SetActive(false);
        //rightPenal.SetActive(false);
        sceneCamera.gameObject.SetActive(true);
        single = true;
      //  NextButton.SetActive(false);
       
        animPenal.SetActive(true);
       // BackButton.SetActive(false);
        currentAvatar.SetActive(false);
        bgPenal.SetActive(false);
    }

    public void resetAll()
    {
        Debug.Log("Call");
        StopAllCoroutines();
        Destroy(main.gameObject);
        sceneCamera.gameObject.SetActive(true);
        single = true;
       // NextButton.SetActive(false);
        //animPenal.SetActive(true);
        //bottomPenal.SetActive(false);
       // rightPenal.SetActive(false);
        //  BackButton.SetActive(false);
        currentAvatar.SetActive(false);
       // bgPenal.SetActive(false);
    }

}
