using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private int numberOfCasualties;
    private int numberOfLeavers;
    private float injuryPenalty;
    private float leaverPenalty;

    private float totalScore;

    public void IncreaseScore(float a_score)
    {
        totalScore += a_score;
    }

    public void CalculateTotalScore()
    {
        totalScore = totalScore - (numberOfCasualties * injuryPenalty) - (numberOfLeavers * leaverPenalty);
    }
}
