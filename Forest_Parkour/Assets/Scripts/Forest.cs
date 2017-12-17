using UnityEngine;
using System.Collections;

public class Forest : MonoBehaviour {
	public GameObject[] obstacles;
	private Transform player;
	private WayPoints wayPoints;
	public int startLenght=50;
	public int minLenght=100;
	public int maxLenght=200;
	private int targetIndex;
	private EnvGenrator envGenerator;
	void Awake()
	{
		player = GameObject.FindGameObjectWithTag (Tags.player).transform;
		wayPoints = transform.Find ("waypoints").GetComponent<WayPoints> ();
		targetIndex = wayPoints.points.Length - 2;
		envGenerator = Camera.main.GetComponent<EnvGenrator> ();
	}
	// Use this for initialization
	void Start () {
		this.GenerateObstacles ();
	}
	
	// Update is called once per frame
	void Update () {
	  //if (player.position.z > transform.position.z+100) {
		//	Camera.main.SendMessage("GenrateForeat");
		//	GameObject.Destroy(this.gameObject);
		//}
	}

	void GenerateObstacles()
	{
		float start = transform.position.z - 3000;
		float end = start + 3000;
		float z = start + startLenght;
		while (true) {
			z+=Random.Range(minLenght,maxLenght);

		if(z>end){
		     break;
			}else{
				Vector3 position=this.GetWayPositionByZ(z);
				int obsIndex=Random.Range(0,obstacles.Length);
			    GameObject go=GameObject.Instantiate(obstacles[obsIndex],position,Quaternion.identity) as GameObject;
				go.transform.parent=this.transform;
			}
		}
	}
	Vector3 GetWayPositionByZ(float z)
	{
		Transform[] points = wayPoints.points;
		int index = 0;
		for (int i=0; i<points.Length; i++) {
		   if(z<=points[i].position.z&&z>=points[i+1].position.z)
			{
				index=i;
				break;
			}
		}
	return	Vector3.Lerp(points[index+1].position,points[index].position,(z-points[index+1].position.z)/(points[index].position.z-points[index+1].position.z));
	}
   public Vector3 GetTargetPoint()
	{
		while (true) {
			if ((wayPoints.points[targetIndex].position.z - player.position.z) < 100) {
				targetIndex--;
				if(targetIndex<0){
					envGenerator.GenrateForeat();
					Destroy(this.gameObject);
					return envGenerator.forest1.GetTargetPoint();
				}
			}
			else
			{
				return wayPoints.points[targetIndex].position;
			}	
		}

	}
}
