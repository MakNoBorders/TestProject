using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class ClothesCustomizer : MonoBehaviour
{
    public static ClothesCustomizer Instance;

    public enum ClothType { Shirt, Pant, Shoes }
    [HideInInspector]
    public ClothType m_ClothType;

    [Header("Clothes")]
    public GameObject m_Shirt;
    public GameObject m_Pant;
    public GameObject m_Shoes;

    [Header("Cloth Meshes")]
    public Mesh[] m_Shirts_Mesh;
    public Mesh[] m_Pants_Mesh;
    public Mesh[] m_Shoes_Mesh;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
       
    }



    #region Change Clothe Meshes

    public void ApplyChangesToShirt(int MaterialIndex)
    {
        //m_Shirt.GetComponent<SkinnedMeshRenderer>().material = m_Materials[MaterialIndex];
        //SaveAppliedChanges(ClothType.Shirt, MaterialIndex);
    }

    public void ApplyChangesTopPant(int MaterialIndex)
    {
        //m_Pant.GetComponent<SkinnedMeshRenderer>().material = m_Materials[MaterialIndex];
        //SaveAppliedChanges(ClothType.Pant, MaterialIndex);
    }

    public void ApplyChangesToShoes(int MaterialIndex)
    {
        //m_Shoes.GetComponent<SkinnedMeshRenderer>().material = m_Materials[MaterialIndex];
        //SaveAppliedChanges(ClothType.Shoes, MaterialIndex);
    }

    #endregion

    #region Save And Load Changes

    public void SaveAppliedChanges(ClothType l_ClothTypes, int l_MeshIndex)
    {
        if (l_ClothTypes == ClothType.Shirt)
        {
            m_AppliedShirtMeshIndex = l_MeshIndex;
        }
        else if (l_ClothTypes == ClothType.Pant)
        {
            m_AppliedPantMeshIndex = l_MeshIndex;
        }
        else if (l_ClothTypes == ClothType.Shoes)
        {
            m_AppliedShoesMeshIndex = l_MeshIndex;
        }
    }

    void LoadSavedClothes()
    {
        ApplyChangesToShirt(m_AppliedShirtMeshIndex);
        ApplyChangesTopPant(m_AppliedPantMeshIndex);
        ApplyChangesToShoes(m_AppliedShoesMeshIndex);
    }

    #endregion

    #region Properties 

    int m_AppliedShirtMeshIndex
    {
        get { return PlayerPrefs.GetInt("AppliedShirtMeshIndex"); }
        set { PlayerPrefs.SetInt("AppliedShirtMeshIndex", value); }
    }

    int m_AppliedPantMeshIndex
    {
        get { return PlayerPrefs.GetInt("AppliedPantMeshIndex"); }
        set { PlayerPrefs.SetInt("AppliedPantMeshIndex", value); }
    }

    int m_AppliedShoesMeshIndex
    {
        get { return PlayerPrefs.GetInt("AppliedShoesMeshIndex"); }
        set { PlayerPrefs.SetInt("AppliedShoesMeshIndex", value); }
    }

    #endregion

    #region Waste Code

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

    #endregion
}
