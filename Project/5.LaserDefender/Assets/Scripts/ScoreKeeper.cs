using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour 
{
	static private int score = 0;
	private Text text;

	void Start()
	{
		text = this.GetComponent<Text> ();
		reset ();
	}

	void updateScore()
	{
		text.text = "Score: " + score;
	}

	public void addPoint(int point)
	{
		score += point;
		updateScore ();
	}

	static public int getpoint()
	{
		return score;
	}

	void reset()
	{
		score = 0;
		updateScore ();
	}
}
