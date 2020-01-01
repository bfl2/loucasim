using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListController : MonoBehaviour
{
    private GameManager GM;
    private TeamManager TM;
    public string selector = "inplay";
    public GameObject content;
    public GameObject prototype;
    public ListItemHandler listItemHandler;
    private bool isUpdated = false;
    void Start()
    {
        GM = FindObjectOfType<GameManager>();
        TM = FindObjectOfType<TeamManager>();

        if (GM == null|| TM == null || content == null)
        {
            Debug.LogError("Game Manager or Team Manager or content is null");
        }
    }
    void Update()
    {
        if (!isUpdated)
        {
            PlayerList playerList = this.GetSourceList();
            foreach (PlayerInfo player in playerList.players.ToArray())
            {
                InsertNewItem(player);
            }
            isUpdated = true;
        }

    }

    void InsertNewItem(PlayerInfo playerinfo)
    {
        GameObject newContent = Instantiate(prototype.gameObject) as GameObject;
        listItemHandler = newContent.GetComponent<ListItemHandler>();
        newContent.transform.SetParent(content.transform, false);

        listItemHandler.SetPlayerInfo(playerinfo);
        listItemHandler.SetButtonAttr();

    }

    private PlayerList GetSourceList()
    {
        if(selector == "inplay")
        {
            return GM.playersInPlay;
        }
        else if(selector == "hired")
        {
            return TM.teamInfo.hiredPlayers;
        }
        else
        {
            return new PlayerList();
        }
    }
}
