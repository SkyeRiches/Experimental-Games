using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    private float totalOrderScore;
    private float orderScore;

    private void Start()
    {
        totalOrderScore = 0;
    }

    public float CalculateIngredient(float a_baseScore, float a_errorMargin)
    {
        float ingredScore = 0;
        ingredScore = a_baseScore * (1 - (a_errorMargin * 0.2f));

        return ingredScore;
    }

    public float CalculateOrder(float[] a_ingredientScores, float a_timeTaken)
    {
        for (int i = 0; i < a_ingredientScores.Length; i++)
        {
            orderScore += a_ingredientScores[i];
        }

        orderScore = orderScore / a_timeTaken;

        return orderScore;
    }

    public float FinalScore(float[] a_orderScores, float a_lawsuitAmount, float a_lostMoney, float a_casualtyTotal, float a_leaverTotal)
    {
        float finalScore = 0;
        

        for (int i = 0; i < a_orderScores.Length; i++)
        {
            totalOrderScore += a_orderScores[i];
        }

        finalScore = totalOrderScore;
        finalScore -= a_casualtyTotal * a_lawsuitAmount;
        finalScore -= a_leaverTotal * a_lostMoney;

        return finalScore;
    }
}
