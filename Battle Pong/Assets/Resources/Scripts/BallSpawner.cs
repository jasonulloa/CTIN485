﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BallSpawner : NetworkBehaviour {
	public GameObject player1Info;
	public GameObject player2Info;
	public GameObject ballPrefab;
	public TextMesh title;
	public bool player1Start = false;
	public bool player2Start = false;
	GameObject ball;
	Ball ballScript;
	bool gameover = false;
	bool ballInPlay = false;
	float counter = 5;

	void Start(){
		player1Info.SetActive (false);
		player2Info.SetActive (false);
	}

	void Update(){
		if (player1Start || player2Start) {
			title.text = " ";
		}

		if (player1Start && player2Start) {
			player1Info.SetActive (true);
			player2Info.SetActive (true);
			if (counter <= 0) {
				if (!ballInPlay) {
					if (isServer) {
						CmdStartLaunchBall ();
						ballInPlay = true;
					}
				}
			}

			counter -= 0.05f;
		}

		if (ballScript != null) {
			gameover = ballScript.gameover;
		}

		if (gameover) {
			if (isServer) {
				if (Input.GetKey (KeyCode.Return)) {
					ballScript.restartGame ();
				}
			}
		}
	}
	
	public override void OnStartServer ()
	{
		Vector3 spawnPosition = GameObject.Find ("Ball Spawn Point").transform.position;
		Quaternion spawnRotation = Quaternion.Euler (Vector3.zero);

		ball = (GameObject)Instantiate (ballPrefab, spawnPosition, spawnRotation);
		NetworkServer.Spawn (ball);
		ballScript = ball.GetComponent<Ball> ();
	}

	[Command]
	void CmdStartLaunchBall(){
		int rt_arc = Random.Range (45, 135);
		int lft_arc = Random.Range (225, 315);
		int choice = Random.Range (0, 2);
		int arc = 0;
		if (choice == 0) {
			arc = lft_arc;
		} else {
			arc = rt_arc;
		}
		Vector3 point = new Vector3(0, arc, 0);
		ball.transform.Rotate (point);
		ball.GetComponent<Rigidbody> ().velocity = ball.transform.forward * 75.0f;
		counter = 5;
	}
}