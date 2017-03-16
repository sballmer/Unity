using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPath : MonoBehaviour 
{
    private ScoreKeeper score;
    private PinSetter pinSetter;

    // Use this for initialization
	void Start () 
    {
        score = GameObject.FindObjectOfType<ScoreKeeper>();
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
	}

    void OnTriggerExit(Collider collide)
    {
        if (!pinSetter.ballIn)
        {
            Ball theBall = collide.GetComponent<Ball>();

            if (theBall)
            {
                theBall.ResetPosition();
                score.Scored(0, true);
            }
        }
    }
}
