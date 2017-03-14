using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour 
{
    public Text ScoreDisplay;
    private Pin[] allPins;
    private Swiper swiper;
    private SideCamera sideCam;
    private CameraManager mainCam;

	// Use this for initialization
	void Start () 
    {
        allPins = GameObject.FindObjectsOfType<Pin>();
        swiper = GameObject.FindObjectOfType<Swiper> ();
        sideCam = GameObject.FindObjectOfType<SideCamera> ();
        mainCam = GameObject.FindObjectOfType<CameraManager> ();
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

	void OnTriggerEnter(Collider collider)
	{
		print ("enter with: " + collider.gameObject.name);
	}

	void OnTriggerExit(Collider collider)
	{
		print ("exit with: " + collider.gameObject.name);

        Ball theBall = collider.GetComponent<Ball> ();
        if (theBall)
        {
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
        //mainCam.ResetCameraPlacement ();
    }
}
