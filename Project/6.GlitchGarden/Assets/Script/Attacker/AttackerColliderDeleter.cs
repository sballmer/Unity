using UnityEngine;
using System.Collections;

public class AttackerColliderDeleter : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D collider)
	{
		Attacker enemy = collider.gameObject.GetComponent<Attacker> ();

		if (enemy)
			Destroy(collider.gameObject);
	}
}
