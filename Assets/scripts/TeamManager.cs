using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using Assets.scripts;

[SerializeField]
public class TeamManager : MonoBehaviour {

    private static TeamManager instance = null;
    public bool setupTeamFlag = false;
    public TeamInfo teamInfo;
    public Dropdown dropdown;
    public List<string> names = new List<string>() { "Telar a Live", "Amestrar Macacos", "Cultivar Tomates", "Negociar Macacos", "Invocador de Ninjas" };
    private  GerenciadorCenas gerenciadorCenas;
    private GameManager GM;


    // Use this for initialization

    private void Awake()
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
    }

    private void InitTeamData(StatusCode.DataOrigin dataOrigin)
    {
        dropdown = FindObjectOfType<Dropdown>();
        if (dropdown != null && dataOrigin == StatusCode.DataOrigin.DEFAULT)
        {
            PopulateHabilitiesList();
        }
        GM = FindObjectOfType<GameManager>();
        if (GM)
        {
            GM.InitializeInPlayList(dataOrigin);
        }
        else
        {
            Debug.Log("Couldnt find game manager");
        }

    }
    public void SaveInPlayGameData()
    {
        string dataAsJson = JsonUtility.ToJson(teamInfo, true);
        string filePath = Application.dataPath + "/GameData/team/teamInfo.json";
        File.WriteAllText(filePath, dataAsJson);
        Debug.Log(dataAsJson);
        GM.SaveInPlayGameData();
    }

    public void LoadInPlayGameData()
    {
        string filePath = Application.dataPath + "/GameData/team/teamInfo.json";
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            Debug.Log(dataAsJson);

            teamInfo = JsonUtility.FromJson<TeamInfo>(dataAsJson);
            Debug.Log("Received TeamInfo data " + teamInfo.money + " " + teamInfo.coachName);

        }
        else
        {
            Debug.Log("gameData file not found at " + filePath);
        }
        InitTeamData(StatusCode.DataOrigin.SAVE_DATA);
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
        Debug.Log("Level Loaded" + scene.name);
        gerenciadorCenas = FindObjectOfType<GerenciadorCenas>();
        if (scene.name == "02 Team Creation")
        {
            setupTeamFlag = false;
            InitTeamData(StatusCode.DataOrigin.DEFAULT);
            teamInfo.money = 15000;
            SaveInPlayGameData();
        }
        else if (scene.name == "04 Lobby" && !setupTeamFlag)
        {
            setupTeamFlag = true;
            LoadInPlayGameData();
            UpdateUIDisplay();
        } else if (scene.name == "04 Lobby")
        {
            UpdateUIDisplay();
        }
    }



    private void PopulateHabilitiesList()
    {
        dropdown.AddOptions(names);
    }

    private void UpdateUIDisplay()
    {
        GameObject moneyDisplay = GameObject.FindGameObjectWithTag("currencyDisplay");
        GameObject coachName = GameObject.FindGameObjectWithTag( "InfoHud" );
        if (moneyDisplay != null)
        {
            if(moneyDisplay.GetComponent<Text>())
                moneyDisplay.GetComponent<Text>().text = "$L " + teamInfo.money.ToString();
        }
        if (coachName != null)
        {
            if (coachName.GetComponent<Text>())
                coachName.GetComponent<Text>().text = "Coach: \n" +  teamInfo.coachName.ToString();
        }
    }



    public void SetPlayerName(string name)
    {
        teamInfo.coachName = name;
        Debug.Log("Coach name: " + teamInfo.coachName);
    }

    public void SetTeamName(string name)
    {
        teamInfo.teamName = name;
        Debug.Log("team name: " + teamInfo.teamName);
    }

    public void SetAbilityId(int id)
    {
        teamInfo.abilityId = id;
        Debug.Log("Ability ID "+ teamInfo.abilityId);
    }

    public void VerifyFields()
    {
        GameObject cvs = GameObject.Find("Canvas");
        InputField[] inputFields = cvs.GetComponentsInChildren<InputField>();
        teamInfo.coachName = inputFields[0].text;
        teamInfo.teamName = inputFields[1].text;
        teamInfo.abilityId = cvs.GetComponentInChildren<Dropdown>().value;

        if (teamInfo.teamName.Length>1 && teamInfo.coachName.Length > 1 && teamInfo.abilityId >=0)
        {
            SaveInPlayGameData();
            this.setupTeamFlag = true;
        }
        else
        {
            Debug.Log("One of the fields is invalid(input fields or dropdown)");
        }
    }
}
