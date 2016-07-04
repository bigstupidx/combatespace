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
		Events.OnAvatarFall += OnAvatarFall;
		Events.OnAvatarStandUp += OnAvatarStandUp;
		Events.OnKO += OnKO;
	
		hero = Game.Instance.GetComponent<ComputeFight> ().hero;
		character = Game.Instance.GetComponent<ComputeFight> ().character;

		fStatus = Game.Instance.GetComponent<FightStatus> ();
	}

	void OnDestroy(){
		Events.OnComputeCharacterPunched -= OnComputeCharacterPunched;
		Events.OnComputeHeroPunched -= OnComputeHeroPunched;
		Events.OnAvatarFall -= OnAvatarFall;
		Events.OnAvatarStandUp -= OnAvatarStandUp;
		Events.OnKO -= OnKO;
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
				else
					clip = vclips.combos.GetNext ("Combos");
			} else {
				clip = vclips.combos.GetNext ("Combos");
			}
			voice.clip = clip;
			voice.Play();
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
				else if (action.Equals (CharacterActions.actions.ATTACK_L_CORTITO))
					clip = vclips.combos.GetNext ("Combos_Upper");
				else
					clip = vclips.combos.GetNext ("Combos");
			} else {
				clip = vclips.combos.GetNext ("Combos");
			}
			voice.clip = clip;
			voice.Play();
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
	}
	void OnAvatarStandUp(bool isHero){
		if (!voice.isPlaying) {
			voice.PlayOneShot (vclips.knock.GetNext ("Knock_levanta"));
		}
	}

	void OnKO(bool isHero){
		AudioClip clip = null;

		if (isHero) {
			if (fStatus.caidas_hero == 3)
				clip = vclips.finPelea.GetNext ("FinPelea_KOT");
			else
				clip = vclips.finPelea.GetNext ("FinPelea_KO");
		} else {
			if (fStatus.caidas_character == 3)
				clip = vclips.finPelea.GetNext ("FinPelea_KOT");
			else
				clip = vclips.finPelea.GetNext ("FinPelea_KO");
		}

		voice.clip = clip;
		voice.Play();
	}

	// Update is called once per frame
	void Update () {
	
	}
}
