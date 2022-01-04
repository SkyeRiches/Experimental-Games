using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CompletedIngredient : Ingredient // a ingredient that will tell the game to move on to the next object
{
    // Update is called once per frame
    public override void Update() {

        if (ingredientSteps.Count != 0) {

            nextStep = ingredientSteps[stepInt];
        }
    }

    public override void Generate() {
        // the only step required is the 'complete' step that tells the game to move on
        stepInt = 0;
        ingredientSteps.Add("Complete");
    }
}
