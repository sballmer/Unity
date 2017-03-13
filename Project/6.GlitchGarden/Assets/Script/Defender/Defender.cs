using UnityEngine;
using System.Collections;

public class Defender : MonoBehaviour 
{
	public int price;

	private Stars star;
	
	void Start()
	{
		star = GameObject.FindObjectOfType<Stars>();
	}
	
	void addStars(int amount)
	{
		star.addStars (amount);
	}
}
