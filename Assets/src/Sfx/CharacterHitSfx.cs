using UnityEngine;
using System.Collections;

public class CharacterHitSfx : MonoBehaviour {

	public AudioClip[] ganchoClips;
	public AudioClip[] jabsClips;
	public AudioClip[] abajoClips;
	public AudioClip[] ganchoBlockClips;
	public AudioClip[] jabsBlockClips;
	public AudioClip[] abajoBlockClips;

	private float volLowRange = 0.5f;
	private float volHighRange = 1.0f;

	private float pitchLowRange = 0.9f;
	private float pitchHighRange = 1.2f;

	private AudioSource source;


    void OnAudioEnable(float volume)
    {
        if (volume == 1)
        {
            Events.OnComputeCharacterPunched += OnComputeCharacterPunched;
            Events.OnCharacterBlockPunch += OnCharacterBlockPunch;
        }
        else
        {
            Events.OnComputeCharacterPunched -= OnComputeCharacterPunched;
            Events.OnCharacterBlockPunch -= OnCharacterBlockPunch;
        }
    }


	void Start () {
        Events.OnAudioEnable += OnAudioEnable;        
		source = GetComponent<AudioSource> ();
        OnAudioEnable(Data.Instance.settings.volume);
	}

	void OnDestroy(){
        Events.OnAudioEnable -= OnAudioEnable;
        Events.OnComputeCharacterPunched -= OnComputeCharacterPunched;
        Events.OnCharacterBlockPunch -= OnCharacterBlockPunch;
	}

	void OnComputeCharacterPunched(HeroActions.actions action){
		Debug.Log (action);
		AudioClip clip = null;
		if(action.Equals(HeroActions.actions.GANCHO_UP_L)||action.Equals(HeroActions.actions.GANCHO_UP_R))
			clip = ganchoClips [(int)(Random.value * ganchoClips.Length)];
		else if(action.Equals(HeroActions.actions.CORTITO_L)||action.Equals(HeroActions.actions.CORTITO_R))
			clip = jabsClips [(int)(Random.value * jabsClips.Length)];
		else if(action.Equals(HeroActions.actions.GANCHO_DOWN_L)||action.Equals(HeroActions.actions.GANCHO_DOWN_R))
			clip = abajoClips [(int)(Random.value * abajoClips.Length)];
		float vol = Random.Range (volLowRange, volHighRange);
		source.volume = vol;
		float pitch = Random.Range (pitchLowRange, pitchHighRange);
		source.pitch = pitch;
		source.PlayOneShot (clip);
		Debug.Log (clip.name);
	}

	void OnCharacterBlockPunch(HeroActions.actions action){
		Debug.Log (action);
		AudioClip clip = null;
		if(action.Equals(HeroActions.actions.GANCHO_UP_L)||action.Equals(HeroActions.actions.GANCHO_UP_R))
			clip = ganchoBlockClips [(int)(Random.value * ganchoBlockClips.Length)];
		else if(action.Equals(HeroActions.actions.CORTITO_L)||action.Equals(HeroActions.actions.CORTITO_R))
			clip = jabsBlockClips [(int)(Random.value * jabsBlockClips.Length)];
		else if(action.Equals(HeroActions.actions.GANCHO_DOWN_L)||action.Equals(HeroActions.actions.GANCHO_DOWN_R))
			clip = abajoBlockClips [(int)(Random.value * abajoBlockClips.Length)];
		float vol = Random.Range (volLowRange, volHighRange);
		source.volume = vol;
		float pitch = Random.Range (pitchLowRange, pitchHighRange);
		source.pitch = pitch;
		source.PlayOneShot (clip);
		Debug.Log (clip.name);
	}
}
