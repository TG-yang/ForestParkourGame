using UnityEngine;
using System.Collections;

public class PoliceCar : MonoBehaviour {

	public AudioSource tiresMusic;
	private bool havePlayMusic=false;

	
	// Update is called once per frame
	void Update () {
	
		if (havePlayMusic == false && GameController.gameState == GameState.End) {
			tiresMusic.Play();
			havePlayMusic=true;
		}
	}
}
