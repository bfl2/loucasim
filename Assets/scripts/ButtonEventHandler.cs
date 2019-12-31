using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventHandler : MonoBehaviour
{
    public TeamManager TM;
    public GameManager GM;
    public UpdatePlayerInfo updatePlayerInfo;
    void Start()
    {
        this.TM = FindObjectOfType<TeamManager>();
        this.GM = FindObjectOfType<GameManager>();
        this.updatePlayerInfo = FindObjectOfType<UpdatePlayerInfo>();

        if (this.GM == null || this.TM == null || this.updatePlayerInfo == null)
        {
            Debug.LogError("Failed to get TM/GM/UpdatePlayerInfo object");
        }
    }

    void Update()
    {

    }

    public void BuyFocusedPlayer()
    {
        PlayerInfo playerInfo = this.updatePlayerInfo.currentFocusedPlayer;
        if(TM.teamInfo.money >= playerInfo.stats.value)
        {
            //this.GM.playersInPlay.Remove(playerInfo); // dont remove player from list for now
            this.TM.teamInfo.hiredPlayers.Add(playerInfo);
            this.TM.teamInfo.money -= playerInfo.stats.value;
        }
        else
        {
            Debug.Log("Failed to buy player, insufficient money");
        }
    }
}
