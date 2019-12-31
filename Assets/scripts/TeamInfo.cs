using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class TeamInfo  {

    [SerializeField]
    public int money;
    [SerializeField]
    public PlayerList hiredPlayers;
    [SerializeField]
    public string teamName;
    [SerializeField]
    public string coachName;
    [SerializeField]
    public int abilityId;
}
