using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vegetable : Ingredient
{

    [SerializeField]
    private bool hasLeaves;
    [SerializeField]
    private bool needsChopping;

    

    // Start is called before the first frame update
    void Start() {
        GenerateVegetable();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void GenerateVegetable() {
        if (hasLeaves) {
            ingredientSteps.Add("PeelLeaves");

        }
        if (needsChopping) {
            ingredientSteps.Add("Chop");
        }
    }

}
