using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	private static int enemyCounter = 0;

	public float startingLife = 100f;
	public float life { get; private set;}
	public float fireRate = 0.5f;
	public GameObject projectile;
	public int enemyPoint = 10;
	public AudioClip enemyLaserSound;
	public AudioClip enemyExplodeSound;
	public GameObject explosionAnimation;

	private ScoreKeeper score;

	void Start()
	{
		score = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
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
			Dead ();
		}
	}

	void fire()
	{
		Instantiate (projectile, transform.position, Quaternion.identity);
		AudioSource.PlayClipAtPoint (enemyLaserSound, this.transform.position);
	}

	static public int getEnemyNumber()
	{
		return enemyCounter;
	}

	void Dead()
	{
		AudioSource.PlayClipAtPoint (enemyExplodeSound, this.transform.position);

		enemyCounter--;
		
		score.addPoint(enemyPoint);

		Instantiate (explosionAnimation, this.transform.position, Quaternion.identity);
		
		Destroy(this.gameObject);
	}

	static public void reset()
	{
		enemyCounter = 0;
	}
}
