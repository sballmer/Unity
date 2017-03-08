using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LabelScore : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		this.gameObject.GetComponent<Text> ().text = "You scored: " + ScoreKeeper.getpoint ();
	}
}
