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
    void Start () {

    }

    // Update is called once per frame
    void Update() {
        getRankImage(currentFocusedPlayer.stats.mmr);
        this.transform.GetChild(1).GetComponent<Image>().sprite = medal;
        this.transform.GetChild(3).GetComponent<Text>().text = currentFocusedPlayer.stats.zoabilidade.ToString();
        this.transform.GetChild(4).GetComponent<Text>().text = currentFocusedPlayer.stats.growthPotential.ToString();
        this.transform.GetChild(5).GetComponent<Text>().text = currentFocusedPlayer.stats.value.ToString();
        this.transform.GetChild(6).GetComponent<Text>().text = currentFocusedPlayer.stats.upkeep.ToString();
        this.transform.GetChild(7).GetComponent<Text>().text = currentFocusedPlayer.name;
        this.transform.GetChild(8).GetComponent<Text>().text = IntToRole(currentFocusedPlayer.position);
        Debug.Log("mmr:" + currentFocusedPlayer.stats.mmr);
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
    public Sprite getRankImage(int mmr)
    {
        int[] mmrRanges = {0,170,340,510,680,850, 1020,1190, 1360, 1530, 1700, 1870, 2040, 2210, 2380,
                            2550,2720, 2890, 3060, 3230, 3400, 3570, 3740,3910, 4080, 4250, 4420, 4590, 4760, 4930, 5100, 5270, 5440, 5610, 5780, 6000, 7000, 8000};
        int index;
        for (index = 0; index < mmrRanges.Length; index++)
        {
            if (mmr<170)
            {
                break;
            }if(mmr > 7000)
            {
                index = mmrRanges.Length;
                break;
            }
            if ((mmr < mmrRanges[index + 1])&&(mmr>mmrRanges[index]))
            {
                break;
            }
        }
        index++;
        string path = Application.dataPath;
        string subDir = "Images/formated/rank (" + index+")"; 
        medal = Resources.Load<Sprite>(subDir);//located at resource folder

        return medal;
    }
}
