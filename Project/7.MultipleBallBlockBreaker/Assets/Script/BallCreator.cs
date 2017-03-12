using UnityEngine;
using System.Collections;

public class BallCreator : MonoBehaviour 
{
	// the ball modele that is created from
	public GameObject ballModel;

	// the number of ball to create every launch (increase during the game)
	private int numberOfBall;

	// the actual number of ball sent during launching
	private int numberOfBallSent;

	// one ball is created every ... during launching
	private const float dtBallLauncher = 0.1f;

	// direction (changed with the mouse before launching)
	private float direction;

	// if we are in the game (when there are balls in the game)
	private bool inGame = false;

	// if the first ball launched during the game has already been destroyed
	private bool firstBallHasBeenDestroyed = false;

	// brick creator access
	public BrickCreator brickCreator;

	// access to ballDisplay
	public BallDisplay ballDisplay;

	// Use this for initialization
	void Start () 
	{
		direction = 0f;
		numberOfBall = 1;
		numberOfBallSent = numberOfBall;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (inGame) 
		{
			// check if there still are some balls in the game
			if (Ball.getBallNumber() == 0)
			{
				inGame = false;
				brickCreator.nextLevel();
				numberOfBall = brickCreator.getLevel();
			}
		}
		else
		{
			// check if the user click to launch the game
			if (Input.GetMouseButtonDown (0))
				LaunchBall ();

			// set the direction from the ball start point to the mouse
			Vector2 ball2mouse = mousePos() - Ball.GetStartingPos();
			direction = Mathf.Atan2(ball2mouse.y, ball2mouse.x);
		}
	}

	// launching all the balls
	void LaunchBall()
	{
		// init stuff
		numberOfBallSent = 0;
		inGame = true;
		firstBallHasBeenDestroyed = false;

		// call recursively makeABall
		makeABall ();
		InvokeRepeating ("makeABall", dtBallLauncher, dtBallLauncher);
	}

	void makeABall()
	{
		// stop condition of the repeatingInvoke
		if (numberOfBallSent >= numberOfBall) 
		{
			CancelInvoke ("makeABall");
		} 
		else 
		{
			// create the ball
			if (ballModel) {
				// create a ball
				GameObject obj = Instantiate (ballModel) as GameObject;

				// set the ball direction
				Ball theBall = obj.GetComponent<Ball> ();
				theBall.setDirection (direction);

				// set the ball the be a child of this.
				obj.transform.SetParent (this.transform);
			}

			// one ball more sent, increment the counter !
			numberOfBallSent++;
		}
	}

	// get the mousePos coordinate in the canvas frame
	Vector2 mousePos()
	{
		Vector2 mouse = Input.mousePosition;
		mouse.x = mouse.x / Screen.width  * 800f - 400f;
		mouse.y = mouse.y / Screen.height * 600f - 300f;

		return mouse;
	}

	public void BallDestroyed(float xpos)
	{
		if (!firstBallHasBeenDestroyed)
		{
			// set trigger to true
			firstBallHasBeenDestroyed = true;

			// set the new ball start pos
			Ball.setStartXPos(xpos);

			// set new start pos
			ballDisplay.setPos(xpos);
		}
	}
}
