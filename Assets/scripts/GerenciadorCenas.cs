using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorCenas : MonoBehaviour {

    public bool fixedSceneTime;
    public float delay = 2.5f;
    // Use this for initialization
    private void Start()
    {
        
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            quitGame();
        }
        if(fixedSceneTime && Time.timeSinceLevelLoad > delay)
        {
            LoadNextLevel();
        }
        
    }
    public void SceneSwitch(string Scene)
    {
        SceneManager.LoadScene(Scene, LoadSceneMode.Single);
    }
    public void SceneSwitch(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
    }
    public void LoadNextLevel()
    {
        // SceneManager.GetActiveScene().buildIndex ou gameObject.scene.buildIndex    //funcionam
        SceneManager.LoadScene(gameObject.scene.buildIndex + 1);
    }

    public void SceneSwitchWithAction(string Scene)
    {
        if(Scene == "03a Lobby Tutorial")
        {
            TeamManager TM = FindObjectOfType<TeamManager>();
            TM.VerifyFields();
        }
        SceneSwitch(Scene);
    }
    
     void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
       Debug.Log("Level Loaded "+ scene +" // " + mode);

   
    }
    public void quitGame()
    {
        Application.Quit();
    }

}
