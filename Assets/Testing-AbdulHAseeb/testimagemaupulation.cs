using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testimagemaupulation : MonoBehaviour
{

    public GameObject m_OtherObject;

    public int scaler;

    void Start()
    {
        Texture2D texture = new Texture2D(200, 200);
        GetComponent<Renderer>().material.mainTexture = texture;

        float x1 = transform.position.x;
        float x2 = m_OtherObject.transform.position.x;

        float y1 = transform.position.y;
        float y2 = m_OtherObject.transform.position.y;

        float xDifference = (x1 - x2);

        float yDifference = (y1 - y2);

        int xStart = 0;
        int yStart = 0;

        int xEnd = texture.width;
        int yEnd = texture.height;

        if(xDifference>0)
        {
            xStart = (int)(xDifference * scaler);
        }
        else
        {
            xEnd = texture.width - (int)(Mathf.Abs(xDifference) * scaler);
        }

        if (yDifference > 0)
        {
            yStart = (int)(yDifference * scaler);
        }
        else
        {
            yEnd = texture.width - (int)(Mathf.Abs(yDifference) * scaler);
        }

        for (int y = yStart; y < yEnd; y++)
        {
            for (int x = xStart; x < xEnd; x++)
            {
                //Color color = ((x & y) != 0 ? Color.white : Color.gray);
                
                    texture.SetPixel((x), (y), Color.red);
                
            }
        }
        texture.Apply();
    }
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
