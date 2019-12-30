using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats {

    public int initialMmr;
    public int mmr;
    public int growthPotential;
    public int zoabilidade;
    public int value; //passe
    public int upkeep; //salario

    public void SetValues(int init, int mmr, int growth, int zoab, int value, int upkeep)
    {
        this.initialMmr = init;
        this.mmr = mmr;
        this.growthPotential = growth;
        this.zoabilidade = zoab;
        this.value = value;
        this.upkeep = upkeep;

    }
}
