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
        if (GM!=null){
            GM.SaveInPlayGameData();
        }else{
            Debug.Log("couldnt find game manager");
        }


    }
}
