using UnityEngine;
using System.Collections;

public class Camera_Battle : MonoBehaviour {
	private GameObject player_1;
	private GameObject player_2;
	// Use this for initialization

	void Start () {
		player_1 = GameObject.Find ("1P");
		player_2 = GameObject.Find ("2P");
	}
	// Update is called once per frame

	void Update () {
//		Vector3 center =  Vector3.Lerp(player_1.transform.position, player_2.transform.position, 0.5f);
//		transform.position = center + Vector3.forward * -10;
	}
}