using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
	public Vector3 launchSpeed;

	private Rigidbody myRigidBody;
	private AudioSource myAudioSource;

	// Use this for initialization
	void Start () 
	{
		myRigidBody = GetComponent<Rigidbody> ();
		myAudioSource = GetComponent<AudioSource> ();

		Launch ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void Launch ()
	{
		myRigidBody.velocity = launchSpeed;
		myAudioSource.Play ();
	}
}
