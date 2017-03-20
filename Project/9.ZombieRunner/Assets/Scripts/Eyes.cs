using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour 
{
    private Camera eyesCamera;
    private float defaultFOV;

	// Use this for initialization
	void Start () 
    {
        eyesCamera = GetComponent<Camera> ();
        defaultFOV = eyesCamera.fieldOfView;
	}

    void Update()
    {
        if (Input.GetButton("Zoom"))
            eyesCamera.fieldOfView = defaultFOV / 1.5f;
        else
            eyesCamera.fieldOfView = defaultFOV;
    }
}
