using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour 
{
	public GameObject projectile, projectileParent, GunPos;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void Fire()
	{
		GameObject obj = Instantiate (projectile) as GameObject;
		obj.transform.position = GunPos.transform.position;
		obj.transform.SetParent (projectileParent.transform);
	}
}
