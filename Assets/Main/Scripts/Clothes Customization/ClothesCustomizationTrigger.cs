using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesCustomizationTrigger : MonoBehaviour
{
    public ClothesCustomizer.ClothType ClothType;


    public Material material;


    //public int ClothTypeIndex;

    //public float[] BlendInitialValues;
    //public float[] BlendFinalValues;

    //[Header("Set Morph Values")]
    //public int MorphIndex;
    //public float MorphFrom;
    //public float MorphTo;
    //public float MorphTime;
    //public AnimationCurve AnimCurve;

    public void TriggerCustomization()
    {
        if(ClothType==ClothesCustomizer.ClothType.Shirt)
        {
            ClothesCustomizer.Instance.ApplyChangesToShirt(material);
        }
        else if (ClothType == ClothesCustomizer.ClothType.Pant)
        {
            ClothesCustomizer.Instance.ApplyChangesTopPant(material);
        }
        else if (ClothType == ClothesCustomizer.ClothType.Shoes)
        {
            ClothesCustomizer.Instance.ApplyChangesToShoes(material);
        }

        //CharacterCustomizationManager.Instance.ModifyCharacter_Face(MorphIndex, MorphFrom, MorphTo, MorphTime, AnimCurve);
    }
}
