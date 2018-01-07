using UnityEngine;
using System.Collections;

public class Enemy_Weapon_1 : MonoBehaviour {

	private Rigidbody2D weapon;
	private Collider2D body;
	private Animator anim;
	public float speed = 10f, destroy_time = 5;
	bool touch = false;
	int cnt_flame = 0;
	private AudioSource Sound_Explosion;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		weapon = GetComponent<Rigidbody2D>();
		body = GetComponent<BoxCollider2D> ();
		weapon.velocity = new Vector3 (-Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)*speed, Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)*speed, 0f);
		AudioSource[] audioSources = GetComponents<AudioSource>();
		Sound_Explosion = audioSources[0];
	}
	
	// Update is called once per frame
	void Update () {
		if (GameController.play_mode == 4) {
			weapon.velocity = new Vector3 (0f, 0f, 0f);
		} else if(touch==false){
			weapon.velocity = new Vector3(-Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)*speed, Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)*speed, 0f);
			cnt_flame++;
			if (cnt_flame * Time.deltaTime == destroy_time) Destroy(gameObject);
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
