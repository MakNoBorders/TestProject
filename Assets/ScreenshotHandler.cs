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
    public int width;
    public int height;
    private void Awake()
    {
        instance = this;
        myCamera = gameObject.GetComponent<Camera>();
        
    }
    private void Start()
    {
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
            
            //byte[] byteArray = renderResult.EncodeToPNG();
            //System.IO.File.WriteAllBytes("Assets/CameraScreenshot.png", byteArray);
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
    private void TakeScreenshot(int width, int height)
    {
        myCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;

    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.LogError("Press button");
        //    //            ScreenCapture.CaptureScreenshot("SomeLevel");
        //    TakeScreenshot(500, 500);
        //}
    }

    void OnMouseDown()
    {
        ScreenCapture.CaptureScreenshot("SomeLevel");
        Debug.LogError("Done");
    }
}
