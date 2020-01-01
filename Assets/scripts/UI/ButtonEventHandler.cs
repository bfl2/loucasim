using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventHandler : MonoBehaviour
{
    public TeamManager TM;
    public GameManager GM;
    public UpdatePlayerInfo updatePlayerInfo;
    public GameObject content;
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

    private void RemovePlayerFromUIList(PlayerInfo playerInfo)
    {
        foreach(Transform child in content.transform)
        {
            ListItemHandler childItemHandler = child.gameObject.GetComponent<ListItemHandler>();
            if(childItemHandler.playerinfo.id == playerInfo.id)
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void BuyFocusedPlayer()
    {
        PlayerInfo playerInfo = this.updatePlayerInfo.currentFocusedPlayer;
        if(TM.teamInfo.money >= playerInfo.stats.value)
        {
            this.updatePlayerInfo.currentFocusedPlayer = null;
            this.updatePlayerInfo.DisplayBoughtHUD();
            RemovePlayerFromUIList(playerInfo);
            this.TM.teamInfo.hiredPlayers.Add(playerInfo);
            this.TM.teamInfo.money -= playerInfo.stats.value;
            this.GM.playersInPlay.Remove(playerInfo); // dont remove player from list for now
        }
        else
        {
            Debug.Log("Failed to buy player, insufficient money");
        }
    }
}
