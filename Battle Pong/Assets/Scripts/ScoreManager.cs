using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ScoreManager : NetworkBehaviour {
	[SyncVar (hook = "OnChangeScore1")] public int score1 = 0;
	[SyncVar (hook = "OnChangeScore2")] public int score2 = 0;
	public TextMesh display1;
	public TextMesh display2;

	void Update () {
		if (display1 && display2) {
			display1.text = "Player 1: " + score1.ToString ();
			display2.text = "Player 2: " + score2.ToString ();
		}
	}

	public void OnChangeScore1(int score){
		score1 = score;
	}

	public void OnChangeScore2(int score){
		score2 = score;
	}
}