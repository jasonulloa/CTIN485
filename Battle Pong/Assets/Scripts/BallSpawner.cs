using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BallSpawner : NetworkBehaviour {
	public GameObject ballPrefab;
	public bool player1Start = false;
	public bool player2Start = false;
	GameObject ball;
	bool ballInPlay = false;
	//int counter = 3;

	void Update(){
		if (player1Start && player2Start) {
			if (!ballInPlay) {
				if (isServer) {
					CmdLaunchBall ();
					ballInPlay = true;
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
	}

	[Command]
	void CmdLaunchBall(){
		Quaternion launchRotation = Quaternion.Euler (Random.Range (0.0f, 360.0f), 0, 0);
		ball.transform.rotation = launchRotation;
		ball.GetComponent<Rigidbody> ().velocity = ball.transform.forward * 50.0f;
	}
}