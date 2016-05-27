using UnityEngine;
using System.Collections;

public class FightStatus : MonoBehaviour {    

    public float heroStatus = 1;
    public float characterStatus = 1;

    public ProgressBar HeroProgressBar;
    public ProgressBar EnemyProgressBar;

	void Start () {
        Events.OnChangeStatusHero += OnChangeStatusHero;
        Events.OnChangeStatusCharacter += OnChangeStatusCharacter;
	}
    void OnDestroy()
    {
        Events.OnChangeStatusHero -= OnChangeStatusHero;
        Events.OnChangeStatusCharacter -= OnChangeStatusCharacter;
    }

    void OnChangeStatusHero(float damage)
    {
        heroStatus -= damage / 100;
        HeroProgressBar.SetProgress(heroStatus);
        if (heroStatus <= 0)
            Events.OnKO(false);
	}
    void OnChangeStatusCharacter(float damage)
    {
        characterStatus -= damage / 100;
        EnemyProgressBar.SetProgress(characterStatus);
        if (characterStatus <= 0)
            Events.OnKO(true);
    }
}
