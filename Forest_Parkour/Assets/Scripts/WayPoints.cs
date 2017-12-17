using UnityEngine;
using System.Collections;

public class WayPoints : MonoBehaviour {

	public Transform[] points;

	void OnDrawGizmos()
	{
		iTween.DrawPath (points);
	}
}
