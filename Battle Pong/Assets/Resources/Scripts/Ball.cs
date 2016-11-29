using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	public AudioSource strike;
	public AudioClip boop;
	public GameObject spawnPoint;
	public bool gameover = false;
	GameObject ball;
	ScoreManager scoreman;
	int point;
	float counter = 3;
	bool launch = false;

	void Awake() {
		spawnPoint = GameObject.Find ("Ball Spawn Point");
		strike = GetComponent<AudioSource>();
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

		if (gameover) {
			scoreman = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
			scoreman.ChangeString1("Game Over");
			scoreman.ChangeString2("Host Press Enter to Restart");
		}
	}

	void OnCollisionEnter(Collision collision) {
		GameObject hit = collision.gameObject;
		scoreman = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();

		if (hit.tag == "Respawn") {
			if (hit.transform.position.x > 0) {
				point = scoreman.score1;
				point++;
				scoreman.ChangeScore1 (point);
			} else if (hit.transform.position.x < 0) {
				point = scoreman.score2;
				point++;
				scoreman.ChangeScore2 (point);
			}

			if (point < 11) {
				gameObject.transform.position = spawnPoint.transform.position;
				int rt_arc = Random.Range (45, 135);
				int lft_arc = Random.Range (225, 315);
				int choice = Random.Range (0, 2);
				int arc = 0;
				if (choice == 0) {
					arc = lft_arc;
				} else {
					arc = rt_arc;
				}
				Vector3 direction = new Vector3(0, arc, 0);
				ball.transform.Rotate (direction);
				gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
				launch = true;
			} else {
				gameObject.transform.position = new Vector3 (999, 999, 999);
				gameObject.transform.rotation = Quaternion.Euler (Vector3.zero);
				gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
				gameover = true;
			}
		} else if (hit.tag != "Text") {
			strike.PlayOneShot(boop, 1);
		}
	}

	void Relaunch(){
		ball.GetComponent<Rigidbody> ().velocity = ball.transform.forward * 75.0f;
		counter = 3;
		launch = false;
	}

	public void restartGame(){
		gameover = false;
		ball.transform.position = spawnPoint.transform.position;
		scoreman = GameObject.Find ("ScoreManager").GetComponent<ScoreManager> ();
		scoreman.ChangeString1(" ");
		scoreman.ChangeString2(" ");
		scoreman.ChangeScore1(0);
		scoreman.ChangeScore2(0);
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in players) {
			Health health = player.GetComponent<Health> ();
			health.OnChangeHealth (100);
		}
		counter = 5;
		launch = true;
	}
}