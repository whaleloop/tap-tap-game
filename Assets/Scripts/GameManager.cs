using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


	private int levelVal = 1;
	private Text levelText;

	public CharacterManager ninja;
	public int level { 
		get { 
			return levelVal; 
		} 
	}

	void OnEnable ()
	{
		ninja = GameObject.Find("Ninja").GetComponent<CharacterManager> ();
		levelText = GameObject.Find ("LevelText").GetComponent<Text> ();
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

	public void UpdateLevel ()
	{
		levelVal++;
		levelText.text = level.ToString ();
	}
}
