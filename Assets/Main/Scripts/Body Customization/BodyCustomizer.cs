using System.Collections;
using UnityEngine;

public class BodyCustomizer : MonoBehaviour
{
    // -- Variable Declaration 
    // Public Var --- CharacterName
    // Constant Var -- CHARACTER_NAME
    // Private Var ----_characterName
    // Inside Method Or Methdo Param -- characterName

    public static BodyCustomizer Instance;

    public enum MorphType { Face, Body }
    [HideInInspector]
    public MorphType MorphTypes;

    [Header("Character Components")]
    public GameObject Character;
    public GameObject CharacterBody;
    public GameObject CharacterFace;

    public GameObject m_Shirt;
    public GameObject m_Pant;

    // ** Skin Mesh Renderer ** //
    SkinnedMeshRenderer face_SkinMeshRenderer;

    // ** Applied Morph Values ** //  if already a morph is applied
    bool isMorphApplied;
    int appliedMorphOneIndex;
    int appliedMorphTwoIndex;
    float appliedMorphToValue;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        face_SkinMeshRenderer = CharacterFace.GetComponent<SkinnedMeshRenderer>();
        LoadLastAppliedFaceMorph(); // load the last applied face morph at start

        //face_SkinMeshRenderer.SetBlendShapeWeight(0, 100);
    }

    public void ModifyCharacter_Face(int morphIndex, int morphFrom, int morphTo, float morphTime, AnimationCurve animCurve)
    {
        //if (appliedMorphIndex == morphIndex) return; // if last applied morph index is same as new one then return, e.g pressing the same morph button
        ResetAllWeights();
        // RemoveAnyExistingBlendShape(); // if any morph is applied before then reset its values, to apply the new one
        SaveCurrentApplieBlendShape(morphFrom, morphTo); // save the applied morph
        StopAllCoroutines();

        //if(morphFrom!=-1)
        StartCoroutine(FaceModificationAnimation(morphTime, morphIndex, morphFrom, morphTo, animCurve)); // Apply New Morph

        // if (morphFrom != -1)
        // StartCoroutine(FaceModificationAnimation(morphTime, morphIndex, morphFrom, morphTo, animCurve)); // Apply New Morph
    }

    IEnumerator FaceModificationAnimation(float animateTime, int morphIndex, int morphFrom, int morphTo, AnimationCurve animCurve)
    {
        float t = 0;
        while (t <= animateTime)
        {
            t += Time.fixedDeltaTime;
            float blendWeight = Mathf.Lerp(0, 100, animCurve.Evaluate(t / animateTime));
            if (morphFrom != -1)
                face_SkinMeshRenderer.SetBlendShapeWeight(morphFrom, blendWeight);

            if (morphTo != -1)
                face_SkinMeshRenderer.SetBlendShapeWeight(morphTo, blendWeight);

            yield return null;
        }
    }

    public void RemoveAnyExistingBlendShape()
    {
        //face_SkinMeshRenderer.SetBlendShapeWeight(appliedMorphIndex, 0);
    }

    private void SaveCurrentApplieBlendShape(int l_one, int l_two)
    {
        //PlayerPrefs.SetInt("IsFaceMorphApplied", 1);
        PlayerPrefs.SetInt("FaceMorphIndexOne", l_one);
        PlayerPrefs.SetInt("FaceMorphIndexTwo", l_two);

        //isMorphApplied = true;
        //appliedMorphIndex = morphIndex;
        //appliedMorphToValue = morphTo;

        appliedMorphOneIndex = l_one;
        appliedMorphTwoIndex = l_two;
    }

    void LoadLastAppliedFaceMorph()
    {
        //isMorphApplied = PlayerPrefs.GetInt("IsFaceMorphApplied") == 1 ? true : false;
        //appliedMorphIndex = PlayerPrefs.GetInt("AppliedFaceMorphIndex");
        //appliedMorphToValue = PlayerPrefs.GetFloat("AppliedFaceMorphToValue");

        appliedMorphOneIndex = PlayerPrefs.GetInt("FaceMorphIndexOne");
        appliedMorphTwoIndex = PlayerPrefs.GetInt("FaceMorphIndexTwo");

        if (true)//isMorphApplied)
        {
            if (appliedMorphOneIndex != -1)
                face_SkinMeshRenderer.SetBlendShapeWeight(appliedMorphOneIndex, 100);

            if (appliedMorphTwoIndex != -1)
                face_SkinMeshRenderer.SetBlendShapeWeight(appliedMorphTwoIndex, 100);
        }
    }

    void ResetAllWeights()
    {
        int blends = face_SkinMeshRenderer.sharedMesh.blendShapeCount;

        for (int i = 0; i < blends; i++)
        {
            face_SkinMeshRenderer.SetBlendShapeWeight(i, 0);
        }
    }



    public void CustomizeBody(float weight)
    {
        m_Shirt.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, weight);
        m_Pant.GetComponent<SkinnedMeshRenderer>().SetBlendShapeWeight(0, weight);
    }
}
