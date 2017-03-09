using UnityEngine;
using System.Collections;

public class Parameter
{
	private string key;
	private float min, max;
	private float defaultValue;
	private float value;
	
	public Parameter(string _key, float _min, float _max, float _defaultValue, bool setValueToDefault = false)
	{
		key = _key;
		min = _min;
		max = _max;
		defaultValue = _defaultValue;
		
		if (setValueToDefault)
			setToDefault ();
	}
	
	public float get()
	{
		return PlayerPrefs.GetFloat (key, defaultValue);
	}
	
	public void set(float value)
	{
		if (value >= min && value <= max)
			PlayerPrefs.SetFloat (key, value);
		else
			Debug.LogError ("The value is not in the range");
	}
	
	public float getDefault()
	{
		return defaultValue;
	}
	
	public void setToDefault()
	{
		set (defaultValue);
	}
}

public static class PlayerPreferencesManager 
{
	public static Parameter Volume = new Parameter("master_volume", 	// key
	                                               0f,					// min value
	                                               1f,					// max value
	                                               0.75f);				// default value

	public static Parameter Difficulty = new Parameter("difficulty", 	// key
	                                               		1f,				// min value
	                                               		3f,				// max value
	                                               		2f);			// default value
}
