using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame{

	public enum MyGameState{
		Menu,
		Playing,
		End
	}

	public class MyGameController : MonoBehaviour {

		public static MyGameState gameState = MyGameState.Menu;
		public GameObject tapToStartUI;
		public GameObject gameoverUI;

		private void Update(){
			if (gameState == MyGameState.Menu) {
				if (Input.GetMouseButtonDown (0)) {
					gameState = MyGameState.Playing;
				}
			} else if (gameState == MyGameState.End) {
				gameoverUI.SetActive (true);
				if (Input.GetMouseButtonDown (0)) {
					gameState = MyGameState.Menu;
					Application.LoadLevel (0);
				}
			}
		}
	}
}
