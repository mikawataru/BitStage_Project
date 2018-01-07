using UnityEngine;
using System.Collections;

public class Item_HP : MonoBehaviour {

	private AudioSource Sound_Item;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		AudioSource[] audioSources = GetComponents<AudioSource>();
		Sound_Item = audioSources[0];
	}

	void OnTriggerEnter2D(Collider2D col){//当たり判定
		
		if(col.gameObject.CompareTag("Player")){
			Sound_Item.PlayOneShot (Sound_Item.clip);
			GameController.hp += 5f;
			Destroy (gameObject,0.3f);
		}
	}
		
}
