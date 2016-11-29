using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PaddleControl : NetworkBehaviour {
	public AudioSource laser;
	public AudioClip pew;
	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	float speed;
	float fireCooldown = 1.5f;
	bool canFire = true;

	void Start () {
		speed = 1.0f;
		laser = GetComponent<AudioSource>();
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
			if (canFire) {
				CmdFire ();
				canFire = false;
			}
		}

		if (!canFire && fireCooldown >= 0) {
			fireCooldown -= 0.05f;
			if (fireCooldown <= 0) {
				canFire = true;
				fireCooldown = 1.5f;
			}
		}
	}

	[Command]
	void CmdFire(){
		GameObject bullet = (GameObject)Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
		bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.up * 50.0f;
		NetworkServer.Spawn (bullet);
		laser.PlayOneShot(pew, 1);
		Destroy (bullet, 3);
	}

	public override void OnStartLocalPlayer(){
		GetComponent<Renderer> ().material = (Material)Resources.Load("Materials/Pad1", typeof(Material));
	}

	public override void OnStartClient(){
		BallSpawner	spawner = GameObject.Find ("Ball Spawn Point").GetComponent<BallSpawner> ();
		if (transform.position.x < 0) {
			spawner.player1Start = true;
			spawner.player1Info.SetActive (true);
		} else if (transform.position.x > 0) {
			spawner.player2Start = true;
			spawner.player2Info.SetActive (true);
		}
		NetworkManagerHUD netHUD = GameObject.Find ("NetworkManager").GetComponent<NetworkManagerHUD> ();
		netHUD.showGUI = false;
	}
}