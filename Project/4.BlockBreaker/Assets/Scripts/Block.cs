using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour 
{
	public static int totalNumberOfBlock = 0;
	public Sprite[] blockSprite;

	private LevelManager levelManager;
	private int timesHit;
	private bool isBreakable;

	// Use this for initialization
	void Start () 
	{
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		timesHit = 0;
		isBreakable = (this.tag != "Unbreakable");

		if (isBreakable)
			totalNumberOfBlock++;

		loadSprite ();
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		timesHit++;

		if (timesHit >= blockSprite.Length && isBreakable) {
			totalNumberOfBlock--;
			if (totalNumberOfBlock <= 0) 
			{
				levelManager.LoadNextLevel ();
			}

			Destroy (gameObject);
		} 
		else 
		{
			loadSprite ();
		}
	}

	void loadSprite()
	{
		this.GetComponent<SpriteRenderer> ().sprite = blockSprite [timesHit];
	}
}
