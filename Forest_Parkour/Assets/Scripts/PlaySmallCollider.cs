using UnityEngine;
using System.Collections;

public class PlaySmallCollider : MonoBehaviour {

	private PlayerAnimation playerAnimation;
	void Awake()
	{
		playerAnimation = GameObject.FindGameObjectWithTag (Tags.player).GetComponent<PlayerAnimation> ();
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == Tags.obstaccke && GameController.gameState == GameState.Playing && playerAnimation.animationState == AnimationState.Slide) {
			GameController.gameState=GameState.End;		
		}
	}
}
