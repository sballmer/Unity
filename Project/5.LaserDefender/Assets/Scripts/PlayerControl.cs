using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
	public float speed = 10.0f;

	private float xmin = -5, xmax = 5;

	void Start()
	{
		// sprite
		Sprite currentSprite = this.GetComponent<SpriteRenderer> ().sprite;

		// distance from the camera
		float distance = this.transform.position.z - Camera.main.transform.position.z;

		// computing xmin and xmax
		xmin = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance)).x - currentSprite.bounds.min.x;
		xmax = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, distance)).x - currentSprite.bounds.max.x;

		/*
		 * Camera.main.ViewPortToWorldPoint convert a "camera point" to "world point". 
		 * 0,0 is at bottom left and 1,1 top right. z need to be equal to the distance between the object and the camera.
		 */ 
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) 
		{
			// adding offset
			Vector3 pos = this.transform.position + Vector3.left * speed * Time.deltaTime;

			// clamping to the screen
			pos.x = Mathf.Clamp(pos.x, xmin, xmax);

			// setting position
			this.transform.position = pos;
		}
		else if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) 
		{
			// adding offset
			Vector3 pos = this.transform.position + Vector3.right * speed * Time.deltaTime;
			
			// clamping to the screen
			pos.x = Mathf.Clamp(pos.x, xmin, xmax);
			
			// setting position
			this.transform.position = pos;
		}
	}
}
