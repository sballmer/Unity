using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Brick : MonoBehaviour 
{
	// life of the brick
	private int life = 10;

	// the UI TEXT
	private Text text;

	// Use this for initialization
	void Start () 
	{
		// get and update text
		UpdateText ();
	}

	// update the text
	void UpdateText()
	{
		// check if the text has been set
		checkTextComponent ();

		// update display
		text.text = life.ToString ();
	}

	// set the life of the brick
	public void setLife(int newlife)
	{
		// life is unsigned
		if (newlife > 0) 
		{
			life = newlife;
			UpdateText ();
		} 

		// dead condition
		else if (newlife == 0)
			Dead ();

		else
			Debug.LogError("life need to be >= 0");
		
	}

	// getter on the life
	public int getLife()
	{
		return life;
	}

	// set the position of the brick
	public void setPos(float x, float y)
	{
		this.transform.position = new Vector3 (x, y, 0f);
	}

	// collision call back
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<Ball> ())
			gotHit ();
	}

	// when the brick got hit by a ball
	void gotHit()
	{
		// decrease life
		life--;

		// check dead condition
		if (life > 0)
			UpdateText ();
		else 
			Dead ();
	}

	// destructor of the gameobject
	void Dead()
	{
		Destroy (this.gameObject);
	}

	// check if the text has been settled, and set it otherwise
	void checkTextComponent()
	{
		if (text == null)
			text = GetComponentInChildren<Text> ();
	}
}
