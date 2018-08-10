using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TeamManager : MonoBehaviour {

    public bool setupTeamFlag = false;
    public Dropdown dropdown;
    public List<string> names = new List<string>() { "Telar a Live", "Amestrar Macacos", "Negociador de Bananas", "Negociados de Macacos", "Invocador de Ninjas" };
    private string teamName;
    private string coachName;
    private int abilityId;
    public GerenciadorCenas gerenciadorCenas;


    // Use this for initialization
    void Start()
    {
        dropdown = FindObjectOfType<Dropdown>();
        if (dropdown!=null)
        {
            PopulateHabilitiesList();
        }
            
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void PopulateHabilitiesList()
    {
       
        dropdown.AddOptions(names);

    }



    public void SetPlayerName(string name)
    {
        coachName = name;
        Debug.Log("team name: " + coachName);
    }

    public void SetTeamName(string name)
    {
        teamName = name;
        Debug.Log("team name: " + teamName);
    }

    public void SetAbilityId(int id)
    {
        abilityId = id;
        Debug.Log("Ability ID "+abilityId);
    }

    public void VerifyFields()
    {
        if(teamName.Length>1 && coachName.Length > 1 &&  abilityId>=0)
        {
            gerenciadorCenas.LoadNextLevel();
        }
        else
        {
            Debug.Log("One of the fields is invalid(input fields or dropdown)");
        }
    }
}
