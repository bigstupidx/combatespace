using UnityEngine;
using System.Collections;

public class CutScenesInGame : MonoBehaviour {

    public Animation heroAnimations;
    public Animation characterAnimations;
    public Hero hero;
	public VoiceClips vclips;
	public AudioSource voice;

    void OnEnable()
    {
        hero.gameObject.SetActive(false);
    }
    public void BothWalk()
    {
        heroAnimations.CrossFade("roundStart", 0.3f);
        characterAnimations.CrossFade("roundStart", 0.3f);
    }
    public void BothStart()
    {
        heroAnimations.CrossFade("start1", 0.2f);
        characterAnimations.CrossFade("start1", 0.2f);
    }
    public void BothIdle()
    {
        heroAnimations.CrossFade("idle", 0.2f);
        characterAnimations.CrossFade("idle", 0.2f);
    }



    public void YouWinByKO()
    {
        heroAnimations.Play("festeja");
        characterAnimations.Play("levanta2");
        Invoke("TimeOutYouWinKO", 5);
    }
    void TimeOutYouWinKO()
    {
        characterAnimations.CrossFade("derrota", 0.2f);
    }




    public void YouLoseByKO()
    {
        heroAnimations.Play("levanta2");
        characterAnimations.Play("festeja");
        Invoke("TimeOutYouLoseKO", 6);
    }
    void TimeOutYouLoseKO()
    {
        heroAnimations.CrossFade("derrota", 0.2f);
    }




    public void HeroFesteja()
    {
        heroAnimations.Play("festeja");
    }
    public void CharacterFesteja()
    {
        characterAnimations.Play("festeja");
    }
    public void HeroDerrota()
    {
        heroAnimations.Play("derrota");
    }
    public void CharacterDerrota()
    {
        characterAnimations.Play("derrota");
    }

	public void VoiceBienvenida(){
		voice.PlayOneShot (vclips.bienvenida.GetNext ("Bienvenida"));
	}

	void VoiceIntro(){		
		voice.PlayOneShot (vclips.intro.GetNext ("Intro"));
	}
}
