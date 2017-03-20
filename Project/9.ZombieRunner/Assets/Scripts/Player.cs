using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    public GameObject playerSpawPoint;
    public bool respawnButton;

    private Transform[] spawnPos;

	// Use this for initialization
	void Start () 
    {
        respawnButton = false;
        spawnPos = playerSpawPoint.GetComponentsInChildren<Transform>();

        SetPosToARandomStartingPoint();
	}

    void Update()
    {
        if (respawnButton)
        {
            ReSpawn();
            respawnButton = false;
        }
    }

    void SetPosToARandomStartingPoint()
    {
        int index = Random.Range(1, spawnPos.Length); // position 0 make the camera falling to the infinite... 
        transform.position = spawnPos[index].position;
        transform.rotation = spawnPos[index].rotation;
    }

    public void ReSpawn()
    {
        SetPosToARandomStartingPoint();
    }
}
