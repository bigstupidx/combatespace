using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class VoiceClips : MonoBehaviour {

	public AudioClip[] Bienvenida;
	public AudioClip[] Intro;
	public AudioClip[] Comienzo_Pelea;
	public AudioClip[] Comienzo_Round;
	public AudioClip[] Fin_Round;
	public AudioClip[] Idle;
	public AudioClip[] Idle_Later;
	public AudioClip[] Idle_Agite;
	public AudioClip[] Bloqueo;
	public AudioClip[] Combos;
	public AudioClip[] Combos_Izq;
	public AudioClip[] Combos_Izq_Abajo;
	public AudioClip[] Combos_Der;
	public AudioClip[] Combos_Upper;
	public AudioClip[] Combos_Jab;
	public AudioClip[] Combos_Gancho;
	public AudioClip[] Tambalea;
	public AudioClip[] Knock;
	public AudioClip[] Knock_primera;
	public AudioClip[] Knock_segunda;
	public AudioClip[] Knock_levanta;
	public AudioClip[] FinPelea;
	public AudioClip[] FinPelea_KO;
	public AudioClip[] FinPelea_KOT;
	public AudioClip[] FinPelea_Win;
	public AudioClip[] FinPelea_Win_Reta;
	public AudioClip[] FinPelea_Win_Camp;
	public AudioClip[] FinPelea_Lose;
	public AudioClip[] FinPelea_Lose_Reta;
	public AudioClip[] FinPelea_Lose_Camp;
	public AudioClip[] Epilogo_Win;
	public AudioClip[] Epilogo_Lose;



	public VoiceTopic bienvenida;
	public VoiceTopic intro;
	public VoiceTopic comienzoPelea;
	public VoiceTopic comienzoRound;
	public VoiceTopic finRound;
	public VoiceTopic idle;
	public VoiceTopic combos;
	public VoiceTopic bloqueo;
	public VoiceTopic tambalea;
	public VoiceTopic knock;
	public VoiceTopic finPelea;
	public VoiceTopic epilogo;

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
			//Debug.Log (key + " count: " + count [key]);
		}

		public AudioClip GetNext(string key){
			//Debug.Log (key + " count: " + count [key]);
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

		bienvenida = new VoiceTopic();
		bienvenida.Add ("Bienvenida", Bienvenida);

		intro = new VoiceTopic();
		intro.Add ("Intro", Intro);

		comienzoPelea = new VoiceTopic();
		comienzoPelea.Add ("Comienzo_Pelea", Comienzo_Pelea);

		comienzoRound = new VoiceTopic();
		comienzoRound.Add ("Comienzo_Round", Comienzo_Round);

		finRound = new VoiceTopic();
		finRound.Add ("Fin_Round", Fin_Round);

		idle = new VoiceTopic();
		idle.Add ("Idle", Idle);
		idle.Add ("Idle_Later", Idle_Later);
		idle.Add ("Idle_Agite", Idle_Agite);

		combos = new VoiceTopic();
		combos.Add ("Combos", Combos);
		combos.Add ("Combos_Izq", Combos_Izq);
		combos.Add ("Combos_Izq_Abajo", Combos_Izq_Abajo);
		combos.Add ("Combos_Der", Combos_Der);
		combos.Add ("Combos_Upper", Combos_Upper);
		combos.Add ("Combos_Jab", Combos_Jab);
		combos.Add ("Combos_Gancho", Combos_Gancho);

		bloqueo = new VoiceTopic();
		bloqueo.Add ("Bloqueo", Bloqueo);

		tambalea = new VoiceTopic();
		tambalea.Add ("Tambalea", Tambalea);

		knock = new VoiceTopic();
		knock.Add ("Knock", Knock);
		knock.Add ("Knock_primera", Knock_primera);
		knock.Add ("Knock_segunda", Knock_segunda);
		knock.Add ("Knock_levanta", Knock_levanta);

		finPelea = new VoiceTopic();
		finPelea.Add ("FinPelea", FinPelea);
		finPelea.Add ("FinPelea_KO", FinPelea_KO);
		finPelea.Add ("FinPelea_KOT", FinPelea_KOT);
		finPelea.Add ("FinPelea_Win", FinPelea_Win);
		finPelea.Add ("FinPelea_Win_Reta", FinPelea_Win_Reta);
		finPelea.Add ("FinPelea_Win_Camp", FinPelea_Win_Camp);
		finPelea.Add ("FinPelea_Lose", FinPelea_Lose);
		finPelea.Add ("FinPelea_Lose_Reta", FinPelea_Lose_Reta);
		finPelea.Add ("FinPelea_Lose_Camp", FinPelea_Lose_Camp);

		epilogo = new VoiceTopic();
		finPelea.Add ("Epilogo_Win", Epilogo_Win);
		finPelea.Add ("Epilogo_Lose", Epilogo_Lose);
	}	
}
