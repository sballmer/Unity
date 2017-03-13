using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Life : MonoBehaviour 
{
	public int startingLife;

	private LevelManager levelManager;
	private int life;
	private Text text;

	// Use this for initialization
	void Start () 
	{
		life = startingLife;
		text = GetComponent<Text> ();
		levelManager = GameObject.FindObjectOfType<LevelManager> ();
		UpdateText ();
	}
	
	public int getLife()
	{
		return life;
	}

	public void setLife(int newLife)
	{
		if (newLife > 0)
			life = newLife;
		else
			levelManager.LoadLevel("03b Lose");

		UpdateText ();
	}

	public void increaseLife(int amount = 1)
	{
		setLife (life + amount);
	}

	public void decreaseLife(int amount = 1)
	{
		setLife (life - amount);
	}

	void UpdateText()
	{
		text.text = life.ToString ();
	}
}
