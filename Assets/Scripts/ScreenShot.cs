using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShot : MonoBehaviour
{
    bool takePicture;
    [SerializeField]
    RawImage preview;
    string path;

    private void Start()
    {
        path = Application.persistentDataPath + "/" + "ScreenShot.jpg";
    }
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(takePicture)
        {
            takePicture = false;

            var temRend = RenderTexture.GetTemporary(source.width,source.height);
            Graphics.Blit(source, temRend);

            Texture2D tempText = new Texture2D(source.width, source.height, TextureFormat.RGBA32, false);
            Rect rect = new Rect(0, 0, source.width, source.height);
            tempText.ReadPixels(rect, 0, 0, false);
            tempText.Apply();
            preview.texture = tempText;
            preview.transform.localScale = Vector3.one;
            Save(path,tempText);
        }
        Graphics.Blit(source, destination);
    }

    public void TakeScreenShot()
    {
        takePicture = true;
    }

    public void BackButton()
    {
        preview.transform.localScale = Vector3.zero;
    }

    void Save(string path,Texture2D tex)
    {
        byte[] bytes = tex.EncodeToJPG();
        File.WriteAllBytes(path, bytes);
        Debug.Log("File saved");
    }
}
