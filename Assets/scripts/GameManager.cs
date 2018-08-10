using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;

using System.Collections.Generic;       //Allows us to use Lists. 

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public List<PlayerInfo> players;
    public PlayerInfo player;
    private string gameDataFileName = "playerslist.json";

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        LoadGameData();

    }

    //Update is called every frame.
    void Update()
    {

    }

    private void LoadGameData()
    { 
        string filePath = Path.Combine(Application.dataPath, gameDataFileName);
        Debug.Log(Application.dataPath);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            Debug.Log(dataAsJson);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            player = JsonUtility.FromJson<PlayerInfo>(dataAsJson);
        }
        else
        {
            Debug.Log("gameData file not found");
        }
       
        Debug.Log("Player "+ player.name);
    }

}