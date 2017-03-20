using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour 
{
    [Tooltip ("seconds in the game per real seconds, Try 60")]
    public float timeScale;

    private Quaternion startQuaternion;

	// Use this for initialization
	void Start () 
    {
        startQuaternion = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () 
    {
        transform.RotateAround (transform.position, Vector3.forward, Time.deltaTime / 360f * timeScale);
	}

    void restSun()
    {
        transform.rotation = startQuaternion;
    }
}
