using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame{

	public enum MyAnimationState{
		Idle,
		Run,
		TurnLeft,
		TurnRight,
		Slide,
		Jump,
		Death
	}

	public class MyPlayerAnimation : MonoBehaviour {

		private bool havePlayDeath=false;
		public AudioSource footStepMusic;
		private RunnerMove m_RunnerMove;
		private MyAnimationState animationState = MyAnimationState.Idle;
		private Animation animation;

		private void Awake(){
			m_RunnerMove = gameObject.GetComponent<RunnerMove> ();
			animation = this.transform.Find ("Prisoner").GetComponent<Animation> ();
		}

		private void Update(){
			if (MyGameController.gameState == MyGameState.Menu) {
				animationState = MyAnimationState.Idle;
			} else if(MyGameController.gameState == MyGameState.Playing){
				animationState = MyAnimationState.Run;
				if (m_RunnerMove.nowOffsetIndex < m_RunnerMove.targetOffsetIndex) {
					animationState = MyAnimationState.TurnRight;
				} else if (m_RunnerMove.nowOffsetIndex > m_RunnerMove.targetOffsetIndex) {
					animationState = MyAnimationState.TurnRight;
				} else if (m_RunnerMove.isJump) {
					animationState = MyAnimationState.Jump;
				} else if (m_RunnerMove.isSliding) {
					animationState = MyAnimationState.Slide;
				}
			}
			if (MyGameController.gameState == MyGameState.End) {
				animationState = MyAnimationState.Death;
			}
			if (animationState == MyAnimationState.Run) {
				if (!footStepMusic.isPlaying) {
					footStepMusic.Play ();
				}
			} else {
				if (footStepMusic.isPlaying) {
					footStepMusic.Stop ();
				}
			}
		}

		private void LateUpdate(){
			switch (animationState) {
			case MyAnimationState.Idle:
				PlayIdle ();
				break;
			case MyAnimationState.Run:
				AnimationPlay ("run");
				break;
			case MyAnimationState.Jump:
				AnimationPlay ("jump");
				break;
			case MyAnimationState.Slide:
				AnimationPlay ("slide");
				break;
			case MyAnimationState.TurnLeft:
				animation ["left"].speed = 2f;
				AnimationPlay ("left");
				break;
			case MyAnimationState.TurnRight:
				animation ["right"].speed = 2f;
				AnimationPlay ("right");
				break;
			case MyAnimationState.Death:
				PlayDeath ();
				break;
			}
			
		}

		private void PlayIdle(){
			if (animation.IsPlaying ("Idle_1") == false && animation.IsPlaying ("Idle_2") == false) {
				animation.Play("Idle_1");
				animation.PlayQueued("Idle_2");
			}
		}

		private void AnimationPlay(string AnimationName){
			if (animation.IsPlaying (AnimationName) == false) {
				animation.Play (AnimationName);
			}
		}
		private void PlayDeath(){
			if (animation.IsPlaying ("Death") == false && !havePlayDeath) {
				animation.Play ("Death");
				havePlayDeath = true;
			}
		}
	}



}