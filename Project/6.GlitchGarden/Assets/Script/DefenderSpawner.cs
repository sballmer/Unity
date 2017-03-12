using UnityEngine;
using System.Collections;

public class DefenderSpawner : MonoBehaviour 
{
	public Camera myCamera;

	private GameObject defender;

	// Use this for initialization
	void Start () 
	{
		defender = GameObject.Find ("Defenders");

		if (defender == null)
			defender = new GameObject ("Defenders");
	}

	void OnMouseDown()
	{
		if (Button.selectedDefender) 
		{
			GameObject newDefender = Instantiate (Button.selectedDefender, camera2WorldCoordinate (), Quaternion.identity) as GameObject;
			newDefender.transform.SetParent (defender.transform);
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
