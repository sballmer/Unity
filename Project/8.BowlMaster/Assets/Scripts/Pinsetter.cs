using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pinsetter : MonoBehaviour 
{
    public Text ScoreDisplay;
    private Pin[] allPins;

	// Use this for initialization
	void Start () 
    {
        allPins = GameObject.FindObjectsOfType<Pin>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int CountStanding()
    {
        int counter = 0;

        foreach (Pin singlePins in allPins)
        {
            singlePins.checkActive();

            if (singlePins.isActive)
                counter++;
        }

        ScoreDisplay.text = counter.ToString();

        return counter;
    }
}
