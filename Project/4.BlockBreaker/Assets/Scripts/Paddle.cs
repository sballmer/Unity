using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour 
{
	public bool autoPlay = false;
	private Ball ball;

	// Use this for initialization
	void Start () 
	{
		ball = GameObject.FindObjectOfType<Ball> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (autoPlay) 
		{
			MoveWithBall();
		} 
		else 
		{
			MoveWithMouse ();
		}
	}

	void MoveWithMouse()
	{
		float mousePosX = Input.mousePosition.x; // / Screen.width * 761f;
		
		this.transform.position = new Vector3 (Mathf.Clamp (mousePosX, 39f, 760f), 
		                                       this.transform.position.y,
		                                       this.transform.position.z);
	}

	void MoveWithBall()
	{
		this.transform.position = new Vector3 (Mathf.Clamp (ball.transform.position.x, 39f, 760f), 
		                                       this.transform.position.y,
		                                       this.transform.position.z);
	}
}
