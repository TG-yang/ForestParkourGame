using UnityEngine;
using System.Collections;

public class EnvGenrator : MonoBehaviour {

	public Forest forest1;
	public Forest forest2;
	public int forestCount = 2;
	public GameObject[] forests;
    public void GenrateForeat()
	{
		forestCount++;
		int type = Random.Range (0, 3);
	    GameObject newforest=GameObject.Instantiate (forests [type],
		               new Vector3(0,0,forestCount*3000),Quaternion.identity) as GameObject;
		Debug.Log (forestCount);
		forest1 = forest2;
		forest2 = newforest.GetComponent<Forest> ();
	}
}
