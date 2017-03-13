using UnityEngine;
using System.Collections;

public class Stone : MonoBehaviour 
{
	private bool underAttack;
	private GameObject attacker;
	private Animator anim;
	private const string IsAttacked = "IsAttacked";


	void Start()
	{
		underAttack = false;
		attacker = null;
		anim = GetComponent<Animator> ();
		anim.SetBool (IsAttacked, false);
	}

	void Update()
	{
		if (underAttack && attacker == null) 
		{
			underAttack = false;
			anim.SetBool(IsAttacked, false);
		}
	}

	public void beingAttacked(GameObject enemy)
	{
		attacker = enemy;
		underAttack = true;
		anim.SetBool (IsAttacked, true);
	}
}
