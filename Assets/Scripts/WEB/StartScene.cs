using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    public void LoadScene(string SceneName)
    {
        ServerData.instance.LoadScene( SceneName);
    }
}
