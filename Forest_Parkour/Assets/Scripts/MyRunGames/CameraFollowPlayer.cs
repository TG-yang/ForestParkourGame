using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame{

	public class CameraFollowPlayer : MonoBehaviour {

		private Transform player;
		private Vector3 offset;
		public float speed = 1;

		private void Awake(){
			player = GameObject.FindGameObjectWithTag ("Player").transform;
			offset = player.position - transform.position;
		}

		private void Update(){
			Vector3 offsetUpdate = player.position + offset;
			transform.position = Vector3.Lerp (transform.position, offsetUpdate, speed * Time.deltaTime);
		}
	}
}
