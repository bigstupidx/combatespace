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
    public void Init(float _fuerza, float _resistencia, float _defensa, float _velocidad)
    {
        fuerza.value = _fuerza/100;
        resistencia.value = _resistencia / 100;
        defensa.value = _defensa / 100;
        velocidad.value = _velocidad / 100;
    }
}
