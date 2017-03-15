using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
	public Vector3 launchSpeed;
	public bool inPlay { get; private set;}

	private Rigidbody myRigidBody;
    private Vector3 ballStartPos;
	//private AudioSource myAudioSource;

	// Use this for initialization
	void Start () 
	{
		inPlay = false;
		myRigidBody = GetComponent<Rigidbody> ();
		//myAudioSource = GetComponent<AudioSource> ();
        ballStartPos = transform.position;

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
		ResetPosition ();

		inPlay = true;
		myRigidBody.useGravity = true;
		myRigidBody.velocity = velocity;
		//myAudioSource.Play ();
	}

	public void ResetPosition()
	{
		inPlay = false;
		myRigidBody.useGravity = false;
        transform.position = ballStartPos;
        myRigidBody.velocity = Vector3.zero;
		myRigidBody.angularVelocity = Vector3.zero;
	}
}
