using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScore
{
    public enum ScoreDisplay {
        NO_DISPLAY,             // no shot and nothing to display
        FIRST_DISPLAY,          // first shot to display, not a strike
        SECOND_DISPLAY,         // first and second shot to display, not a spar
        WAITING_NEXT_FIRST,     // first and spare to display, don't show total score, waiting for next shot
        DISPLAY_SPARE,          // first and spare and total score to display
        WAITING_NEXT_SECOND,    // strike to display, don't show second and total score, waiting for the 2 next shots
        DISPLAY_STRIKE,         // display strike and total score, (but not the second one..)

       /* third shot special case*/ 
    
        SPARE_WAITING_THIRD,    // first and spare to display, not total score, waiting for end score
        DISPLAY_SPARE_THIRD,    // first, spare, third point and total points to display
        SPARE_STRIKE,           // first, spare and third as strike and total points to display
        STRIKE_WAITING_TWO,     // display first as strike but no total score
        STRIKE_WAITING_ONE,     // display first as strike, second but no total score
        STRIKE_THIRD,           // display first as strike, second, third and total score
        STRIKE_SPARE,           // display first as strike, second, third as spare and total score
        STRIKE_TWICE,           // display first as strike and second as strike, no total score
        STRIKE_TWICE_THIRD,     // display first as strike, second as strike, third and total score
        STRIKE_XXX              // display the 3 shot as strike !!
    };

    public int  firstScore = 0, // first score shot, if strike, 10
                secondScore = 0, // second score shot
                thirdScore = 0, // third score shot
                totalScore = 0; // total score of the level, contain strike and spare bonuses

    // describe what we shall display
    public ScoreDisplay state = ScoreDisplay.NO_DISPLAY;
}

public class ScoreKeeper : MonoBehaviour 
{
    // type declaration
    private enum LevelPhase
    {
        FIRST_SHOT,
        SECOND_SHOT,
        THIRD_SHOT,
        GAME_FINISHED
    };
        
    // public members


    //private members
    private LevelPhase actualPhase;
    private LevelScore[] levelScore = null;
    private int level;
    private Swiper swiper;
    private ScoreDisplayer scoreDisplay;

	// Use this for initialization
	void Start () 
    {
        actualPhase = LevelPhase.FIRST_SHOT;
        level = 1;
        swiper = GameObject.FindObjectOfType<Swiper>();

        levelScore = new LevelScore[10];

        for (int i = 0 ; i < levelScore.Length ; i++)
            levelScore[i] = new LevelScore();

        scoreDisplay = GameObject.FindObjectOfType<ScoreDisplayer>();
	}
        

