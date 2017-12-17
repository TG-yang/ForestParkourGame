using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame{
	//动态随机构建障碍物
	public class MyForest : MonoBehaviour {

		public GameObject[] obstacles;
		public float maxLength = 200;
		public float minLength = 100;
		public float startLength = 50;

		private Transform player;
		private EnvironmentGenerator m_EnvirGenerator;
		private MyWayPoints wayPoints;
		private int targetIndex;

		private void Awake(){
			player = GameObject.FindGameObjectWithTag ("Player").transform;
			m_EnvirGenerator = Camera.main.GetComponent<EnvironmentGenerator> ();
			wayPoints = GameObject.FindGameObjectWithTag ("waypoints").GetComponent<MyWayPoints> ();
			targetIndex = wayPoints.points.Length - 2;
		}

		private void Start(){
			GenerateObstacles ();
		}

		private void GenerateObstacles(){
			float start = transform.position.z - 3000;
			float end = start + 3000;
			float z = start + startLength;

			while (true) {
				z += Random.Range (minLength, maxLength);
				if (z > end) {
					break;
				} else {
					Vector3 point = this.GetWayPointsByZ (z);
					int obstaclesIndex = Random.Range (0, 7);
					GameObject go = GameObject.Instantiate (obstacles [obstaclesIndex], point, Quaternion.identity, this.transform) as GameObject;
				}
			}
		}

		private Vector3 GetWayPointsByZ(float z){
			Transform[] points = wayPoints.points;
			int index = 0;
			for (int i = 0; i < points.Length; ++i) {
				if (z <= points [i].position.z && z >= points [i + 1].position.z) {
					index = i;
					break;
				}
			}
			return Vector3.Lerp (points [index].position, points [index + 1].position, 
				(points [index].position.z - z) / (points [index].position.z - points [index + 1].position.z));
		}

		public Vector3 GetTarget(){
			while (true) {
				if (wayPoints.points [targetIndex].position.z - player.transform.position.z < 100) {
					--targetIndex;
					if(targetIndex<0){
						m_EnvirGenerator.GenerateForest ();
						Destroy (this.gameObject);
						return m_EnvirGenerator.forest1.GetTarget ();
						}

				} else {
					return wayPoints.points [targetIndex].position;
				}
			}
		}
	}
		
}
