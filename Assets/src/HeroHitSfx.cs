using UnityEngine;
using System.Collections;

public class HeroHitSfx : MonoBehaviour {


	public HitAudioClips hitClips;

	private float volLowRange = 0.5f;
	private float volHighRange = 1.0f;

	private float pitchLowRange = 0.8f;
	private float pitchHighRange = 1.2f;

	private AudioSource source;
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		Events.OnComputeHeroPunched += OnComputeHeroPunched;
		Events.OnHeroBlockPunch += OnHeroBlockPunch;
	}

	void OnDestroy(){
		Events.OnCheckHeroHitted -= OnComputeHeroPunched;
		//Events.OnHeroBlockPunch -= OnHeroBlockPunch;
	}

	void OnComputeHeroPunched(CharacterActions.actions action){
		Debug.Log (action);
		AudioClip clip = null;
		if(action.Equals(CharacterActions.actions.ATTACK_L)||action.Equals(CharacterActions.actions.ATTACK_R))
			clip = hitClips.ganchoClips [(int)(Random.value * hitClips.ganchoClips.Length)];
		else if(action.Equals(CharacterActions.actions.ATTACK_L_CORTITO)||action.Equals(CharacterActions.actions.ATTACK_R_CORTITO))
			clip = hitClips.jabsClips [(int)(Random.value * hitClips.jabsClips.Length)];
		/*else if(action.Equals(HeroActions.actions.GANCHO_DOWN_L)||action.Equals(HeroActions.actions.GANCHO_DOWN_R))
			clip = abajoClips [(int)(Random.value * abajoClips.Length)];*/
		float vol = Random.Range (volLowRange, volHighRange);
		source.volume = vol;
		float pitch = Random.Range (pitchLowRange, pitchHighRange);
		source.pitch = pitch;
		source.PlayOneShot (clip);
		Debug.Log (clip.name);
	}

	void OnHeroBlockPunch(CharacterActions.actions action){
		Debug.Log (action);
		AudioClip clip = null;
		if(action.Equals(CharacterActions.actions.ATTACK_L)||action.Equals(CharacterActions.actions.ATTACK_R))
			clip = hitClips.ganchoBlockClips [(int)(Random.value * hitClips.ganchoBlockClips.Length)];
		else if(action.Equals(CharacterActions.actions.ATTACK_L_CORTITO)||action.Equals(CharacterActions.actions.ATTACK_R_CORTITO))
			clip = hitClips.jabsBlockClips [(int)(Random.value * hitClips.jabsBlockClips.Length)];		
		float vol = Random.Range (volLowRange, volHighRange);
		source.volume = vol;
		float pitch = Random.Range (pitchLowRange, pitchHighRange);
		source.pitch = pitch;
		source.PlayOneShot (clip);
		Debug.Log (clip.name);
	}
}
