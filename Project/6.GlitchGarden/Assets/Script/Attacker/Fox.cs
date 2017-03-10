using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Attacker))]
public class Fox : MonoBehaviour 
{
	private Animator anim;
	private Attacker attacker;

	// Use this for initialization
	void Start () 
	{
		anim = this.GetComponent<Animator> ();
		attacker = this.GetComponent<Attacker> ();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		GameObject collideWith = collider.gameObject;

		if (!collideWith.GetComponent<Defender> ()) 
			return;
		else if (collideWith.GetComponent<Stone> ())
			anim.SetTrigger ("Jump");
		else
		{
			attacker.aggress(collideWith);
			anim.SetBool("IsAttacking", true);
		}
	}
}
