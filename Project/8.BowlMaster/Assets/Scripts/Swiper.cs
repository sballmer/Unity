using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swiper : MonoBehaviour 
{
    private PinsGroup pinsGroup;
    private Animator anim;

	// Use this for initialization
	void Start () 
    {
        pinsGroup = GameObject.FindObjectOfType<PinsGroup>();
        anim = GetComponent<Animator> ();
	}
	
    void makePinsUp()
    {
        pinsGroup.makeAllPinsUpIfHit();
    }

    void ResetPinsGravity()
    {
        pinsGroup.ResetGravityToAllPins();
    }

    public void ResetPinsPosition()
    {
        print ("jkdsfbjfh");
        pinsGroup.ResetPositionToAllPins();
    }

    public void TidyPins()
    {
        anim.SetTrigger ("TidyPins");
    }

    public void ResetPins()
    {
        anim.SetTrigger ("ResetPins");
    }
}
