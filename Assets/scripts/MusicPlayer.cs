using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

    public AudioClip[] Clips;
    public PlayerPrefsManager prefsManager;
    private AudioSource audio;
    // Use this for initialization


    private void Start()
    {


    }

    void Awake () {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        int level= scene.buildIndex;
        audio = GetComponent<AudioSource>();
        Debug.Log("Volume: "+ PlayerPrefsManager.GetMasterVolume());

        if (level < 8)
        {
            if (Clips[level] != null)
            {
                Debug.Log("Clip: " + Clips[level] + "|");
                audio.volume = PlayerPrefsManager.GetMasterVolume();
                audio.clip = Clips[level];
                audio.loop = true;
                audio.Play();
            }
        }
    }

    public void changeVolume(float vol)
    {
        audio.volume = vol;
    }
   
}
