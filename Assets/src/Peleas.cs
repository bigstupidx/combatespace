using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class Peleas
{
    public int peleas_g;
    public int peleas_p;
    public int retos_g;
    public int retos_p;

    public void SetPeleas(int peleas_g, int peleas_p, int retos_g, int retos_p)
    {
        this.peleas_g = peleas_g;
        this.peleas_p = peleas_p;
        this.retos_g = retos_g;
        this.retos_p = retos_p;
    }
}
