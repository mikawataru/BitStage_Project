using UnityEngine;
using System.Collections;

public class Boss_1 : MonoBehaviour {
	public GameObject player;
	Rigidbody2D Boss;
	Renderer Image;
	bool IsDamage = false, Move_mode = true;
	int break_cnt,effect_cnt;
	int flame_cnt = 1, attack_cnt = 0, weapon_1_cnt = 0;
	float hp_max = 300;
	Vector3[,] pos = new Vector3[3,3];
	int x =2, y=1, pos_x, pos_y;
	public float speed = 10f;
	public GameObject Weapon_1;
	public GameObject Weapon_2;
	public GameObject break_Effect;
	int attack_mode;
	float attak_reset,weapon_1_rad;
	private AudioSource Sound_break;
	private AudioSource Sound_Weapon_1;
	private AudioSource Sound_Weapon_2;

	
	// Use this for initialization
	void Start () {
//		player = GameObject.Find ("Unitychan").GetComponent<GameObject> ();
		Boss = gameObject.GetComponent<Rigidbody2D> ();
		if (GameController.gamelevel == 1) {
			attak_reset = 0.6f;
			weapon_1_rad = 20;
		} else {
			attak_reset = 1f;
			weapon_1_rad = 30;
		}
		AudioSource[] audioSources = GetComponents<AudioSource>();
		Sound_break = audioSources [0];
		Sound_Weapon_1 = audioSources [1];
		Sound_Weapon_2 = audioSources [2];
		GameController.hp_Boss = hp_max;
		Image = gameObject.GetComponent<SpriteRenderer> ();
		pos [2,2] = new Vector3 (375, 40,0);
		pos [2,1] = new Vector3 (375, 37,0);
		pos [2,0] = new Vector3 (375, 35,0);
		pos [1,2] = new Vector3 (354, 40,0);
		pos [1,1] = new Vector3 (354, 37,0);
		pos [1,0] = new Vector3 (354, 35,0);
		pos [0,2] = new Vector3 (354, 40,0);
		pos [0,1] = new Vector3 (354, 37,0);
		pos [0,0] = new Vector3 (354, 35,0);
		transform.position = pos [x, y];
		pos_x = x;
		pos_y = y;
	}
	// Update is called once per frame
	void Update () {
		if (GameController.IsBoss && GameController.play_mode == 1) {
			iTween.MoveUpdate(Boss.gameObject,iTween.Hash("position",pos[x,y],"time",2.0f));
			flame_cnt++;
			if (Move_mode) {
				if (flame_cnt * Time.deltaTime >= 2) {
					Move_mode = false;
					flame_cnt = 0;
				}
			}else{
				attack (attack_mode);
				attack_cnt++;
				if (attack_cnt * Time.deltaTime >= 3 *attak_reset) {
					attack_cnt = 0;
					attack_mode = Random.Range (0, 2);
				}
				if (flame_cnt * Time.deltaTime >= 10 * attak_reset) {
					flame_cnt = 0;
					Move_mode = true;
					x = Random.Range(0,3);
					y = Random.Range(0,3);
				}
			}
		}
	}

	void FixedUpdate(){
		Debug.Log (GameController.hp_Boss);
		if(GameController.hp_Boss<=0){		
			GameController.IsBoss = false;	
			effect_cnt++;
			break_cnt++;
			if (effect_cnt * Time.deltaTime >= 0.2) {
				Instantiate (break_Effect, transform.position + Random.insideUnitSphere*1.5f, transform.rotation);
				Sound_break.PlayOneShot (Sound_break.clip);
				effect_cnt = 0;
			}
			if (break_cnt * Time.deltaTime >= 5) {
				Destroy (gameObject);
				GameController.play_mode = 2;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if(col.gameObject.CompareTag("Weapon")){
			GameController.hp_Boss -= 10;
			IsDamage = true;
		}
	}

	void attack(int i){
		if (i == 0) {
			weapon_1_cnt++;
			if (weapon_1_cnt * Time.deltaTime >= 0.3 * attak_reset && attack_cnt * Time.deltaTime <= 1.5) {
				Sound_Weapon_1.PlayOneShot (Sound_Weapon_1.clip);
				if (transform.position.x > player.transform.position.x) {
					Instantiate (Weapon_1, transform.position + new Vector3 (0f, -1f, 0f), Quaternion.Euler (0, 0, 90));
				} else {
					Instantiate (Weapon_1, transform.position + new Vector3 (0f, -1f, 0f), Quaternion.Euler (0, 0, 270));
				}
				weapon_1_cnt = 0;
			}
		} else if (i == 1 && attack_cnt == 0) {
			Sound_Weapon_2.PlayOneShot (Sound_Weapon_2.clip);
			for (int j = 0; j < 360/weapon_1_rad; j++) {
				Instantiate (Weapon_2, transform.position, Quaternion.Euler(0,0,j*weapon_1_rad));
			}
		}
	}
}