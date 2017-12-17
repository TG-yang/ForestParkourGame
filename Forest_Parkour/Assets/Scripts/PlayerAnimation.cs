using UnityEngine;
using System.Collections;

public enum AnimationState
{
	Idle,
	Run,
	TurnLeft,
	TurnRight,
	Slide,
	Jump,
	Death
}
public class PlayerAnimation : MonoBehaviour {

	private Animation animation;
	public AnimationState animationState=AnimationState.Idle;
	private PlayerMove playerMove;
	public AudioSource musicFootStep;
	void Awake()
	{ 
		animation = transform.Find ("Prisoner").GetComponent<Animation>();
		playerMove = this.GetComponent<PlayerMove> ();
	}

	// Update is called once per frame
	void Update () {
	
		if (GameController.gameState == GameState.Menu) {
						animationState = AnimationState.Idle;
				} 
		else if (GameController.gameState == GameState.Playing) {
						animationState = AnimationState.Run;	
						if (playerMove.isJumping) {
								animationState = AnimationState.Jump;
						}
						if (playerMove.targetLaneIndex > playerMove.nowLaneIndex) {
								animationState = AnimationState.TurnRight;
						}
						if (playerMove.targetLaneIndex < playerMove.nowLaneIndex) {
								animationState = AnimationState.TurnLeft;
						}
						if (playerMove.isSliding) {
								animationState = AnimationState.Slide;
						}
				} 
		else if (GameController.gameState == GameState.End) {
			animationState=AnimationState.Death;
		}
		if (animationState == AnimationState.Run )
		{
			if(!musicFootStep.isPlaying)
				musicFootStep.Play ();	
		}
		else 
		{
			if(musicFootStep.isPlaying)
				musicFootStep.Stop();
		}
	}
	void LateUpdate()
	{
		switch (animationState) {
		case AnimationState.Idle:
			PlayIdle();
			break;
		case AnimationState.Run:
			PlayAnimation("run");
			break;
		case AnimationState.TurnLeft:
			animation["left"].speed=2f;
			PlayAnimation("left");
			break;
		case AnimationState.TurnRight:
			animation["right"].speed=2f;
			PlayAnimation("right");
			break;
		case AnimationState.Slide:
			PlayAnimation("slide");
			break;
		case AnimationState.Jump:
			PlayAnimation("jump");
			break;
		case AnimationState.Death:
			PlayDeath();
			break;
		}
	}
	void PlayIdle()
	{
		if (animation.IsPlaying ("Idle_1") == false && animation.IsPlaying ("Idle_2") == false) {
			animation.Play("Idle_1");
			animation.PlayQueued("Idle_2");
		}
	}
	void PlayAnimation(string animationName)
	{
		if(animation.IsPlaying(animationName)==false)
		 {
			animation.Play(animationName);
		 }
	}
	private bool havePlayDeath=false;
	private void PlayDeath()
	{
		if (!animation.IsPlaying ("death") && havePlayDeath == false) {
			animation.Play("death");
			havePlayDeath=true;
		}
	}
}
