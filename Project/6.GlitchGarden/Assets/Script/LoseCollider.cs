using UnityEngine;
using System.Collections;

public class LoseCollider : MonoBehaviour 
{
	public int life = 10;
	private Life lifeManager;

	void Start()
	{
		lifeManager = GameObject.FindObjectOfType<Life> ();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		Attacker enemy = collider.gameObject.GetComponent<Attacker> ();

		if (enemy)
			lifeManager.decreaseLife ();
	}
}
