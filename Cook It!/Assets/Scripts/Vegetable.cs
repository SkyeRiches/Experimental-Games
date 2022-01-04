using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vegetable : Ingredient
{
    // what type of vegetable it is will dictate which steps it requires (no need to peel leaves off a tomato)
    [SerializeField]
    private bool hasLeaves;
    [SerializeField]
    private bool needsChopping;

    // data variables
    private int timesChopped = 0;
    private int timesPulled = 0;

    private int bestChops;
    private int bestPulls;

    // getters and setters
    public bool hasLeaf
    {
        get { return hasLeaves; }
    }
    public bool needsChop
    {
        get { return needsChopping; }
    }

    public int chops {
        get { return timesChopped; }
        set { timesChopped = value; }
    }

    public int pulls {
        get { return timesPulled; }
        set { timesPulled = value; }
    }

    public int idealChops {
        get { return bestChops; }
        set { bestChops = value; }
    }

    public int idealPulls {
        get { return bestPulls; }
        set { bestPulls = value; }
    }

    private void Awake() 
    {
        // randomise how many chops the vegetable needs
        bestChops = Random.Range(1, 6);
    }

    // Update is called once per frame
    public override void Update() 
    {
        if (ingredientSteps.Count != 0) 
        {
            // update the active step
            nextStep = ingredientSteps[stepInt];
        }
    }

    public override void Generate() 
    {
        // initialise values
        idealPosition = new Vector3(-0.441f, 1.8f, -2.4f);
        stepInt = 0;

        // add steps based on type of vegetable
        if (hasLeaves) 
        {
            ingredientSteps.Add("PeelLeaves");
            timesPulled = 0;
            bestPulls = Random.Range(1, 6);

        }
        if (needsChopping) 
        {
            ingredientSteps.Add("Chop");
            timesChopped = 0;
            idealChops = Random.Range(1, 6);
        }
        ingredientSteps.Add("Complete");
    }
}
