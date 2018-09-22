using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class TeamInfo  {

    [SerializeField]
    public int money;
    public PlayerList hiredPlayers;
    public string teamName;
    public string coachName;
    public int abilityId;
}
