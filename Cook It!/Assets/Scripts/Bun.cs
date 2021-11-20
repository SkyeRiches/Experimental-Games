using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bun : Ingredient
{
    // Update is called once per frame
    public override void Update()
    {
        if (ingredientSteps.Count != 0) {
            nextStep = ingredientSteps[stepInt];
        }
    }

    public override void Generate() {
        stepInt = 0;

        Debug.Log("Called");

        ingredientSteps.Add("Bun Step");
        ingredientSteps.Add("Complete");
    }
}
