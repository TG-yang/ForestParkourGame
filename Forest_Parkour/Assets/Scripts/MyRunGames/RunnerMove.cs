using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame{

	public enum MyTouchDirection{
		None,
		Left,
		Right,
		Top,
		Button
	}

	public class RunnerMove : MonoBehaviour {

		private EnvironmentGenerator m_EnvirGenerator;
		private Transform prisoner;
		private Vector3 lastMouseDown = Vector3.zero;
		private TouchDirection touchDirection=TouchDirection.None;

		public float moveSpeed;
		public float horizontalSpeed;

		public int nowOffsetIndex = 1;
		public int targetOffsetIndex = 1;
		private int[] offsetX = { -14, 1, 14 };
		private float moveHorizontal;

		public bool isSliding = false;
		public float slideTime = 1.4f;
		private float slideTimer;

		public bool isJump = false;
		public float jumpSpeed = 50f;
		public float jumpHight = 20f;
		private bool isUp = false;
		private float haveJumpHight;

		public AudioSource jumpLandMusic;

		private void Awake(){
			m_EnvirGenerator = Camera.main.GetComponent<EnvironmentGenerator> ();
			prisoner = this.transform.Find ("prisoner").transform;
		}

		private void Update(){
			if (MyGameController.gameState == MyGameState.Playing) {
				Vector3 targetPosition = m_EnvirGenerator.forest1.GetTarget ();
				targetPosition = new Vector3 (targetPosition.x + offsetX [nowOffsetIndex], targetPosition.y, targetPosition.z);
				Vector3 moveDir = targetPosition - transform.position;
				transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
				MoveControl ();
			}
		}

		private void MoveControl(){
			MyTouchDirection direction = GetTouchDirection ();

			if (nowOffsetIndex != targetOffsetIndex) {
				float length = Mathf.Lerp (0, moveHorizontal, horizontalSpeed * Time.deltaTime);
				transform.position = new Vector3 (transform.position.x + length, transform.position.y, transform.position.z);
				moveHorizontal -= length;
				if (Mathf.Abs (moveHorizontal) < 0.5f) {
					transform.position = new Vector3 (transform.position.x + moveHorizontal, transform.position.y, transform.position.z);
					moveHorizontal = 0;
					nowOffsetIndex = targetOffsetIndex;
				}
			}
			if (isSliding) {
				slideTimer += Time.deltaTime;
				if (slideTimer > slideTime) {
					slideTimer = 0;
					isSliding = false;
				}
			}

			if (isJump) {
				float moveY = jumpSpeed * Time.deltaTime;
				if (isUp) {
					prisoner.position = new Vector3 (prisoner.position.x, prisoner.position.y + moveY, prisoner.position.z);
					haveJumpHight += moveY;
					if (Mathf.Abs (haveJumpHight - jumpHight) < 0.5f) {
						prisoner.position = new Vector3 (prisoner.position.x, prisoner.position.y + (jumpHight - haveJumpHight), prisoner.position.z);
						isUp = false;
						haveJumpHight = jumpHight;
					}
				} else {
					prisoner.position = new Vector3 (prisoner.position.x, prisoner.position.y - moveY, prisoner.position.z);
					haveJumpHight -= moveY;
					if (Mathf.Abs (haveJumpHight) > 0.5f) {
						prisoner.position = new Vector3 (prisoner.position.x, prisoner.position.y - haveJumpHight, prisoner.position.z);
						isJump = false;
						haveJumpHight = 0;
						jumpLandMusic.Play ();
					}
				}
			}


		}

		private MyTouchDirection GetTouchDirection(){
			if (Input.GetMouseButtonDown (0)) {
				lastMouseDown = Input.mousePosition;
			}
			if (Input.GetMouseButtonUp (0)) {
				Vector3 mouseUp = Input.mousePosition;
				Vector3 mouseDir = mouseUp - lastMouseDown;
				float mouseOffsetX = mouseDir.x;
				float mouseOffsetY = mouseDir.y;
				if (Mathf.Abs (mouseOffsetX) > 50 || Mathf.Abs (mouseOffsetY) > 50) {
					if (Mathf.Abs (mouseOffsetX) > Mathf.Abs (mouseOffsetY) && mouseOffsetX > 0) {
						if (targetOffsetIndex < 2) {
							moveHorizontal = 14;
							++targetOffsetIndex;
							return MyTouchDirection.Right;
						}
					} else if (Mathf.Abs (mouseOffsetX) > Mathf.Abs (mouseOffsetY) && mouseOffsetX < 0) {
						if (targetOffsetIndex > 0) {
							moveHorizontal = -14;
							--targetOffsetIndex;
							return MyTouchDirection.Left;
						}
					} else if (Mathf.Abs (mouseOffsetX) < Mathf.Abs (mouseOffsetY) && mouseOffsetY > 0) {
						if (!isJump) {
							isJump = true;
							isUp = true;
							return MyTouchDirection.Top;
						}
					} else if (Mathf.Abs (mouseOffsetX) < Mathf.Abs (mouseOffsetY) && mouseOffsetY < 0) {
						if (!isSliding) {
							isSliding = true;
							slideTimer = 0;
							return MyTouchDirection.Button;
						}
					}
				}
			}

			return MyTouchDirection.None;
		}
	}

}