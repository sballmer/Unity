using UnityEngine;
using System.Collections;

public class PlayerPreferencesManager : MonoBehaviour 
{
	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFFICULTY_KEY = "difficulty";
	const string LEVEL_KEY = "level_unlocked_";

	public static void setMasterVolume ( float volume)
	{
		if (volume > 0f && volume < 1f)
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);
		else
			Debug.LogError ("Master volume out of range");
	}

	public static float getMasterVolume()
	{
		return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
	}
}
