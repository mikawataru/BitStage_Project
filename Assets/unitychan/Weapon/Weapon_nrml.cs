using UnityEngine;
using System.Collections;

public class Weapon_nrml : MonoBehaviour {

	private GameObject player;
	private Rigidbody2D Bullet;
	private Animator anim;
	private Collider2D body;
	bool touch = false;
	Vector2 dir;
	public float speed = 10f, destroy_time = 3;


	void Start () {
		player = GameObject.Find("Unitychan");
		Bullet = GetComponent<Rigidbody2D>();
		body = GetComponent<Collider2D> ();
		anim = GetComponent<Animator>();
		dir = new Vector2 (speed * player.transform.localScale.x, Bullet.velocity.y);
		Bullet.velocity = dir;
		Vector2 temp = transform.localScale;
		temp.x = player.transform.localScale.x;
		transform.localScale = temp;
		Destroy(gameObject, destroy_time);
	}

	void Update(){
		if (GameController.play_mode == 4) {
			Bullet.velocity = new Vector2 (0f, 0f);
		} else if(touch==false){
			Bullet.velocity = dir;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		touch = true;
		body.enabled = false;
		anim.SetTrigger ("break");
		Destroy(gameObject,0.5f);
		Bullet.velocity = new Vector2 (0,0);
	}

	// Update is called once per frame
}
