using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideCamera : MonoBehaviour 
{
    private Camera thisCam;
    private Camera mainCam;

	// Use this for initialization
	void Start ()
    {
        thisCam = GetComponent<Camera> ();
        mainCam = GameObject.FindObjectOfType<CameraManager> ().GetComponent<Camera> ();

        setMainCamera (false);
	}
	
    public void setMainCamera(bool trigger)
    {
        if (trigger)
        {
            mainCam.enabled = false;
            thisCam.enabled = true;
        }
        else
        {
            thisCam.enabled = false;
            mainCam.enabled = true;
        }
    }
}
