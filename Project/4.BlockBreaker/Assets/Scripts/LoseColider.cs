using UnityEngine;
using System.Collections;

public class LoseColider : MonoBehaviour 
{
	public LevelManager levelManager;

	public void OnTriggerEnter2D(Collider2D collider)
	{
		levelManager.LoadLevel ("Lose");
	}
}
