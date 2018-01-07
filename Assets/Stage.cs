using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour {

	public GameObject Enemy_Hard;
	public Text Pause_text;
	public Text Goal_text;
	public Text GameOver_text;
	public Button Retry_Button;
	public Text Retry_text;
	public Button Exit_Button;
	public Text Exit_text;
	private AudioSource BGM_Normal;
	private AudioSource BGM_Boss;
	private AudioSource Sound_Input;
	private AudioSource Sound_Menu;
	private AudioSource Sound_Back;
	private AudioSource BGM_GameOver;
	private AudioSource BGM_GameClear;
	private AudioSource Sound_GameClear;

	// Use this for initialization
	void Start () {
		if (GameController.gamelevel == 1) {
			Enemy_Hard.SetActive (true);
		}
		AudioSource[] audioSources = GetComponents<AudioSource>();
		BGM_Normal = audioSources[0];
		BGM_Boss = audioSources[4];
		Sound_Input = audioSources[1];
		Sound_Menu = audioSources[2];
		Sound_Back = audioSources[3];
		BGM_GameOver = audioSources [5];
		BGM_GameClear = audioSources [6];
		Sound_GameClear = audioSources [7];
		Retry_Button.interactable = false;
		Retry_text.enabled = false;
		Exit_Button.interactable = false;
		Exit_text.enabled = false;


	}

	// Update is called once per frame
	void Update () {
		if (GameController.play_mode == 5 && !GameController.IsBoss) {
			BGM_Normal.Stop ();
		} else if(GameController.play_mode == 5 && GameController.IsBoss){
			BGM_Boss.Play ();
			GameController.play_mode = 1;
		}
	}

	void FixedUpdate(){
		if (GameController.play_mode == 3) {
			GameOver_text.enabled = true;
			IsSelect ();
			if (!BGM_GameOver.isPlaying) {
				BGM_GameOver.PlayDelayed (0.5f);
			}
		} else if (GameController.play_mode == 2) {
			Goal_text.enabled = true;
			IsSelect ();
			if (!BGM_GameClear.isPlaying) {
				Sound_GameClear.PlayOneShot (Sound_GameClear.clip);
				BGM_GameClear.PlayDelayed (3.5f);
			}
		} else if (GameController.play_mode == 1 && Input.GetButtonDown ("START")) {
			Pause_text.enabled = true;
			IsSelect ();
			GameController.play_mode = 4;
		} else if (GameController.play_mode == 4 ) {
			if(Input.GetButtonDown ("B")||Input.GetButtonDown ("START")){
				Pause_text.enabled = false;
				NoSelect ();
				GameController.play_mode = 1;
			}
		}

		if (GameController.Select_mode == true) {
			if ((int)Input.GetAxis("Horizontal")==-1) {
				Retry_Button.Select ();
			} else if ((int)Input.GetAxis("Horizontal")==1) {
				Exit_Button.Select ();
			}
		}

		if (GameController.hp_Boss <= 0) {
			if (BGM_Boss.isPlaying) {
				BGM_Boss.Stop ();
			}
		}

	}

	public void IsSelect(){
		GameController.Select_mode = true;
		Retry_Button.interactable = true;
		Retry_text.enabled = true;
		Exit_Button.interactable = true;
		Exit_text.enabled = true;
		BGM_Normal.Stop ();
		BGM_Boss.Stop ();
	}

	public void NoSelect(){
		GameController.Select_mode = false;
		Retry_Button.interactable = false;
		Retry_text.enabled = false;
		Exit_Button.interactable = false;
		Exit_text.enabled = false;
		Sound_Back.PlayOneShot (Sound_Back.clip);
		if (!GameController.IsBoss) {
			BGM_Normal.Play ();
		} else {
			BGM_Boss.Play ();
		}
	}

	public void Select_Retry(){
		GameController.Select_mode = false;
		GameController.play_mode = 0;
		Sound_Input.PlayOneShot (Sound_Input.clip);
		SceneManager.LoadScene (2);
		GameController.reset ();
	}

	public void Select_Exit(){
		GameController.Select_mode = false;
		GameController.play_mode = 0;
		Sound_Input.PlayOneShot (Sound_Input.clip);
		SceneManager.LoadScene (0);
		GameController.reset ();
	}
}
