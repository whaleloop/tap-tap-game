using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

	private int max_health = 3;
	private int tempMaxHealth;
	private int current_health;
	private float healthBarStartingWidth;
	private int dropAmount = 3;
	private int tempDropAmount;
	private bool isDead = false;
	private float fadeDuration = 0.5f;

	private GameObject healthBar;
	private RectTransform healthBarTransform;
	private Image healthBarImage;
	private GameManager gameManager;
	private SpriteRenderer spriteRender;
	private Text enemyText;

	public GameObject coinPrefab;

	void OnEnable ()
	{
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		healthBar = GameObject.Find ("HealthBar");
		enemyText = GameObject.Find ("EnemyText").GetComponent<Text> ();
		healthBarImage = healthBar.GetComponent<Image> ();
		healthBarTransform = healthBar.GetComponent<RectTransform> ();
		spriteRender = gameObject.GetComponentsInChildren<SpriteRenderer> ()[0];
		healthBarStartingWidth = healthBarTransform.rect.width;
		GenerateEnemy ();
	}

	public void DoDamage (int damage)
	{
		current_health -= damage;
		if (current_health <= 0)
		{
			current_health = 0;
			if (!isDead)
			{
				UpdateHealthBar ();
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
		float newWidth = healthBarStartingWidth * ((float)(current_health) / (float)(tempMaxHealth));
		healthBarTransform.sizeDelta = new Vector2 (newWidth, healthBarTransform.rect.height);
	}

	void KillEnemy ()
	{
		FadeOut ();
		SpawnCoins ();
		gameManager.UpdateLevel();
		StartCoroutine (Respawn ());
	}

	IEnumerator Respawn ()
	{
		yield return new WaitForSeconds (0.5f);
		GenerateEnemy ();
	}

	void SpawnCoins ()
	{
		tempDropAmount = dropAmount;
		if (gameManager.level % 10 == 0)
		{
			tempDropAmount = (int)Mathf.Floor (dropAmount * 2.5f);
		}
		for (int i=0; i<tempDropAmount; i++)
		{
			GameObject coin = Instantiate (coinPrefab);
			coin.GetComponent<CoinManager>().SetValue (gameManager.level);
		}
	}

	public void GenerateEnemy ()
	{
		tempMaxHealth = max_health;
		if (gameManager.level % 10 == 0)
		{
			tempMaxHealth = (int)Mathf.Floor(max_health * 2.5f);
			max_health++;
		}
		if (gameManager.level % 50 == 0)
		{
			dropAmount++;
		}
		isDead = false;
		current_health = tempMaxHealth;
		UpdateHealthBar ();
		FadeIn ();
	}

	void FadeOut ()
	{
		StartCoroutine (Fade (false, Time.time, 1f, 0));
	}

	void FadeIn ()
	{
		StartCoroutine (Fade (true, Time.time, 0, 1f));
	}

	IEnumerator Fade (bool isFadeIn, float startTime, float startOpacity, float endOpacity)
	{
		float t = 0;
		float alpha = Mathf.SmoothStep (startOpacity, endOpacity, t);
		while ((isFadeIn && alpha < endOpacity) || (!isFadeIn && alpha > endOpacity))
		{
			alpha = Mathf.SmoothStep (startOpacity, endOpacity, t);
			spriteRender.color = new Color(spriteRender.color.r, spriteRender.color.g, spriteRender.color.b, alpha);
			healthBarImage.color = new Color(healthBarImage.color.r, healthBarImage.color.g, healthBarImage.color.b, alpha);
			enemyText.color = new Color(enemyText.color.r, enemyText.color.g, enemyText.color.b, alpha);
			t = (Time.time - startTime) / fadeDuration;
			yield return null;
		}
	}
}
