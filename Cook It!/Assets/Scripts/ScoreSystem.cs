using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class responsible for calculating and storing the player's score
/// </summary>
public class ScoreSystem : MonoBehaviour
{
    private int numberOfLeavers;
    [SerializeField] private float leaverPenalty;

    private float totalScore;
    private float rating;

    private int numberOfErrors;
    private int numberOfIngredients;

    #region Getters/Setters
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
    #endregion

    // Increments the score by the passed value, and updates the score UI
    public void IncreaseScore(float a_score)
    {
        totalScore += a_score;
        
        gameObject.GetComponent<GUIManager>().Score = totalScore;
    }

    // At the end of the game, this calculates the final score by removing any penalties the player has
    public void CalculateTotalScore()
    {
        totalScore = totalScore - (numberOfLeavers * leaverPenalty);

        PlayerPrefs.SetFloat("Score", totalScore);
        PlayerPrefs.SetInt("Leavers", numberOfLeavers);
    }
}
