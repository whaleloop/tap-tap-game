using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour {

	private int value = 1;
	private float minX = -11.53f;
	private float maxX = -5.66f;
	private float minY = -3.53f;
	private float maxY = -2.92f;
	private CharacterManager ninja;

	public void SetValue (int level)
	{
		value += (int)Mathf.Floor (level / 10);
	}

	void OnEnable ()
	{
		float x = Random.Range (minX, maxX);
		float y = Random.Range (minY, maxY);
		gameObject.transform.position = new Vector3 (x, y, 0);
		gameObject.name = "Coin";
		ninja = GameObject.Find("Ninja").GetComponent<CharacterManager> ();
		StartCoroutine (AutoCollectCoins ());
	}

	IEnumerator AutoCollectCoins ()
	{
		yield return new WaitForSeconds (3);
		ninja.AddCoins (CollectCoins ());
	}

	public int CollectCoins ()
	{
		StartCoroutine (DestroyCoin ());
		return value;
	}

	IEnumerator DestroyCoin ()
	{
		yield return new WaitForSeconds (0.01f);
		Destroy(gameObject);
	}
}
