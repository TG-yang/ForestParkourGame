using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	private Transform player;
	private Vector3 offset=Vector3.zero;
	public float moveSpeed=1;
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag (Tags.player).transform;
		offset = transform.position - player.position;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	 
		Vector3 targetPosition = player.position + offset;
		transform.position= Vector3.Lerp (transform.position, targetPosition, moveSpeed * Time.deltaTime);
	}
}
