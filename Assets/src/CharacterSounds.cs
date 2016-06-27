using UnityEngine;
using System.Collections;

public class CharacterSounds : MonoBehaviour {

	public HitAudioClips hitClips;
	public BreakAudioClips breakClips;
	public  AudioSource receiveSource;
	public AudioSource damage;

	private float volLowRange = 0.5f;
	private float volHighRange = 1.0f;

	private float pitchLowRange = 0.9f;
	private float pitchHighRange = 1.2f;


	// Use this for initialization
	void Start () {
		//receiveSource = GetComponent<AudioSource> ();
		Events.OnComputeCharacterPunched += OnComputeCharacterPunched;
		Events.OnCharacterBlockPunch += OnCharacterBlockPunch;
	}

	void OnDestroy(){
		Events.OnComputeCharacterPunched -= OnComputeCharacterPunched;
		Events.OnCharacterBlockPunch -= OnCharacterBlockPunch;
	}

	void OnComputeCharacterPunched(HeroActions.actions action, int combo){
		//Debug.Log (action);
		AudioClip clip = null;
		AudioClip breakClip = null;
		if(action.Equals(HeroActions.actions.GANCHO_UP_L)||action.Equals(HeroActions.actions.GANCHO_UP_R))
			clip = hitClips.ganchoClips [(int)(Random.value * hitClips.ganchoClips.Length)];
		else if(action.Equals(HeroActions.actions.CORTITO_L)||action.Equals(HeroActions.actions.CORTITO_R))
			clip = hitClips.jabsClips [(int)(Random.value * hitClips.jabsClips.Length)];
		else if(action.Equals(HeroActions.actions.GANCHO_DOWN_L)||action.Equals(HeroActions.actions.GANCHO_DOWN_R))
			clip = hitClips.abajoClips [(int)(Random.value * hitClips.abajoClips.Length)];
		float vol = Random.Range (volLowRange, volHighRange);
		receiveSource.volume = vol;
		float pitch = Random.Range (pitchLowRange, pitchHighRange);
		receiveSource.pitch = pitch;
		receiveSource.PlayOneShot (clip);

		breakClip = breakClips.breakClips[(int)(Random.value * breakClips.breakClips.Length)];
		damage.PlayOneShot (breakClip);
		//Debug.Log (clip.name);
	}

	void OnCharacterBlockPunch(HeroActions.actions action){
		//Debug.Log (action);
		AudioClip clip = null;
		if(action.Equals(HeroActions.actions.GANCHO_UP_L)||action.Equals(HeroActions.actions.GANCHO_UP_R))
			clip = hitClips.ganchoBlockClips [(int)(Random.value * hitClips.ganchoBlockClips.Length)];
		else if(action.Equals(HeroActions.actions.CORTITO_L)||action.Equals(HeroActions.actions.CORTITO_R))
			clip = hitClips.jabsBlockClips [(int)(Random.value * hitClips.jabsBlockClips.Length)];
		else if(action.Equals(HeroActions.actions.GANCHO_DOWN_L)||action.Equals(HeroActions.actions.GANCHO_DOWN_R))
			clip = hitClips.abajoBlockClips [(int)(Random.value * hitClips.abajoBlockClips.Length)];
		float vol = Random.Range (volLowRange, volHighRange);
		receiveSource.volume = vol*0.5f;
		float pitch = Random.Range (pitchLowRange, pitchHighRange);
		receiveSource.pitch = pitch;
		receiveSource.PlayOneShot (clip);
		//Debug.Log (clip.name);
	}
}
