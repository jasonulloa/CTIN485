using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
	public const int maxHealth = 100;
	[SyncVar (hook = "OnChangeHealth")] public int currentHealth = maxHealth;
	public HealthbarManager healthMan;
	public RectTransform healthbar;
	public int playerID;
	public GameObject healthbarManager;

	public void SetPlayer(int ID){
		this.playerID = ID;

		if (playerID == 1) {
			this.healthbarManager = GameObject.Find ("Player One Healthbar");
			this.healthMan = this.healthbarManager.GetComponent<HealthbarManager> ();
			this.healthbar = this.healthMan.healthbar;
		} else if (playerID == 2) {
			this.healthbarManager = GameObject.Find ("Player Two Healthbar");
			this.healthMan = this.healthbarManager.GetComponent<HealthbarManager> ();
			this.healthbar = this.healthMan.healthbar;
		}
	}

	public void TakeDamage(int amount){
		if (!isServer) {
			return;
		}

		this.currentHealth -= amount;
		if (this.currentHealth <= 0) {
			this.currentHealth = 0;
			Debug.Log ("U R DED LOL");
		}

		if (this.healthbar == null) {
			this.healthbar = this.healthMan.healthbar;
		}
	}

	public void OnChangeHealth(int health){
		this.healthbar.sizeDelta = new Vector2 (health * 2, this.healthbar.sizeDelta.y);
	}
}