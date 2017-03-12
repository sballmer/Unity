using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	[Range (0.1f, 5f)]
	public float speed;

	public float dammage;

	void Start()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector3 (speed, 0f, 0f);
	}

	public float getDammage()
	{
		return dammage;
	}
}
