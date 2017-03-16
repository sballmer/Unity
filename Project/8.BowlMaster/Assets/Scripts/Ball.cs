using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
	public Vector3 launchSpeed;
	public bool inPlay { get; private set;}
    public float maxTimeLaunch = 25f; // in seconds
    public float minSpeedRequired = 600f;

	private Rigidbody myRigidBody;
    private Vector3 ballStartPos;
    private PinSetter pinSetter;
	//private AudioSource myAudioSource;

	// Use this for initialization
	void Start () 
	{
		inPlay = false;
		myRigidBody = GetComponent<Rigidbody> ();
		//myAudioSource = GetComponent<AudioSource> ();
        ballStartPos = transform.position;
        pinSetter = GameObject.FindObjectOfType<PinSetter>();

		ResetPosition ();
	}

    void Update()
    {
        if (transform.position.y < 0f)
            ResetPosition ();
    }

	public void defaultLaunch()
	{
		Launch (launchSpeed);
	}

	public void Launch (Vector3 velocity)
	{
        if (nextLaunchAvailable() && velocity.magnitude > minSpeedRequired)
        {
    		inPlay = true;
    		myRigidBody.useGravity = true;
    		myRigidBody.velocity = velocity;
    		//myAudioSource.Play ();
            Invoke("OutOfTimeLaunchBall", maxTimeLaunch);
        }
	}

	public void ResetPosition()
	{
        CancelInvoke("OutOfTimeLaunchBall");
		inPlay = false;
		myRigidBody.useGravity = false;
        transform.position = ballStartPos;
        myRigidBody.velocity = Vector3.zero;
		myRigidBody.angularVelocity = Vector3.zero;
	}

    bool nextLaunchAvailable()
    {
        return !inPlay && !pinSetter.waitForPinsMoving;
    }

    void OutOfTimeLaunchBall()
    {
        ResetPosition();
        pinSetter.checkScore();
    }
}
