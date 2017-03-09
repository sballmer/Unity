using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionManager : MonoBehaviour 
{
	public LevelManager levelManager;
	public Slider volumeSlider, difficultySlider;


	private const float defaultVolume = 0.5f, defaultDifficulty = 1f;
	private MusicManager musicManager;

	// Use this for initialization
	void Start () 
	{
		musicManager = GameObject.FindObjectOfType<MusicManager> ();

		// setting stored values
		volumeSlider.value = PlayerPreferencesManager.getMasterVolume ();
		difficultySlider.value = PlayerPreferencesManager.getDifficulty ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (musicManager)
			musicManager.setVolume (volumeSlider.value);
	}

	void saveOptions()
	{
		PlayerPreferencesManager.setMasterVolume (volumeSlider.value);
		PlayerPreferencesManager.setDifficulty (difficultySlider.value);
	}

	public void saveAndExit()
	{
		// save 
		saveOptions ();

		// exit
		levelManager.LoadLevel ("01a Start");
	}

	public void setDefaultValues()
	{
		volumeSlider.value = defaultVolume;
		difficultySlider.value = defaultDifficulty;
	}
}
