using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour 
{
	public float stopAt;
    public Ball ball;

	private Vector3 ball2Cam;
    private Vector3 StartingPosition;

	// Use this for initialization
	void Start () 
	{
        StartingPosition = this.transform.position;
        ResetCameraPlacement ();
        print ("camera pos: " + StartingPosition);
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (ball && ball.transform.position.z < stopAt) // in front of head pin
            this.transform.position = ball.transform.position + ball2Cam;
	}

    public void ResetCameraPlacement()
    {
        transform.position = StartingPosition;

        if (ball)
            ball2Cam = this.transform.position - ball.transform.position;
        else
            Debug.LogWarning ("balls is not followed any more");
    }
}
