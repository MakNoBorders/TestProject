using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class ClothesCustomizer : MonoBehaviour
{
    public static ClothesCustomizer Instance;

    public enum ClothType { Shirt, Pant, Shoes }
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

    public Material[] m_Materials;


    private void Awake()
    {

        Instance = this;
    }

    private void Start()
    {
        LoadSavedClothes();
    }

    public void ApplyChangesToShirt(int MaterialIndex)
    {
        Shirt.GetComponent<SkinnedMeshRenderer>().material = m_Materials[MaterialIndex];
        SaveAppliedChanges(ClothType.Shirt, MaterialIndex);

    }

    public void ApplyChangesTopPant(int MaterialIndex)
    {
        Pant.GetComponent<SkinnedMeshRenderer>().material = m_Materials[MaterialIndex];
        SaveAppliedChanges(ClothType.Pant, MaterialIndex);
    }

    public void ApplyChangesToShoes(int MaterialIndex)
    {
        Shoes.GetComponent<SkinnedMeshRenderer>().material = m_Materials[MaterialIndex];
        SaveAppliedChanges(ClothType.Shoes, MaterialIndex);
    }



    public void SaveAppliedChanges(ClothType clothTypes, int Index)
    {
        if (clothTypes == ClothType.Shirt)
        {
            PlayerPrefs.SetInt("Shirt", Index);
        }
        else if (clothTypes == ClothType.Pant)
        {
            PlayerPrefs.SetInt("Pant", Index);
        }
        else if (clothTypes == ClothType.Shoes)
        {
            PlayerPrefs.SetInt("Shoes", Index);
        }
    }

    void LoadSavedClothes()
    {
        int shirtindex = PlayerPrefs.GetInt("Shirt");
        int pantindex = PlayerPrefs.GetInt("Pant");
        int shoesindex = PlayerPrefs.GetInt("Shoes");

        ApplyChangesToShirt(shirtindex);
        ApplyChangesTopPant(pantindex);
        ApplyChangesToShoes(shoesindex);
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

}
