using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	public const int maxHealth = 100;
	public int currentHealth = maxHealth;
	public HealthbarManager healthMan;
	public RectTransform healthbar;
	int playerID;
	GameObject healthbarManager;

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
		this.currentHealth -= amount;
		if (this.currentHealth <= 0) {
			this.currentHealth = 0;
			Debug.Log ("U R DED LOL");
		}

		if (this.healthbar == null) {
			this.healthbar = this.healthMan.healthbar;
		}

		this.healthbar.sizeDelta = new Vector2 (this.currentHealth * 2, this.healthbar.sizeDelta.y);
	}
}