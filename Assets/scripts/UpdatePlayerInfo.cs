using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UpdatePlayerInfo : MonoBehaviour {

    public PlayerInfo currentFocusedPlayer;
    public List<GameObject> childObjects;
    public Sprite medal;

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentFocusedPlayer != null)
        {
            GetRankImage(currentFocusedPlayer.stats.mmr);
            this.transform.GetChild(1).GetComponent<Image>().sprite = medal;
            this.transform.GetChild(3).GetComponent<Text>().text = currentFocusedPlayer.stats.zoabilidade.ToString();
            this.transform.GetChild(4).GetComponent<Text>().text = currentFocusedPlayer.stats.growthPotential.ToString();
            this.transform.GetChild(5).GetComponent<Text>().text = currentFocusedPlayer.stats.value.ToString();
            this.transform.GetChild(6).GetComponent<Text>().text = currentFocusedPlayer.stats.upkeep.ToString();
            this.transform.GetChild(7).GetComponent<Text>().text = currentFocusedPlayer.name;
            this.transform.GetChild(8).GetComponent<Text>().text = IntToRole(currentFocusedPlayer.position);
            //Debug.Log("mmr:" + currentFocusedPlayer.stats.mmr);
        }
    }

    private string IntToRole(int pos)
    {
        string role = "";
        switch (pos)
        {
            case 1:
                role = "Carry";
                break;
            case 2:
                role = "Mid";
                break;
            case 3:
                role = "Offlaner";
                break;
            case 4:
                role = "Support";
                break;
            default:

                break;
        }
        return role;
    }
    public Sprite GetRankImage(int mmr)
    {
        int[] mmrRanges = {0, 80, 150, 200, 330, 480, 520, 690, 760, 930, 1100, 1470, 1540, 1610, 1780,
                            1850, 1920, 2190, 2360, 2530, 2700, 2970, 3140, 3310, 3580, 3750, 3920, 4190, 4360, 4530, 4700, 4970, 5140, 5310, 5580, 6000, 7000, 8000};
        int index;
        for (index = 0; index < mmrRanges.Length; index++)
        {
            if(index == mmrRanges.Length)
            {
                break;
            }
            if (mmr < 80)
            {
                break;
            }
            if(mmr > 7000)
            {
                index = mmrRanges.Length -1;
                break;
            }
            if ((mmr < mmrRanges[index + 1])&&(mmr>mmrRanges[index]))
            {
                break;
            }
        }

        index++;
        string subDir = "Images/formated/rank (" + index + ")";
        medal = Resources.Load<Sprite>(subDir);//located at resource folder

        return medal;
    }


    public void DisplayBoughtHUD()
    {
        GetRankImage(0);
        this.transform.GetChild(1).GetComponent<Image>().sprite = medal;
        this.transform.GetChild(3).GetComponent<Text>().text = "";
        this.transform.GetChild(4).GetComponent<Text>().text = "";
        this.transform.GetChild(5).GetComponent<Text>().text = "";
        this.transform.GetChild(6).GetComponent<Text>().text = "";
        this.transform.GetChild(7).GetComponent<Text>().text = "Player purchased!";
        this.transform.GetChild(8).GetComponent<Text>().text = "";
    }
}
