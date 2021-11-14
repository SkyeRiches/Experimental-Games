using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bun : Ingredient
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {

        if (ingredientSteps.Count != 0) {
            nextStep = ingredientSteps[stepInt];
        }


    }

    public override void Generate() {
        stepInt = 0;
        ingredientSteps.Add("Bun Step");
        ingredientSteps.Add("Complete");

    }
}
