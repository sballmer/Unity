using UnityEngine;
using System.Collections;

public class AttackerSpawner : MonoBehaviour 
{
	public GameObject[] attackersPrefab;
	public int numberOfLane;

	private GameObject attackersPlace;

	void Update()
	{
		foreach (GameObject attacker in attackersPrefab) 
		{
			if (isTime2Spawn(attacker))
				Spawn (attacker);
		}
	}

	void Spawn(GameObject myGameObject) 
	{
		GameObject obj = Instantiate (myGameObject, transform.position, Quaternion.identity) as GameObject;
		obj.transform.SetParent (transform);
	}

	bool isTime2Spawn(GameObject attackerObj)
	{
		Attacker attacker = attackerObj.GetComponent<Attacker> ();
		float spawnPerSecond = 1f / attacker.seenPerSecond;
		float threshold = Time.deltaTime * spawnPerSecond / numberOfLane;

		return Random.value < threshold;
	}
}
