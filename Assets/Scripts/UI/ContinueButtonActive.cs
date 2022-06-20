using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ContinueButtonActive : MonoBehaviour
{
    public static bool alreadyPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        
        if(alreadyPlaying==true)
        {
            GetComponent<Button>().interactable = true;            
        }
        else if (alreadyPlaying == false)
        {
            GetComponent<Button>().interactable = false;
        }
       /* if(PlayerPrefs.HasKey("Health") || PlayerPrefs.HasKey("Health") && PlayerPrefs.GetInt("Health") == 0)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }*/
    }
    public void AlreadyPlay()
    {
        alreadyPlaying = true;
    }
    public void ResetTokens()
    {
        Test.tokensNumber = 3;
    }
}
