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
   // public GameObject posePrefab;
    public GameObject Downloaded;
    public Animation DownloadedAnimation;
    public GameObject mainCharacter;
    public GameObject _characterDownloaded;
    public GameObject _characterActive;
    private ScreenshotHandler ssHandler;
    public Image resultImage;
    
    public Image frame;
    public GameObject camer;
    private static RuntimeAnimatorController mainAnim;
    public GameObject loading;
    public GameObject[] toEnable;

    public RawImage testimage;

    [Header("Take Screen Shoot")]
    public int XBottomStart;
    public int XTopStart;
    public int YBottomStart;
    public int YTopStart;

    [Header("Final Image Panel")]
    public GameObject m_FinalImage_Panel;
    public Image finalImage;

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
        ssHandler.resultImage = resultImage;
        ssHandler.finalImage = finalImage;
        ssHandler.frame = frame;
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
        ssHandler.resultImage = resultImage;
        ssHandler.finalImage = finalImage;
        ssHandler.frame = frame;
        ssHandler.TakeScreenshot(500, 500);

    }


    public void TakeSSFrame()
    {
        Texture2D bottom = frame.sprite.texture;
        Texture2D top = resultImage.sprite.texture;

        Texture2D temptexture = new Texture2D(bottom.width, bottom.height);
        temptexture.SetPixels(bottom.GetPixels());
        temptexture.Apply();

        Vector3 PosDiff = -frame.GetComponent<RectTransform>().localPosition + resultImage.GetComponent<RectTransform>().localPosition;

        if (PosDiff.x > 0)
        {
            XBottomStart = (int)((PosDiff.x/ (Screen.width / 2))*500); 
            XTopStart = 0;
        }
        else
        {
            XTopStart = (int)((Mathf.Abs(PosDiff.x) / (Screen.width / 2)) * 500);
            XBottomStart = 0;
        }

        if (PosDiff.y > 0)
        {
            YBottomStart = (int)((PosDiff.y / (Screen.width / 2)) * 500);
            YTopStart = 0;
        }
        else 
        {
            YTopStart = (int)((Mathf.Abs(PosDiff.y) / (Screen.width / 2)) * 500);
            YBottomStart = 0;
        }

        Color a = new Color();

        for (int x = 0; x < bottom.width; x++)
        {
            for (int y = 0; y < bottom.height; y++)
            {
                if((y + YTopStart) <= bottom.height)
                 a = top.GetPixel(x+XTopStart, y+YTopStart);

                if ((y + YBottomStart) <= bottom.height)
                    if (a.a > 0.5f)
                    temptexture.SetPixel(x+XBottomStart, y+YBottomStart, a);
            }
        }


        temptexture.Apply();

        Sprite sprite = Sprite.Create(temptexture, new Rect(0.0f, 0.0f, temptexture.width, temptexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        finalImage.sprite = sprite;
        finalImage.gameObject.SetActive(true);

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
