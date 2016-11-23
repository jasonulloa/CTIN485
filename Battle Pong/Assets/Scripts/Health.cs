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
	bool isDead = false;
	float counter = 3;

	void Update(){
		if (isDead) {
			if (counter <= 0) {
				RpcRespawn ();
			}

			counter -= 0.05f;
		}
	}

	public override void OnStartClient(){
		if (transform.position.x < 0) {
			healthbarManager = GameObject.Find ("Player One Healthbar");
			healthMan = healthbarManager.GetComponent<HealthbarManager> ();
			healthbar = healthMan.healthbar;
			spawnPoint = GameObject.Find ("Player One Spawn Point");
		} else if (transform.position.x > 0) {
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
			transform.position = new Vector3(999, 999, 999);
			isDead = true;
		}
	}

	public void OnChangeHealth(int health){
		healthbar.sizeDelta = new Vector2 (health * 2, healthbar.sizeDelta.y);
		currentHealth = health;
	}

	[ClientRpc]
	void RpcRespawn(){
		if (isLocalPlayer) {
			currentHealth = maxHealth;
			transform.position = spawnPoint.transform.position;
		}
	}
}