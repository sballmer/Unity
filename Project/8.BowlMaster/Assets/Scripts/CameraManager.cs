using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour 
{
	public float stopAt;
	public GameObject toFollow;

	private Vector3 him2Cam;

	// Use this for initialization
	void Start () 
	{
		if (toFollow)
			him2Cam = this.transform.position - toFollow.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (toFollow && transform.position.z < stopAt) // in front of head pin
			this.transform.position = toFollow.transform.position + him2Cam;
	}
}
