using UnityEngine;
using System.Collections;

public enum GameState
{
    Menu,
	Playing,
	End
}

public class GameController : MonoBehaviour {

	public static GameState gameState=GameState.Menu; 
	public GameObject tapToStartUI;
	public GameObject gameoverUI;
	void Update()
	{
		if (gameState == GameState.Menu) {
		   if(Input.GetMouseButtonDown(0))
			{
				gameState=GameState.Playing;
			}
		}
		if (gameState == GameState.End) {
			gameoverUI.SetActive(true);
			if (Input.GetMouseButtonDown (0)) {
				gameState= GameState.Menu;
				Application.LoadLevel(0);		
			}
		}

	}
}
