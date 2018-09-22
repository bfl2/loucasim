using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class PlayerList {

    public List<PlayerInfo> players;

    public PlayerList(List<PlayerInfo> players)
    {
        this.players = players;
    }
    public PlayerList()
    {
    }

    public void Add(PlayerInfo player)
    {
        player.id = players.Count;
        players.Add(player);
    }
}
