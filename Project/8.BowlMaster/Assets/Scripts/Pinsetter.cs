using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour 
{
    public Text ScoreDisplay;
    private Swiper swiper;
    private SideCamera sideCam;

    private bool ballInBox = false;

	// Use this for initialization
	void Start () 
    {
        swiper = GameObject.FindObjectOfType<Swiper> ();
        sideCam = GameObject.FindObjectOfType<SideCamera> ();
	}

    public int CountStanding()
    {
        int counter = 0;

        foreach (Pin singlePins in GameObject.FindObjectsOfType<Pin>())
        {
            singlePins.checkActive();

            if (singlePins.isActive)
                counter++;
        }

        ScoreDisplay.text = counter.ToString();

        return counter;
    }

	void OnTriggerEnter(Collider collider)
	{
        Ball theBall = collider.GetComponent<Ball> ();
        if (theBall)
        {
            ballInBox = true;
        }
	}

	void OnTriggerExit(Collider collider)
	{
        Ball theBall = collider.GetComponent<Ball> ();
        if (theBall)
        {
            ballInBox = false;
            theBall.ResetPosition ();
            CountStanding ();
            swiper.TidyPins ();
            sideCam.setMainCamera (true);
            Invoke ("thing", 7f);

            return;
        }
	}

    void thing()
    {
        sideCam.setMainCamera (false);
    }
}
