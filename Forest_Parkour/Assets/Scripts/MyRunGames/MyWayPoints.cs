using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame{

	public class MyWayPoints : MonoBehaviour{

		public Transform[] points;

		private void OnDrawGizmos(){
			iTween.DrawLine (points);
		}
	}

}