using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		float mousePosX = Input.mousePosition.x; // / Screen.width * 761f;

		this.transform.position = new Vector3 (Mathf.Clamp (mousePosX, 39f, 760f), 
		                                       this.transform.position.y,
		                                       this.transform.position.z);
	}
}
