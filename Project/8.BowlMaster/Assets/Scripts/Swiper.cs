using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swiper : MonoBehaviour 
{
    private PinsGroup pinsGroup;

	// Use this for initialization
	void Start () 
    {
        pinsGroup = GameObject.FindObjectOfType<PinsGroup>();
	}
	
    void makePinsUp()
    {
        pinsGroup.makeAllPinsUpIfHit();
    }

    void ResetPinsGravity()
    {
        pinsGroup.ResetGravityToAllPins();
    }

    void ResetPinsPosition()
    {
        pinsGroup.ResetPositionToAllPins();
    }
}
