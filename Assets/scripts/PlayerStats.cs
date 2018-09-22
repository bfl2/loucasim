using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats {

    public int initialMmr;
    public int mmr;
    public float growthPotential;
    public float zoabilidade;
    public float value; //passe
    public float upkeep; //salario

    public void SetValues(int init, int mmr, float growth, float zoab, float value, float upkeep)
    {
        initialMmr = init;
        this.mmr = mmr;
        growthPotential = growth;
        zoabilidade = zoab;
        this.value = value;
        this.upkeep = upkeep;

    }
}
