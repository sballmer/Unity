using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayer : MonoBehaviour 
{
    // public type definition
    public enum DISPLAYER {FIRST_SHOT, SECOND_SHOT, THIRD_SHOT, SUBTOTAL, TOTAL};
    public enum SPECIAL {SPAR, STRIKE, NONE};

    // public member
    public Text totalText, thirdShot;

    // private member
    private SingleDisplayer[] allDisplayer;

	// Use this for initialization
	void Start () 
    {
        allDisplayer = GetComponentsInChildren<SingleDisplayer>();

        foreach (SingleDisplayer display in allDisplayer)
        {
            display.first.text = "";
            display.second.text = "";
            display.total.text = "";
        }

        thirdShot.text = "";
	}

    public void setScore(int level, int score, DISPLAYER theDisplay)
    {
//        print("score : " + score + " to " + theDisplay.ToString() + " in level " + level);
        if (level >= 1 && level <= 10)
        {
            if (theDisplay == DISPLAYER.FIRST_SHOT)
                allDisplayer[level - 1].first.text = score.ToString();
            else if (theDisplay == DISPLAYER.SECOND_SHOT)
                allDisplayer[level - 1].second.text = score.ToString();
            else if (theDisplay == DISPLAYER.SUBTOTAL)
                allDisplayer[level - 1].total.text = score.ToString();
            else if (theDisplay == DISPLAYER.THIRD_SHOT)
                thirdShot.text = score.ToString();
            else if (theDisplay == DISPLAYER.TOTAL)
                totalText.text = score.ToString();
        }
    }

    public void setScore(int level, SPECIAL score, DISPLAYER theDisplay)
    {
//        print("score : " + score + " to " + theDisplay.ToString() + " in level " + level);
        string symbole = "";

        if (score == SPECIAL.SPAR)
            symbole = "/";
        else if (score == SPECIAL.STRIKE)
            symbole = "X";
        else if (score == SPECIAL.NONE)
            symbole = " ";


        if (level >= 1 && level <= 10)
        {
            if (theDisplay == DISPLAYER.FIRST_SHOT)
                allDisplayer[level - 1].first.text = symbole;
            else if (theDisplay == DISPLAYER.SECOND_SHOT)
                allDisplayer[level - 1].second.text = symbole;
            else if (theDisplay == DISPLAYER.SUBTOTAL)
                allDisplayer[level - 1].total.text = symbole;
            else if (theDisplay == DISPLAYER.THIRD_SHOT)
                thirdShot.text = symbole;
            else if (theDisplay == DISPLAYER.TOTAL)
                totalText.text = symbole;
        }
    }

    public bool test()
    {

        return true;
    }
}
