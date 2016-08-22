using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tutorial : MonoBehaviour {

    public GameObject cartel;
    public Text cartel_field;

    public GameObject barraEnergia;
    public GameObject barraPotencia;
    public GameObject button;

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
                print("cortito " + id);
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
        clicked = false;
        barraEnergia.SetActive(false);
        barraPotencia.SetActive(false);

        id++;
        switch (id)
        {
            case 0: SetCartel("Aprendé los básicos", true); break;
            case 1: cartel.SetActive(true); break;
            case 2: SetCartel("Bien!, ahora el UPPERCUT al cuerpo", false); break;
            case 3: SetCartel("Perfecto! ahora el GANCHO", false); break;
            case 4: SetCartel("Bien! Ahora la defensa", false); break;
            case 5: barraEnergia.SetActive(true); SetCartel("Ahora... esta es la barra de tu energía", true); break;
            case 6: barraPotencia.SetActive(true); SetCartel("y esta es la potencia: si pegás mucho, esperá que se llene, sino el daño será muy bajo", true); break;
            case 7: SetCartel("Prefecto... ¡Ahora un poco de práctica!", true); break;
        }
    }
    bool clicked;
    public void CartelClicked()
    {
        print("CartelClicked" + id);
        clicked = true;
        if (id ==7)
        {
            Events.OnTutorialReady(1);
            Data.Instance.LoadLevel("Game");
        }
        else if(id >4)
        {
            SetNextTutorial();
        }
        else
        {
            if (id == 0)
                id++;

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
            case 1: anim.Play("cortito", 0,0); SetSignal(true, "Golpe Directo", "(Tap en la pantalla)"); 
                break;
            case 2: anim.Play("gancho_abajo", 0, 0); SetSignal(true, "Uppercut", "(Swipe Ascendente)"); break;
            case 3: anim.Play("gancho_arriba", 0, 0); SetSignal(true, "Gancho", "(Swipe descendente)"); break;
            case 4: anim.Play("defense", 0, 0); SetSignal(true, "Defensa", "(Mantener: Dos dedos)"); break;
        }        
    }
    void SetCartel(string _text, bool ShowButton)
    {
        cartelON = true;
        defensePanel.SetActive(false);
        cartel_field.text = _text;
        Invoke("Dlay", 1);
        if (!ShowButton)
        {
            button.SetActive(false);
            cartelON = false;
            Invoke("DelayNext", 3);
        }
        else
            button.SetActive(true);
    }
    void DelayNext()
    {
        if (clicked) return;
        CartelClicked();
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
    public void Exit()
    {
        Data.Instance.LoadLevel("03_Home");
    }
}
