using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Title : MonoBehaviour {

	private AudioSource Sound_Input;
	public GameObject START;
	public GameObject HARD;
	public GameObject EASY;
	public Button Button_HARD;
	public Button Button_EASY;
	public Text GameLevel;
	bool IsSelectLevel;
	int Select = 1;
	int cnt_flame = 0;
	// Use this for initialization
	void Start () {
		AudioSource[] audioSources = GetComponents<AudioSource>();
		Sound_Input = audioSources[0];
	}
	
	// Update is called once per frame
	void Update () {
		if (cnt_flame != 0) {
			cnt_flame++;
			if (cnt_flame * Time.deltaTime >= 2) {
				SceneManager.LoadScene (Select);
			}
		}
		if (IsSelectLevel == true) {
			if ((int)Input.GetAxis("Vertical")==1) {
				Button_HARD.Select ();
			} else if ((int)Input.GetAxis("Vertical")==-1) {
				Button_EASY.Select ();
			}
		}
	}

	public void Select_Start(){
		Sound_Input.PlayOneShot (Sound_Input.clip);
		IsSelectLevel = true;
		START.SetActive (false);
		HARD.SetActive (true);
		EASY.SetActive (true);
		Button_EASY.interactable = true;
		Button_HARD.interactable = true;
		GameLevel.enabled = true;
		Button_EASY.Select ();
	}

	public void Select_HARD(){
		GameController.gamelevel = 1;
		Sound_Input.PlayOneShot (Sound_Input.clip);
		cnt_flame++;
		Button_EASY.interactable = false;
		Button_HARD.interactable = false;
	}

	public void Select_EASY(){
		GameController.gamelevel = 0;
		Sound_Input.PlayOneShot (Sound_Input.clip);
		cnt_flame++;
		Button_EASY.interactable = false;
		Button_HARD.interactable = false;
	}
		
}
