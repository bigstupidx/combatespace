using UnityEngine;
using System.Collections;

public class HeroSounds : MonoBehaviour {

	public HitAudioClips hitClips;
	public BreakAudioClips breakClips;
	public AudioSource receiveSource;
	public AudioSource sendSource;
	public AudioSource damage;

	private float volLowRange = 0.5f;
	private float volHighRange = 1.0f;

	private float pitchLowRange = 0.8f;
	private float pitchHighRange = 1.2f;

	// Use this for initialization
	void Start () {
		//receiveSource = GetComponent<AudioSource> ();
		Events.OnComputeHeroPunched += OnComputeHeroPunched;
		Events.OnHeroBlockPunch += OnHeroBlockPunch;
        Events.OnHeroSound += OnHeroSound;
	}

	void OnDestroy(){
		Events.OnComputeHeroPunched -= OnComputeHeroPunched;
		Events.OnHeroBlockPunch -= OnHeroBlockPunch;
        Events.OnHeroSound -= OnHeroSound;
	}

	void OnComputeHeroPunched(CharacterActions.actions action){
		//Debug.Log (action);
		AudioClip clip = null;
		AudioClip breakClip = null;
		if(action.Equals(CharacterActions.actions.ATTACK_L)||action.Equals(CharacterActions.actions.ATTACK_R))
			clip = hitClips.ganchoClips [(int)(Random.value * hitClips.ganchoClips.Length)];
		else if(action.Equals(CharacterActions.actions.ATTACK_L_CORTITO)||action.Equals(CharacterActions.actions.ATTACK_R_CORTITO))
			clip = hitClips.jabsClips [(int)(Random.value * hitClips.jabsClips.Length)];
		/*else if(action.Equals(HeroActions.actions.GANCHO_DOWN_L)||action.Equals(HeroActions.actions.GANCHO_DOWN_R))
			clip = abajoClips [(int)(Random.value * abajoClips.Length)];*/
		float vol = Random.Range (volLowRange, volHighRange);
		receiveSource.volume = vol;
		float pitch = Random.Range (pitchLowRange, pitchHighRange);
		receiveSource.pitch = pitch;
		receiveSource.PlayOneShot (clip);

		breakClip = breakClips.breakClips[(int)(Random.value * breakClips.breakClips.Length)];
		damage.PlayOneShot (breakClip);
		//Debug.Log (clip.name);
	}

	void OnHeroBlockPunch(CharacterActions.actions action){
		//Debug.Log (action);
		AudioClip hitClip = null;
		if(action.Equals(CharacterActions.actions.ATTACK_L)||action.Equals(CharacterActions.actions.ATTACK_R))
			hitClip = hitClips.ganchoBlockClips [(int)(Random.value * hitClips.ganchoBlockClips.Length)];
		else if(action.Equals(CharacterActions.actions.ATTACK_L_CORTITO)||action.Equals(CharacterActions.actions.ATTACK_R_CORTITO))
			hitClip = hitClips.jabsBlockClips [(int)(Random.value * hitClips.jabsBlockClips.Length)];	
		float vol = Random.Range (volLowRange, volHighRange);
		receiveSource.volume = vol*0.5f;
		float pitch = Random.Range (pitchLowRange, pitchHighRange);
		receiveSource.pitch = pitch;
		receiveSource.PlayOneShot (hitClip);
	}

    void OnHeroSound(HeroActions.actions action)
    {
		//Debug.Log (action);
		if (!action.Equals (HeroActions.actions.DEFENSE) && !action.Equals (HeroActions.actions.IDLE)) {
			float vol = Random.Range (volLowRange-0.4f, volHighRange-0.4f);
			receiveSource.volume = vol;
			float pitch = Random.Range (pitchLowRange, pitchHighRange - 0.2f);
			sendSource.pitch = pitch;
			sendSource.Play ();
		}
	}
}
