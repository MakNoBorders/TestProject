using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyCustomizationTrigger : MonoBehaviour
{
    [Header("Set Morph Values")]
    public BodyCustomizer.MorphType MorphTypes;
    public int MorphOne;
    public int MorphTwo;
    public float MorphTime;
    public AnimationCurve AnimCurve;

    public float Weight;

    

    public void TriggerCustomization()
    {
        if(MorphTypes==BodyCustomizer.MorphType.Face)
        BodyCustomizer.Instance.ModifyCharacter_Face((int)MorphTypes, MorphOne, MorphTwo, MorphTime, AnimCurve);

        if (MorphTypes == BodyCustomizer.MorphType.Body)
            BodyCustomizer.Instance.CustomizeBody(Weight);
    }
}
