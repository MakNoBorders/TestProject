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
    public Image resultImage;
    public Image finalImage;
    public Image frame;
    public GameObject camer;
    private static RuntimeAnimatorController mainAnim;
    public GameObject loading;
    public GameObject[] toEnable;

    public RawImage testimage;


    public GameObject m_Plane;
    public float scaler;

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

        //print(resultImage.GetComponent<RectTransform>().localPosition.ToString());

        Texture2D temptexture = new Texture2D(bottom.width, bottom.height);

        temptexture.SetPixels(bottom.GetPixels());
        temptexture.Apply();


        Vector3 PosDiff = -frame.rectTransform.localPosition + resultImage.rectTransform.localPosition;

        int XStart = 0, YStart = 0, XEnd = 0, YEnd = 0;

        if (PosDiff.x > 0)
        {
            XStart = (int)(PosDiff.x * scaler);
        }
        else
        {
            XStart = (int)(PosDiff.x * scaler);
            //XEnd = bottom.width - (int)(Mathf.Abs(PosDiff.x) * scaler);
        }

        if (PosDiff.y > 0)
        {
            YStart = (int)(PosDiff.y * scaler);
        }
        else
        {
            YStart = (int)(PosDiff.y * scaler);
            //YEnd = bottom.height - (int)(Mathf.Abs(PosDiff.y) * scaler);
        }

       



        for (int x = XStart; x < bottom.width; x++)
        {
            for (int y = YStart; y < bottom.height; y++)
            {
                Color a = top.GetPixel(x-XStart, y- YStart);
                if(a.a>0.5f)
                temptexture.SetPixel(x, y, a);
            }
        }

       // m_Plane.GetComponent<Renderer>().material.mainTexture = bottom;

        temptexture.Apply();

        



        // return bottom;


        // Texture2D combined = bottom.AlphaBlend(top);
        Sprite sprite = Sprite.Create(temptexture, new Rect(0.0f, 0.0f, temptexture.width, temptexture.height), new Vector2(0.5f, 0.5f), 100.0f);
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
        ssHandler.TakeScreenshot(200, 200);

    }

}
