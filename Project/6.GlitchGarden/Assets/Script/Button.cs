using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Button : MonoBehaviour 
{
	public GameObject defenderPrefab;
	public static GameObject selectedDefender = null;

	private Button[] allButtons;
	private Stars star;

	private static Color COLOR_DESACTIVATED = Color.black,
						 COLOR_ACTIVATED = Color.white,
						 COLOR_ACTIVATED_NOT_ENOUGH_STARS = new Color (255f, 0f, 0f, 128f);

	void Start()
	{
		allButtons = FindObjectsOfType<Button> ();
		star = GameObject.FindObjectOfType<Stars>();

		GetComponent<SpriteRenderer> ().color = Color.black;

		GetComponentInChildren<Text> ().text = defenderPrefab.GetComponent<Defender> ().price.ToString();
	}

	void Update()
	{
		if (selectedDefender == defenderPrefab)
			ColorizeButton();
	}

	void OnMouseDown()
	{
		foreach (Button element in allButtons) 
		{
			element.GetComponent<SpriteRenderer> ().color = COLOR_DESACTIVATED;
		}

		selectedDefender = defenderPrefab;
		ColorizeButton();
	}

	void ColorizeButton()
	{
		if (selectedDefender) 
		{
			Defender defender = selectedDefender.GetComponent<Defender>();

			if (star.EnoughStarsFor(defender.price) == Stars.Status.ENOUGH_STARS)
				GetComponent<SpriteRenderer> ().color = COLOR_ACTIVATED;
			else
				GetComponent<SpriteRenderer> ().color = COLOR_ACTIVATED_NOT_ENOUGH_STARS;
		}
	}
}
