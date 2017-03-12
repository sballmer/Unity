using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour 
{
	public GameObject defenderPrefab;
	public static GameObject selectedDefender = null;

	private Button[] allButtons;

	void Start()
	{
		allButtons = FindObjectsOfType<Button> ();

		GetComponent<SpriteRenderer> ().color = Color.black;
	}

	void OnMouseDown()
	{
		foreach (Button element in allButtons) 
		{
			element.GetComponent<SpriteRenderer> ().color = Color.black;
		}

		GetComponent<SpriteRenderer> ().color = Color.white;
		selectedDefender = defenderPrefab;
	}
}
