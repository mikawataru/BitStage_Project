using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Unitychan : MonoBehaviour {
	Rigidbody2D player;
	CircleCollider2D body;
	Slider HPgage;
	Animator anim;
	SpriteRenderer renderer;
	public GameObject Boss;
	public GameObject weapon_nrml;
	public GameObject weapon_sp;
	public GameObject Item;
	public GameObject Effect_Material_1;
	private float speed_run = 5f, speed_jump = 15f;
	private Vector2 dir;
	private bool flag_Fall = false, flag_damage = false, flag_shot = false;
	private float hp_max = 10;
	private int flame_cnt_damage = 0, cnt_jump = 0;
	private AudioSource Sound_Shot;

	void Start () {
		renderer = gameObject.GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		player = GetComponent<Rigidbody2D>();
		body = GetComponent<CircleCollider2D>();
		HPgage = GameObject.Find("HPgage").GetComponent<Slider>();
		dir = transform.localScale;
		GameController.hp = hp_max;
		AudioSource[] audioSources = GetComponents<AudioSource>();
		Sound_Shot = audioSources[0];

	}

	// Update is called once per frame
	void Update () {
		
		if (GameController.play_mode == 1) {
			int x = (int)Input.GetAxis ("Horizontal_1");

			if (x != 0) {
				player.velocity = new Vector2 (x * speed_run, player.velocity.y);
				if (x != dir.x) {
					dir = transform.localScale;
					dir.x = x;
					transform.localScale = dir;
				}
				anim.SetBool ("Run", true);
			} else {
				player.velocity = new Vector2 (0, player.velocity.y);
				anim.SetBool ("Run", false);
			}

			if (Input.GetButtonDown ("A_1")) {
				if (cnt_jump < 2) {
					player.velocity = new Vector2 (player.velocity.x, speed_jump);
					flag_Fall = false;
					anim.SetTrigger ("Jump");
					cnt_jump++;
				}
			}

			if (Input.GetButtonDown ("B_1")) {
				Sound_Shot.PlayOneShot (Sound_Shot.clip);
				anim.SetBool ("Shot", true);
				Instantiate (weapon_nrml, transform.position + new Vector3 (dir.x * 1f, 0f, 0f), transform.rotation);
			} else {
				anim.SetBool ("Shot", false);
			}
		} else if (GameController.play_mode == 5) {
			if (Vector3.Distance (transform.position, Boss.transform.position) > 10) {
				player.velocity = new Vector2 (speed_run, player.velocity.y);
				anim.SetBool ("Run", true);
			} else {
				player.velocity = new Vector2 (0, 0);
				anim.SetBool ("Run", false);
				GameController.IsBoss = true;
			}
		} else if (GameController.play_mode == 2) {
			anim.SetBool ("Shot",false);
			anim.SetBool ("Run",false);
			player.velocity = new Vector2 (0, player.velocity.y);
		}

		if (player.velocity.y < -0.01 && !flag_Fall) {
			flag_Fall = true;
			anim.SetTrigger ("Fall");
			if (cnt_jump == 0) {
				cnt_jump++;
			}
		} else if(player.velocity.y >= 0 && flag_Fall){
			flag_Fall = false;
			cnt_jump = 0;
			anim.SetTrigger ("Land");
		}
	}

	void FixedUpdate (){
		if (flag_damage) {
			flame_cnt_damage++;
			float level = Mathf.Abs(Mathf.Sin(Time.time * 20));
			renderer.color = new Color(1f,1f,1f,level);
			if (flame_cnt_damage * Time.deltaTime >= 0.8f) {
				flag_damage = false;
				gameObject.layer = 9;
				renderer.color = new Color(1f,1f,1f,1f);
				flame_cnt_damage = 0;
			}
		}
		HPgage.value = GameController.hp;
		if (GameController.hp <= 0) {
			Destroy (gameObject);
			Dead_Effect (transform.position);
			GameController.play_mode = 3;
		}

		if (GameController.play_mode == 4) {
			anim.speed = 0;
			player.Sleep ();
		} else {
			player.WakeUp ();
			anim.speed = 1;
		}
	}


	void OnCollisionEnter2D(Collision2D col){//当たり判定
		
		if(GameController.play_mode==0) GameController.play_mode = 1;

		if (col.gameObject.CompareTag ("Enemy") || col.gameObject.CompareTag ("Enemy_Weapon")) {
			if (!flag_damage) {
				flag_damage = true;
				GameController.hp -= 1;
				gameObject.layer = 10;
			}
		} else if (col.gameObject.CompareTag ("Dead")) {
			GameController.play_mode = 3;
			GameController.hp = 0;
		}
	}

	void Dead_Effect(Vector3 pos){
		for (int i = 0; i < 12; i++) {
			Instantiate (Effect_Material_1, pos, Quaternion.Euler(0,0,i*30));
		}
	}
}
