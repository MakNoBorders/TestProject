using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public struct Panel
{
    public GameObject panel;
    public string panelName;
}

public class CharacterCustomizationUIManager : MonoBehaviour
{
    public GameObject BodyCustomizationUISection;
    public GameObject ClothesCustomizationUISection;

    [Header("Body Customization Panels")]
    public Panel[] Panels_BodyCustomization; // face , body etc panels

    [Header("Clothes Customization Panels")]
    public Panel[] Panels_ClothesCustomization; // shirts, pants and shoes panels


    public GameObject m_Character;

    public GameObject BlinkAnimationPanel;
    public AnimationCurve m_AnimCurve;
    public AnimationCurve m_AnimCurve1;
    public float m_AnimTime;

    public Color colortwo;

    Dictionary<int, string> panelNames_BodyCustomization = new Dictionary<int, string>();
    Dictionary<int, string> panelNames_ClothesCustomization = new Dictionary<int, string>();


    void Start()
    {
        for (int i = 0; i < Panels_BodyCustomization.Length; i++)
        {
            panelNames_BodyCustomization.Add(i, Panels_BodyCustomization[i].panelName);
        }

        for (int i = 0; i < Panels_ClothesCustomization.Length; i++)
        {
            panelNames_ClothesCustomization.Add(i, Panels_ClothesCustomization[i].panelName);
        }

        //BlinkAnimationPanel.GetComponent<Image>().color = Color.clear;

        //StartCoroutine(PanelBlinkAnimation(m_AnimTime));
    }

    public void GoToMainMenu()
    {
        ResetCameraPOsition();
    }

    public void LoadPanel_BodyCustomization(string panelName)
    {
        for (int i = 0; i < Panels_BodyCustomization.Length; i++)
        {
            if (panelName == Panels_BodyCustomization[i].panelName)
            {
                Panels_BodyCustomization[i].panel.SetActive(true);

                if(panelName=="Face")
                {
                    ChangeCameraPosition();
                }
                else
                {
                    ResetCameraPOsition();
                }
            }
            else
            {
                Panels_BodyCustomization[i].panel.SetActive(false);
            }
        }
    }

    public void LoadPanel_ClothesCustomization(string panelName)
    {
        for (int i = 0; i < Panels_ClothesCustomization.Length; i++)
        {
            if (panelName == Panels_ClothesCustomization[i].panelName)
            {
                Panels_ClothesCustomization[i].panel.SetActive(true);
            }
            else
            {
                Panels_ClothesCustomization[i].panel.SetActive(false);
            }
        }
    }

    public void LoadSection_BodyCustomization()
    {
        BodyCustomizationUISection.SetActive(true);
        ClothesCustomizationUISection.SetActive(false);

        ChangeCameraPosition();

    }

    public void LoadSection_ClothesCustomization()
    {
        BodyCustomizationUISection.SetActive(false);
        ClothesCustomizationUISection.SetActive(true);

        ResetCameraPOsition();
    }

    void ChangeCameraPosition()
    {
        StartPanelBlinkAnimation();
        Camera.main.transform.position = new Vector3(-0.15f, 1.5f, -10.0f);
        Camera.main.orthographicSize = 0.5f;

        m_Character.GetComponent<Animator>().SetBool("Idle", true);

       // BlinkAnimationPanel.SetActive(true);

        //StartCoroutine(ZoomCamera(0.2f, true));
    }

    void ResetCameraPOsition()
    {
        StartPanelBlinkAnimation();
        Camera.main.transform.position = new Vector3(-0.15f, -0.3f, -10.0f);
        Camera.main.orthographicSize = 2.5f;

        m_Character.GetComponent<Animator>().SetBool("Idle", false);

        // BlinkAnimationPanel.SetActive(true);

        //StartCoroutine(ZoomCamera(0.2f, false));
    }

    IEnumerator ZoomCamera(float l_TimeLimit,bool l_Dir)
    {
        float l_t = 0;

        Vector3 p_from = new Vector3(0, -0.3f, -10.0f);
        Vector3 p_to = new Vector3(0, 1.5f, -10.0f);

        float o_from = 2.5f;
        float o_to = 0.5f;

        while (l_t <=l_TimeLimit)
        {
            l_t += Time.fixedDeltaTime;
            Camera.main.transform.position = Vector3.Lerp((l_Dir? p_from:p_to), (l_Dir ? p_to : p_from), (l_t / l_TimeLimit));
            Camera.main.orthographicSize = Mathf.Lerp((l_Dir ? o_from : o_to), (l_Dir ? o_to : o_from), (l_t / l_TimeLimit));
            yield return null;
        }

        BlinkAnimationPanel.SetActive(false);
    }

    void StartPanelBlinkAnimation()
    {
        StartCoroutine(PanelBlinkAnimation(m_AnimTime));
    }

    IEnumerator PanelBlinkAnimation(float l_TimeLimit)
    {
        float l_t = 0;

        //Vector3 p_from = new Vector3(0, -0.3f, -10.0f);
        //Vector3 p_to = new Vector3(0, 1.5f, -10.0f);

        //float o_from = 2.5f;
        //float o_to = 0.5f;

        Color c_from = colortwo;
        Color c_to = Color.white;

        float t1=0, t2=0;

        while (l_t <= l_TimeLimit)
        {
            l_t += Time.fixedDeltaTime;

            //Camera.main.transform.position = Vector3.Lerp((l_Dir ? p_from : p_to), (l_Dir ? p_to : p_from), (l_t / l_TimeLimit));
            //Camera.main.orthographicSize = Mathf.Lerp((l_Dir ? o_from : o_to), (l_Dir ? o_to : o_from), (l_t / l_TimeLimit));
            if (l_t <= l_TimeLimit/2)
            {
                t1 += Time.fixedDeltaTime;
                BlinkAnimationPanel.GetComponent<Image>().color = Color.Lerp(c_from, c_to,m_AnimCurve.Evaluate(t1 / (l_TimeLimit/2)));
            }
            else
            {
                t2 += Time.fixedDeltaTime;
                BlinkAnimationPanel.GetComponent<Image>().color = Color.Lerp(c_to, c_from, m_AnimCurve1.Evaluate(t2 / (l_TimeLimit/2)));
            }

            yield return null;
        }
    }
}
