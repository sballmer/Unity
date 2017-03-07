﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	private static int enemyCounter = 0;

	public float startingLife = 100f;
	public float life { get; private set;}
	public float fireRate = 0.5f;
	public GameObject projectile;

	void Start()
	{
		life = startingLife;
		enemyCounter++;
	}

	void Update()
	{
		if (Random.value < (Time.deltaTime * fireRate))
			fire ();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		Projectile laser = collider.gameObject.GetComponent<Projectile> ();

		if (laser) // a projectile object collide the enemy
		{
			takeDommage(laser.getDamageAmount() );
			laser.hit();
		}
	}

	void takeDommage(float dommage)
	{
		life -= dommage;

		if (life <= 0f) 
		{
			enemyCounter--;

			if (enemyCounter <= 0)
				GameObject.FindObjectOfType<LevelManager>().LoadLevel("Win Screen");

			Destroy(this.gameObject);
		}
	}

	void fire()
	{
		Instantiate (projectile, transform.position, Quaternion.identity);
	}
}
