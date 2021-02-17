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
    public Camera renderCamera;
    bool single = true;
    public GameObject currentAvatar;
    public static AudioClip clipAudio;
    public GameObject classObject;
    public GameObject loadingPenal;
    public GameObject ScriptComponent;
    public GameObject rawText1;
    public GameObject rawText2;
    public GameObject rawText3;
    public GameObject[] toClose;
    public GameObject Frame;
    public Sprite defaultSprite;

    public void Load(string url)
    {
        
        if (single)
        {

            foreach (GameObject objects in toClose )
            {
                objects.SetActive(false);
            }
            toClose[4].gameObject.SetActive(true);
            StartCoroutine(GetAssetBundleFromServerUrl(url));


            single = false;
        }
        else
        {
            Destroy(main);
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
                            loadingPenal.SetActive(false);
                            main = go.transform.gameObject;
                            currentAvatar.SetActive(true);
                            animator = main.transform.GetComponent<Animator>().runtimeAnimatorController;
                            clipAudio = main.transform.GetChild(0).GetComponent<AudioSource>().clip;
                            classObject.transform.GetComponent<AudioSource>().clip = clipAudio;
                            classObject.transform.GetComponent<AudioSource>().Play();
                            currentAvatar.GetComponent<Animator>().runtimeAnimatorController = animator;
                            ScriptComponent.GetComponent<ReplayCam>().StartRecording();
                            RuntimeAnimatorController ac = currentAvatar.GetComponent<Animator>().runtimeAnimatorController;
                            float time = ac.animationClips[0].length;
                            StartCoroutine(stopClip(time));
                        }
                    }
                    GetAllCameras();
                    assetBundle.Unload(false);
                }

            }
           

        }
    }
    IEnumerator stopClip(float count)
    {
        yield return new WaitForSeconds(count);
        ScriptComponent.GetComponent<ReplayCam>().StopRecording();
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
        Debug.Log("Call");
        StopAllCoroutines();
        ScriptComponent.GetComponent<ReplayCam>().microphoneSource.GetComponent<AudioSource>().Stop();
      // Destroy(main.gameObject);
        sceneCamera.gameObject.SetActive(true);
        renderCamera.gameObject.SetActive(true);
        single = true;
      }

    public void ClearBG()
    {
        Frame.gameObject.GetComponent<Image>().sprite = defaultSprite;
    }
}
