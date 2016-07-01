using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PeleasUI : MonoBehaviour {

    public Text peleas_g;
    public Text peleas_p;
    public Text retos_g;
    public Text retos_p;

    public void Init(Peleas peleas)
    {
        this.peleas_g.text = "Peleas ganadas: " + peleas.peleas_g;
        this.peleas_p.text = "Peleas perdidas: " + peleas.peleas_p;
        this.retos_g.text = "Retos ganados: " + peleas.retos_g;
        this.retos_p.text = "Retos perdidos: " + peleas.retos_p;
    }
}
