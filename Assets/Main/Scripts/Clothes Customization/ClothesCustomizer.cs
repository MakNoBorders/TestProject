using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class ClothesCustomizer : MonoBehaviour
{
    public static ClothesCustomizer Instance;

    public enum ClothType { Shirt,Pant,Shoes}
    [HideInInspector]
    public ClothType clothTypes;

    public GameObject Shirt;
    public GameObject Pant;
    public GameObject Shoes;

    //[Header("Shirts")]
    //public GameObject[] Shirts;

    //[Header("Pants")]
    //public GameObject[] Pants;

    //[Header("Shoes")]
    //public GameObject[] Shoes;


    private void Awake()
    {

        Instance = this;
    }

    public void ApplyChangesToShirt(Material material)
    {
        Shirt.GetComponent<SkinnedMeshRenderer>().material = material;
    }

    public void ApplyChangesTopPant(Material material)
    {
        Pant.GetComponent<SkinnedMeshRenderer>().material = material;
    }

    public void ApplyChangesToShoes(Material material)
    {
        Shoes.GetComponent<SkinnedMeshRenderer>().material = material;
    }








    public void ChangeShirt(int shirtIndex)
    {
        //for(int i=0;i<Shirts.Length;i++)
        //{
        //    Shirts[i].SetActive(i == shirtIndex ? true : false);
        //}
        //ModifyShirtAccordingToTheBodySize(Shirts[shirtIndex]);
    }

    public void ChangePant(int pantIndex)
    {
        //for (int i = 0; i < Pants.Length; i++)
        //{
        //    Pants[i].SetActive(i == pantIndex ? true : false);
        //}
        //ModifyPantAccordingToTheBodySize(Pants[pantIndex]);
    }

    public void ChangeShoes(int shoesIndex)
    {
        //for(int i=0;i<Shoes.Length;i++)
        //{
        //    Shoes[i].SetActive(i == shoesIndex ? true : false);
        //}
    }

    void ModifyShirtAccordingToTheBodySize(GameObject selectedShirt)
    {

    }

    void ModifyPantAccordingToTheBodySize(GameObject selectedPant)
    {

    }

    public void SaveAppliedChanges(ClothType clothTypes,int Index)
    {
        if (clothTypes == ClothType.Shirt)
        {

        }
        else if(clothTypes==ClothType.Pant)
        {

        }
        else if(clothTypes==ClothType.Shoes)
        {
            
        }
    }
}
