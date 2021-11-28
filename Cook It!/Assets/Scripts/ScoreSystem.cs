using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    private int numberOfCasualties;
    private int numberOfLeavers;
    [SerializeField] private float injuryPenalty;
    [SerializeField] private float leaverPenalty;

    private float totalScore;

    public int CasualtyNumber
    {
        get { return numberOfCasualties; }
        set { numberOfCasualties = value; }
    }

    public int LeaverNumber
    {
        get { return numberOfLeavers; }
        set { numberOfLeavers = value; }
    }

    public void IncreaseScore(float a_score)
    {
        totalScore += a_score;
    }

    public void CalculateTotalScore()
    {
        totalScore = totalScore - (numberOfCasualties * injuryPenalty) - (numberOfLeavers * leaverPenalty);
    }
}
