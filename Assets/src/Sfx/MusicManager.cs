using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	public AudioClip introMusic;
	public AudioClip menuMusic;
	public GameObject fade;
	private AudioSource source;

	// Use this for initialization
	void Start () {		
		source = gameObject.GetComponent<AudioSource> ();
		Events.OnAudioEnable += OnAudioEnable;
		Events.OnGameOver += OnGameOver;
		Events.OnRoundComplete += OnRoundComplete;
		Events.OnRoundStart += OnRoundStart;
	}

	void OnDestroy(){		
		Events.OnAudioEnable -= OnAudioEnable;
		Events.OnGameOver -= OnGameOver;
		Events.OnRoundComplete -= OnRoundComplete;
		Events.OnRoundStart -= OnRoundStart;

	}
	void OnAudioEnable(float vol)
	{		
		source.volume = vol;
	}

	void OnGameOver(){
		FadeIn (4f, introMusic);
	}

	void OnRoundComplete(){
		FadeIn (1f, introMusic);
	}

	void OnRoundStart(){
		FadeOut (2f);
	}

	public void MusicChange(string scene){		
		if (scene.Equals ("0_Splash")) {
			source.clip = introMusic;
			source.Play ();
		} else if (scene.Equals ("03_Home") && (Data.Instance.lastScene.Equals ("0_Splash") || Data.Instance.lastScene.Equals ("Game") || Data.Instance.lastScene.Equals ("06_Summary"))) {
			Debug.Log ("ACA");
			source.clip = menuMusic;
			source.Play ();
		}else if (scene.Equals ("Game")) {
			source.clip = null;
			source.Stop ();
			//source.clip = introMusic;
			//source.Play ();
		}
	}

	public void FadeIn(float seconds, AudioClip clip){
		GameObject f = Instantiate (fade);
		f.transform.parent = Data.Instance.gameObject.transform;
		Fade fadeIn = f.GetComponent<Fade> ();
		fadeIn.OnBeginMethod = () => {
			source.clip = clip;
			source.Play ();
		};
		fadeIn.OnLoopMethod = () => {
			float vol = Mathf.Lerp (0, 1, fadeIn.time);
			source.volume = vol;
		};
		fadeIn.OnEndMethod = () => {
			fadeIn.Destroy();
		};
		fadeIn.StartFadeIn (seconds);
	}

	public void FadeOut(float seconds){
		GameObject f = Instantiate (fade);
		f.transform.parent = Data.Instance.gameObject.transform;
		Fade fadeOut = f.GetComponent<Fade> ();

		fadeOut.OnBeginMethod = () => {			
		};
		fadeOut.OnLoopMethod = () => {
			float vol = Mathf.Lerp (0, 1, fadeOut.time);
			source.volume = vol;
		};
		fadeOut.OnEndMethod = () => {
			source.Stop();
			source.volume = 1f;
			fadeOut.Destroy();
		};
		fadeOut.StartFadeOut (seconds);
	}
}
