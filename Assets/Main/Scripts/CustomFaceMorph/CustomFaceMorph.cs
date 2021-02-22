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

    [Header("Selected Trigger/Constraints/Movement/Direction")]
    GameObject m_FaceMorphTriggerSelected; // Selected Trigger
    bool isMorphTriggerSelected;
    float xMin, xMax, yMin, yMax; // Movement Constraint For The Trigger
    int horMorphIndex; // in custom morph we can two blend shapes for one trigger
    int verMorphIndex;
    int verDir;
    int horDir;

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

    //** When we select a trigger on the custom morph panel, then is it initialize in this class, and its movemnet is also controlled by this class

    #region Select Morph Trigger, Update Position, Constraint, Apply Face Morph

    public void SelectedFaceMorphTrigger(GameObject selectedObject,float xmin,float xmax,float ymin,float ymax,int hormorphindex,int vermorphindex,int hordir,int verdir)
    {
        m_FaceMorphTriggerSelected = selectedObject;
        isMorphTriggerSelected = true;
        this.xMin = xmin;
        this.xMax = xmax;
        this.yMin = ymin;
        this.yMax = ymax;

        horMorphIndex = hormorphindex;
        verMorphIndex = vermorphindex;

        horDir = hordir;
        verDir = verdir;

        m_XMorph_Slider.gameObject.SetActive(horMorphIndex != -1 ? true : false);
        m_YMorph_Slider.gameObject.SetActive(verMorphIndex != -1 ? true : false);

        m_XMorph_Slider.value = 0;
        m_YMorph_Slider.value = 0;
    }

    void UpdateTriggerState()
    {
        if (isMorphTriggerSelected)
        {
            Vector3 pos = m_FaceMorphTriggerSelected.transform.position + new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), -1);
            m_FaceMorphTriggerSelected.transform.position = Vector3.Lerp(m_FaceMorphTriggerSelected.transform.position, pos, 0.0125f);

            ApplyConstraints(m_FaceMorphTriggerSelected, xMin, xMax, yMin, yMax);

            m_XMorph_Slider.value += horDir * Input.GetAxis("Mouse X") * 10;
            m_YMorph_Slider.value += verDir * Input.GetAxis("Mouse Y") * 10;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (isMorphTriggerSelected)
                m_FaceMorphTriggerSelected.GetComponent<SpriteRenderer>().color = m_ColorOne;
            isMorphTriggerSelected = false;
            m_FaceMorphTriggerSelected = null;
        }

        //m_CharacterFace.GetComponent<SkinnedMeshRenderer>().BakeMesh(l_mesh);
        //Vector3 setposition = transform.position + transform.InverseTransformPoint(l_mesh.vertices[l_mesh.triangles[1616 * 3 + 1]]) + m_OffSet;
        //Vector3 l_SpawnPosition = setposition;
        //m_SpriteToSpawn.transform.position = l_SpawnPosition;
    }

    void ApplyConstraints(GameObject selectedObject,float xmin,float xmax,float ymin,float ymax)
    {
        float xTemp = Mathf.Clamp(selectedObject.transform.position.x, xmin, xmax);
        float yTemp = Mathf.Clamp(selectedObject.transform.position.y, ymin, ymax);
        selectedObject.transform.position = new Vector3(xTemp, yTemp, -1);
    }

    public void ApplyFaceMorph() // Changing the FaceMorph Values With Sliders
    {
        if(horMorphIndex!=-1)
        m_FaceSkinMeshRenderer.SetBlendShapeWeight(horMorphIndex, m_XMorph_Slider.value);

        if(verMorphIndex!=-1)
        m_FaceSkinMeshRenderer.SetBlendShapeWeight(verMorphIndex, m_YMorph_Slider.value);
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
