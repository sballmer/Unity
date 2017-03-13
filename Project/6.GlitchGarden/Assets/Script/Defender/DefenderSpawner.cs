using UnityEngine;
using System.Collections;

public class DefenderSpawner : MonoBehaviour 
{
	public Camera myCamera;

	private GameObject defender;
	private Stars star;

	// Use this for initialization
	void Start () 
	{
		star = GameObject.FindObjectOfType<Stars>();
		defender = GameObject.Find ("Defenders");

		if (defender == null)
			defender = new GameObject ("Defenders");
	}

	void OnMouseDown()
	{
		// if there are a selected defender
		if (Button.selectedDefender) 
		{
			// get the defender object from gameobject
			Defender defenderComponent = Button.selectedDefender.GetComponent<Defender>();

			// if we can afford the defender
			if (star.removeStars (defenderComponent.price) == Stars.Status.ENOUGH_STARS) 
			{
				// create the defender
				GameObject newDefender = Instantiate (Button.selectedDefender, camera2WorldCoordinate (), Quaternion.identity) as GameObject;
				newDefender.transform.SetParent (defender.transform);
			}
		}
	}

	Vector3 camera2WorldCoordinate()
	{
		Vector3 weird = Input.mousePosition;
		weird.z = 10f;

		Vector3 ret = myCamera.ScreenToWorldPoint (weird);
		ret.x = Mathf.RoundToInt(ret.x);
		ret.y = Mathf.RoundToInt(ret.y);
		ret.z = 0f;

		return ret;
	}
}
