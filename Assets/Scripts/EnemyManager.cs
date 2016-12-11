using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

	private int max_health = 10;
	private int current_health;
	private RectTransform healthBar;
	private float healthBarStartingWidth;
	private int dropAmount = 3;
	private bool isDead = false;

	public GameObject coinPrefab;

	void OnEnable ()
	{
		current_health = max_health;
		healthBar = GameObject.Find ("HealthBar").GetComponent<RectTransform> ();
		healthBarStartingWidth = healthBar.rect.width;
	}

	public void DoDamage (int damage)
	{
		current_health -= damage;
		if (current_health <= 0)
		{
			current_health = 0;
			if (!isDead)
			{
				KillEnemy ();
				isDead = true;
			}
			
		}
		else
		{
			UpdateHealthBar ();
		}
	}

	void UpdateHealthBar ()
	{
		float newWidth = healthBarStartingWidth * ((float)(current_health) / (float)(max_health));
		healthBar.sizeDelta = new Vector2 (newWidth, healthBar.rect.height);
	}

	void KillEnemy ()
	{
		gameObject.SetActive (false);
		SpawnCoins ();
	}

	void SpawnCoins ()
	{
		for (int i=0; i<dropAmount; i++)
		{
			Instantiate (coinPrefab);
		}
	}
}
