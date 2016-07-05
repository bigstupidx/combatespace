using UnityEngine;
using System.Collections;

public class BreathSfx : MonoBehaviour {

	public AudioClip[] clips;

	private float volLowRange = 0.5f;
	private float volHighRange = 0.9f;

	private AudioSource source;
	private int next;
	private bool paused;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();

		Events.OnHeroAguanteStatus += OnHeroAguanteStatus;
		Events.OnRoundComplete += OnRoundComplete;
		Events.OnRoundStart += OnRoundStart;
	}

	void OnDestroy(){
		Events.OnHeroAguanteStatus -= OnHeroAguanteStatus;
		Events.OnRoundComplete -= OnRoundComplete;
		Events.OnRoundStart -= OnRoundStart;
	}
	
	// Update is called once per frame
	void Update () {
		if (!source.isPlaying && !paused) {
			source.clip = clips [next];
			source.loop = true;
			float vol = Random.Range (volLowRange, volHighRange);
			source.volume = vol;
			source.Play ();
		}
	}

	public void OnHeroAguanteStatus(float progress){
		int sel = (int)((clips.Length-1) * progress);
		//Debug.Log (" P: "+progress+" S: "+sel);
		if (source.clip != clips [sel]) {
			next = sel;
			source.loop = false;
		}		
	}

	void OnRoundStart()
	{		
		paused = false;
		source.Play ();
	}
	void OnRoundComplete()
	{
		paused = true;
		source.Pause ();
	}
}
