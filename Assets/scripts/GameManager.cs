using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.IO;
using System.Collections.Generic;       //Allows us to use Lists.
using System;
using Assets.scripts;

[Serializable]
public class GameManager : MonoBehaviour
{

    private static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    [SerializeField]
    public PlayerList playersAllDefault;
    [SerializeField]
    public PlayerList playersInPlay;
    public PlayerInfo currentFocusedPlayer;
    public TeamManager teamManager;
    private int zoabilidadePenalty = 1000;
    private float upkeepValueRatio = 0.04f;
    public bool playerListLoaded = false;
    private readonly string allPlayersDefaultData = "/GameData/playersListDefault.json";
    private readonly string allPlayersInPlayData = "/GameData/playersListInPlay.json";

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
        LoadInPlayGameData();
        CalculatePlayersPrice();
        DisplayListOfPlayers(playersInPlay);

    }
    private void OnLevelWasLoaded(int level)
    {
        if(level == 3) //Team Creation Level
        {
            InitializeInPlayList(StatusCode.DataOrigin.DEFAULT);
            LoadInPlayGameData();
        }
    }

    void DisplayListOfPlayers(PlayerList list)
    {


        for(int i=0; i< list.players.Count;i++)
        {
            Debug.Log("ID: "+ list.players[i].id +" Name: " + list.players[i].name + " MMR: " + list.players[i].stats.mmr);
        }
    }

    public void InitializeInPlayList(StatusCode.DataOrigin initMode)
    {
        string filePath;
        if (initMode == StatusCode.DataOrigin.SAVE_DATA)
        {
            filePath = Application.dataPath + allPlayersInPlayData;
        }
        else {
            filePath = Application.dataPath + allPlayersDefaultData;
        }
        Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            Debug.Log(dataAsJson);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            playersInPlay = JsonUtility.FromJson<PlayerList>(dataAsJson);
            Debug.Log("List of players retrieved :" + dataAsJson);
        }
        else
        {
            Debug.Log("gameData file not found");
        }

    }

    void CalculatePlayersPrice()
    {
        foreach(PlayerInfo player in playersAllDefault.players)
        {
            player.stats.value = player.stats.mmr/2 + (player.stats.initialMmr*((100+ player.stats.growthPotential)/2))/100 - (player.stats.zoabilidade * zoabilidadePenalty)/100;
            player.stats.upkeep = (int)(player.stats.value * upkeepValueRatio);
        }

    }

    public void LoadInPlayGameData()
    {
        string filepath = Application.dataPath + allPlayersInPlayData;
        Debug.Log(Application.dataPath);
        if (File.Exists(filepath))
        {
            string dataAsJson = File.ReadAllText(filepath);
            Debug.Log(dataAsJson);
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            playersInPlay = JsonUtility.FromJson<PlayerList>(dataAsJson);
        }
        else
        {
            Debug.Log("gameData file not found at "+ filepath);
        }

    }
     public void LoadDefaultGameData()
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
        string filepath = Application.dataPath + allPlayersInPlayData;
        string dataAsJson = JsonUtility.ToJson(playersInPlay, true);
        File.WriteAllText(filepath, dataAsJson);
        Debug.Log(dataAsJson + playersInPlay);
    }

}