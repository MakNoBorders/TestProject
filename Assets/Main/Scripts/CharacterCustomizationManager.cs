using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizationManager : MonoBehaviour
{
    // -- Variable Declaration 
    // Public Var --- CharacterName
    // Constant Var -- CHARACTER_NAME
    // Private Var ----_characterName
    // Inside Method Or Methdo Param -- characterName

    public static CharacterCustomizationManager Instance;

   

  

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

   
}
