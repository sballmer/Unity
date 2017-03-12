using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour 
{
	public GameObject projectile, GunPos;

	private GameObject projectileParent;

	// Use this for initialization
	void Start () 
	{
		projectileParent = GameObject.Find ("Projectiles");

		if (projectileParent == null)
			projectileParent = new GameObject("Projectiles");
	}

	void Fire()
	{
		GameObject obj = Instantiate (projectile) as GameObject;
		obj.transform.position = GunPos.transform.position;
		obj.transform.SetParent (projectileParent.transform);
	}
}
