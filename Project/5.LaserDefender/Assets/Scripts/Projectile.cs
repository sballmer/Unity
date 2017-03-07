using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public float speed = 15f;
	public float dammage = 100f;

	// Use this for initialization
	void Start () 
	{
		this.rigidbody2D.velocity = new Vector2(0f, speed);
	}

	public float getDamageAmount()
	{
		return dammage;
	}

	public void hit()
	{
		Destroy (this.gameObject);
	}
}
