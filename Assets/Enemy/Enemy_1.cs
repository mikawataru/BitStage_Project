using UnityEngine;
using System.Collections;

public class Enemy_1 : MonoBehaviour {

	public float speed = 5;
	SpriteRenderer Renderer;
	public GameObject weapon;
	private GameObject player;
	private Collider2D body;
	private int cnt_falame = 0, shot_flame;
	private Animator anim;
	private Rigidbody2D Rigid;
	private AudioSource Sound_Explosion;
	private bool mode = false;//false:待機，true:active，

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		body = GetComponent<CircleCollider2D> ();
		Rigid = GetComponent<Rigidbody2D> ();
		player = GameObject.Find ("Unitychan");
		Renderer = gameObject.GetComponent<SpriteRenderer>();
		Renderer.enabled = false;
		body.enabled = false;
		shot_flame = cnt_falame;
		AudioSource[] audioSources = GetComponents<AudioSource>();
		Sound_Explosion = audioSources[0];
	}
	
	// Update is called once per frame
	void Update () {
		if (GameController.play_mode != 4) {
			if (!mode && Vector3.Distance (transform.position, player.transform.position) <= 20) {
				mode = !mode;
				Renderer.enabled = true;
				body.enabled = true;
				shot_flame = cnt_falame;
			}
			if (mode) {
				Rigid.velocity = new Vector3 (-transform.localScale.x * speed, 0f, 0f);
				if (cnt_falame == shot_flame) {
					Instantiate (weapon, transform.position + new Vector3 (-Mathf.Sin (transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Cos (transform.rotation.eulerAngles.z * Mathf.Deg2Rad), 0f), transform.rotation);
				}
			} 

			cnt_falame++;
			if (cnt_falame == 60) {
				cnt_falame = 0;
			}
		} else {
			Rigid.velocity = new Vector3 (0f, 0f, 0f);
		}

		if (GameController.IsBoss == true)gameObject.SetActive (false);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag ("Weapon")) {
			anim.SetTrigger ("break");
			Sound_Explosion.PlayOneShot (Sound_Explosion.clip);
			body.enabled = false;
			Destroy (gameObject, 0.4f);
			GameController.score_attack++;
		}
	}
}




