using UnityEngine;
using System.Collections;

public class Camera_Main : MonoBehaviour {
	private GameObject player;
	private Vector3 pos;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Unitychan");
	}
	
	// Update is called once per frame
	void Update () {
		pos = new Vector3 (player.transform.position.x, player.transform.position.y+2, -10);
/*		if (player.transform.position.y - transform.position.y > 2) {
			pos.y = player.transform.position.y - 2;
		}
*/		transform.position = pos;
	}
}
