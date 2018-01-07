using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TestButtonClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("Start()");
	}

	// Update is called once per frame
	void Update () {

	}

	/// ボタンをクリックした時の処理
	public void OnClick() {
		Debug.Log("Button click!");
	}

}