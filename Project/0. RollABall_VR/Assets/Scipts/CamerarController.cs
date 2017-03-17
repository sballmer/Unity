using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerarController : MonoBehaviour 
{
    public GameObject player;

    private Vector3 offset;

	// Use this for initialization
	void Start () 
    {
        offset = transform.position - player.transform.position;
	}
	
	// LateUpdate is called once per frame but after the rendering of every object in the scene, one way to make sure the camera capture well everything
	void LateUpdate () 
    {
        transform.position = player.transform.position + offset;
    }
}
