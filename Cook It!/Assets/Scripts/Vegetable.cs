using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vegetable : Ingredient
{

    [SerializeField]
    private bool hasLeaves;
    [SerializeField]
    private bool needsChopping;

    private int timesChopped = 0;
    private int timesPulled = 0;

    private int bestChops;
    private int bestPulls;

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


    private void Awake() {

    bestChops = Random.Range(1, 6);

    // Generate();

}

    // Update is called once per frame
    public override void Update() {
        if (ingredientSteps.Count != 0) {
            
            nextStep = ingredientSteps[stepInt];
        }
    }

    public override void Generate() {
        stepInt = 0;

        Debug.Log("Called");

        if (hasLeaves) {
            ingredientSteps.Add("PeelLeaves");
            timesPulled = 0;
            bestPulls = 3;

        }
        if (needsChopping) {
            ingredientSteps.Add("Chop");
            timesChopped = 0;
            idealChops = Random.Range(1, 6);
            // Debug.Log(idealChops);
        }
        ingredientSteps.Add("Complete");
        // Debug.Log("Steps: " + ingredientSteps);
    }

}
