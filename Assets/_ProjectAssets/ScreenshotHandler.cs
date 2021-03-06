using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotHandler : MonoBehaviour
{

    private static ScreenshotHandler instance;

    public Image resultImage;
    public Image finalImage;
    public Image frame;

    public Camera PoseCamera;
    private bool takeScreenshotOnNextFrame=false;
    private int width;
    private int height;

    private RenderTexture renderTextureOld;

    private void Awake()
    {
        instance = this;
        width = 500;
        height = 500;
        //PoseCamera = GameObject.Find("RenderCamera").GetComponent<Camera>();
    }

    

    private void OnPostRender()
    {
        if (takeScreenshotOnNextFrame)
        {
            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = PoseCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);

            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);
            Sprite sprite = Sprite.Create(renderResult, new Rect(0.0f, 0.0f, renderResult.width, renderResult.height), new Vector2(0.5f, 0.5f), 100.0f);
            resultImage.sprite = sprite;
            resultImage.sprite.texture.Apply();

            //byte[] byteArray = renderResult.EncodeToPNG();
            //System.IO.File.WriteAllBytes("Assets/CameraScreenshotNew.png", byteArray);
            Debug.LogError("TookScreenshot");

            RenderTexture.ReleaseTemporary(renderTexture);
            PoseCamera.targetTexture = null;
            PoseCamera.targetTexture = renderTextureOld;
            frame.gameObject.SetActive(true);
            resultImage.gameObject.SetActive(true);
            //Destroy(gameObject);
            //Destroy(PoseManager.instance._characterActive);

        }
    }

    public void TakeScreenshot(int width, int height)
    {
        renderTextureOld = PoseCamera.targetTexture;
        PoseCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        StartCoroutine(WaitForOneFrame());
    }

    IEnumerator WaitForOneFrame()
    {
        yield return new WaitForEndOfFrame();
            takeScreenshotOnNextFrame = true;
    }
   
}
