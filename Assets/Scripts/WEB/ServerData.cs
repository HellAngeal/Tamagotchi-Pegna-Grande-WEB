using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using SimpleJSON;
using UnityEngine.SceneManagement;

public class ServerData : MonoBehaviour
{
    public static ServerData instance;

    public string userEmail,userName,userPoints,updateDataURL;

    public List<UserLeaderboard> UserLeaderboardPoints;



    void Awake()
    {
        if(instance==null)
        {
            instance=this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void UpdatePoints()
    {
        StartCoroutine(delaySaveBones());
    }

    IEnumerator delaySaveBones()
    {
        UserLeaderboardPoints.Clear();
         UDataUpdate uDataUpdate=new UDataUpdate(this.userEmail,this.userPoints);
 string json=JsonUtility.ToJson(uDataUpdate);
      
            var request = new UnityWebRequest(updateDataURL, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
if (request.isNetworkError || request.isHttpError)
{
   
Debug.Log(request.error);
}
else
{
string data=request.downloadHandler.text;
        Debug.Log(data);

         JSONNode jsonNode = SimpleJSON.JSON.Parse(data);
                 
                 if(jsonNode["status"].Value.ToString()=="false")
                 {
                //LoadingPanel.SetActive(false);
                Debug.LogError(jsonNode["error"].Value.ToString());
                 
                 }
                 else
                 {
                     Debug.LogError(jsonNode["Leaderboard"].Count);

                     for(int i=0;i<jsonNode["Leaderboard"].Count;i++)
                     {
                         UserLeaderboard userLeaderboard=new UserLeaderboard(jsonNode["Leaderboard"]["User"+i]["UName"].Value,jsonNode["Leaderboard"]["User"+i]["UPoints"].Value);
                         UserLeaderboardPoints.Add(userLeaderboard);
                     }
    }
}
    }

    	public void LoadScene(string SceneName)
	{
		//Application.LoadLevel (SceneName);
		SceneManager.LoadScene (SceneName);
	}

   
}
[System.Serializable]
public class UserLeaderboard
{
    public string UName;
    public string UBones;

    public UserLeaderboard(string UName,string UBones)
    {
        this.UName=UName;
        this.UBones=UBones;
    }
}

