using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
	public const int maxHealth = 100;
	[SyncVar (hook = "OnChangeHealth")] public int currentHealth = maxHealth;
	HealthbarManager healthMan;
	RectTransform healthbar;
	GameObject healthbarManager;
	GameObject spawnPoint;
	//int playerID;

	public override void OnStartClient(){
		if (transform.position.x < 0) {
			//playerID = 1;
			healthbarManager = GameObject.Find ("Player One Healthbar");
			healthMan = healthbarManager.GetComponent<HealthbarManager> ();
			healthbar = healthMan.healthbar;
			spawnPoint = GameObject.Find ("Player One Spawn Point");
		} else if (transform.position.x > 0) {
			//playerID = 2;
			healthbarManager = GameObject.Find ("Player Two Healthbar");
			healthMan = healthbarManager.GetComponent<HealthbarManager> ();
			healthbar = healthMan.healthbar;
			spawnPoint = GameObject.Find ("Player Two Spawn Point");
		}
	}

	public void TakeDamage(int amount){
		if (!isServer) {
			return;
		}

		currentHealth -= amount;
		if (currentHealth <= 0) {
			currentHealth = maxHealth;
			RpcRespawn ();
		}
	}

	public void OnChangeHealth(int health){
		healthbar.sizeDelta = new Vector2 (health * 2, healthbar.sizeDelta.y);
		currentHealth = health;
	}

	[ClientRpc]
	void RpcRespawn(){
		if (isLocalPlayer) {
			transform.position = spawnPoint.transform.position;
		}
	}
}