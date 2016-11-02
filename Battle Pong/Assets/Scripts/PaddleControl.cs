using UnityEngine;
using System.Collections;

public class PaddleControl : MonoBehaviour {
	float speed;

	void Start () {
		speed = 1.0f;
	}

	void Update () {
		if (Input.GetKey(KeyCode.UpArrow)){
			this.transform.Translate(-speed, 0, 0);
		}
		if (Input.GetKey(KeyCode.DownArrow)){
			this.transform.Translate(speed, 0, 0);
		}
	}
}
