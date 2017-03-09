using UnityEngine;
using System.Collections;

public class StartVolume : MonoBehaviour 
{
	private MusicManager musicManager;

	// Use this for initialization
	void Start () 
	{
		musicManager = GameObject.FindObjectOfType<MusicManager> ();

		if (musicManager)
			musicManager.setVolume (PlayerPreferencesManager.Volume.get ());
		else 
			Debug.LogWarning ("No music Manager found");
	}
}
