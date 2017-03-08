using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour 
{
	public float fadeInTime = 2f; // in sec
	
	private Color currentColor = Color.black;
	private Image fadePanel;

	// Use this for initialization
	void Start () 
	{
		fadePanel = this.GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.timeSinceLevelLoad <= fadeInTime) {
			currentColor.a = 1 - Time.timeSinceLevelLoad / fadeInTime;
			fadePanel.color = currentColor;
		} else {
			this.gameObject.SetActive(false);
		}
	}
}
