using UnityEngine;
using System.Collections;

public class BreathSfx : MonoBehaviour {

	public AudioClip[] clips;

	private float volLowRange = 0.5f;
	private float volHighRange = 0.9f;

	private AudioSource source;
	private int next;

	// Use this for initialization
	void Awake () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!source.isPlaying) {
			source.clip = clips [next];
			source.loop = true;
			float vol = Random.Range (volLowRange, volHighRange);
			source.volume = vol;
			source.Play ();
		}
	}

	public void SetBreathProgress(float progress){
		int sel = (int)((clips.Length-1) * progress);
		//Debug.Log (" P: "+progress+" S: "+sel);
		if (source.clip != clips [sel]) {
			next = sel;
			source.loop = false;
		}		
	}
}
