using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Stars : MonoBehaviour 
{
	public enum Status {ENOUGH_STARS, NOT_ENOUGH_STARS};
	public int startingStarsAmount;
	private int starsNumber = 0;
	private Text theText;

	// Use this for initialization
	void Start () 
	{
		theText = GetComponent<Text> ();
		starsNumber = startingStarsAmount;
		UpdateStarText ();
	}
	
	public int getStarsNumber()
	{
		return starsNumber;
	}

	public void setStarsNumber(int newStarNumber)
	{
		starsNumber = newStarNumber;
		checkStarsNumber ();
		UpdateStarText ();
	}

	public void addStars(int amount)
	{
		starsNumber += amount;
		UpdateStarText ();
	}

	public Status removeStars(int amount)
	{
		if (EnoughStarsFor (amount) == Status.ENOUGH_STARS) 
		{
			starsNumber -= amount;
			UpdateStarText ();
			return Status.ENOUGH_STARS;
		} 
		else
			return Status.NOT_ENOUGH_STARS;
	}

	public Status EnoughStarsFor(int amount)
	{
		if (starsNumber - amount >= 0) 
		{
			return Status.ENOUGH_STARS;
		}
		else 
		{
			return Status.NOT_ENOUGH_STARS;
		}
	}

	void UpdateStarText()
	{
		theText.text = starsNumber.ToString ();
	}

	Status checkStarsNumber()
	{
		if (EnoughStarsFor(0) == Status.NOT_ENOUGH_STARS) 
		{
			starsNumber = 0;
			return Status.NOT_ENOUGH_STARS;
		} 
		else
			return Status.ENOUGH_STARS;
	}
}
