using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ScoreManager : NetworkBehaviour {
	[SyncVar (hook = "ChangeScore1")] public int score1 = 0;
	[SyncVar (hook = "ChangeScore2")] public int score2 = 0;
	[SyncVar (hook = "ChangeString1")] public string gameoverText = " ";
	[SyncVar (hook = "ChangeString2")] public string restartText = " ";
	public TextMesh display1;
	public TextMesh display2;
	public TextMesh gameover1;
	public TextMesh gameover2;

	void Update () {
		if (display1 && display2) {
			display1.text = "Player 1: " + score1.ToString ();
			display2.text = "Player 2: " + score2.ToString ();
		}

		if (gameover1 && gameover2) {
			gameover1.text = gameoverText;
			gameover2.text = restartText;
		}
	}

	public void ChangeScore1(int score){
		score1 = score;
	}

	public void ChangeScore2(int score){
		score2 = score;
	}

	public void ChangeString1(string text){
		gameoverText = text;
	}

	public void ChangeString2(string text){
		restartText = text;
	}
}