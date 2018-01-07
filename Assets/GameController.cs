using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public static int play_mode;
	public static bool Select_mode = false;
	public static int gamelevel;
	public static int score_attack = 0;
	public static float hp;
	public static bool IsBoss;
	public static float hp_Boss;
	// Use this for initialization
	void Start () {
		play_mode = 0;
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	public static void reset(){
		play_mode = 0;
		Select_mode = false;
		score_attack = 0;
		IsBoss = false;
	}
}
