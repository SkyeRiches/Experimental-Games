using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private float customerSatisfaction;

    private float[] ingredientScores;
    private int arrayPos = 0;

    private List<float> orderScores;
    float[] orderScoresToArray;

    private int peopleLeaving;
    private int peopleInjured;
    private float lawsuit = 500;
    private float lostCustom = 20;

    #region Getters/Setters
    private int CustomersWalkingOut
    {
        get { return peopleLeaving; }
        set { peopleLeaving = value; }
    }

    private int CustomersBeingInjured
    {
        get { return peopleInjured; }
        set { peopleInjured = value; }
    }

    #endregion

    #region functions
    // Start is called before the first frame update
    void Start()
    {
        ingredientScores = new float[30];
        orderScores = new List<float>();
    }

    private void Update()
    {

    }

    public void CalculateIngredientScore(float a_baseScore, float a_errorMargin)
    {
        float ingredScore = gameObject.GetComponent<ScoreCalculator>().CalculateIngredient(a_baseScore, a_errorMargin);
        ingredientScores[arrayPos] = ingredScore;
        arrayPos++;

    }

    public void CalculateOrderScore(float a_timeTaken, float a_patience)
    {
        float orderScore = gameObject.GetComponent<ScoreCalculator>().CalculateOrder(ingredientScores, a_timeTaken, a_patience);
        arrayPos = 0;

        Debug.Log(orderScore);

        orderScores.Add(orderScore);
    }

    public float CalculateFinalScore()
    {
        orderScoresToArray = orderScores.ToArray();

        float finalScore = gameObject.GetComponent<ScoreCalculator>().FinalScore(orderScoresToArray, lawsuit, lostCustom, peopleInjured, peopleLeaving);
        return finalScore;
    }
    #endregion
}
