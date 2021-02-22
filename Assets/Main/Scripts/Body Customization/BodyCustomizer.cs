using System.Collections;
using UnityEngine;

public class BodyCustomizer : MonoBehaviour
{
    public static BodyCustomizer Instance;

    public enum MorphType { Face, Body }  // To Set The "BodyCustomizerTrigger" To Modify Either The Face Or Body
    [HideInInspector]
    public MorphType m_MorphType;

    [Header("Character Components")]
    public GameObject m_CharacterFace;
    public GameObject m_CharacterBody;

    [Header("Body Meshes")]
    public Mesh[] m_BodyMeshes;

    // ** Skin Mesh Renderer ** //
    SkinnedMeshRenderer m_FaceSkinMeshRenderer;
    SkinnedMeshRenderer m_BodySkinMeshRenderer;

    // ** Applied Morph Values ** //  if already a morph is applied
    int m_AppliedMorphOneIndex;
    int m_AppliedMorphTwoIndex;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        Initialization();
    }

    #region Initilization

    void Initialization()
    {
        m_AppliedMorphOneIndex = -1;
        m_AppliedMorphTwoIndex = -1;
        m_FaceSkinMeshRenderer = m_CharacterFace.GetComponent<SkinnedMeshRenderer>();
        LoadLastAppliedFaceBlendShape();
    }

    #endregion

    #region Apply Face Blend Shape

    public void ModifyCharacter_Face (int l_IndexOne, int l_IndexTwo, float l_AnimateTime, AnimationCurve l_AnimCurve)
    {
        SaveCurrentApplieBlendShape(l_IndexOne, l_IndexTwo); 
        StartCoroutine(FaceModificationAnimation(l_AnimateTime, l_IndexOne, l_IndexTwo, l_AnimCurve));
    }

    IEnumerator FaceModificationAnimation(float l_AnimateTime, int l_IndexOne, int l_IndexTwo, AnimationCurve l_AnimCurve)
    {
        float l_t = 0;
        while (l_t <= l_AnimateTime)
        {
            l_t += Time.fixedDeltaTime;
            float blendWeight = Mathf.Lerp(0, 100, l_AnimCurve.Evaluate(l_t / l_AnimateTime));

            if (l_IndexOne != -1)
                m_FaceSkinMeshRenderer.SetBlendShapeWeight(l_IndexOne, blendWeight);

            if (l_IndexTwo != -1)
                m_FaceSkinMeshRenderer.SetBlendShapeWeight(l_IndexTwo, blendWeight);

            float l_ResetBlendWeight = Mathf.Lerp(100, 0, (l_t / l_AnimateTime));

            if (m_AppliedMorphOneIndex != -1)
                m_FaceSkinMeshRenderer.SetBlendShapeWeight(m_AppliedMorphOneIndex, l_ResetBlendWeight);  /// Reseting the last applied blendshape

            if (m_AppliedMorphTwoIndex != -1)
                m_FaceSkinMeshRenderer.SetBlendShapeWeight(m_AppliedMorphTwoIndex, l_ResetBlendWeight);

            yield return null;
        }
    }

    #endregion

    #region Change Body Size, Save ,Load

    public void ChangeBodySize(int l_BodyMeshIndex) // by applying meshes of different sizes
    {
        m_BodySkinMeshRenderer.sharedMesh = m_BodyMeshes[l_BodyMeshIndex];
    }

    void SaveAppliedBodyMeshIndex(int l_BodyMeshIndex)
    {
        m_AppliedBodyMeshIndex = l_BodyMeshIndex;
    }

    void LoadAppliedBodyMesh()
    {
        m_BodySkinMeshRenderer.sharedMesh = m_BodyMeshes[m_AppliedBodyMeshIndex];
    }

    #endregion

    #region Save And Load Face Blend Shapes

    private void SaveCurrentApplieBlendShape(int l_one, int l_two)
    {
        CustomFaceMorph.Instance.m_IsCustomBlendShapesApplied = false; // remove the custom applied morph to apply the new one

        m_IsFaceBlendShapeApplied = true;
        PlayerPrefs.SetInt("FaceMorphIndexOne", l_one);
        PlayerPrefs.SetInt("FaceMorphIndexTwo", l_two);

        m_AppliedMorphOneIndex = l_one;
        m_AppliedMorphTwoIndex = l_two;
    }

    void LoadLastAppliedFaceBlendShape()
    {
        if (!m_IsFaceBlendShapeApplied) return;

        m_AppliedMorphOneIndex = PlayerPrefs.GetInt("FaceMorphIndexOne");
        m_AppliedMorphTwoIndex = PlayerPrefs.GetInt("FaceMorphIndexTwo");

        if (m_AppliedMorphOneIndex != -1)
            m_FaceSkinMeshRenderer.SetBlendShapeWeight(m_AppliedMorphOneIndex, 100);

        if (m_AppliedMorphTwoIndex != -1)
            m_FaceSkinMeshRenderer.SetBlendShapeWeight(m_AppliedMorphTwoIndex, 100);

    }

    #endregion

    #region Properties

    bool m_IsFaceBlendShapeApplied
    {
        get { return PlayerPrefs.GetInt("FaceBlendShapeApplied") == 1 ? true : false; }
        set { PlayerPrefs.SetInt("FaceBlendShapeApplied", value ? 1 : 0); }
    }

    int m_AppliedBodyMeshIndex
    {
        get { return PlayerPrefs.GetInt("AppliedBodyMeshIndex"); }
        set { PlayerPrefs.SetInt("AppliedBodyMeshIndex", value); }
    }

    #endregion

    #region Waste Code

    public void ModifyCharacter_Body(float weight)
    {
        //m_Shirt.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, weight);
        //m_Pant.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, weight);

        //PlayerPrefs.SetInt("IsBodyMorphApplied", 1);
        //PlayerPrefs.SetFloat("BodyMorph", weight);
    }

    public void LoadLastAppliedBodyMorph()
    {
        if(PlayerPrefs.GetInt("IsBodyMorphApplied")==1)
        {
            //m_Shirt.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, PlayerPrefs.GetFloat("BodyMorph"));
            //m_Pant.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, PlayerPrefs.GetFloat("BodyMorph"));
        }
    }

    void ResetAllWeights()
    {
        int blends = m_FaceSkinMeshRenderer.sharedMesh.blendShapeCount;

        for (int i = 0; i < blends; i++)
        {
            m_FaceSkinMeshRenderer.SetBlendShapeWeight(i, 0);
        }
    }

    #endregion
}
