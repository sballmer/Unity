using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Brick : MonoBehaviour 
{
	private int life = 10;
	private Text text;

	// Use this for initialization
	void Start () 
	{
		text = GetComponentInChildren<Text> ();
		UpdateText ();
	}
	
	void UpdateText()
	{
		text.text = life.ToString ();
	}

	public void setLife(int newlife)
	{
		if (newlife > 0) {
			life = newlife;
			UpdateText ();
		} 
		else if (newlife == 0)
			Dead ();
		else
			Debug.LogError("life need to be >= 0");
		
	}

	public int getLife()
	{
		return life;
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.GetComponent<Ball> ())
			gotHit ();
	}

	void gotHit()
	{
		life--;

		if (life > 0)
			UpdateText ();
		else 
			Invoke ("Dead", 0.1f);
	}

	void Dead()
	{
		Destroy (this.gameObject);
	}
}
