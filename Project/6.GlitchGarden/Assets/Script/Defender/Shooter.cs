using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour 
{
	public GameObject projectile, GunPos;

	private GameObject projectileParent;
	private bool isInAttackingState;
	private Animator anim;
	private GameObject laneSpawner;

	// Use this for initialization
	void Start () 
	{
		projectileParent = GameObject.Find ("Projectiles");

		if (projectileParent == null)
			projectileParent = new GameObject("Projectiles");

		anim = GetComponent<Animator> ();

		isInAttackingState = anim.GetBool ("IsAttacking");

		findLaneSpawner ();
	}

	void Update()
	{
		if (isInAttackingState && !isAttackerAheadInLane ()) 
		{
			anim.SetBool ("IsAttacking", false);
			isInAttackingState = false;
		} 
		else if (!isInAttackingState && isAttackerAheadInLane ()) 
		{
			anim.SetBool ("IsAttacking", true);
			isInAttackingState = true;
		}
	}

	void findLaneSpawner()
	{
		AttackerSpawner[] spawner = GameObject.FindObjectsOfType<AttackerSpawner> ();

		foreach (AttackerSpawner child in spawner) 
		{
			if (child.gameObject.transform.position.y == this.transform.position.y)
			{
				laneSpawner = child.gameObject;
				return;
			}
		}

		Debug.Log ("Could not find any lane for the shooter " + name);
	}

	void Fire()
	{
		GameObject obj = Instantiate (projectile) as GameObject;
		obj.transform.position = GunPos.transform.position;
		obj.transform.SetParent (projectileParent.transform);
	}

	bool isAttackerAheadInLane()
	{
		if(laneSpawner.transform.childCount <= 0)
			return false;

		foreach (Transform attackerInLane in laneSpawner.transform)
		{
			if (attackerInLane.position.x > this.transform.position.x)
				return true;
		}

		return false;
	}
}
