using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public GameManager gameManager;
    public TeamManager teamManager;
    public Text Clock;

    public GameObject DireTowers;
    public GameObject RadiantTowers;

    public int[,] towersAlive = new int[,]
   {
            {3, 3, 3, 3},
            {3, 3, 3, 3},
   };

    public float tickRate = 1.0f;
    public float gameTime = 0.0f;
    public float secondsToTick = 1.0f;
    private float lastTick = 0.0f;



    enum Lane
    {
        TOP=0,
        MID,
        BOT,
        CORE
    }

    enum Team
    {
        RADIANT = 0,
        DIRE
    }


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        teamManager = FindObjectOfType<TeamManager>();

        if(false&&(gameManager == null || teamManager == null))
        {
            Debug.Log("Failed to retrieve GM or TM");
            throw new System.Exception();
        }
    }

    void Update()
    {
        if((lastTick + secondsToTick) < Time.time)
        {
            this.gameTime += this.tickRate;
            Debug.Log(Time.time);
            lastTick = Time.time;
            this.UpdateUI();
        }
    }

    void UpdateUI()
    {
        this.UpdateClock();
        this.DeleteTower(Team.RADIANT, 0);
        this.DeleteTower(Team.DIRE, Lane.BOT);

    }

    void DeleteTower(Team team, Lane lane)
    {
        GameObject laneParent;
        GameObject towerToDelete;
        if (team == Team.RADIANT)
        {
            laneParent = this.RadiantTowers.transform.GetChild((int)lane).gameObject;
        }
        else
        {
            laneParent = this.DireTowers.transform.GetChild((int)lane).gameObject;
        }
        if(laneParent.transform.childCount > 0)
        {
            towerToDelete = laneParent.transform.GetChild(0).gameObject;
            Destroy(towerToDelete);
        }
        else
        {
            Debug.Log(string.Format("Tried to delete a tower from an empty lane({0})", lane));
        }
        //Update towers matrix
        towersAlive[(int)team, (int)lane] = laneParent.transform.childCount;
    }

    void UpdateClock()
    {
        string s = this.gameTime.ToString("0.00", CultureInfo.InvariantCulture);
        string[] parts = s.Split('.');
        int minutes = int.Parse(parts[0]);
        int seconds = int.Parse(parts[1]);
        this.Clock.text = string.Format("{0:00}:{1:00}", minutes, seconds * (0.6));
    }
}
