using UnityEngine;
using System.Collections;

public class BallCreator : MonoBehaviour 
{
	public GameObject ballModel;

	private int numberOfBall;
	private int numberOfBallSent;

	private const float dtBallLauncher = 0.2f;
	private float direction;

	// Use this for initialization
	void Start () 
	{
		direction = 0f;
		numberOfBall = 10;
		numberOfBallSent = numberOfBall;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (numberOfBallSent == numberOfBall) 
		{
			if (Input.GetMouseButtonDown (0))
				LaunchBall ();

			Vector2 ball2mouse = mousePos() - Ball.GetStartingPos();
			direction = Mathf.Atan2(ball2mouse.y, ball2mouse.x);
		}
	}

	void LaunchBall()
	{
		numberOfBallSent = 0;

		InvokeRepeating ("makeABall", 0.001f, dtBallLauncher);
	}

	void makeABall()
	{
		if (ballModel) 
		{
			GameObject obj = Instantiate (ballModel) as GameObject;

			Ball theBall = obj.GetComponent<Ball>();

			obj.GetComponent<Ball>().setDirection(direction);

			print (theBall);
			//theBall.setDirection(direction);

			obj.transform.parent = transform;
		}

		numberOfBallSent++;

		if (numberOfBallSent >= numberOfBall)
			CancelInvoke ("makeABall");
	}

	Vector2 mousePos()
	{
		Vector2 mouse = Input.mousePosition;
		mouse.x = mouse.x / Screen.width  * 800f - 400f;
		mouse.y = mouse.y / Screen.height * 600f - 300f;

		return mouse;
	}
}
