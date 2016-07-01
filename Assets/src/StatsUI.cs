using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour {

    public Slider fuerza;
    public Slider resistencia;
    public Slider defensa;
    public Slider velocidad;

	void Start () {
	    
	}
    public void Init(Stats stats)
    {
        print("stats" + stats.Power);
        fuerza.value = (float)stats.Power / 100;
        resistencia.value = (float)stats.Resistence / 100;
        defensa.value = (float)stats.Defense / 100;
        velocidad.value = (float)stats.Speed / 100;
    }
}
