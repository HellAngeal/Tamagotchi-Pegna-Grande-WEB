using UnityEngine;
using System.Collections;

// this script will save all Care values when we exit the game or pause the game
public class CareSaver : MonoBehaviour {

    public Care[] AllCares;

    public void SaveAllCare()
    {
        foreach (Care c in AllCares)
            c.SaveCare();

        PlayerPrefs.Save();
    }

    public void DeleteAllCare()
    {
        foreach (Care c in AllCares)
            c.SaveCare();

        PlayerPrefs.DeleteAll();
    }

    void OnApplicationQuit()
    {
        SaveAllCare();
    }

    void OnApplicationPause(bool paused)
    {
        if (paused)
            SaveAllCare();
    }
}
