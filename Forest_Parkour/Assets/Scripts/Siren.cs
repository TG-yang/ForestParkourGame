using UnityEngine;
using System.Collections;

public class Siren : MonoBehaviour {

	public float roateSpeed=180f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate (Vector3.up*roateSpeed*Time.deltaTime);
	}
}
