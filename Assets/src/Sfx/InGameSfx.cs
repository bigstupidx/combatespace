using UnityEngine;
using System.Collections;

public class InGameSfx : MonoBehaviour {

	public AudioClip publico;
	public AudioClip bell;

	private AudioSource source;

	private Hero hero;
	private Character character;
	private FightStatus fStatus;

	// Use this for initialization
	void Start () {

		source = GetComponent<AudioSource> ();

		Events.OnComputeCharacterPunched += OnComputeCharacterPunched;
		Events.OnComputeHeroPunched += OnComputeHeroPunched;
		Events.OnAvatarFall += OnAvatarFall;
		//Events.OnAvatarStandUp += OnAvatarStandUp;
		Events.OnKO += OnKO;
		Events.OnRoundComplete += OnRoundComplete;
		Events.OnRoundStart += OnRoundStart;
	
		hero = Game.Instance.GetComponent<ComputeFight> ().hero;
		character = Game.Instance.GetComponent<ComputeFight> ().character;

		fStatus = Game.Instance.GetComponent<FightStatus> ();
		source.Play ();
	}

	void OnDestroy(){
		Events.OnComputeCharacterPunched -= OnComputeCharacterPunched;
		Events.OnComputeHeroPunched -= OnComputeHeroPunched;
		Events.OnAvatarFall -= OnAvatarFall;
		//Events.OnAvatarStandUp -= OnAvatarStandUp;
		Events.OnKO -= OnKO;
		Events.OnRoundComplete -= OnRoundComplete;
		Events.OnRoundStart -= OnRoundStart;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnComputeCharacterPunched(HeroActions.actions action){		
		if (hero.combo > 1) {
			source.PlayOneShot(publico);
		}
	}

	void OnComputeHeroPunched(CharacterActions.actions action){
		if (character.combo > 1) {			
			source.PlayOneShot(publico);
		}

	}
	void OnAvatarFall(bool isHero){		
		source.PlayOneShot(publico);
	}

	void OnKO(bool isHero){		
		source.PlayOneShot(publico);
	}

	void OnRoundStart()
	{
		source.PlayOneShot(bell);
		source.Play ();
	}
	void OnRoundComplete()
	{
		source.Stop ();
		source.PlayOneShot(bell);
	}
}