    public void Scored(int points, bool added = false) // total points of the level
    {
        if (points < 0)
            points = 0;
        if (points > 10)
            points = 10;


        if (!added && actualPhase == LevelPhase.SECOND_SHOT)
            points -= levelScore[levelIndex()].firstScore;
        
        else if (!added && actualPhase == LevelPhase.SECOND_SHOT)
            points -= levelScore[levelIndex()].firstScore + levelScore[levelIndex()].secondScore;


        switch (actualPhase)
        {
            case LevelPhase.FIRST_SHOT:

                // add previous bonuses
                if (!isFirstLevel())
                {
                    if (levelScore[levelBefore()].state == LevelScore.ScoreDisplay.WAITING_NEXT_FIRST)
                    {
                        levelScore[levelBefore()].state = LevelScore.ScoreDisplay.DISPLAY_SPARE;
                        computeTotalScore(levelBefore());
                    }
                }

                if (points < 10)
                {
                    levelScore[levelIndex()].firstScore = points;
                    levelScore[levelIndex()].state = LevelScore.ScoreDisplay.FIRST_DISPLAY;
                    actualPhase = LevelPhase.SECOND_SHOT;
                }
                else
                {
                    if (isLastLevel()) // strike in the last level
                    {
                        levelScore[levelIndex()].firstScore = points;
                        levelScore[levelIndex()].state = LevelScore.ScoreDisplay.STRIKE_WAITING_TWO;
                        actualPhase = LevelPhase.SECOND_SHOT;
                    }
                    else // just a normal strike in the game
                    {
                        levelScore[levelIndex()].firstScore = points;
                        levelScore[levelIndex()].state = LevelScore.ScoreDisplay.WAITING_NEXT_SECOND;
                        actualPhase = LevelPhase.FIRST_SHOT;
                        incrementLevel();
                    }
                }
            break;

            case LevelPhase.SECOND_SHOT:

                // add previous bonus
                if (!isFirstLevel())
                {
                    if (levelScore[levelBefore()].state == LevelScore.ScoreDisplay.WAITING_NEXT_SECOND)
                    {
                        levelScore[levelBefore()].state = LevelScore.ScoreDisplay.DISPLAY_STRIKE;
                        computeTotalScore(levelBefore());
                    }
                }


                int firstPlusSecond = levelScore[levelIndex()].firstScore + levelScore[levelIndex()].secondScore;

                if (firstPlusSecond < 10) // no spare
                {
                    levelScore[levelIndex()].secondScore = points;
                    levelScore[levelIndex()].state = LevelScore.ScoreDisplay.SECOND_DISPLAY;

                    if (isLastLevel())
                        actualPhase = LevelPhase.GAME_FINISHED;
                    else
                    {
                        incrementLevel();
                        actualPhase = LevelPhase.FIRST_SHOT;
                    }

                }
                else // spare !
                {
                    if (isLastLevel())
                    {
                        levelScore[levelIndex()].secondScore = points;
                        levelScore[levelIndex()].state = LevelScore.ScoreDisplay.SPARE_WAITING_THIRD;
                        actualPhase = LevelPhase.THIRD_SHOT;
                    }
                    else
                    {
                        levelScore[levelIndex()].secondScore = points;
                        levelScore[levelIndex()].state = LevelScore.ScoreDisplay.WAITING_NEXT_FIRST;
                        actualPhase = LevelPhase.FIRST_SHOT;
                    }
                }

            break;

            case LevelPhase.THIRD_SHOT:
                levelScore[levelIndex()].thirdScore = points;

                switch (levelScore[levelIndex()].state)
                {
                    case LevelScore.ScoreDisplay.SPARE_WAITING_THIRD:
                        if (points == 10)
                            levelScore[levelIndex()].state = LevelScore.ScoreDisplay.SPARE_STRIKE;
                        else
                            levelScore[levelIndex()].state = LevelScore.ScoreDisplay.DISPLAY_SPARE_THIRD;
                    break;

                    case LevelScore.ScoreDisplay.STRIKE_WAITING_ONE:
                        if (points == 10)
                            levelScore[levelIndex()].state = LevelScore.ScoreDisplay.STRIKE_SPARE;
                        else
                            levelScore[levelIndex()].state = LevelScore.ScoreDisplay.STRIKE_THIRD;
                    break;

                    case LevelScore.ScoreDisplay.STRIKE_TWICE:
                        if (points == 10)
                            levelScore[levelIndex()].state = LevelScore.ScoreDisplay.STRIKE_XXX;
                        else
                            levelScore[levelIndex()].state = LevelScore.ScoreDisplay.STRIKE_TWICE_THIRD;
                    break;
                }
                        
                actualPhase = LevelPhase.GAME_FINISHED;
            break;
        }

        if (!isFirstLevel())
            computeTotalScore(levelBefore());
        
        computeTotalScore();
        refreshTab();

        handleSwiper();
    }

    int levelIndex()
    {
        return level - 1;
    }

    int levelBefore()
    {
        return level - 2;
    }

    bool isFirstLevel()
    {
        return level == 1;
    }

    bool isLastLevel()
    {
        return level == 10;
    }

