using UnityEngine;
using System.Collections;

public class ProjectileStop : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D collider)
	{
		Destroy (collider.gameObject);
	}
}
