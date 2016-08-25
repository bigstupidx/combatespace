using UnityEngine;
using System.Collections;

public class VoiceManager : MonoBehaviour {

	private AudioSource voice;
	private VoiceClips vclips;

	private Hero hero;
	private Character character;
	private FightStatus fStatus;

	// Use this for initialization
	void Start () {

		voice = GetComponent<AudioSource> ();
		vclips = GetComponent<VoiceClips> ();

		Events.OnComputeCharacterPunched += OnComputeCharacterPunched;
		Events.OnComputeHeroPunched += OnComputeHeroPunched;
		Events.OnHeroBlockPunch += OnHeroBlockPunch;
		Events.OnCharacterBlockPunch += OnCharacterBlockPunch;
		Events.OnAvatarFall += OnAvatarFall;
		Events.OnAvatarStandUp += OnAvatarStandUp;
		Events.OnKO += OnKO;
        Events.OnAudioEnable += OnAudioEnable;
		Events.OnRoundStart += OnRoundStart;
		Events.OnRoundComplete += OnRoundComplete;
		Events.OnChangeStatusHero += OnChangeStatusHero;
		Events.OnChangeStatusCharacter += OnChangeStatusCharacter;
	
		hero = Game.Instance.GetComponent<ComputeFight> ().hero;
		character = Game.Instance.GetComponent<ComputeFight> ().character;

		fStatus = Game.Instance.GetComponent<FightStatus> ();
        OnAudioEnable(Data.Instance.settings.volume);

		//Invoke ("Begins", 0.25f);
	}

	void OnDestroy(){
		Events.OnComputeCharacterPunched -= OnComputeCharacterPunched;
		Events.OnComputeHeroPunched -= OnComputeHeroPunched;
		Events.OnHeroBlockPunch -= OnHeroBlockPunch;
		Events.OnCharacterBlockPunch -= OnCharacterBlockPunch;
		Events.OnAvatarFall -= OnAvatarFall;
		Events.OnAvatarStandUp -= OnAvatarStandUp;
		Events.OnKO -= OnKO;
        Events.OnAudioEnable -= OnAudioEnable;
		Events.OnRoundStart -= OnRoundStart;
		Events.OnRoundComplete -= OnRoundComplete;
		Events.OnChangeStatusHero -= OnChangeStatusHero;
		Events.OnChangeStatusCharacter -= OnChangeStatusCharacter;
	}
    void OnAudioEnable(float vol)
    {
        voice.volume = vol;
    }

	void Begins(){		
		voice.PlayOneShot (vclips.bienvenida.GetNext ("Bienvenida"));
		Invoke ("Intro", 5f);
	}

	void Intro(){		
		voice.PlayOneShot (vclips.intro.GetNext ("Intro"));
		//Invoke ("Comienzo", 7f);
	}

	void OnRoundStart(){		
		Invoke ("Comienzo", 2f);
	}

	void Comienzo(){
		Debug.Log ("Round: " + Game.Instance.fightStatus.Round);
		if (Game.Instance.fightStatus.Round == 1)
			voice.PlayOneShot (vclips.comienzoPelea.GetNext ("Comienzo_Pelea"));
		else {
			voice.PlayOneShot (vclips.comienzoRound.GetNext ("Comienzo_Round"));
			Invoke ("Idle", 3f);
		}
	}

	void OnRoundComplete(){
		Debug.Log ("ACA");
	}

