using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bun : Ingredient
{
    // Update is called once per frame
    public override void Update()
    {
        // update the current step of the bun
        if (ingredientSteps.Count != 0) {
            nextStep = ingredientSteps[stepInt];
        }
    }

    // generate the steps and position of the bun
    public override void Generate() {
        stepInt = 0;
        idealPosition = new Vector3(-0.2f, 1.8f, -9f);
        ingredientSteps.Add("Bun Step");
        ingredientSteps.Add("Complete");
    }
}
