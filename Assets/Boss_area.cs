using UnityEngine;
using System.Collections;

public class Boss_area : MonoBehaviour {

	public GameObject door_1;
	public GameObject door_2;
	public GameObject door_3;
	int cnt_flame = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameController.play_mode == 5) {
			Doorlock ();
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.CompareTag ("Player")) {
			GameController.play_mode = 5;
		}
	}

	void Doorlock(){
		cnt_flame++;
		if (cnt_flame*Time.deltaTime >= 0.3) {
			door_1.SetActive (true);
		} 
		if (cnt_flame * Time.deltaTime >= 0.6) {
			door_2.SetActive (true);
		}
		if (cnt_flame * Time.deltaTime >= 0.9) {
			door_3.SetActive (true);
		}
	}
}
