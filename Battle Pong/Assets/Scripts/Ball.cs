using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	public GameObject spawnPoint;

	void Start() {
		spawnPoint = GameObject.Find ("Ball Spawn Point");
	}

	void OnCollisionEnter(Collision collision) {
		GameObject hit = collision.gameObject;

		if (hit.tag == "Respawn") {
			gameObject.transform.position = spawnPoint.transform.position;
			gameObject.transform.rotation = Quaternion.Euler (Vector3.zero);
			gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}
	}
}