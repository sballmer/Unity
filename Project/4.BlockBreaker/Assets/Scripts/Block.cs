using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour 
{
	public GameObject smoke, littleSmoke;
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

			showSmoke();
			Destroy (gameObject);
		} 
		else 
		{
			showLittleSmoke();
			loadSprite ();
		}
	}

	void showSmoke()
	{
		if (smoke != null) 
		{
			GameObject smokeGenerated = Instantiate (smoke, this.transform.position, Quaternion.identity) as GameObject;
			smokeGenerated.particleSystem.startColor = this.GetComponent<SpriteRenderer> ().color;
		} 
		else
			Debug.LogError ("no smoke particule game object specified for block");
	}

	void showLittleSmoke()
	{
		if (littleSmoke != null) 
		{
			GameObject smokeGenerated = Instantiate (littleSmoke, this.transform.position, Quaternion.identity) as GameObject;
			smokeGenerated.particleSystem.startColor = this.GetComponent<SpriteRenderer> ().color;
		} 
		else
			Debug.LogError ("no smoke particule game object specified for block");
	}

	void loadSprite()
	{
		if (blockSprite [timesHit] != null)
			this.GetComponent<SpriteRenderer> ().sprite = blockSprite [timesHit];
		else
			Debug.LogError ("no sprite specified for the block");
	}
}
