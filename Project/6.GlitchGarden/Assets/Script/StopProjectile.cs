using UnityEngine;
using System.Collections;

public class StopProjectile : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D collide)
	{
		if (collide.gameObject.GetComponent<Projectile>() )
			Destroy(collide.gameObject);
	}
}
