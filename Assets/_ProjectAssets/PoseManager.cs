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
    public Animation DownloadedAnimation;
    public GameObject mainCharacter;
    public GameObject _characterDownloaded;
    public GameObject _characterActive;
    private ScreenshotHandler ssHandler;
    
    
    
    public GameObject camer;
    private static RuntimeAnimatorController mainAnim;
    public GameObject loading;
    public GameObject[] toEnable;

    public RawImage testimage;

    [Header("Take Screen Shoot")]
    public int m_XBottomStart;
    public int m_XTopStart;
    public int m_YBottomStart;
    public int m_YTopStart;

    [Header("Final Image Panel")]
    public GameObject m_FinalImage_Panel;
    public Image m_BackGroundImage_Image;
    public Image m_CapturedImage_Image;
    public Image m_FinalImage_Image;

    private void Awake()
    {
        instance = this;
        mainAnim = mainCharacter.GetComponent<Animator>().runtimeAnimatorController;        

    }

    private void Start()
    {
        camer.AddComponent<ScreenshotHandler>();
    }

    public void InitiateScreenShot()
    {
       
        //Downloaded = Instantiate(gameObject);
        //_characterDownloaded = Downloaded.transform.GetChild(0).transform.gameObject;       
        ssHandler = camer.GetComponent<ScreenshotHandler>();
        ssHandler.resultImage = m_CapturedImage_Image;
        ssHandler.finalImage = m_FinalImage_Image;
        ssHandler.frame = m_BackGroundImage_Image;
        ssHandler.PoseCamera = camer.GetComponent<Camera>();
        ssHandler.TakeScreenshot(500, 500);
        //_characterActive = Instantiate(mainCharacter);
        //_characterActive.GetComponent<Animator>().runtimeAnimatorController = null;
        //GetBonesTrans(_characterActive);

    }

    public void DownloadPose(GameObject prefab)
    {
        InitiateScreenShot();
    }

    public void DownloadAnim(string url)
    {
        foreach(GameObject objects in toEnable)
        {
            objects.SetActive(true);
        }
        toEnable[4].gameObject.SetActive(false);
        StartCoroutine(DownloadAssetBundle(url));
    }

    public void AssignMainAnim()
    {
        mainCharacter.GetComponent<Animator>().runtimeAnimatorController = mainAnim;
    }

    public IEnumerator DownloadAssetBundle(string uri)
    {
        // WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Get(uri))
        {


            var operation = www.SendWebRequest();
            while (!operation.isDone)
            {
                loading.SetActive(true);
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

                    //var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("pose") as GameObject;
                    var prefab = myLoadedAssetBundle.LoadAsset<RuntimeAnimatorController>("poses") as RuntimeAnimatorController;
                    mainCharacter.gameObject.GetComponent<Animator>().runtimeAnimatorController = prefab;
                    //Instantiate(prefab);
                    InitiateScreenShot();
                    myLoadedAssetBundle.Unload(false);
                    loading.SetActive(false);
                }
            }
        }
    }

    public void InitiateScreenShotAnimation(RuntimeAnimatorController animController)
    {
        //_characterActive = Instantiate(mainCharacter);
        //_characterActive.gameObject.GetComponent<Animator>().runtimeAnimatorController = animController;
        //_characterActive.AddComponent<ScreenshotHandler>();
       // ssHandler = Downloaded.GetComponent<ScreenshotHandler>();
        ssHandler.resultImage = m_CapturedImage_Image;
        ssHandler.finalImage = m_FinalImage_Image;
        ssHandler.frame = m_BackGroundImage_Image;
        ssHandler.TakeScreenshot(500, 500);

    }


    public void TakeSSFrame()
    {
        Texture2D l_Bottom = m_BackGroundImage_Image.sprite.texture; // Background image
        Texture2D l_Top = m_CapturedImage_Image.sprite.texture; // captured image of the character

        Texture2D l_GeneratedTexture = new Texture2D(l_Bottom.width, l_Bottom.height);  // Creating a new texture of same width and height
        l_GeneratedTexture.SetPixels(l_Bottom.GetPixels());
        l_GeneratedTexture.Apply();

        Vector3 PosDiff = -m_BackGroundImage_Image.GetComponent<RectTransform>().localPosition + m_CapturedImage_Image.GetComponent<RectTransform>().localPosition;

        // Taking the position difference of the background imaged and captured image

        if (PosDiff.x > 0)
        {
            m_XBottomStart = (int)((PosDiff.x/ (Screen.width / 2))*(Screen.width/2)); 
            m_XTopStart = 0;
        }
        else
        {
            m_XTopStart = (int)((Mathf.Abs(PosDiff.x) / (Screen.width / 2)) * (Screen.width / 2));
            m_XBottomStart = 0;
        }

        if (PosDiff.y > 0)
        {
            m_YBottomStart = (int)((PosDiff.y / (Screen.width / 2)) * (Screen.width / 2));
            m_YTopStart = 0;
        }
        else 
        {
            m_YTopStart = (int)((Mathf.Abs(PosDiff.y) / (Screen.width / 2)) * (Screen.width / 2));
            m_YBottomStart = 0;
        }

        Color l_PixelColor = new Color();

        for (int x = 0; x < l_Bottom.width; x++)
        {
            for (int y = 0; y < l_Bottom.height; y++)
            {
                if((y + m_YTopStart) <= l_Bottom.height)
                    l_PixelColor = l_Top.GetPixel(x+m_XTopStart, y+m_YTopStart);

                if ((y + m_YBottomStart) <= l_Bottom.height)
                    if (l_PixelColor.a > 0.5f)
                    l_GeneratedTexture.SetPixel(x+m_XBottomStart, y+m_YBottomStart, l_PixelColor);
            }
        }


        l_GeneratedTexture.Apply();

        Sprite l_GeneratedSprite_Sprite = Sprite.Create(l_GeneratedTexture, new Rect(0.0f, 0.0f, l_GeneratedTexture.width, l_GeneratedTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        m_FinalImage_Image.sprite = l_GeneratedSprite_Sprite;
        m_FinalImage_Image.gameObject.SetActive(true);

        m_FinalImage_Panel.SetActive(true);
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
        ssHandler.TakeScreenshot(200, 200);
    }
}
