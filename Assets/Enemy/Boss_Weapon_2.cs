using UnityEngine;
using System.Collections;

public class Boss_Weapon_2 : MonoBehaviour {

	public float speed;
	Rigidbody2D ball;
	SpriteRenderer renderer;
	// Use this for initialization
	void Start () {
		ball = GetComponent<Rigidbody2D> ();
		renderer = gameObject.GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
		ball.velocity = new Vector3 (speed * Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad),speed * Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad),0);
	}

	void FixedUpdate(){
		float level = Mathf.Abs(Mathf.Sin(Time.time * 20));
		renderer.color = new Color(1f,1f,1f,level);
	}

	void OnCollisionEnter2D(Collision2D col){
		if (!col.gameObject.CompareTag ("Weapon")) {
			Destroy (gameObject);
		}
	}
}
