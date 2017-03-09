using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour 
{

	public AudioClip[] levelMusicChangeArray;

	private AudioSource audioSource;

	void Awake() 
	{
		DontDestroyOnLoad (gameObject);
		Debug.Log ("Don't destory on load: " + name);
	}
	
	void Start () 
	{
		audioSource = GetComponent<AudioSource>();
	}
	
	void OnLevelWasLoaded (int level) 
	{
		if (level >= levelMusicChangeArray.Length)
			return;

		AudioClip thisLevelMusic = levelMusicChangeArray[level];
		Debug.Log ("Playing clip: " + thisLevelMusic);
		
		if (thisLevelMusic) 
		{ // If there's some music attached
			audioSource.clip = thisLevelMusic;
			audioSource.loop = true;
			audioSource.Play ();
		}
	}

	public void setVolume(float volume)
	{
		if (volume >= 0f && volume <= 1f)
			audioSource.volume = volume;
		else
			Debug.LogError ("Volume range invalid");
	}
}
