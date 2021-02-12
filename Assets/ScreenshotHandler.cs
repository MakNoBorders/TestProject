using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotHandler : MonoBehaviour
{

    private static ScreenshotHandler instance;

    private Camera myCamera;
    private bool takeScreenshotOnNextFrame;
    public Image resultImage;
    public Image finalImage;
    public Image frame;
    public int width;
    public int height;

    public float value=300;
    Texture2D border;
    private void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
        
    }
    private void Start()
    {
        //border = new Texture2D(2, 2, TextureFormat.ARGB32, false);
        
        //TakeScreenshot(500, 500);
    }
    private void OnPostRender()
    {
        if (takeScreenshotOnNextFrame)
        {
            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = myCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);
            Sprite sprite = Sprite.Create(renderResult, new Rect(0.0f, 0.0f, renderResult.width, renderResult.height), new Vector2(0.5f, 0.5f), 100.0f);
            resultImage.sprite = sprite;
            resultImage.sprite.texture.Apply();

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes("Assets/CameraScreenshotNew.png", byteArray);
            Debug.LogError("TookScreenshot");

            RenderTexture.ReleaseTemporary(renderTexture);
            myCamera.targetTexture = null;
            resultImage.gameObject.SetActive(true);

        }
    }

    public void TakeSS()
    {
        TakeScreenshot(width, height);
    }

    public void TakeSSFrame()
    {
        //RectTransform rt = frame.GetComponent<RectTransform>();
        //float wdth = rt.rect.width;
        //float hgth = rt.rect.height;
        ////TakeScreenshot(Screen.width, Screen.height/5);
        //TakeScreenshot((int)wdth, (int)hgth);
        Texture2D bottom = frame.sprite.texture;
        Texture2D top = resultImage.sprite.texture;

        Texture2D combined = bottom.AlphaBlend(top);
        Sprite sprite = Sprite.Create(combined, new Rect(0.0f, 0.0f, combined.width, combined.height), new Vector2(0.5f, 0.5f), 100.0f);
        finalImage.sprite = sprite;
    }
    private void TakeScreenshot(int width, int height)
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
//        myCamera.targetTexture = RenderTexture.GetTemporary(Screen.width, Screen.height/2, 16);
        takeScreenshotOnNextFrame = true;

    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        Debug.LogError($"{Screen.width} x {Screen.height}");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.LogError("Press button");
            //            ScreenCapture.CaptureScreenshot("SomeLevel");
            TakeScreenshot(500, 500);
        }
        //{
        //    StartCoroutine("Capture");
        //}
    }

    void OnMouseDown()
    {
        ScreenCapture.CaptureScreenshot("SomeLevel");
        Debug.LogError("Done");
    }

    //IEnumerator Capture()
    //{
    //    yield return new WaitForEndOfFrame();
    //    myCamera.targetTexture = RenderTexture.GetTemporary(Screen.width, Screen.height/2, 16);


    //    RenderTexture renderTexture = myCamera.targetTexture;

    //    Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);

    //    Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
    //    renderResult.ReadPixels(rect, 0, 0);
    //    Sprite sprite = Sprite.Create(renderResult, new Rect(0.0f, 0.0f, renderResult.width, renderResult.height), new Vector2(0f, 0f), 100.0f);
    //    //resultImage.sprite = sprite;
    //    //resultImage.sprite.texture.Apply();

    //    byte[] byteArray = renderResult.EncodeToPNG();
    //    System.IO.File.WriteAllBytes("Assets/CameraScreenshot.png", byteArray);
    //    Debug.LogError("TookScreenshot");

    //    RenderTexture.ReleaseTemporary(renderTexture);
    //    myCamera.targetTexture = null;
    //    resultImage.gameObject.SetActive(true);



    //    //border = new Texture2D(Screen.width, Screen.height, TextureFormat.ARGB32, false);
    //    //border.ReadPixels(new Rect(-10, -10, Screen.width, Screen.height ), 0, 0);
    //    //border.Apply();
    //    //byte[] byteArray = border.EncodeToPNG();
    //    //System.IO.File.WriteAllBytes("Assets/CameraScreenshot.png", byteArray);
    //    //Debug.LogError("Captured");

    //}

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height/2), border, ScaleMode.StretchToFill); //top
        //GUI.DrawTexture(new Rect(200, 300, 300, 2), border, ScaleMode.StretchToFill); // bottom
        //GUI.DrawTexture(new Rect(200, 100, 2, 200), border, ScaleMode.StretchToFill); //left
        //GUI.DrawTexture(new Rect(500, 100, 2, 200), border, ScaleMode.StretchToFill); // right
    }
}
