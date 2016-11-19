using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class BallSpawner : NetworkBehaviour {
	public GameObject ballPrefab;
	public bool player1Start = false;
	public bool player2Start = false;
	GameObject ball;
	bool ballInPlay = false;
	float counter = 5;

	void Update(){
		if (player1Start && player2Start) {
			if (counter <= 0) {
				if (!ballInPlay) {
					if (isServer) {
						CmdLaunchBall ();
						ballInPlay = true;
					}
				}
			}

			counter -= 0.05f;
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
		Quaternion launchRotation = Quaternion.Euler (0, Random.Range (0.0f, 360.0f), 0);
		ball.transform.rotation = launchRotation;
		ball.GetComponent<Rigidbody> ().velocity = ball.transform.forward * 50.0f;
		counter = 5;
	}
}