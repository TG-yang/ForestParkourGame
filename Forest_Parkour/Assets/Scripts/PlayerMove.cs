using UnityEngine;
using System.Collections;

public enum TouchDirection
{
	None,
	Left,
	Right,
	Top,
	Bottom
}
public class PlayerMove : MonoBehaviour {

	public float moveSpeed=100;
	private EnvGenrator envGenerator;
	private TouchDirection touchDirection=TouchDirection.None;
	private Vector3 lastMouseDown=Vector3.zero;
	public int nowLaneIndex=1;
	public int targetLaneIndex=1;
	private float moveHorizontal;
	public float moveHorizontalSpeed=6;
	private float[] xOffest=new float[]{-14,0,14};
	public bool isSliding=false;
	public float slidTime=1.4f;
	private float slidTimer=0;

	private Transform prisoner;
	public bool isJumping = false;
	public float jumpSpeed = 50f;
	public float jumpHeight=20f;
	private bool isUp=true;
	private float haveJumpHeight=0;

	public AudioSource jumpLandMusic;

	void Awake()
	{
		envGenerator = Camera.main.GetComponent<EnvGenrator> ();
		prisoner = this.transform.Find ("Prisoner").transform;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (GameController.gameState == GameState.Playing) {
			Vector3 targetPosition = envGenerator.forest1.GetTargetPoint ();
			targetPosition=new Vector3(targetPosition.x+xOffest[targetLaneIndex],targetPosition.y,targetPosition.z);
			Vector3 moveDir = targetPosition - transform.position;
			transform.position += moveDir.normalized * moveSpeed * Time.deltaTime;
			//transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime);
		     
			MoveControl();
		}
	}
	void MoveControl()
	{
		TouchDirection direction = GetTouchDirection ();
		//Debug.Log (direction);

		if (targetLaneIndex != nowLaneIndex) {
			float moveLength=Mathf.Lerp(0,moveHorizontal,moveHorizontalSpeed*Time.deltaTime);
			transform.position=new Vector3(transform.position.x+moveLength,transform.position.y,transform.position.z);
			moveHorizontal-=moveLength;
			if(Mathf.Abs(moveHorizontal)<0.5f)
			{
			   transform.position= new Vector3(transform.position.x+moveHorizontal,transform.position.y,transform.position.z);
				moveHorizontal=0;
				nowLaneIndex=targetLaneIndex;
			}
		}
		if (isSliding) {
			slidTimer+=Time.deltaTime;
			if(slidTimer>slidTime)
			{
				slidTimer=0;
				isSliding=false;
			}
		
		}
		if (isJumping) {
		  float yMove=jumpSpeed*Time.deltaTime;	
		  if(isUp)
			{
				prisoner.position=new Vector3(prisoner.position.x,prisoner.position.y+yMove,prisoner.position.z);
				haveJumpHeight+=yMove;
				if(Mathf.Abs(jumpHeight-haveJumpHeight)<0.5f)
				{
					prisoner.position=new Vector3(prisoner.position.x,prisoner.position.y+jumpHeight-haveJumpHeight,prisoner.position.z);
					isUp=false;
					haveJumpHeight=jumpHeight;
				}
			}
			else
			{
				prisoner.position=new Vector3(prisoner.position.x,prisoner.position.y-yMove,prisoner.position.z);
				haveJumpHeight-=yMove;
				if(Mathf.Abs(haveJumpHeight-0)<0.5f)
				{
					prisoner.position=new Vector3(prisoner.position.x,prisoner.position.y-(haveJumpHeight),prisoner.position.z);
					isJumping=false;
					haveJumpHeight=0;
					jumpLandMusic.Play();
				}
			}
		}
	}
	TouchDirection GetTouchDirection()
	{
         if (Input.GetMouseButtonDown (0)) {
			lastMouseDown=Input.mousePosition;		
		 }
		 if (Input.GetMouseButtonUp (0)) {
			Vector3 mouseUp=Input.mousePosition;
			Vector3 touchOffset=mouseUp-lastMouseDown;
			float offset_x=Mathf.Abs(touchOffset.x);
			float offset_y=Mathf.Abs(touchOffset.y);
			if(offset_x>50||offset_y>50)
			{
				if(offset_x>offset_y&&touchOffset.x>0)
				{
					if(targetLaneIndex<2)
					{
						targetLaneIndex++;
						moveHorizontal=14;
					}
					return TouchDirection.Right;
				}
				else if(offset_x>offset_y&&touchOffset.x<0)
				{
					if(targetLaneIndex>0)
					{
						targetLaneIndex--;
						moveHorizontal=-14;
					}
					return TouchDirection.Left;
				}
				else if(offset_x<offset_y&&touchOffset.y>0)
				{
					if(!isJumping)
					{
						isJumping=true;
						isUp=true;
					}
					return TouchDirection.Top;
				}
				else if(offset_x<offset_y&&touchOffset.y<0)
				{
					isSliding=true;
					slidTimer=0;
					return TouchDirection.Bottom;
				}
			}
		  }
		return TouchDirection.None;
	}
}
