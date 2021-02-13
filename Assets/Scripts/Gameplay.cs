using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public Sprite[] spriteArray;
    public Image myImage;
    public Image cpuImage;
    private int index=0;

    public Text statusText;
    
    //selections
    private int playerSelection = 0;
    private int cpuSelection = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ChangeSprites", 0.1f, 0.1f);
    }

    public void ChangeSprites()
    {
        statusText.enabled = false;
        if (index < spriteArray.Length-1)
        {
            index++;
        }
        else
        {
            index = 0;
        }
        myImage.sprite = spriteArray[index];
        cpuImage.sprite = spriteArray[index];

    }

    public void selectRock()
    {
        playerSelection = 0;
        cpuSelection = Random.Range(0, 3);
        CancelInvoke("ChangeSprites");
        GamePlay();
    }

    public void selectPaper()
    {
        playerSelection = 1;
        cpuSelection = Random.Range(0, 3);
        CancelInvoke("ChangeSprites");
        GamePlay();
    }

    public void selectScissor()
    {
        playerSelection = 2;
        cpuSelection = Random.Range(0, 3);
        CancelInvoke("ChangeSprites");
        GamePlay();
    }

    private void GamePlay()
    {
        myImage.sprite = spriteArray[playerSelection];
        cpuImage.sprite = spriteArray[cpuSelection];

        if (playerSelection == 0 && cpuSelection == 0)
        {
            statusText.text = "Draw";
            statusText.enabled = true;
            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        }
        else if (playerSelection == 0 && cpuSelection == 1)
        {
            statusText.text = "You Lose";
            statusText.enabled = true;
            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        }
        else if (playerSelection == 0 && cpuSelection == 2)
        {
            statusText.text = "You Win";
            statusText.enabled = true;
            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        }
        else if(playerSelection == 1 && cpuSelection == 0)
        {
            statusText.text = "You Win";
            statusText.enabled = true;
            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        }
        else if (playerSelection == 1 && cpuSelection == 1)
        {
            statusText.text = "Draw";
            statusText.enabled = true;
            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        }
        else if (playerSelection == 1 && cpuSelection == 2)
        {
            statusText.text = "You Lose";
            statusText.enabled = true;
            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        }
        else if (playerSelection == 2 && cpuSelection == 0)
        {
            statusText.text = "You Lose";
            statusText.enabled = true;
            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        }
        else if (playerSelection == 2 && cpuSelection == 1)
        {
            statusText.text = "You Win";
            statusText.enabled = true;
            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        }
        else if (playerSelection == 2 && cpuSelection == 2)
        {
            statusText.text = "Draw";
            statusText.enabled = true;
            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        }

        //switch (playerSelection)
        //{
        //    case 0:
        //        if (cpuSelection == 0)
        //        {
        //            statusText.text = "Draw";
        //            statusText.enabled = true;
        //            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        //        }
        //        else if (cpuSelection == 1)
        //        {
        //            statusText.text = "You Lose";
        //            statusText.enabled = true;
        //            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        //        }
        //        else if (cpuSelection == 2)
        //        {
        //            statusText.text = "You Win";
        //            statusText.enabled = true;
        //            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        //        }
        //        break;

        //    case 1:
        //        if (cpuSelection == 0)
        //        {
        //            statusText.text = "You Win";
        //            statusText.enabled = true;
        //            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        //        }
        //        else if (cpuSelection == 1)
        //        {
        //            statusText.text = "Draw";
        //            statusText.enabled = true;
        //            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        //        }
        //        else if (cpuSelection == 2)
        //        {
        //            statusText.text = "You Lose";
        //            statusText.enabled = true;
        //            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        //        }
        //        break;

        //    case 2:
        //        if (cpuSelection == 0)
        //        {

        //            statusText.text = "You Lose";
        //            statusText.enabled = true;
        //            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        //        }
        //        else if (cpuSelection == 1)
        //        {

        //            statusText.text = "You Win";
        //            statusText.enabled = true;
        //            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        //        }
        //        else if (cpuSelection == 2)
        //        {
        //            statusText.text = "Draw";
        //            statusText.enabled = true;
        //            InvokeRepeating("ChangeSprites", 2f, 0.1f);
        //        }
        //        break;
        //    default:

        //        break;
        //}
    }
}
