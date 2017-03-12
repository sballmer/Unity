using UnityEngine;
using System.Collections;

public class Attacker : MonoBehaviour 
{
	[Range (0f, 1f)] 
	public float walkSpeed;
	public float dammage;

	private Rigidbody2D rigidBody;
	private GameObject attackingTarget;
	private Animator anim;
	private const float periodAttack = 1f; // 1 hz
	private bool isAttacking = false;

	// Use this for initialization
	void Start () 
	{
		attackingTarget = null;
		rigidBody = this.gameObject.GetComponent<Rigidbody2D> ();
		anim = this.gameObject.GetComponent<Animator> ();
	}

	void Update()
	{
		if (isAttacking && attackingTarget == null)
		{
			anim.SetBool("IsAttacking", false);
			CancelInvoke("givaDammage");
			isAttacking = false;
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		GameObject fromObj = collider.gameObject;
		Projectile from = fromObj.GetComponent<Projectile> ();

		if (from != null) 
		{
			GetComponent<Health>().ReceiveDammage(from.getDammage() );
			Destroy(fromObj);
		}
	}

	public void setSpeedFactor(float factor)
	{
		rigidBody.velocity = new Vector3 ( -walkSpeed * factor, 0f, 0f);
	}

	public void aggress(GameObject target)
	{
		attackingTarget = target;
		isAttacking = true;
		InvokeRepeating ("giveDammage", 0.0001f, periodAttack);
	}
	
	void giveDammage()
	{
		if (attackingTarget)
		{
			attackingTarget.GetComponent<Health>().ReceiveDammage (dammage);
		}
	}
}
