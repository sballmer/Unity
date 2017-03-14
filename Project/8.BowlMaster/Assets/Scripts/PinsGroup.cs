using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinsGroup : MonoBehaviour 
{
    private Pin[] allPin;

	// Use this for initialization
	void Start () 
    {
        allPin = this.GetComponentsInChildren<Pin>();
	}

    public void makeAllPinsUpIfHit()
    {
        foreach (Pin singlePin in allPin)
        {
            singlePin.SetPinUp();
        }
    }

    public void ResetGravityToAllPins()
    {
        foreach (Pin singlePin in allPin)
        {
            singlePin.ResetGravity();
        }
    }

    public void ResetPositionToAllPins()
    {
        print("spawing pins again");
    }
}
