using UnityEngine;
using System.Collections;

public class InGameSfx : MonoBehaviour {

	public AudioClip publico;
	public AudioClip bell;
	public float minCrowdVol = 0.4f;
	public float maxCrowdVol = 0.7f;
	public float crowdPitch = 0.9f;

	private AudioSource source;

	private Hero hero;
	private Character character;
	private FightStatus fStatus;
	private float vol;

    
	void Start () {
		source = GetComponent<AudioSource> ();

		Events.OnComputeCharacterPunched += OnComputeCharacterPunched;
		Events.OnComputeHeroPunched += OnComputeHeroPunched;
		Events.OnAvatarFall += OnAvatarFall;
        Events.OnAllRoundsComplete += OnAllRoundsComplete;
		Events.OnKO += OnKO;
		Events.OnRoundComplete += OnRoundComplete;
		Events.OnRoundStart += OnRoundStart;
	
		hero = Game.Instance.GetComponent<ComputeFight> ().hero;
		character = Game.Instance.GetComponent<ComputeFight> ().character;

		fStatus = Game.Instance.GetComponent<FightStatus> ();
		source.Play ();

        Events.OnAudioEnable += OnAudioEnable;
        OnAudioEnable(Data.Instance.settings.volume);
	}



	void OnDestroy(){
        Events.OnAudioEnable -= OnAudioEnable;
		Events.OnComputeCharacterPunched -= OnComputeCharacterPunched;
		Events.OnComputeHeroPunched -= OnComputeHeroPunched;
		Events.OnAvatarFall -= OnAvatarFall;
        Events.OnAllRoundsComplete -= OnAllRoundsComplete;
		Events.OnKO -= OnKO;
		Events.OnRoundComplete -= OnRoundComplete;
		Events.OnRoundStart -= OnRoundStart;
	}

    void OnAudioEnable(float volume)
    {
		vol = volume;
		source.volume = vol;
    }

	void OnComputeCharacterPunched(HeroActions.actions action){		
		if (hero.combo > 1) {
			source.pitch = crowdPitch;
			source.volume = vol * Random.Range (minCrowdVol, maxCrowdVol);
			source.PlayOneShot(publico);
		}
	}

	void OnComputeHeroPunched(CharacterActions.actions action){
		if (character.combo > 1) {
			source.pitch = crowdPitch;
			source.volume = vol * Random.Range (minCrowdVol, maxCrowdVol);
			source.PlayOneShot(publico);
		}

	}
	void OnAvatarFall(bool isHero){
		source.pitch = crowdPitch;
		source.volume = vol * Random.Range (minCrowdVol, maxCrowdVol);
		source.PlayOneShot(publico);
	}

	void OnKO(bool isHero){
		source.pitch = crowdPitch;
		source.volume = vol * Random.Range (minCrowdVol, maxCrowdVol);
		source.PlayOneShot(publico);
	}

	void OnRoundStart()
	{
		source.pitch = 1f;
		source.volume = vol;
		source.PlayOneShot(bell);
		//source.Play ();
	}
    void OnAllRoundsComplete()
    {
        OnRoundComplete();
    }
	void OnRoundComplete()
	{
		//source.Stop ();
		source.pitch = 1f;
		source.volume = vol;
		source.PlayOneShot(bell);
	}
}
