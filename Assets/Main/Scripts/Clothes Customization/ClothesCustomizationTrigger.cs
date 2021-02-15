using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesCustomizationTrigger : MonoBehaviour
{
    public ClothesCustomizer.ClothType ClothType;

    public int MaterialIndex;

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
        if (ClothType == ClothesCustomizer.ClothType.Shirt)
        {
            ClothesCustomizer.Instance.ApplyChangesToShirt(MaterialIndex);
        }
        else if (ClothType == ClothesCustomizer.ClothType.Pant)
        {
            ClothesCustomizer.Instance.ApplyChangesTopPant(MaterialIndex);
        }
        else if (ClothType == ClothesCustomizer.ClothType.Shoes)
        {
            ClothesCustomizer.Instance.ApplyChangesToShoes(MaterialIndex);
        }

        //CharacterCustomizationManager.Instance.ModifyCharacter_Face(MorphIndex, MorphFrom, MorphTo, MorphTime, AnimCurve);
    }
}
