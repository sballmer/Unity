﻿using UnityEngine;
using System.Collections;

public class Cheat : MonoBehaviour 
{
	private LevelManager levelManager;

	// Use this for initialization
	void Start () 
	{
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.N))
			levelManager.LoadNextLevel ();
	}
}
