using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoseManager : MonoBehaviour
{

    public GameObject PosePrefab;
    public ScreenshotHandler SSinstance;
    public Image resultImage;
    public Image finalImage;
    public Image frame;

    public void InitiateScreenShot()
    {
        Instantiate(PosePrefab);
        PosePrefab.AddComponent<ScreenshotHandler>();
        SSinstance = PosePrefab.GetComponent<ScreenshotHandler>();
        SSinstance.resultImage = resultImage;
        SSinstance.finalImage = finalImage;
        SSinstance.frame = frame;

    }

    public void TakeSSFrame()
    {
        Texture2D bottom = frame.sprite.texture;
        Texture2D top = resultImage.sprite.texture;

        Texture2D combined = bottom.AlphaBlend(top);
        Sprite sprite = Sprite.Create(combined, new Rect(0.0f, 0.0f, combined.width, combined.height), new Vector2(0.5f, 0.5f), 100.0f);
        finalImage.sprite = sprite;
    }

}
