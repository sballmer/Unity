using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	public void LoadLevel (string name)
	{
		Debug.Log ("New level loaded : " + name);
		Block.totalNumberOfBlock = 0;
		Application.LoadLevel (name);
	}

	public void QuitRequest()
	{
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

	public void LoadNextLevel()
	{
		Block.totalNumberOfBlock = 0;
		Application.LoadLevel(Application.loadedLevel + 1);
	}
}
