using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using System.Collections.Generic;       //Allows us to use Lists. 
using System;

[Serializable]
public class GameManager : MonoBehaviour
{

    private static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    [SerializeField]
    public PlayerList playersAllDefault;
    public PlayerList playersInPlay;
    public PlayerInfo currentFocusedPlayer;
    public TeamManager teamManager;
    private int zoabilidadePenalty = 1000;
    private float upkeepValueRatio = 0.04f;
    public bool playerListLoaded = false;
    private string allPlayersDefaultData = "/GameData/playersListDefault.json";
    private string allPlayersInPlayData = "/GameData/playersListInPlay.json";

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

    void Start()
    {
        LoadDefaultGameData();
        CalculatePlayersPrice();
        LoadInPlayGameData();
        DisplayListOfPlayers(playersInPlay);
        
    }
    private void OnLevelWasLoaded(int level)
    {
        if(level == 3) //Team Creation Level
        {
            InitializeInPlayList();
        }
    }

    void DisplayListOfPlayers(PlayerList list)
    {


        for(int i=0; i< list.players.Count;i++)
        {
            Debug.Log("ID: "+ list.players[i].id +" Name: " + list.players[i].name + " MMR: " + list.players[i].stats.mmr);
        }
    }

    public void InitializeInPlayList()
    {
        playersInPlay = new PlayerList();
        playersInPlay.players = playersAllDefault.players.GetRange(0, playersAllDefault.players.Count);
        //playersInPlay.players = playersAllDefault.players;
    }

    void CalculatePlayersPrice()
    {
        foreach(PlayerInfo player in playersAllDefault.players)
        {
            player.stats.value = Mathf.Round(( (player.stats.mmr + player.stats.initialMmr*(1+ player.stats.growthPotential))/2 ) - player.stats.zoabilidade*zoabilidadePenalty);
            player.stats.upkeep = Mathf.Round(player.stats.value * upkeepValueRatio);
        }

    }

    private void LoadInPlayGameData()
    {
        string filePath = Path.Combine(Application.dataPath, allPlayersInPlayData);
        Debug.Log(Application.dataPath);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            Debug.Log(dataAsJson);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            playersInPlay = JsonUtility.FromJson<PlayerList>(dataAsJson);
        }
        else
        {
            Debug.Log("gameData file not found at "+ filePath);
        }

    }
     private void LoadDefaultGameData()
    {
        string filePath = Application.dataPath + allPlayersDefaultData;
        Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            Debug.Log(dataAsJson);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            playersAllDefault = JsonUtility.FromJson<PlayerList>(dataAsJson);
            Debug.Log("List of players retrieved :" + dataAsJson);
        }
        else
        {
            Debug.Log("gameData file not found");
        }

        
    }

    public void SaveInPlayGameData()
    {
        string filepath = allPlayersInPlayData;
        string dataAsJson = JsonUtility.ToJson(playersInPlay, true);
        string filePath = Application.dataPath + filepath;
        File.WriteAllText(filePath, dataAsJson);
        Debug.Log(dataAsJson + playersInPlay);
    }

}