	void OnComputeCharacterPunched(HeroActions.actions action){		
		if (hero.combo > 1 && !voice.isPlaying ) {
			float r = Random.value;
			AudioClip clip = null;
			if (r > 0.5f) {
				if (action.Equals (HeroActions.actions.GANCHO_UP_L))
					clip = vclips.combos.GetNext ("Combos_Izq");
				else if (action.Equals (HeroActions.actions.GANCHO_DOWN_L))
					clip = vclips.combos.GetNext ("Combos_Izq_Abajo");
				else if (action.Equals (HeroActions.actions.GANCHO_DOWN_R) || action.Equals (HeroActions.actions.GANCHO_UP_R))
					clip = vclips.combos.GetNext ("Combos_Der");
				else if (action.Equals (HeroActions.actions.CORTITO_L) || action.Equals (HeroActions.actions.CORTITO_R))
					clip = vclips.combos.GetNext ("Combos_Upper");
				else if (action.Equals (HeroActions.actions.GANCHO_DOWN_L) || action.Equals (HeroActions.actions.GANCHO_DOWN_R))
					clip = vclips.combos.GetNext ("Combos_Jab");
				else if (action.Equals (HeroActions.actions.GANCHO_UP_L) || action.Equals (HeroActions.actions.GANCHO_UP_R))
					clip = vclips.combos.GetNext ("Combos_Gancho");
				else
					clip = vclips.combos.GetNext ("Combos");
			} else {
				clip = vclips.combos.GetNext ("Combos");
			}
			voice.clip = clip;
			voice.Play();
			Invoke ("Idle_Agite", 3f);
		}
	}

	void OnComputeHeroPunched(CharacterActions.actions action){
		if (character.combo > 1 && !voice.isPlaying ) {
			float r = Random.value;
			AudioClip clip = null;
			if (r > 0.5f) {
				if (action.Equals (CharacterActions.actions.ATTACK_L))
					clip = vclips.combos.GetNext ("Combos_Izq");
				else if (action.Equals (CharacterActions.actions.ATTACK_R) || action.Equals (CharacterActions.actions.ATTACK_R_CORTITO))
					clip = vclips.combos.GetNext ("Combos_Der");
				else if (action.Equals (CharacterActions.actions.ATTACK_L_CORTITO) || action.Equals (CharacterActions.actions.ATTACK_R_CORTITO))
					clip = vclips.combos.GetNext ("Combos_Upper");
				else if (action.Equals (CharacterActions.actions.ATTACK_L) || action.Equals (CharacterActions.actions.ATTACK_R))
					clip = vclips.combos.GetNext ("Combos_Gancho");
				else
					clip = vclips.combos.GetNext ("Combos");
			} else {
				clip = vclips.combos.GetNext ("Combos");
			}
			voice.clip = clip;
			voice.Play();
			Invoke ("Idle_Agite", 3f);
		}

	}

	void OnHeroBlockPunch(CharacterActions.actions action){			
		float r = Random.value;
		if (r > 0.95f && !voice.isPlaying) {
			voice.clip = vclips.bloqueo.GetNext ("Bloqueo");
			voice.Play();
			Invoke ("Idle", 3f);
		}
	}

	void OnCharacterBlockPunch(HeroActions.actions action){
		float r = Random.value;
		if (r > 0.95f && !voice.isPlaying) {
			voice.clip = vclips.bloqueo.GetNext ("Bloqueo");
			voice.Play();
			Invoke ("Idle", 3f);
		}
	}

	void OnChangeStatusHero(float damage){		
		if (Game.Instance.fightStatus.HeroProgressBar.image.color.Equals (Game.Instance.fightStatus.HeroProgressBar.color_3)) {
			if (!voice.isPlaying) {
				voice.PlayOneShot (vclips.tambalea.GetNext ("Tambalea"));
				Invoke ("Idle_Later", 3f);
			}
		}
	}

