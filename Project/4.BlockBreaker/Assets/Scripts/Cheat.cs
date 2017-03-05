using UnityEngine;
using System.Collections;

public class Cheat : MonoBehaviour 
{
	private LevelManager levelManager;
	private Paddle paddle;

	// Use this for initialization
	void Start () 
	{
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.N)) // N pressed : going to next level
			levelManager.LoadNextLevel ();

		else if (Input.GetKeyDown (KeyCode.A)) // A pressed : entering or quitting autoplay
		{
			loadPaddle();
			paddle.autoPlay = !(paddle.autoPlay);
		}
	}

	void loadPaddle()
	{
		paddle = GameObject.FindObjectOfType<Paddle> ();
	}
}
