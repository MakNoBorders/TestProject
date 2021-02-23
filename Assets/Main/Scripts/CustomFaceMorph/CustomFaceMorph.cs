using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomFaceMorph : MonoBehaviour
{
    public static CustomFaceMorph Instance;

    [Header("UI")]
    public Slider m_XMorph_Slider;  // Sliders to change the Slected Face Blend Shapes
    public Slider m_YMorph_Slider;

    [Header("Colors")]
    public Color m_ColorOne; // On Click Change the Color Of The Trigger Sprite
    public Color m_ColorTwo;

    [Header("Face")]
    public GameObject m_CharacterFace;
    SkinnedMeshRenderer m_FaceSkinMeshRenderer;

    [Header("Custom Face Blend Shape Triggers")]
    public GameObject[] m_CustomBlendTriggers;

    [Header("Selected Trigger/Constraints/Movement/Direction")]
    GameObject m_FaceMorphTriggerSelected; // Selected Trigger
    [HideInInspector] public GameObject m_AssociatedTrigger;
    bool isMorphTriggerSelected;
    float xMin, xMax, yMin, yMax; // Movement Constraint For The Trigger
    int m_BlendShapeIndex;
    int m_MovementDirection;
    bool m_HorOrVer;

    int horMorphIndex; // in custom morph we can two blend shapes for one trigger
    int verMorphIndex;
    int verDir;
    int horDir;
    float movementScaler;

    // ** Is CustomMorph Panel Activated ** //
    [HideInInspector]
    public bool m_IsCustomMorphPanelActivated;


    [Header("Testing")]
    public GameObject m_SpriteToSpawn;
    public Vector3 m_OffSet;
    Mesh l_mesh;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        m_FaceSkinMeshRenderer = m_CharacterFace.GetComponent<SkinnedMeshRenderer>();
        LoadSavedBlendShape();
    }

    void Update()
    {
        if (m_IsCustomMorphPanelActivated)
            UpdateTriggerState();
    }


    public void OnLoadCustomFaceMorphPanel()
    {
        m_IsCustomMorphPanelActivated = true;

        LoadCustomBlendShapeTriggers();
    }

    //** When we select a trigger on the custom morph panel, then is it initialize in this class, and its movemnet is also controlled by this class

    #region Select Morph Trigger, Update Position, Constraint, Apply Face Morph

    public void SelectedFaceMorphTrigger(GameObject l_SelectedTrigger,GameObject l_AssociatedTrigger,float xmin,float xmax,float ymin,float ymax,int l_BlendShapeIndex,int l_MovementDirection,float l_MovementScaler,bool l_HorOrVer)//True For Horizontal And False For Vertical
    {
        m_FaceMorphTriggerSelected = l_SelectedTrigger;
        m_AssociatedTrigger = l_AssociatedTrigger;
        
        this.xMin = xmin;
        this.xMax = xmax;
        this.yMin = ymin;
        this.yMax = ymax;

        m_BlendShapeIndex = l_BlendShapeIndex;
        m_MovementDirection = l_MovementDirection;
        movementScaler = l_MovementScaler;

        m_HorOrVer = l_HorOrVer;

        isMorphTriggerSelected = true;

        //horMorphIndex = hormorphindex;
        //verMorphIndex = vermorphindex;
        //horDir = hordir;
        //verDir = verdir;

        m_XMorph_Slider.gameObject.SetActive(l_HorOrVer);
        m_YMorph_Slider.gameObject.SetActive(!l_HorOrVer);

        //m_XMorph_Slider.value = 0;
        //m_YMorph_Slider.value = 0;
    }

    void UpdateTriggerState()
    {
        if (isMorphTriggerSelected)
        {
            if (m_HorOrVer)
            {
                print("Sidler x");
                m_XMorph_Slider.value += m_MovementDirection * Input.GetAxis("Mouse X") * 10;
            }

            if (!m_HorOrVer)
            {
                print("Sidler x");
                m_YMorph_Slider.value += m_MovementDirection * Input.GetAxis("Mouse Y") * 10;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //if (isMorphTriggerSelected)
              //  m_FaceMorphTriggerSelected.GetComponent<SpriteRenderer>().color = m_ColorOne;
            isMorphTriggerSelected = false;
            //m_FaceMorphTriggerSelected = null;
        }

        //m_CharacterFace.GetComponent<SkinnedMeshRenderer>().BakeMesh(l_mesh);
        //Vector3 setposition = transform.position + transform.InverseTransformPoint(l_mesh.vertices[l_mesh.triangles[1616 * 3 + 1]]) + m_OffSet;
        //Vector3 l_SpawnPosition = setposition;
        //m_SpriteToSpawn.transform.position = l_SpawnPosition;
    }

    void LoadCustomBlendShapeTriggers()
    {
        float l_XMin, l_XMax, l_YMin, l_YMax;

        int l_Index = 1;

        l_XMin = m_CustomBlendTriggers[l_Index].GetComponent<CustomFaceMorphTrigger>().XMin;
        l_XMax = m_CustomBlendTriggers[l_Index].GetComponent<CustomFaceMorphTrigger>().XMax;
        l_YMin = m_CustomBlendTriggers[l_Index].GetComponent<CustomFaceMorphTrigger>().YMin;
        l_YMax = m_CustomBlendTriggers[l_Index].GetComponent<CustomFaceMorphTrigger>().YMax;

        int l_AssociatedBlendShape = m_CustomBlendTriggers[l_Index].GetComponent<CustomFaceMorphTrigger>().m_BlendShapeIndex;

        bool l_HorOrVer= m_CustomBlendTriggers[l_Index].GetComponent<CustomFaceMorphTrigger>().m_HorOrVer;

        if (m_IsCustomBlendShapesApplied)
        {
            //if(!m_HorOrVer)
            //m_CustomBlendTriggers[l_Index].transform.position = new Vector3(m_CustomBlendTriggers[l_Index].transform.position.x, -(l_YMax - l_YMin) * PlayerPrefs.GetFloat("CustomFaceMorph" + l_AssociatedBlendShape) * 0.01f + l_YMax, 0);

            if (m_HorOrVer)
            { }
                m_CustomBlendTriggers[l_Index].transform.position = new Vector3( (l_XMax - l_XMin) * PlayerPrefs.GetFloat("CustomFaceMorph" + l_AssociatedBlendShape) * 0.01f + l_XMin, m_CustomBlendTriggers[l_Index].transform.position.y, 0);


            m_XMorph_Slider.value = PlayerPrefs.GetFloat("CustomFaceMorph" + l_AssociatedBlendShape);
        }
    }

    void ApplyConstraints(GameObject selectedObject,float xmin,float xmax,float ymin,float ymax)
    {
        float xTemp = Mathf.Clamp(selectedObject.transform.position.x, xmin, xmax);
        float yTemp = Mathf.Clamp(selectedObject.transform.position.y, ymin, ymax);
        selectedObject.transform.position = new Vector3(xTemp, yTemp, -1);
    }

    public void ApplyFaceMorph() // Changing the FaceMorph Values With Sliders
    {
        if (m_HorOrVer)
            m_FaceSkinMeshRenderer.SetBlendShapeWeight(m_BlendShapeIndex, m_XMorph_Slider.value);

        if (!m_HorOrVer)
            m_FaceSkinMeshRenderer.SetBlendShapeWeight(m_BlendShapeIndex, m_YMorph_Slider.value);

        //if (horMorphIndex != -1)
        //    m_FaceSkinMeshRenderer.SetBlendShapeWeight(horMorphIndex, m_XMorph_Slider.value);

        //if (verMorphIndex != -1)
        //    m_FaceSkinMeshRenderer.SetBlendShapeWeight(verMorphIndex, m_YMorph_Slider.value);

        if (m_FaceMorphTriggerSelected == null) return;


        if (m_HorOrVer)
        {
            print("inside hori");

            if (m_MovementDirection == -1)
                m_FaceMorphTriggerSelected.transform.position = new Vector3(-(xMax - xMin) * m_XMorph_Slider.value * 0.01f + xMax, m_FaceMorphTriggerSelected.transform.position.y, 3);

            if (m_MovementDirection == 1)
                m_FaceMorphTriggerSelected.transform.position = new Vector3((xMax - xMin) * m_XMorph_Slider.value * 0.01f + xMin, m_FaceMorphTriggerSelected.transform.position.y, 3);

        }

        if (!m_HorOrVer)
        {




            if (m_MovementDirection == -1)
                m_FaceMorphTriggerSelected.transform.position = new Vector3(m_FaceMorphTriggerSelected.transform.position.x, -(yMax - yMin) * m_YMorph_Slider.value * 0.01f + yMax, 3);

            if (m_MovementDirection == 1)
                m_FaceMorphTriggerSelected.transform.position = new Vector3(m_FaceMorphTriggerSelected.transform.position.x, (yMax - yMin) * m_YMorph_Slider.value * 0.01f + yMin, 3);


        }
        //Vector3 pos = m_FaceMorphTriggerSelected.transform.position + new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), -1) * movementScaler;
        //m_FaceMorphTriggerSelected.transform.position = Vector3.Lerp(m_FaceMorphTriggerSelected.transform.position, pos, 0.0125f);

        //ApplyConstraints(m_FaceMorphTriggerSelected, xMin, xMax, yMin, yMax);

        if (m_AssociatedTrigger != null)
        {
            float l_XMin, l_XMax, l_YMin, l_YMax;

            l_XMin = m_AssociatedTrigger.GetComponent<CustomFaceMorphTrigger>().XMin;
            l_XMax = m_AssociatedTrigger.GetComponent<CustomFaceMorphTrigger>().XMax;
            l_YMin = m_AssociatedTrigger.GetComponent<CustomFaceMorphTrigger>().YMin;
            l_YMax = m_AssociatedTrigger.GetComponent<CustomFaceMorphTrigger>().YMax;

            if (m_HorOrVer)
            {
                print("inside hori");

                if (m_MovementDirection == -1)
                m_AssociatedTrigger.transform.position = new Vector3(-(l_XMax - l_XMin) * m_XMorph_Slider.value * 0.01f + l_XMax, m_AssociatedTrigger.transform.position.y, 3);

                if (m_MovementDirection == 1)
                    m_AssociatedTrigger.transform.position = new Vector3((l_XMax - l_XMin) * m_XMorph_Slider.value * 0.01f + l_XMin, m_AssociatedTrigger.transform.position.y, 3);

            }


            //Vector3 postwo = m_AssociatedPoint.transform.position + new Vector3(-Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), -1);
            //m_AssociatedPoint.transform.position = Vector3.Lerp(m_AssociatedPoint.transform.position, postwo, 0.0125f);
        }


    }

    #endregion

    #region Save And Load Saved BlendShapes

    public void SaveAppliedBlendShape()
    {
        m_IsCustomBlendShapesApplied = true;
        int count = m_FaceSkinMeshRenderer.sharedMesh.blendShapeCount;

        PlayerPrefs.SetInt("CustomFaceMorphCount", count); // How many blend shapes does the face have ,, 
        for (int i = 0; i < count; i++)
        {
            PlayerPrefs.SetFloat("CustomFaceMorph" + i.ToString(), m_FaceSkinMeshRenderer.GetBlendShapeWeight(i)); // Saving the blend shape values
        }
    }

    public void LoadSavedBlendShape()
    {
        if (!m_IsCustomBlendShapesApplied) return;

        int count = m_FaceSkinMeshRenderer.sharedMesh.blendShapeCount;
        for (int i = 0; i < count; i++)
        {
            m_FaceSkinMeshRenderer.SetBlendShapeWeight(i, PlayerPrefs.GetFloat("CustomFaceMorph" + i.ToString()));
        }
    }

    #endregion

    #region Properties

    public bool m_IsCustomBlendShapesApplied
    {
        get { return (PlayerPrefs.GetInt("CustomFaceMorphApplied") == 1 ? true : false); }
        set { PlayerPrefs.SetInt("CustomFaceMorphApplied", value ? 1 : 0); }
    }

    #endregion

    #region Waste Code

    public void ResetMorphing()
    {
        int count = m_FaceSkinMeshRenderer.sharedMesh.blendShapeCount;

        for (int i = 0; i < count; i++)
        {
            m_FaceSkinMeshRenderer.SetBlendShapeWeight(i, 0);
        }
    }

    public void InstantiatePoint()
    {
        Mesh mesh = m_CharacterFace.GetComponent<SkinnedMeshRenderer>().sharedMesh;

        Vector3 l_SpawnPosition = transform.position + transform.InverseTransformPoint(mesh.vertices[mesh.triangles[1616 * 3 + 1]]) + m_OffSet;
        m_SpriteToSpawn.transform.position = l_SpawnPosition;
    }

    #endregion
}
