using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItemHandler : MonoBehaviour
{
    private Button button;
    private Text text;
    public PlayerInfo playerinfo;
    private UpdatePlayerInfo updatePlayerInfo;
    bool isUpdated = false;
    void Start()
    {
        GetRefs();
        if (text == null || button == null)
        {
            Debug.Log("text or button is null");
        }

    }
    void Update()
    {
        if (!isUpdated && playerinfo != null)
        {
            SetButtonAttr();
        }
    }

    void GetRefs()
    {
        button = this.transform.GetChild(0).GetComponent<Button>();
        text = this.transform.GetChild(0).GetComponentInChildren<Text>();
        GameObject updatePlayerInfoGO = (GameObject.FindGameObjectWithTag("InfoHud"));

        if (updatePlayerInfoGO != null)
        {
            updatePlayerInfo = updatePlayerInfoGO.GetComponent<UpdatePlayerInfo>();
        }
    }

    public void SetPlayerInfo(PlayerInfo pInfo)
    {
        this.playerinfo = pInfo;
    }

    public void SetButtonAttr()
    {
        if (text == null || button == null)
        {
            GetRefs();
        }

        switch (playerinfo.position)
        {
            case 1:
                this.button.image.color = Color.blue;
                break;
            case 2:
                this.button.image.color = Color.red;
                break;
            case 3:
                this.button.image.color = Color.yellow;
                break;
            case 4:
                this.button.image.color = Color.green;
                break;
        }
        this.text.text = playerinfo.name;
        isUpdated = true;
    }

    public void SetFocusedPlayer()
    {
        if(updatePlayerInfo != null)
        {
            updatePlayerInfo.currentFocusedPlayer = playerinfo;
        }
        else
        {
            Debug.Log("updatePlayerInfo is null");
        }

    }

}
