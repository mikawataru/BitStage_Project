using UnityEngine;
using System.Collections;

public class Enemy_area_1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.CompareTag("Enemy")) {
			Vector2 dir = col.transform.localScale;
			Vector3 rot = col.transform.rotation.eulerAngles;
			dir.x *= (-1);
			rot.z = rot.z + 180;
			col.transform.localScale = dir;
			col.transform.eulerAngles = rot;
		}
	}
}