    void computeTotalScore(int index = -1)
    {
        int level_index = (index == -1 ? levelIndex() : index);

        switch (levelScore[level_index].state)
        {
            case LevelScore.ScoreDisplay.SECOND_DISPLAY:         // first and second shot to display, not a spar
                levelScore[level_index].totalScore = levelScore[level_index].firstScore + levelScore[level_index].secondScore;
            break;

            case LevelScore.ScoreDisplay.DISPLAY_SPARE:          // first and spare and total score to display
                levelScore[level_index].totalScore = /* spare == 10points */ 10 + levelScore[level_index + 1].firstScore;
            break;

            case LevelScore.ScoreDisplay.DISPLAY_STRIKE:         // display strike and total score, (but not the second one..)
                levelScore[level_index].totalScore = /* strike == 10points */ 10 + levelScore[level_index + 1].firstScore +
                                                                                   levelScore[level_index + 1].secondScore;
            break;


            // third shot special case 


            case LevelScore.ScoreDisplay.DISPLAY_SPARE_THIRD:    // first, spare, third point and total points to display
                levelScore[level_index].totalScore = levelScore[level_index].firstScore +
                                                     levelScore[level_index].secondScore +
                                                     2 * levelScore[level_index].thirdScore;
            break;

            case LevelScore.ScoreDisplay.SPARE_STRIKE:           // first, spare and third as strike and total points to display
                levelScore[level_index].totalScore = levelScore[level_index].firstScore +
                                                     levelScore[level_index].secondScore +
                                                     2 * levelScore[level_index].thirdScore;
            break;

            case LevelScore.ScoreDisplay.STRIKE_THIRD:           // display first as strike, second, third and total score
                levelScore[level_index].totalScore = levelScore[level_index].firstScore +
                                                     2 * (levelScore[level_index].secondScore +
                                                          levelScore[level_index].thirdScore);
            break;

            case LevelScore.ScoreDisplay.STRIKE_SPARE:           // display first as strike, second, third as spare and total score
                levelScore[level_index].totalScore = levelScore[level_index].firstScore +
                                                     2 * (levelScore[level_index].secondScore +
                                                          levelScore[level_index].thirdScore);
            break;

            case LevelScore.ScoreDisplay.STRIKE_TWICE_THIRD:     // display first as strike, second as strike, third and total score
                levelScore[level_index].totalScore = levelScore[level_index].firstScore +
                                                     2 * (levelScore[level_index].secondScore +
                                                          levelScore[level_index].thirdScore);
            break;

            case LevelScore.ScoreDisplay.STRIKE_XXX:              // display the 3 shot as strike !!
                levelScore[level_index].totalScore = 50; // ! yeah !
            break;
        }
    }

    void refreshTab()
    {
        int counter = 1;
        int continousScore = 0;

        const ScoreDisplayer.SPECIAL NONE = ScoreDisplayer.SPECIAL.NONE;
        const ScoreDisplayer.SPECIAL SPAR = ScoreDisplayer.SPECIAL.SPAR;
        const ScoreDisplayer.SPECIAL STRIKE = ScoreDisplayer.SPECIAL.STRIKE;

        const ScoreDisplayer.DISPLAYER FIRST = ScoreDisplayer.DISPLAYER.FIRST_SHOT;
        const ScoreDisplayer.DISPLAYER SECOND = ScoreDisplayer.DISPLAYER.SECOND_SHOT;
        const ScoreDisplayer.DISPLAYER THIRD = ScoreDisplayer.DISPLAYER.THIRD_SHOT;
        const ScoreDisplayer.DISPLAYER SUBTOTAL = ScoreDisplayer.DISPLAYER.SUBTOTAL;

        foreach (LevelScore i_score in levelScore)
        {
//                string toPrint = "Level " + counter + ":\n";
//                toPrint += "first shot: ";

//                     if (i_score.state == LevelScore.ScoreDisplay.NO_DISPLAY)            toPrint += NONE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.FIRST_DISPLAY)         toPrint += i_score.firstScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.SECOND_DISPLAY)        toPrint += i_score.firstScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.WAITING_NEXT_FIRST)    toPrint += i_score.firstScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_SPARE)         toPrint += i_score.firstScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.WAITING_NEXT_SECOND)   toPrint += STRIKE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_STRIKE)        toPrint += STRIKE + "\n";
//
//                else if (i_score.state == LevelScore.ScoreDisplay.SPARE_WAITING_THIRD)   toPrint += i_score.firstScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_SPARE_THIRD)   toPrint += i_score.firstScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.SPARE_STRIKE)          toPrint += i_score.firstScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_WAITING_TWO)    toPrint += STRIKE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_WAITING_ONE)    toPrint += STRIKE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_THIRD)          toPrint += STRIKE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_SPARE)          toPrint += STRIKE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_TWICE)          toPrint += STRIKE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_TWICE_THIRD)    toPrint += STRIKE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_XXX)            toPrint += STRIKE + "\n";

                 if (i_score.state == LevelScore.ScoreDisplay.NO_DISPLAY)            scoreDisplay.setScore(counter, NONE, FIRST);
            else if (i_score.state == LevelScore.ScoreDisplay.FIRST_DISPLAY)         scoreDisplay.setScore(counter, i_score.firstScore, FIRST);
            else if (i_score.state == LevelScore.ScoreDisplay.SECOND_DISPLAY)        scoreDisplay.setScore(counter, i_score.firstScore, FIRST);
            else if (i_score.state == LevelScore.ScoreDisplay.WAITING_NEXT_FIRST)    scoreDisplay.setScore(counter, i_score.firstScore, FIRST);
            else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_SPARE)         scoreDisplay.setScore(counter, i_score.firstScore, FIRST);
            else if (i_score.state == LevelScore.ScoreDisplay.WAITING_NEXT_SECOND)   scoreDisplay.setScore(counter, STRIKE, FIRST);
            else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_STRIKE)        scoreDisplay.setScore(counter, STRIKE, FIRST);

