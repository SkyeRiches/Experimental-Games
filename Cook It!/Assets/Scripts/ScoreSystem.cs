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
    private float rating;

    private int numberOfErrors;
    private int numberOfIngredients;

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

    public int ErrorNumber
    {
        get { return numberOfErrors; }
        set { numberOfErrors = value; }
    }
    public int NumberIngredients
    {
        get { return numberOfIngredients; }
        set { numberOfIngredients = value; }
    }

    public void IncreaseScore(float a_score)
    {
        totalScore += a_score;
        gameObject.GetComponent<GUIManager>().Orders++;
        gameObject.GetComponent<GUIManager>().Score = totalScore;
    }

    public void CalculateTotalScore()
    {
        totalScore = totalScore - (numberOfCasualties * injuryPenalty) - (numberOfLeavers * leaverPenalty);
    }

    public void CalculateRating()
    {
        rating = 5 * (1 - (numberOfErrors / numberOfIngredients));
    }
}
