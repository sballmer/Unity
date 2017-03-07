using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour 
{
	public float speed = 10.0f;

	float xmin = -5, xmax = 5;

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
