using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Select : MonoBehaviour {
	
	Rigidbody2D player;
	CircleCollider2D body;
	Animator anim;
	SpriteRenderer renderer;
	public GameObject weapon_nrml;
	public GameObject weapon_sp;
	private float speed_run = 5f, speed_jump = 15f;
	private Vector2 dir;
	private bool flag_Fall = false;
	private int cnt_jump = 0;
	private AudioSource Sound_Shot;

	// Use this for initialization
	void Start () {
		renderer = gameObject.GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		player = GetComponent<Rigidbody2D>();
		body = GetComponent<CircleCollider2D>();
		dir = transform.localScale;
		AudioSource[] audioSources = GetComponents<AudioSource>();
		Sound_Shot = audioSources[0];
	}

	// Update is called once per frame
	void Update () {
		int x = (int)Input.GetAxis ("Horizontal_1");

		if ( x != 0 ) {
			player.velocity = new Vector2 (x * speed_run, player.velocity.y);
			if (x != dir.x) {
				dir = transform.localScale;
				dir.x = x;
				transform.localScale = dir;
			}
			anim.SetBool ("Run", true);
		}else{
			player.velocity = new Vector2 (0, player.velocity.y);
			anim.SetBool ("Run", false);
		}

		if (Input.GetButtonDown("A_1")) {
			if (cnt_jump < 2) {
				player.velocity = new Vector2 (player.velocity.x, speed_jump);
				flag_Fall = false;
				anim.SetTrigger ("Jump");
				cnt_jump++;
			}
		}

		if (Input.GetButtonDown("B_1")) {
			Sound_Shot.PlayOneShot (Sound_Shot.clip);
			anim.SetBool ("Shot", true);
			Instantiate (weapon_nrml, transform.position + new Vector3 (dir.x * 1f, 0f, 0f), transform.rotation);
		} else {
			anim.SetBool ("Shot", false);
		}

		if (player.velocity.y < -0.01 && !flag_Fall) {
			flag_Fall = true;
			anim.SetTrigger ("Fall");
		} else if(player.velocity.y >= 0 && flag_Fall){
			flag_Fall = false;
			cnt_jump = 0;
			anim.SetTrigger ("Land");
		}
	}

	void OnCollisionEnter2D(Collision2D col){//当たり判定
	}
}
