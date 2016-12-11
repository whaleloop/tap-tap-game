using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

	private int strength = 1;
	private EnemyManager enemy;
	private int coins = 0;

	void OnEnable ()
	{
		enemy = GameObject.Find ("Enemy").GetComponent<EnemyManager> ();
	}

	public void Attack ()
	{
		GetComponent<Animator> ().Play("Ninja_Attack");
		enemy.DoDamage (strength);
	}

	public void AddCoins (int value)
	{
		coins += value;
		Debug.Log ("Current amount of coins: " + coins.ToString ());
	}
}
