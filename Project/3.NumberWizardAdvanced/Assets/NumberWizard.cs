using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NumberWizard : MonoBehaviour 
{
	public Text text;

	int max = 1000;
	int min = 0;
	int guess = 0;

	int maxNumberOfGuess = 10;

	// Use this for initialization
	void Start () 
	{
		max = 1000;
		min = 0;
		guess = max / 2;
		maxNumberOfGuess = 10;

		text.text = "Is it the number " + guess + " ?\n" + maxNumberOfGuess + " tries left.";
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			GuessHigher();
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
	    {
			GuessLower ();
		}

		if (Input.GetKeyDown(KeyCode.Return))
		{
			GuessEqual();
		}

		if (max == min) 
		{
			GuessEqual();
		}
		*/
	}

	public void GuessHigher()
	{
		min = guess;
		computeGuess();
	}

	public void GuessLower()
	{
		max = guess;
		computeGuess();
	}

	public void GuessEqual()
	{
		Application.LoadLevel ("lose");
	}

	void computeGuess()
	{
		guess = (max + min) / 2;
		maxNumberOfGuess--;

		if (maxNumberOfGuess <= 0) 
		{
			Start();
			Application.LoadLevel("Win");
		}

		text.text = "Is it the number " + guess + " ?\n" + maxNumberOfGuess + " tries left.";
	}
}
