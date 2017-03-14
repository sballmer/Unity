using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Ball))]
public class DragLaunch : MonoBehaviour 
{
	private Ball ball;
	private float dragStartTime;
	private Vector3 dragStartPos;
	//private Rigidbody ballRigidbody;

	// Use this for initialization
	void Start () 
	{
		ball = GetComponent<Ball> ();
		//ballRigidbody = ball.GetComponent<Rigidbody> ();
	}

	public void DragStart()
	{
		// capture time and position
		dragStartTime = Time.time;
		dragStartPos = Input.mousePosition;
	}

	public void DragEnd()
	{
		// capture time and position
		float dragEndTime = Time.time;
		Vector3 dragEndPos = Input.mousePosition;;

		// compute velocity
		Vector3 velocity = (dragEndPos - dragStartPos) / (dragEndTime - dragStartTime);

		// inverse y and z (due to camera frame)
		velocity.z = velocity.y;
		velocity.y = 0f;

		// launch the ball
		ball.Launch(velocity);
	}

	public void MoveStart(float x)
	{
		if (!ball.inPlay)
			ball.transform.Translate(Vector3.right * x);
	}
}