	void OnChangeStatusCharacter(float damage){
		if (Game.Instance.fightStatus.HeroProgressBar.image.color.Equals (Game.Instance.fightStatus.HeroProgressBar.color_3)) {
			if (!voice.isPlaying) {
				voice.PlayOneShot (vclips.tambalea.GetNext ("Tambalea"));
				Invoke ("Idle_Later", 3f);
			}
		}
	}
	void OnAvatarFall(bool isHero){

		AudioClip clip = null;
		float r = Random.value;
		if (r > 0.9f) {
			if (isHero) {
				if (fStatus.caidas_hero == 2)
					clip = vclips.knock.GetNext ("Knock_segunda");
				else
					clip = vclips.knock.GetNext ("Knock");
			} else {
				if (fStatus.caidas_character == 2)
					clip = vclips.knock.GetNext ("Knock_segunda");
				else
					clip = vclips.knock.GetNext ("Knock");
			}
		} else {
			clip = vclips.knock.GetNext ("Knock");
		}

		voice.clip = clip;
		voice.Play();
		Invoke ("Idle_Agite", 3f);

	}
	void OnAvatarStandUp(bool isHero){
		if (!voice.isPlaying) {
			voice.PlayOneShot (vclips.knock.GetNext ("Knock_levanta"));
			Invoke ("Idle_Agite", 3f);
		}
	}

	void OnKO(bool isHero){
		AudioClip clip = null;
		float r = Random.value;
		if (isHero) {
			if (r < 0.5f) {
				if (fStatus.caidas_hero == 3)
					clip = vclips.finPelea.GetNext ("FinPelea_KOT");
				else
					clip = vclips.finPelea.GetNext ("FinPelea_KO");
			} else {
				float r2 = Random.value;
				if (r2 < 0.5f) {
					clip = vclips.finPelea.GetNext ("FinPelea_Lose");
				} else {
					if(Data.Instance.playerSettings.heroData.stats.score<Data.Instance.playerSettings.characterData.stats.score)
						clip = vclips.finPelea.GetNext ("FinPelea_Lose_Reta");
					else
						clip = vclips.finPelea.GetNext ("FinPelea_Lose");
				}
			}
			Invoke ("Epilogo_Lose", 4.5f);
		} else {
			if (r < 0.5f) {
				if (fStatus.caidas_character == 3)
					clip = vclips.finPelea.GetNext ("FinPelea_KOT");
				else
					clip = vclips.finPelea.GetNext ("FinPelea_KO");
			} else {
				float r2 = Random.value;
				if (r2 < 0.5f) {
					clip = vclips.finPelea.GetNext ("FinPelea_Win");
				} else {
					if(Data.Instance.playerSettings.heroData.stats.score<Data.Instance.playerSettings.characterData.stats.score)
						clip = vclips.finPelea.GetNext ("FinPelea_Win_Reta");
					else
						clip = vclips.finPelea.GetNext ("FinPelea_Win_Camp");

				}
			}
			Invoke ("Epilogo_Win", 4.5f);
		}

		voice.clip = clip;
		voice.Play();

	}

	void Epilogo_Lose(){		
		voice.clip = vclips.finPelea.GetNext ("Epilogo_Lose");
		voice.Play();
	}

	void Epilogo_Win(){		
		voice.clip = vclips.finPelea.GetNext ("Epilogo_Wins");
		voice.Play();
	}

	void Idle(){		
		if (!voice.isPlaying && !Game.Instance.fightStatus.state.Equals(FightStatus.states.DONE) && !Game.Instance.fightStatus.state.Equals(FightStatus.states.KO)) {
			voice.PlayOneShot (vclips.idle.GetNext ("Idle"));
		}
	}

	void Idle_Agite(){		
		if (!voice.isPlaying && !Game.Instance.fightStatus.state.Equals(FightStatus.states.DONE) && !Game.Instance.fightStatus.state.Equals(FightStatus.states.KO)) {
			voice.PlayOneShot (vclips.idle.GetNext ("Idle"));
		}
	}

	void Idle_Later(){		
		if (!voice.isPlaying && !Game.Instance.fightStatus.state.Equals(FightStatus.states.DONE) && !Game.Instance.fightStatus.state.Equals(FightStatus.states.KO)) {
			voice.PlayOneShot (vclips.idle.GetNext ("Idle_Later"));
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
