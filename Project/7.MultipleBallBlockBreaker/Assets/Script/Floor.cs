using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D collider)
	{
		Ball theBall = collider.gameObject.GetComponent<Ball> ();
		if (theBall)
			theBall.destroy ();
	}
}
