using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PaddleControl : NetworkBehaviour {
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public int playerID;
	float speed;

	void Start () {
		speed = 1.0f;
	}

	void Update () {
		if (!isLocalPlayer) {
			return;
		}

		if (Input.GetKey(KeyCode.UpArrow)){
			if (this.transform.position.z < 42) {
				this.transform.Translate (0, 0, speed, Space.World);
			}
		}
		if (Input.GetKey(KeyCode.DownArrow)){
			if (this.transform.position.z > -42) {
				this.transform.Translate (0, 0, -speed, Space.World);
			}
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			CmdFire ();
		}
	}

	[Command]
	void CmdFire(){
		GameObject bullet = (GameObject)Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
		bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.up * 50.0f;
		NetworkServer.Spawn (bullet);
		Destroy (bullet, 3);
	}

	public override void OnStartLocalPlayer(){
		GetComponent<MeshRenderer> ().material.color = Color.red;
		if(this.transform.position.x < 0){
			this.playerID = 1;
			this.GetComponent<Health> ().SetPlayer (playerID);
		} else if (this.transform.position.x > 0){
			this.playerID = 2;
			this.GetComponent<Health> ().SetPlayer (playerID);
		}
	}
}