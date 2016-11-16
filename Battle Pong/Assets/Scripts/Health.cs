using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
	public const int maxHealth = 100;
	[SyncVar (hook = "OnChangeHealth")] public int currentHealth = maxHealth;
	public HealthbarManager healthMan;
	public RectTransform healthbar;
	public GameObject healthbarManager;

	public override void OnStartClient(){
		if (this.transform.position.x < 0) {
			this.healthbarManager = GameObject.Find ("Player One Healthbar");
			this.healthMan = this.healthbarManager.GetComponent<HealthbarManager> ();
			this.healthbar = this.healthMan.healthbar;
		} else if (this.transform.position.x > 0) {
			this.healthbarManager = GameObject.Find ("Player Two Healthbar");
			this.healthMan = this.healthbarManager.GetComponent<HealthbarManager> ();
			this.healthbar = this.healthMan.healthbar;
		}
	}

	public void TakeDamage(int amount){
		if (!isServer) {
			return;
		}

		currentHealth -= amount;
		if (currentHealth <= 0) {
			currentHealth = 0;
			Debug.Log ("U R DED LOL");
		}

		if (healthbar == null) {
			healthbar = healthMan.healthbar;
		}
	}

	public void OnChangeHealth(int health){
		healthbar.sizeDelta = new Vector2 (health * 2, healthbar.sizeDelta.y);
	}
}