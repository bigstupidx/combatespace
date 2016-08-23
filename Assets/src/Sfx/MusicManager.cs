using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip introMusic;
	public AudioClip menuMusic;
	private AudioSource source;

	// Use this for initialization
	void Start () {		
		source = gameObject.GetComponent<AudioSource> ();
		Events.OnAudioEnable += OnAudioEnable;
	}

	void OnDestroy(){		
		Events.OnAudioEnable -= OnAudioEnable;
	}
	void OnAudioEnable(float vol)
	{		
		source.volume = vol;
	}

	public void MusicChange(string scene){		
		if (scene.Equals ("0_Splash")) {
			source.clip = introMusic;
			source.Play ();
		} else if (scene.Equals ("03_Home")&& (Data.Instance.lastScene.Equals("0_Splash")||Data.Instance.lastScene.Equals("Game"))){
			source.clip = menuMusic;
			source.Play ();
		}else if (scene.Equals ("Game"))
			source.clip = null;
	}
}
