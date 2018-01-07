using UnityEngine;
using System.Collections;
using System;

public class Sound : MonoBehaviour {

	static public AudioSource BGM_Normal;
	static public AudioSource BGM_Boss;
	static public AudioSource sound_Menu;
	static public AudioSource sound_Input;
	static public AudioSource sound_Back;
	static public AudioSource sound_Shot;
	static public AudioSource sound_Item;
	static public AudioSource sound_Explosion_S;
	static public AudioSource sound_Explosion_M;
	static public AudioSource sound_Explosion_L;

	// Use this for initialization
	void Start () {
		AudioSource[] audioSources = GetComponents<AudioSource>();
		BGM_Normal = audioSources[0];
		BGM_Boss = audioSources[1];
		sound_Menu = audioSources[2];
		sound_Input = audioSources[3];
		sound_Back = audioSources[4];
		sound_Shot = audioSources[5];
		sound_Item = audioSources[6];
		sound_Explosion_S = audioSources[7];
	//	sound_Explosion_M = audioSources[9];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
