using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour {

	AudioSource sound_wp;
	GameObject player;
	public int x = 0;
	private int cnt_move = 0;

	void Start () {
		player = GameObject.Find ("Unitychan");	
		sound_wp = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (cnt_move != 0) {
			cnt_move++;
			if (cnt_move * Time.deltaTime >= 2) {
				SceneManager.LoadScene (x);
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			if (Input.GetAxis ("Vertical_1") == -1) {
				if (cnt_move == 0) {
					sound_wp.PlayOneShot (sound_wp.clip);
				}
				cnt_move++;
				Destroy (player);
			}
		}
	}
}
