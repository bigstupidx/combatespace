using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class VoiceClips : MonoBehaviour {

	public AudioClip[] Comienzo_Pelea;
	public AudioClip[] Comienzo_Round;
	public AudioClip[] Fin_Round;
	public AudioClip[] Idle;
	public AudioClip[] Combos;
	public AudioClip[] Combos_Izq;
	public AudioClip[] Combos_Izq_Abajo;
	public AudioClip[] Combos_Der;
	public AudioClip[] Combos_Upper;
	public AudioClip[] Knock;
	public AudioClip[] Knock_segunda;
	public AudioClip[] Knock_levanta;
	public AudioClip[] FinPelea;
	public AudioClip[] FinPelea_KO;
	public AudioClip[] FinPelea_KOT;


	public VoiceTopic comienzoPelea;
	public VoiceTopic comienzoRound;
	public VoiceTopic finRound;
	public VoiceTopic idle;
	public VoiceTopic combos;
	public VoiceTopic knock;
	public VoiceTopic finPelea;

	[Serializable]
	public class VoiceTopic{

		private Dictionary<string, AudioClip[]> clips;
		private Dictionary<string, int> count;

		public VoiceTopic(){
			clips = new Dictionary<string,AudioClip[]>();
			count = new Dictionary<string,int>();
		}

		public void Add(string key, AudioClip[] ac){
			RandomizeArray (ac);
			clips.Add (key, ac);
			count.Add (key, 0);
		}

		public AudioClip GetNext(string key){
			Debug.Log (key + " count: " + count [key]);
			AudioClip next = null;
			if (clips.ContainsKey (key)) {
				AudioClip[] ac = clips [key];
				next = ac [count[key]];
				count [key] = count [key]+1;
				if (count [key] >= clips [key].Length)
					count [key] = 0;				
			}	
			return next;
		}

		private void RandomizeArray<T>(T[] array)
		{
			int size = array.Length;
			for (int i=0; i < size; i++)
			{
				int indexToSwap = UnityEngine.Random.Range(i, size);
				T swapValue = array[i];
				array[i] = array[indexToSwap];
				array[indexToSwap] = swapValue;
			}
		}
	}

	void Awake(){
		comienzoPelea = new VoiceTopic();
		comienzoPelea.Add ("Comienzo_Pelea", Comienzo_Pelea);

		comienzoRound = new VoiceTopic();
		comienzoRound.Add ("Comienzo_Round", Comienzo_Round);

		finRound = new VoiceTopic();
		finRound.Add ("Fin_Round", Fin_Round);

		idle = new VoiceTopic();
		idle.Add ("Idle", Idle);

		combos = new VoiceTopic();
		combos.Add ("Combos", Combos);
		combos.Add ("Combos_Izq", Combos_Izq);
		combos.Add ("Combos_Izq_Abajo", Combos_Izq_Abajo);
		combos.Add ("Combos_Der", Combos_Der);
		combos.Add ("Combos_Upper", Combos_Upper);

		knock = new VoiceTopic();
		knock.Add ("Knock", Knock);
		knock.Add ("Knock_segunda", Knock_segunda);
		knock.Add ("Knock_levanta", Knock_levanta);

		finPelea = new VoiceTopic();
		finPelea.Add ("FinPelea", FinPelea);
		finPelea.Add ("FinPelea_KO", FinPelea_KO);
		finPelea.Add ("FinPelea_KOT", FinPelea_KOT);
	}	
}
