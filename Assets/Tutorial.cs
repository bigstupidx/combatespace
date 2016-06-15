using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour {

    public GameObject cartel;
    public Text cartel_field;

    public GameObject barraEnergia;
    public GameObject barraPotencia;

    public GameObject defensePanel;

    public Animator anim;
    public GameObject signal;
    public Text title;
    public Text field;

    public int id;
    private bool cartelON;

	void Start () {
        id = -1;
        Events.OnHeroAction += OnHeroAction;
        SetNextTutorial();
        barraEnergia.SetActive(false);
        barraPotencia.SetActive(false);
	}
    void OnDestroy()
    {
        Events.OnHeroAction -= OnHeroAction;
    }
    void OnHeroAction(HeroActions.actions action)
    {
        if (cartelON) return;
        switch (action)
        {
            case HeroActions.actions.CORTITO_L:
            case HeroActions.actions.CORTITO_R:
                if (id == 1) SetNextTutorial(); break;
            case HeroActions.actions.GANCHO_DOWN_L:
            case HeroActions.actions.GANCHO_DOWN_R:
                if (id == 2) SetNextTutorial(); break;
            case HeroActions.actions.GANCHO_UP_L:
            case HeroActions.actions.GANCHO_UP_R:
                if (id == 3) SetNextTutorial(); break;
            case HeroActions.actions.DEFENSE:
                if (id == 4) SetNextTutorial(); break;
        }
    }
    public void SetNextTutorial()
    {
        barraEnergia.SetActive(false);
        barraPotencia.SetActive(false);

        id++;
        switch (id)
        {
            case 0: SetCartel("Aprendé los básicos"); break;
            case 1: SetCartel("Primero, el JAB"); break;
            case 2: SetCartel("Bien!, ahora el UPPERCUT al cuerpo"); break;
            case 3: SetCartel("Perfecto! ahora el GANCHO"); break;
            case 4: SetCartel("Bien! Ahora la defensa"); break;
            case 5: barraEnergia.SetActive(true); SetCartel("Ahora... esta es la barra de tu energía"); break;
            case 6: barraPotencia.SetActive(true); SetCartel("y esta es la potencia: si pegas mucho, esperá que se llene, sino el daño será muy bajo"); break;
            case 7: SetCartel("Prefecto!, estás listo para boxear"); break;
        }
    }
    public void CartelClicked()
    {
        if (id ==7)
        {
            Events.OnTutorialReady(1);
            Data.Instance.LoadLevel("Game");
        }
        else if (id==0 || id >4)
        {
            SetNextTutorial();
        }
        else
        {
            cartelON = false;
            defensePanel.SetActive(true);
            NextSignal();
            cartel.SetActive(false);            
        }
    }
    void NextSignal()
    {
        switch(id)
        {
            case 1: anim.Play("cortito", 0,0); SetSignal(true, "Cortito", "(Swipe hacia el centro)"); break;
            case 2: anim.Play("gancho_abajo", 0, 0); SetSignal(true, "Uppercut", "(Swipe Ascendente)"); break;
            case 3: anim.Play("gancho_arriba", 0, 0); SetSignal(true, "Gancho", "(Swipe descendente)"); break;
            case 4: anim.Play("defense", 0, 0); SetSignal(true, "Defensa", "(Mantener: Dos dedos)"); break;
        }        
    }
    void SetCartel(string _text)
    {
        cartelON = true;
        defensePanel.SetActive(false);
        cartel_field.text = _text;
        Invoke("Dlay", 1);
    }
    void Dlay()
    {
        cartel.SetActive(true);        
    }
    void Defense()
    {
        anim.Play("defense");
    }
    void SetSignal(bool isActive, string _title, string _text)
    {
        signal.SetActive(isActive);
        title.text = _title;
        field.text = _text;
    }
}
