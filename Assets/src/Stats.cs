using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Stats {
    public int Inteligencia;
    public int Power;
    public int Resistence;
    public int Defense;
    public int Speed;

    public void SetStats(int inteligencia, int power, int resistence, int defense, int speed)
    {
        this.Inteligencia = inteligencia;
        this.Power = power;
        this.Resistence = resistence;
        this.Defense = defense;
        this.Speed = speed;
    }
}
