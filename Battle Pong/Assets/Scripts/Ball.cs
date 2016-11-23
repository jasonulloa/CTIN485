using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	public GameObject spawnPoint;
	GameObject ball;
	ScoreManager scoreman;
	float counter = 3;
	bool launch = false;

	void Start() {
		spawnPoint = GameObject.Find ("Ball Spawn Point");
		scoreman = GameObject.Find ("Game Field").GetComponent<ScoreManager> ();
	}

	void Update() {
		if (ball == null) {
			ball = GameObject.FindGameObjectWithTag ("Ball");
		}

		if (launch) {
			if (counter <= 0) {
				if (ball != null) {
					Relaunch ();
				}
			}

			counter -= 0.05f;
		}
	}

	void OnCollisionEnter(Collision collision) {
		GameObject hit = collision.gameObject;

		if (hit.tag == "Respawn") {
			if (hit.transform.position.x > 0) {
				scoreman.score1++;
			} else if (hit.transform.position.x < 0) {
				scoreman.score2++;
			}
			gameObject.transform.position = spawnPoint.transform.position;
			gameObject.transform.rotation = Quaternion.Euler (Vector3.zero);
			gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			launch = true;
		}
	}

	void Relaunch(){
		ball.GetComponent<Rigidbody> ().velocity = ball.transform.forward * 75.0f;
		counter = 3;
		launch = false;
	}
}