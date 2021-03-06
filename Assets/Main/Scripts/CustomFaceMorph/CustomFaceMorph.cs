﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomFaceMorph : MonoBehaviour
{
    public static CustomFaceMorph Instance;

    [Header("UI")]
    public Slider XMorph_Slider;
    public Slider YMorph_Slider;

    [Header("Colors")]
    public Color ColorOne;
    public Color ColorTwo;

    [Header("Face Object")]
    public GameObject FaceObject;
    SkinnedMeshRenderer faceSkinMeshRenderer;


    GameObject faceMorphTriggerSelected;
    bool isMorphTriggerSelected;
    float xMin, xMax, yMin, yMax;

    int horMorphIndex;
    int verMorphIndex;

    int verDir;
    int horDir;

    private void Awake()
    {
        Instance = this;
    }

    
    void Start()
    {
        faceSkinMeshRenderer = FaceObject.GetComponent<SkinnedMeshRenderer>();

        if (PlayerPrefs.GetInt("CustomFaceMorphApplied") == 1)
        {
            //print("inside");
            LoadSaveMorphedValues();
        }
    }

    
    void Update()
    {
        if (isMorphTriggerSelected)
        {
            Vector3 pos= faceMorphTriggerSelected.transform.position+ new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), -1);
            faceMorphTriggerSelected.transform.position = Vector3.Lerp(faceMorphTriggerSelected.transform.position, pos, 0.0125f);

            ApplyConstraints(faceMorphTriggerSelected, xMin, xMax, yMin, yMax);

            XMorph_Slider.value += horDir*Input.GetAxis("Mouse X")*10;
            YMorph_Slider.value += verDir* Input.GetAxis("Mouse Y")*10;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(isMorphTriggerSelected)
            faceMorphTriggerSelected.GetComponent<SpriteRenderer>().color = ColorOne;
            isMorphTriggerSelected = false;
            faceMorphTriggerSelected = null;
        }
    }

    public void SelectedFaceMorphTrigger(GameObject selectedObject,float xmin,float xmax,float ymin,float ymax,int hormorphindex,int vermorphindex,int hordir,int verdir)
    {
        faceMorphTriggerSelected = selectedObject;
        isMorphTriggerSelected = true;
        this.xMin = xmin;
        this.xMax = xmax;
        this.yMin = ymin;
        this.yMax = ymax;

        horMorphIndex = hormorphindex;
        verMorphIndex = vermorphindex;

        horDir = hordir;
        verDir = verdir;

        XMorph_Slider.gameObject.SetActive(horMorphIndex != -1 ? true : false);
        YMorph_Slider.gameObject.SetActive(verMorphIndex != -1 ? true : false);

        XMorph_Slider.value = 0;
        YMorph_Slider.value = 0;
    }

    void ApplyConstraints(GameObject selectedObject,float xmin,float xmax,float ymin,float ymax)
    {
        float xTemp = Mathf.Clamp(selectedObject.transform.position.x, xmin, xmax);
        float yTemp = Mathf.Clamp(selectedObject.transform.position.y, ymin, ymax);
        selectedObject.transform.position = new Vector3(xTemp, yTemp, -1);
    }

    public void ApplyFaceMorph()
    {
        if(horMorphIndex!=-1)
        faceSkinMeshRenderer.SetBlendShapeWeight(horMorphIndex, XMorph_Slider.value);

        if(verMorphIndex!=-1)
        faceSkinMeshRenderer.SetBlendShapeWeight(verMorphIndex, YMorph_Slider.value);
    }


    public void ResetMorphing()
    {
        int count = faceSkinMeshRenderer.sharedMesh.blendShapeCount;

        for(int i=0;i<count;i++)
        {
            faceSkinMeshRenderer.SetBlendShapeWeight(i, 0);
        }
    }


    public void SaveAppliedMorphValues()
    {
        PlayerPrefs.SetInt("CustomFaceMorphApplied", 1);
        int count = faceSkinMeshRenderer.sharedMesh.blendShapeCount;

        PlayerPrefs.SetInt("CustomFaceMorphCount", count);

        for (int i = 0; i < count; i++)
        {
            PlayerPrefs.SetFloat("CustomFaceMorph" + i.ToString(), faceSkinMeshRenderer.GetBlendShapeWeight(i));
        }
    }

    public void LoadSaveMorphedValues()
    {
        int count = faceSkinMeshRenderer.sharedMesh.blendShapeCount;

        for (int i = 0; i < count; i++)
        {
            faceSkinMeshRenderer.SetBlendShapeWeight(i, PlayerPrefs.GetFloat("CustomFaceMorph" + i.ToString()));
        }
    }
}
