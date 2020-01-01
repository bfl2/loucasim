﻿using System.Collections;
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
        this.players.Add(player);
    }

    public void Remove(PlayerInfo player)
    {
        foreach(PlayerInfo pl in this.players)
        {
            if(player.id == pl.id)
            {
                this.players.Remove(pl);
            }
        }
    }
}