            else if (i_score.state == LevelScore.ScoreDisplay.SPARE_WAITING_THIRD)   scoreDisplay.setScore(counter, i_score.firstScore, FIRST);
            else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_SPARE_THIRD)   scoreDisplay.setScore(counter, i_score.firstScore, FIRST);
            else if (i_score.state == LevelScore.ScoreDisplay.SPARE_STRIKE)          scoreDisplay.setScore(counter, i_score.firstScore, FIRST);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_WAITING_TWO)    scoreDisplay.setScore(counter, STRIKE, FIRST);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_WAITING_ONE)    scoreDisplay.setScore(counter, STRIKE, FIRST);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_THIRD)          scoreDisplay.setScore(counter, STRIKE, FIRST);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_SPARE)          scoreDisplay.setScore(counter, STRIKE, FIRST);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_TWICE)          scoreDisplay.setScore(counter, STRIKE, FIRST);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_TWICE_THIRD)    scoreDisplay.setScore(counter, STRIKE, FIRST);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_XXX)            scoreDisplay.setScore(counter, STRIKE, FIRST);

//                toPrint += "second shot: ";
//
//                     if (i_score.state == LevelScore.ScoreDisplay.NO_DISPLAY)            toPrint += NONE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.FIRST_DISPLAY)         toPrint += NONE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.SECOND_DISPLAY)        toPrint += i_score.secondScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.WAITING_NEXT_FIRST)    toPrint += SPAR + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_SPARE)         toPrint += SPAR + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.WAITING_NEXT_SECOND)   toPrint += NONE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_STRIKE)        toPrint += NONE + "\n";
//
//                else if (i_score.state == LevelScore.ScoreDisplay.SPARE_WAITING_THIRD)   toPrint += SPAR + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_SPARE_THIRD)   toPrint += SPAR + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.SPARE_STRIKE)          toPrint += SPAR + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_WAITING_TWO)    toPrint += NONE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_WAITING_ONE)    toPrint += i_score.secondScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_THIRD)          toPrint += i_score.secondScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_SPARE)          toPrint += i_score.secondScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_TWICE)          toPrint += STRIKE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_TWICE_THIRD)    toPrint += STRIKE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_XXX)            toPrint += STRIKE + "\n";

                 if (i_score.state == LevelScore.ScoreDisplay.NO_DISPLAY)            scoreDisplay.setScore(counter, NONE, SECOND);
            else if (i_score.state == LevelScore.ScoreDisplay.FIRST_DISPLAY)         scoreDisplay.setScore(counter, NONE, SECOND);
            else if (i_score.state == LevelScore.ScoreDisplay.SECOND_DISPLAY)        scoreDisplay.setScore(counter, i_score.secondScore, SECOND);
            else if (i_score.state == LevelScore.ScoreDisplay.WAITING_NEXT_FIRST)    scoreDisplay.setScore(counter, SPAR, SECOND);
            else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_SPARE)         scoreDisplay.setScore(counter, SPAR, SECOND);
            else if (i_score.state == LevelScore.ScoreDisplay.WAITING_NEXT_SECOND)   scoreDisplay.setScore(counter, NONE, SECOND);
            else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_STRIKE)        scoreDisplay.setScore(counter, NONE, SECOND);

            else if (i_score.state == LevelScore.ScoreDisplay.SPARE_WAITING_THIRD)   scoreDisplay.setScore(counter, SPAR, SECOND);
            else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_SPARE_THIRD)   scoreDisplay.setScore(counter, SPAR, SECOND);
            else if (i_score.state == LevelScore.ScoreDisplay.SPARE_STRIKE)          scoreDisplay.setScore(counter, SPAR, SECOND);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_WAITING_TWO)    scoreDisplay.setScore(counter, NONE, SECOND);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_WAITING_ONE)    scoreDisplay.setScore(counter, i_score.secondScore, SECOND);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_THIRD)          scoreDisplay.setScore(counter, i_score.secondScore, SECOND);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_SPARE)          scoreDisplay.setScore(counter, i_score.secondScore, SECOND);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_TWICE)          scoreDisplay.setScore(counter, STRIKE, SECOND);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_TWICE_THIRD)    scoreDisplay.setScore(counter, STRIKE, SECOND);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_XXX)            scoreDisplay.setScore(counter, STRIKE, SECOND);

            if (counter == 10) // printing last level
            {
//                    toPrint += "third shot: ";
//
//                         if (i_score.state == LevelScore.ScoreDisplay.NO_DISPLAY)            toPrint += NONE + "\n";
//                    else if (i_score.state == LevelScore.ScoreDisplay.FIRST_DISPLAY)         toPrint += NONE + "\n";
//                    else if (i_score.state == LevelScore.ScoreDisplay.SECOND_DISPLAY)        toPrint += NONE + "\n";
//                    else if (i_score.state == LevelScore.ScoreDisplay.WAITING_NEXT_FIRST)    toPrint += NONE + "\n";
//                    else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_SPARE)         toPrint += NONE + "\n";
//                    else if (i_score.state == LevelScore.ScoreDisplay.WAITING_NEXT_SECOND)   toPrint += NONE + "\n";
//                    else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_STRIKE)        toPrint += NONE + "\n";
//
//                    else if (i_score.state == LevelScore.ScoreDisplay.SPARE_WAITING_THIRD)   toPrint += NONE + "\n";
//                    else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_SPARE_THIRD)   toPrint += i_score.thirdScore + "\n";
//                    else if (i_score.state == LevelScore.ScoreDisplay.SPARE_STRIKE)          toPrint += STRIKE + "\n";
//                    else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_WAITING_TWO)    toPrint += NONE + "\n";
//                    else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_WAITING_ONE)    toPrint += NONE + "\n";
//                    else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_THIRD)          toPrint += i_score.thirdScore + "\n";
//                    else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_SPARE)          toPrint += SPAR + "\n";
//                    else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_TWICE)          toPrint += NONE + "\n";
//                    else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_TWICE_THIRD)    toPrint += i_score.thirdScore + "\n";
//                    else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_XXX)            toPrint += STRIKE + "\n";

                     if (i_score.state == LevelScore.ScoreDisplay.NO_DISPLAY)            scoreDisplay.setScore(counter, NONE, THIRD);
                else if (i_score.state == LevelScore.ScoreDisplay.FIRST_DISPLAY)         scoreDisplay.setScore(counter, NONE, THIRD);
                else if (i_score.state == LevelScore.ScoreDisplay.SECOND_DISPLAY)        scoreDisplay.setScore(counter, NONE, THIRD);
                else if (i_score.state == LevelScore.ScoreDisplay.WAITING_NEXT_FIRST)    scoreDisplay.setScore(counter, NONE, THIRD);
                else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_SPARE)         scoreDisplay.setScore(counter, NONE, THIRD);
                else if (i_score.state == LevelScore.ScoreDisplay.WAITING_NEXT_SECOND)   scoreDisplay.setScore(counter, NONE, THIRD);
                else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_STRIKE)        scoreDisplay.setScore(counter, NONE, THIRD);

                else if (i_score.state == LevelScore.ScoreDisplay.SPARE_WAITING_THIRD)   scoreDisplay.setScore(counter, NONE, THIRD);
                else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_SPARE_THIRD)   scoreDisplay.setScore(counter, i_score.thirdScore, THIRD);
                else if (i_score.state == LevelScore.ScoreDisplay.SPARE_STRIKE)          scoreDisplay.setScore(counter, STRIKE, THIRD);
                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_WAITING_TWO)    scoreDisplay.setScore(counter, NONE, THIRD);
                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_WAITING_ONE)    scoreDisplay.setScore(counter, NONE, THIRD);
                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_THIRD)          scoreDisplay.setScore(counter, i_score.thirdScore, THIRD);
                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_SPARE)          scoreDisplay.setScore(counter, SPAR, THIRD);
                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_TWICE)          scoreDisplay.setScore(counter, NONE, THIRD);
                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_TWICE_THIRD)    scoreDisplay.setScore(counter, i_score.thirdScore, THIRD);
                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_XXX)            scoreDisplay.setScore(counter, STRIKE, THIRD);
            }

