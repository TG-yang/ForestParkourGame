using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RunGame{
	//动态构建环境
	public class EnvironmentGenerator : MonoBehaviour {

		public MyForest forest1;
		public MyForest forest2;
		public GameObject[] gameObject;
		private int count = 2;

		public void GenerateForest(){
			++count;
			int type = Random.Range (0, 3);
			GameObject go = GameObject.Instantiate (gameObject [type], new Vector3 (0, 0, 3000 * count), Quaternion.identity) as GameObject;
			forest1 = forest2;
			forest2 = go.GetComponent<MyForest>();
		}
	}

}