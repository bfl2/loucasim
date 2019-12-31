using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

    public void SaveGameData()
    {
        GameManager GM = FindObjectOfType<GameManager>();
        TeamManager TM = FindObjectOfType<TeamManager>();
        if ( GM != null && TM != null)
        {
            GM.SaveInPlayGameData();
            TM.SaveInPlayGameData();
        }
        else
        {
            Debug.Log("couldnt find game manager or team manager");
        }


    }
}
