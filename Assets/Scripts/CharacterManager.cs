using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

	public int strength = 1;
	private EnemyManager enemy;
	public int coins = 0;

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
	}
}
