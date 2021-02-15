using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PoseManager : MonoBehaviour
{
    public static PoseManager instance;
    public GameObject posePrefab;
    public GameObject Downloaded;
    public GameObject mainCharacter;
    public GameObject _characterDownloaded;
    public GameObject _characterActive;
    private ScreenshotHandler ssHandler;
    public Image resultImage;
    public Image finalImage;
    public Image frame;

    private void Awake()
    {
        instance = this;
    }

    public void InitiateScreenShot(GameObject gameObject)
    {
        Downloaded = Instantiate(gameObject);
        _characterDownloaded = Downloaded.transform.GetChild(0).transform.gameObject;
        Downloaded.AddComponent<ScreenshotHandler>();
        ssHandler = Downloaded.GetComponent<ScreenshotHandler>();
        ssHandler.resultImage = resultImage;
        ssHandler.finalImage = finalImage;
        ssHandler.frame = frame;

        _characterActive = Instantiate(mainCharacter);
        _characterActive.GetComponent<Animator>().runtimeAnimatorController = null;
        GetBonesTrans(_characterActive);

    }

    public void DownloadPose(GameObject prefab)
    {
        //StartCoroutine(DownloadAssetBundle(url));
        InitiateScreenShot(prefab);
    }
   
    
        public IEnumerator DownloadAssetBundle(string uri)
        {
        // WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Get(uri))
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
                    string filePath = Application.persistentDataPath + "/" + "Poses";
                    File.WriteAllBytes(filePath, fileData);
                    if (operation.isDone)
                    {
                        Debug.Log("done");
                    var myLoadedAssetBundle = AssetBundle.LoadFromFile(Application.persistentDataPath + "/" + "Poses");
                    if (myLoadedAssetBundle == null)
                    {
                        Debug.Log("Failed to load AssetBundle!");
                       
                    }

                    var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("pose") as GameObject;
                    //Instantiate(prefab);
                    InitiateScreenShot(prefab);
                    myLoadedAssetBundle.Unload(false);
                }
                }
            }
        }
    

    public void TakeSSFrame()
    {
        Texture2D bottom = frame.sprite.texture;
        Texture2D top = resultImage.sprite.texture;

        Texture2D combined = bottom.AlphaBlend(top);
        Sprite sprite = Sprite.Create(combined, new Rect(0.0f, 0.0f, combined.width, combined.height), new Vector2(0.5f, 0.5f), 100.0f);
        finalImage.sprite = sprite;
        finalImage.gameObject.SetActive(true);
    }

    public void GetBonesTrans(GameObject _character)
    {
        try
        {
            foreach (HumanBodyBones bones in Enum.GetValues(typeof(HumanBodyBones)))
            {
                _character.GetComponent<Animator>().GetBoneTransform(bones).transform.position = _characterDownloaded.GetComponent<Animator>().GetBoneTransform(bones).transform.position;
                _character.GetComponent<Animator>().GetBoneTransform(bones).transform.rotation = _characterDownloaded.GetComponent<Animator>().GetBoneTransform(bones).transform.rotation;
            }
        }
        catch
        {
            Debug.LogError("Error");
        }

        _characterDownloaded.SetActive(false);
        ssHandler.TakeScreenshot(500, 500);

    }

}
