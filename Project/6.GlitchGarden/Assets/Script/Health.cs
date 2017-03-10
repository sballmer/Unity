using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public float startingLife;
	
	private float life;
	
	void Start()
	{
		life = startingLife;
	}
	
	public void ReceiveDammage (float dammage)
	{
		life -= dammage;
		
		if (life <= 0f)
			Destroy (this.gameObject);
	}
}
