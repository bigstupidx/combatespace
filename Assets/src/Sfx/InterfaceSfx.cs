﻿using UnityEngine;
using System.Collections;

public class InterfaceSfx : MonoBehaviour {

	public AudioClip click1;
	public AudioClip click2;
	public AudioClip click3;
	public AudioClip swap1;
	public AudioClip swap2;
	public AudioClip button;

	private AudioSource source;

	// Use this for initialization
	void Start () {
		source = gameObject.GetComponent<AudioSource> ();
	}

	public void PlaySfx(AudioClip ac){
		source.PlayOneShot (ac);
	}
}