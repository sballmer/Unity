using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public float autoLoadNextLevelAfter = 0f;

	void Start () 
	{
		if (autoLoadNextLevelAfter <= 0.001f) 
			Debug.Log ("Level auto load disabled");
		else
			Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
	}

	public void LoadLevel(string name)
	{
		Debug.Log ("New Level load: " + name);
		Application.LoadLevel (name);
	}

	public void QuitRequest()
	{
		Debug.Log ("Quit requested");
		Application.Quit ();
	}
	
	public void LoadNextLevel() 
	{
		Application.LoadLevel(Application.loadedLevel + 1);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		print ("Attacker trigger enter");
	}
}
