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
            case 5:
                SetCartel("¡Bien!. Habrás notado que para girar debes rotar el dispositivo...", true);
                break;
            case 6: barraEnergia.SetActive(true); SetCartel("Ahora... esta es la barra de tu energía", true); break;
            case 7: barraPotencia.SetActive(true); SetCartel("y esta es la potencia: si pegás mucho, esperá que se llene, sino el daño será muy bajo", true); break;          
            case 8: SetCartel("Perfecto... ¡Ahora un poco de práctica!", true); break;
        }
    }
    bool clicked;
    public void CartelClicked()
    {
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click2);
        print("CartelClicked" + id);
        clicked = true;
        if (id == 8)
        {
            Events.OnTutorialReady(1);
            Events.OnLoadingFade(true);
            Invoke("DelayToGoToGame", 1);
        }
        else if(id >4)
        {
            SetNextTutorial();
        }
        else
        {
            if (id == 0)
            {
                Invoke("DelayToNExt", 2);
            }

            cartelON = false;
            defensePanel.SetActive(true);
            NextSignal();
            cartel.SetActive(false);            
        }
    }
    void DelayToGoToGame()
    {
        Data.Instance.LoadLevel("Game");
    }
    void DelayToNExt()
    {
        id = 1;
    }
    void NextSignal()
    {
        switch(id)
        {
            case 0: anim.Play("cortito", 0,0); SetSignal(true, "Golpe Directo", "(Tap en la pantalla)"); 
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
		Data.Instance.interfaceSfx.PlaySfx (Data.Instance.interfaceSfx.click1);
        Data.Instance.LoadLevel("03_Home");
    }
}
