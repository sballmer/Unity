using UnityEngine;
using System.Collections;

public class NumberWizard : MonoBehaviour 
{

	int max = 1000;
	int min = 0;
	int guess = 0;

	// Use this for initialization
	void Start () 
	{
		max = 1000;
		min = 0;
		guess = 0;
		computeGuess ();
		print ("hello, max is " + max + " min is " + min);
		printStuff ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			min = guess;
			computeGuess();
			printStuff();
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
	    {
			max = guess;
			computeGuess();
			printStuff();
		}

		if (Input.GetKeyDown(KeyCode.Return))
		{
			print ("yeeeah got it !, it was " + guess);
			Start();
		}

		if (max == min) 
		{
			print ("yeeeah got it !, it was " + max);
			Start();
		}
	}

	void computeGuess()
	{
		guess = (max + min) / 2;
	}

	void printStuff()
	{
		print ("up or down than " + guess + " ?");
	}
}
