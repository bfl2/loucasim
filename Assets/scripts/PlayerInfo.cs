using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerInfo {

    public int ownerId;
    public int id;
    public string name;
    public int position;
    public PlayerStats stats;
    public string modifiers;

    // Use this for initialization
    public void SetValues(string namec, int positionc, PlayerStats statc, string modifierc)
    {
        name = namec;
        position = positionc;
        stats = statc;
        modifiers = modifierc;
    }

}
