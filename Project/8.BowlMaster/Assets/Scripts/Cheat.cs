using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheat : MonoBehaviour 
{
    private Ball theBall;

	// Use this for initialization
	void Start () 
    {
        theBall = GameObject.FindObjectOfType<Ball> ();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            theBall.defaultLaunch();
        }
	}
}
