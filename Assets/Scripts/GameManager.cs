using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour {

	public CharacterManager ninja;

	void OnEnable ()
	{
		ninja = GameObject.Find("Ninja").GetComponent<CharacterManager> ();
	}
	
	public void OnTouchAreaTouched ()
	{
		ninja.Attack ();
	}

	void Update ()
	{
		if(Input.GetMouseButtonDown(0)){
			Vector2 mousePosition = (Vector2)(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

			if(hitCollider){
				if (hitCollider.transform.name == "TouchableArea")
				{
					OnTouchAreaTouched ();
				}
				else if (hitCollider.transform.name == "Coin")
				{
					CoinManager coinManager = hitCollider.gameObject.GetComponent<CoinManager> ();
					ninja.AddCoins(coinManager.CollectCoins ());
				}
			}
		}
	}
}
