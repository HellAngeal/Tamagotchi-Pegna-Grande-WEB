using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text;
using SimpleJSON;

public class LoginManager : MonoBehaviour
{

    public InputField loginEmail,loginPassword;
    public Toggle rememberMe;

    public InputField signupEmail,signupName,signupPassword,signupConfirmPassword;

    public string loginURL,signUpURL="";

    public Text loginError,signUpError;

    public GameObject LoginPanel,SignUpPanel,LoadingPanel;

    void Start()
    {
        if(PlayerPrefs.GetString("isRemember")=="true")
        {
            LoginUser();
        }
    }


    public void LoginUser()
    {
        StartCoroutine(delayLogin());
    }

     public void SignUpUser()
    {
        StartCoroutine(delaySignUp());
    }

    IEnumerator delayLogin()
    {
        LoadingPanel.SetActive(true);
        string json;
         if(PlayerPrefs.GetString("isRemember")=="true")
        {
             ULoginData uLogin=new ULoginData(PlayerPrefs.GetString("Email"),PlayerPrefs.GetString("Password"));
  json=JsonUtility.ToJson(uLogin);
        }
        else
        {
 ULoginData uLogin=new ULoginData(loginEmail.text,loginPassword.text);
  json=JsonUtility.ToJson(uLogin);
        }
       
      
            var request = new UnityWebRequest(loginURL, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
if (request.isNetworkError || request.isHttpError)
{
    LoadingPanel.SetActive(false);
Debug.Log(request.error);
}
else
{
    
string data=request.downloadHandler.text;
        Debug.Log(data);

         JSONNode jsonNode = SimpleJSON.JSON.Parse(data);
                 
                 if(jsonNode["status"].Value.ToString()=="false")
                 {
                LoadingPanel.SetActive(false);
                  loginError.text=jsonNode["error"].Value.ToString();
                 }
                 else
                 {



                     if(rememberMe.isOn)
                     {
                         PlayerPrefs.SetString("Email",loginEmail.text);
                         PlayerPrefs.SetString("Password",loginPassword.text);
                         PlayerPrefs.SetString("isRemember","true");
                     }

                    ServerData.instance.userEmail=jsonNode["user"]["UEmail"].Value;
                    ServerData.instance.userName=jsonNode["user"]["UName"].Value;
                    ServerData.instance.userPoints=jsonNode["user"]["UPoints"].Value;

                    ServerData.instance.UpdatePoints();

                     FieldsReset();
                   Invoke("NextScene",2f);
                 }
}
    }

    void NextScene()
    {
  Application.LoadLevel(1);
    }

     IEnumerator delaySignUp()
    {
LoadingPanel.SetActive(true);
        USignUpData uSignUp=new USignUpData(signupName.text,signupEmail.text,signupPassword.text,signupConfirmPassword.text,"0");
 string json=JsonUtility.ToJson(uSignUp);
      
            var request = new UnityWebRequest(signUpURL, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler) new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();
if (request.isNetworkError || request.isHttpError)
{
    LoadingPanel.SetActive(false);
Debug.Log(request.error);
}
else
{
string data=request.downloadHandler.text;
        Debug.Log(data);

         JSONNode jsonNode = SimpleJSON.JSON.Parse(data);
                 
                 if(jsonNode["status"].Value.ToString()=="false")
                 {
                LoadingPanel.SetActive(false);
                  signUpError.text=jsonNode["error"].Value.ToString();
                 }
                 else
                 {
                     LoadingPanel.SetActive(false);
                     LoginPanel.SetActive(true);
                     SignUpPanel.SetActive(false);
                     FieldsReset();
                   

                 }
}
    }

    public void FieldsReset()
{
    loginEmail.text="";
    loginPassword.text="";
    signupName.text="";
    signupPassword.text="";
    signupConfirmPassword.text="";
    signupEmail.text="";
    loginError.text="";
    signUpError.text="";
    rememberMe.isOn=false;
}
}



public class ULoginData
{
    public string UEmail;
    public string UPassword;

    public ULoginData(string UEmail,string UPassword)
    {
        this.UEmail=UEmail;
        this.UPassword=UPassword;
    }
}


public class USignUpData
{
    public string UName;
    public string UEmail;
    public string UPassword;
    public string UCPassword;
    public string UPoints;

    public USignUpData(string UName,string UEmail,string UPassword,string UCPassword,string UPoints)
    {
        this.UName=UName;
        this.UEmail=UEmail;
        this.UPassword=UPassword;
        this.UCPassword=UCPassword;
        this.UPoints=UPoints;
    }
}

public class UDataUpdate
{
    public string UEmail;
    public string UPoints;

    public UDataUpdate(string UEmail,string UPoints)
    {
        this.UEmail=UEmail;
        this.UPoints=UPoints;
    }
}