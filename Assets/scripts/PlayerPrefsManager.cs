using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsManager : MonoBehaviour {
 
    const string MASTER_VOLUME_KEY = "master_volume";
    const string DIFFICULT_KEY = "difficulty";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public static void SetMasterVolume(float volume)
    {
        if (volume >= 0 && volume <= 100)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("volume out of range");
        }
    }
    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }
    public static void SetDifficulty(float difficulty)
    {
        if(difficulty>=0f && difficulty<=3f)
        {
            PlayerPrefs.SetFloat(DIFFICULT_KEY, difficulty);
        }
        else
        {
            Debug.LogError("difficulty value out of range, value must be between 0 and 3");
        }

    }
    public static float GetDifficulty()
    {
        return PlayerPrefs.GetFloat(DIFFICULT_KEY);
    }

    
}
