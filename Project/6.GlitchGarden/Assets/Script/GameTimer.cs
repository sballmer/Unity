using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTimer : MonoBehaviour 
{
	[Tooltip ("Time the level will take in seconds")]
	public float gameTime = 5f*60f;

	private LevelManager levelManager;
	private Slider slider;

	// Use this for initialization
	void Start () 
	{
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		slider = GetComponent<Slider> ();
	}
	
	// Update is called once per frame
	void Update () 
	{

		float ratio = Time.timeSinceLevelLoad / gameTime;

		slider.value = ratio;

		if (ratio >= 1f)
			levelManager.LoadLevel ("03a Win");
	}
}
