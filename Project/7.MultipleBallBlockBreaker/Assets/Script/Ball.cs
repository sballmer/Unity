using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	private Rigidbody2D myRigidBody;
	private const float normSpeedDefault = 500f;

	private static float StartingPosition = 0f;
	private const float StartingYPosition = -289f;

	private static int BallNumber = 0;

	void Start()
	{
		BallNumber++;

		transform.position = new Vector3 (StartingPosition, StartingYPosition, 0f);
		checkRigidBody ();

		setNormSpeed(normSpeedDefault);
	}

	public void setSpeed(Vector2 speed)
	{
		checkRigidBody ();
		myRigidBody.velocity = speed;
	}

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

	public void destroy()
	{
		BallNumber--;
		Destroy(this.gameObject);
	}

	public int getBallNumber()
	{
		return BallNumber;
	}

	public static Vector2 GetStartingPos()
	{
		return new Vector2 (StartingPosition, StartingYPosition);
	}

	void checkRigidBody()
	{
		if (myRigidBody == null)
			myRigidBody = this.GetComponent<Rigidbody2D> ();
	}
}
