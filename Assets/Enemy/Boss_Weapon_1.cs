using UnityEngine;
using System.Collections;

public class Boss_Weapon_1 : MonoBehaviour {

	private Rigidbody2D weapon;
	private Collider2D body;
	private Animator anim;
	public float speed = 10f, destroy_time = 5, speed_fall = 5f ;
	GameObject player;
	bool touch = false;
	int cnt_flame = 0;
	bool mode = false;
	private AudioSource Sound_Explosion;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
		anim = GetComponent<Animator> ();
		weapon = GetComponent<Rigidbody2D>();
		body = GetComponent<BoxCollider2D> ();
//		weapon.velocity = new Vector3 (-Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)*speed, Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)*speed, 0f);
		weapon.velocity = new Vector3 (0f, -1*speed_fall, 0f);
		AudioSource[] audioSources = GetComponents<AudioSource>();
		Sound_Explosion = audioSources[0];
	}

	// Update is called once per frame
	void Update () {
		if (player.transform.position.y >= weapon.transform.position.y) {
			mode = true;
		}

		if (mode) {
			weapon.velocity = new Vector3 (-Mathf.Sin (transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * speed, Mathf.Cos (transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * speed, 0f);
		} else {
			weapon.velocity = new Vector3 (0f, -1 * speed_fall, 0f);
		}

		if (GameController.play_mode == 4) {
			weapon.velocity = new Vector3 (0f, 0f, 0f);
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		touch = true;
		body.enabled = false;
		anim.SetTrigger ("break");
		Sound_Explosion.PlayOneShot (Sound_Explosion.clip);
		weapon.velocity = new Vector3 (0, 0, 0);
		Destroy (gameObject, 0.5f);
	}

}
