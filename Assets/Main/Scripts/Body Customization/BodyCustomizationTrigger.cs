using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCustomizationTrigger : MonoBehaviour
{
    [Header("Set Morph Values")]
    public BodyCustomizer.MorphType MorphTypes;
    public int m_BlendShapeOne;   // Blend Shape Index : if you are using only one blendshape then give -1 in the inspector in any one.
    public int m_BlendShapeTwo;
    public float m_BlendTime;
    public AnimationCurve m_AnimCurve;

    [Header("BlendShape Values For Body")]
    public float m_Weight;

    
    public void TriggerCustomization()
    {
        if(MorphTypes==BodyCustomizer.MorphType.Face)
        BodyCustomizer.Instance.ModifyCharacter_Face(m_BlendShapeOne, m_BlendShapeTwo, m_BlendTime, m_AnimCurve);

        if (MorphTypes == BodyCustomizer.MorphType.Body)
            BodyCustomizer.Instance.ModifyCharacter_Body(m_Weight);
    }
}
