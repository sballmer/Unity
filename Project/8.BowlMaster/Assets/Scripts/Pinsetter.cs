using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour 
{
    public Text ScoreDisplay;
    public bool ballIn { get; private set; }
    public bool waitForPinsMoving { get ; private set; }

    //private SideCamera sideCam;
    private ScoreKeeper score;

    private int scoreComputed;

	// Use this for initialization
	void Start () 
    {
        ballIn = false;
        waitForPinsMoving = false;
        //sideCam = GameObject.FindObjectOfType<SideCamera> ();
        score = GameObject.FindObjectOfType<ScoreKeeper> ();

        CountStanding();
	}

    void Update()
    {
        if (waitForPinsMoving)
        {
            if (!IsAPinMoving())
                checkScore();
        }
    }

    public int CountStanding()
    {
        int count = 0;

        foreach (Pin singlePins in GameObject.FindObjectsOfType<Pin>())
        {
            singlePins.checkActive();

            if (singlePins.isActive)
                count++;
        }

        scoreComputed = 10 - count;

        ScoreDisplay.text = scoreComputed.ToString();

        return count;
    }

	void OnTriggerEnter(Collider collider)
	{
        Ball theBall = collider.GetComponent<Ball> ();
        if (theBall)
        {
            ballIn = true;
        }
    }

	void OnTriggerExit(Collider collider)
	{
        Ball theBall = collider.GetComponent<Ball> ();
        if (theBall)
        {
            ballIn = false;
            theBall.ResetPosition ();
            waitForPinsMoving = true;
        }
	}

    public void checkScore()
    {
        CountStanding ();
        score.Scored(scoreComputed);
        waitForPinsMoving = false;

        //sideCam.setMainCamera (false);
    }

    bool IsAPinMoving()
    {
        foreach (Pin singlePin in GameObject.FindObjectsOfType<Pin>())
        {
            if (singlePin.isMoving())
                return true;
        }

        return false;
    }
}
