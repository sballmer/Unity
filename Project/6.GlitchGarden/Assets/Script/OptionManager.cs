using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionManager : MonoBehaviour 
{
	public LevelManager levelManager;
	public Slider volumeSlider, difficultySlider;

	
	private MusicManager musicManager;

	// Use this for initialization
	void Start () 
	{
		musicManager = GameObject.FindObjectOfType<MusicManager> ();

		// setting stored values
		volumeSlider.value = PlayerPreferencesManager.Volume.get();
		difficultySlider.value = PlayerPreferencesManager.Difficulty.get();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (musicManager)
			musicManager.setVolume (volumeSlider.value);
	}

	void saveOptions()
	{
		PlayerPreferencesManager.Volume.set (volumeSlider.value);
		PlayerPreferencesManager.Difficulty.set (difficultySlider.value);
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
		volumeSlider.value = PlayerPreferencesManager.Volume.getDefault();
		difficultySlider.value = PlayerPreferencesManager.Difficulty.getDefault();
	}
}
