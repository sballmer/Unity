using UnityEngine;
using System.Collections;

public class Attacker : MonoBehaviour 
{
	[Range (0f, 1f)] 
	public float walkSpeed;

	private Rigidbody2D rigidBody;
	private GameObject attackingTarget;
	private const float periodAttack = 1f; // 1 hz

	// Use this for initialization
	void Start () 
	{
		attackingTarget = null;
		rigidBody = this.gameObject.GetComponent<Rigidbody2D> ();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log ("Trigg" + name);
	}

	void setSpeed(float speed = 0f)
	{
		walkSpeed = speed;
		rigidBody.velocity = new Vector3 ( -walkSpeed, 0f, 0f);
	}

	public void setSpeedFactor(float factor)
	{
		rigidBody.velocity = new Vector3 ( -walkSpeed * factor, 0f, 0f);
	}

	public void StrikeCurrentTarget()
	{
		Debug.Log (name + " strike his current target");
	}

	public void aggress(GameObject target)
	{
		attackingTarget = target;

		// Invoke (attackingTarget, periodAttack); // invoque periodiqualy the dammage, when do you cancel it ?
	}
}
