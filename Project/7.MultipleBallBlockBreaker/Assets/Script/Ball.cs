using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	// access to the rigidbody
	private Rigidbody2D myRigidBody;

	// norm of the speed of the ball (default value)
	private const float normSpeedDefault = 1000f;

	// x start pos (change at every game)
	private static float StartingXPosition = 0f;

	// y start pos (never changed)
	private const float StartingYPosition = -289f;

	// number of ball in the game
	private static int BallNumber = 0;

	// start
	void Start()
	{
		BallNumber++;

		// set the positiob
		transform.position = new Vector3 (StartingXPosition, StartingYPosition, 0f);

		// set rigidbody component
		checkRigidBody ();

		// set a standart norm speed
		setNormSpeed(normSpeedDefault);
	}

	// set the ball speed (vector2)
	public void setSpeed(Vector2 speed)
	{
		checkRigidBody ();
		myRigidBody.velocity = speed;
	}

	// set the ball direction without changing it's norm-speed
	public void setDirection(float direction)
	{
		checkRigidBody ();

		Vector2 speed = myRigidBody.velocity;

		if (speed.SqrMagnitude () == 0f) {
			speed.Set(normSpeedDefault * Mathf.Cos (direction), 
			          normSpeedDefault * Mathf.Sin (direction));
		} 
		else 
		{
			float norm = speed.magnitude;

			speed.Set(norm * Mathf.Cos (direction), 
			          norm * Mathf.Sin (direction));
		}

		myRigidBody.velocity = speed;
	}

	// set the ball norm speed without changing it's direction
	public void setNormSpeed(float norm)
	{
		checkRigidBody ();
		Vector2 speed = myRigidBody.velocity;

		if (speed.SqrMagnitude () == 0f) 
		{
			speed.Set (0f, norm);
		}
		else
		{
			speed.Normalize ();
			speed *= norm;
		}

		myRigidBody.velocity = speed;
	}

	// destroy a ball
	public void destroy()
	{
		// decrease amount of ball
		BallNumber--;

		// call back to the ballCreator
		this.GetComponentInParent<BallCreator> ().BallDestroyed ( this.transform.position.x );

		//destroy the gameobject
		Destroy(this.gameObject);
	}

	// getter on the ball number
	public static int getBallNumber()
	{
		return BallNumber;
	}

	// getter on the starting pos
	public static Vector2 GetStartingPos()
	{
		return new Vector2 (StartingXPosition, StartingYPosition);
	}

	// check if rigidbody component reference has been attributed
	void checkRigidBody()
	{
		if (myRigidBody == null)
			myRigidBody = this.GetComponent<Rigidbody2D> ();
	}

	public static void setStartXPos(float xpos)
	{
		StartingXPosition = xpos;
	}
}