//                toPrint += "subtotal score: ";
            continousScore += i_score.totalScore;
//
//                     if (i_score.state == LevelScore.ScoreDisplay.NO_DISPLAY)            toPrint += NONE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.FIRST_DISPLAY)         toPrint += NONE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.SECOND_DISPLAY)        toPrint += continousScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.WAITING_NEXT_FIRST)    toPrint += NONE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_SPARE)         toPrint += continousScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.WAITING_NEXT_SECOND)   toPrint += NONE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_STRIKE)        toPrint += continousScore + "\n";
//
//                else if (i_score.state == LevelScore.ScoreDisplay.SPARE_WAITING_THIRD)   toPrint += NONE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_SPARE_THIRD)   toPrint += continousScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.SPARE_STRIKE)          toPrint += continousScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_WAITING_TWO)    toPrint += NONE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_WAITING_ONE)    toPrint += NONE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_THIRD)          toPrint += continousScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_SPARE)          toPrint += continousScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_TWICE)          toPrint += NONE + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_TWICE_THIRD)    toPrint += continousScore + "\n";
//                else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_XXX)            toPrint += continousScore + "\n";

                 if (i_score.state == LevelScore.ScoreDisplay.NO_DISPLAY)            scoreDisplay.setScore(counter, NONE, SUBTOTAL);
            else if (i_score.state == LevelScore.ScoreDisplay.FIRST_DISPLAY)         scoreDisplay.setScore(counter, NONE, SUBTOTAL);
            else if (i_score.state == LevelScore.ScoreDisplay.SECOND_DISPLAY)        scoreDisplay.setScore(counter, continousScore, SUBTOTAL);
            else if (i_score.state == LevelScore.ScoreDisplay.WAITING_NEXT_FIRST)    scoreDisplay.setScore(counter, NONE, SUBTOTAL);
            else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_SPARE)         scoreDisplay.setScore(counter, continousScore, SUBTOTAL);
            else if (i_score.state == LevelScore.ScoreDisplay.WAITING_NEXT_SECOND)   scoreDisplay.setScore(counter, NONE, SUBTOTAL);
            else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_STRIKE)        scoreDisplay.setScore(counter, continousScore, SUBTOTAL);

            else if (i_score.state == LevelScore.ScoreDisplay.SPARE_WAITING_THIRD)   scoreDisplay.setScore(counter, NONE, SUBTOTAL);
            else if (i_score.state == LevelScore.ScoreDisplay.DISPLAY_SPARE_THIRD)   scoreDisplay.setScore(counter, continousScore, SUBTOTAL);
            else if (i_score.state == LevelScore.ScoreDisplay.SPARE_STRIKE)          scoreDisplay.setScore(counter, continousScore, SUBTOTAL);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_WAITING_TWO)    scoreDisplay.setScore(counter, NONE, SUBTOTAL);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_WAITING_ONE)    scoreDisplay.setScore(counter, NONE, SUBTOTAL);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_THIRD)          scoreDisplay.setScore(counter, continousScore, SUBTOTAL);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_SPARE)          scoreDisplay.setScore(counter, continousScore, SUBTOTAL);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_TWICE)          scoreDisplay.setScore(counter, NONE, SUBTOTAL);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_TWICE_THIRD)    scoreDisplay.setScore(counter, continousScore, SUBTOTAL);
            else if (i_score.state == LevelScore.ScoreDisplay.STRIKE_XXX)            scoreDisplay.setScore(counter, continousScore, SUBTOTAL);

//                toPrint += "\n";
//                print(toPrint);
            counter++;
        }

//            print("Very total score: " + continousScore);
        scoreDisplay.setScore(1 /* dont care about level for total score */, continousScore, ScoreDisplayer.DISPLAYER.TOTAL);
    }

    void handleSwiper()
    {
        if (actualPhase == LevelPhase.FIRST_SHOT) // next shot is going to be the first of its level
            swiper.ResetPins();

        switch (actualPhase)
        {
            case LevelPhase.FIRST_SHOT: // next shot is going to be the first of its level
                swiper.ResetPins();
            break;

            case LevelPhase.SECOND_SHOT: // next shot is going to be the second of its level
                swiper.TidyPins();
            break;

            case LevelPhase.THIRD_SHOT: // next shot is going to be the third of its level
                swiper.TidyPins();
            break;

            case LevelPhase.GAME_FINISHED: // da game is done
            break;
        }

    }

    void incrementLevel()
    {
        computeTotalScore();
        level++;
    }
}
