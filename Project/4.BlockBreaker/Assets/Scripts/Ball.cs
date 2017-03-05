using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	private Paddle paddle;

	private Vector3 paddle2Ball;
	private bool onPaddle;

	private const float angleDeg = 60f; //Trigonometric, in degree
	private const float speedNorm = 500f;

	// Use this for initialization
	void Start () 
	{
		paddle = GameObject.FindObjectOfType<Paddle> ();
		onPaddle = true;
		paddle2Ball = this.transform.position - paddle.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (onPaddle) 
		{
			this.transform.position = paddle.transform.position + paddle2Ball;

			if (Input.GetMouseButtonDown(0)) // left click
			{
				onPaddle = false;
				this.rigidbody2D.velocity = new Vector3(speedNorm * Mathf.Cos(Mathf.Deg2Rad * angleDeg), 
				                                        speedNorm * Mathf.Sin(Mathf.Deg2Rad * angleDeg), 
				                                        0f);
			}
		}
	}
}
