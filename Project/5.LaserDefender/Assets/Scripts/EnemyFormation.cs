using UnityEngine;
using System.Collections;

public class EnemyFormation: MonoBehaviour {

	public GameObject[] enemy_prefab;
	public float width = 5f, height = 5f;
	public float lateralSpeed = 2f;
	public float horizontalStep = 0.5f;

	private bool goingRight = true;
	private float xmin = -5f, xmax = 5f;

	// Use this for initialization
	void Start () 
	{
		// distance from the camera
		float distance = this.transform.position.z - Camera.main.transform.position.z;
		
		// computing xmin and xmax
		xmin = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance)).x + width/2;
		xmax = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, distance)).x - width/2;

		// creating enemy at defined position
		foreach (Transform child in transform) 
		{
			createNewEnemy(0, child);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (goingRight)
			this.transform.position += Vector3.right * lateralSpeed * Time.deltaTime;
		else
			this.transform.position += Vector3.left * lateralSpeed * Time.deltaTime;

		checkGoneTooFar ();
	}

	void createNewEnemy(int index, Transform trans)
	{
		// checking that the index is in the good range : [0, length[ 
		if (index >= enemy_prefab.Length || index < 0)
			return;

		// creating an new instance of the enemy
		GameObject enemy = Instantiate (enemy_prefab [index], trans.position, Quaternion.identity) as GameObject;
		enemy.transform.parent = trans;
	}

	void OnDrawGizmos() 
	{
		Gizmos.DrawWireCube (this.transform.position, new Vector3 (width, height, 0f));
	}

	void checkGoneTooFar()
	{
		if ( goingRight && this.transform.position.x > xmax ||
			!goingRight && this.transform.position.x < xmin)
		{
			goingRight = !goingRight;
			//this.transform.position += Vector3.down * horizontalStep;
		}
	}
}
