using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Test : MonoBehaviour {
	[SerializeField] Text tokensText;
	public Button[] play;
	public static int tokensNumber=3;
	bool hasTokens=true;
	void Update ( ) {
		tokensText.text = tokensNumber.ToString();
		if (tokensNumber < 1)
		{
			hasTokens = false;
		}
		TimeSpan oneAM = new TimeSpan (6, 35, 0);
		TimeSpan dateTime = DateTime.Now.TimeOfDay;
		 if (oneAM == dateTime)
		 {
			tokensNumber = 3;
			tokensText.text = tokensNumber.ToString();
		 }
	}

	public void UseToken()
    {
        if (hasTokens==true)
        {
			tokensNumber--;
			PlayerPrefs.Save();
        }
        else
        {
			return;
        }
	}
	public void CheckIfCanPlay()
    {
		if (tokensNumber == 0)
		{
			for (int i = 0; i < play.Length; i++)
			{
				play[i].interactable = false;
			}
		}
	}
}
