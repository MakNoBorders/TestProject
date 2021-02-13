using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct CartedProduct
{
    public enum assettype { Shirts, Pants, Shoes }
    public assettype AssetType;

    public int price;
    public string iconpath;
}

public class ClientDataBaseManager : MonoBehaviour
{
    public static ClientDataBaseManager Instance;
    public enum AssetType {Shirts,Pants,Shoes}
    public AssetType AssetTypes;

    public int Coins;

    public Text m_CartButtnoText;
    int cartedproductindex;

    List<CartedProduct> m_CartedProducts = new List<CartedProduct>();


    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        InitializeClientAssets();
    }


    void InitializeClientAssets()
    {
        if (!IsCoinDataBaseInitialized && Coins > 0)
        {
            PlayerPrefs.SetInt("ClientCoins", Coins);
            IsCoinDataBaseInitialized = true;
        }
        else
        {
            Coins = PlayerPrefs.GetInt("ClientCoins");
        }
    }


    public void AddProductToCart(bool isAdded)
    {
        cartedproductindex = isAdded ? cartedproductindex + 1 : cartedproductindex - 1;

        m_CartButtnoText.text = "Buy("+ cartedproductindex.ToString()+")";
    }


    


    public void LoadClientAssets(AssetType assettype)
    {
        if(assettype==AssetType.Shirts)
        {

        }
    }


    public void CreateClientAssetObject(AssetType assetType,int price,string iconPath)
    {
        int index = PlayerPrefs.GetInt(assetType.ToString()+"LastIndex");
        index += 1;
        PlayerPrefs.SetInt(assetType.ToString()+index.ToString() + "Price",price);
        PlayerPrefs.SetString(assetType.ToString() + index.ToString() + "IconPath", iconPath);
    }


    #region Properties

    bool IsCoinDataBaseInitialized
    {
        get
        {
            return (PlayerPrefs.GetInt("IsCoinDataBaseInitialized") == 0 ? false : true);
        }

        set
        {
            PlayerPrefs.SetInt("IsCoinDataBaseInitialized", value ? 1 : 0);
        }
    }

    #endregion
}
