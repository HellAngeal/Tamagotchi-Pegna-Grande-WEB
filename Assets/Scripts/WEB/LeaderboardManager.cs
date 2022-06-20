using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{

    public GameObject content,prefabUser;

    // Start is called before the first frame update
    void Start()
    {
        FetchLeaderboard();
    }

   void FetchLeaderboard()
   {
       for(int i=0;i<ServerData.instance.UserLeaderboardPoints.Count;i++)
       {
           GameObject obj=Instantiate(prefabUser);
           obj.transform.parent=content.transform;
           obj.transform.localScale=new Vector3(1,1,1);
           obj.transform.GetChild(0).GetComponent<Text>().text=""+(i+1)+").";
            obj.transform.GetChild(1).GetComponent<Text>().text=""+ServerData.instance.UserLeaderboardPoints[i].UName;
             obj.transform.GetChild(2).GetComponent<Text>().text=""+ServerData.instance.UserLeaderboardPoints[i].UBones;
       }
   }

   public void Back()
   {
       Application.LoadLevel(1);
   }


}
