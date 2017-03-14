using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
	public Vector3 launchSpeed;
	public bool inPlay { get; private set;}

	private Rigidbody myRigidBody;
	//private AudioSource myAudioSource;

	// Use this for initialization
	void Start () 
	{
		inPlay = false;
		myRigidBody = GetComponent<Rigidbody> ();
		//myAudioSource = GetComponent<AudioSource> ();

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
		transform.position = new Vector3 (0f, 50f, 30f);
		transform.rotation = Quaternion.identity;
		myRigidBody.velocity = Vector3.zero;
		myRigidBody.angularVelocity = Vector3.zero;
	}
}
