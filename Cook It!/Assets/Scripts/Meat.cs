using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Meat : Ingredient {
    private float sideTemp1;
    private int temp1Bucket;

    private float sideTemp2;
    private int temp2Bucket;

    private int tenderise;


    private float salt;
    private int saltStage;

    private bool stepIsComplete;

    public float cookingSideOne {
        get { return sideTemp1; }
        set { sideTemp1 = value; }
    }

    public float cookingSideTwo{
        get { return sideTemp2; }
        set { sideTemp2 = value; }
        }
        

    public int tenderiseStage {
        get { return tenderise; }
        set { tenderise = value; }
    }


    private void Awake() {

        // Generate();

    }

    // Update is called once per frame
    public override void Update() {
        temp1Bucket = (int)sideTemp1;
        temp2Bucket = (int)sideTemp2;
        tenderiseStage = (int)tenderise;
        saltStage = (int)salt;

        if (ingredientSteps.Count != 0) {
            
            nextStep = ingredientSteps[stepInt];
        }
    }

    public override void Generate() {
        stepInt = 0;

        ingredientSteps.Add("Tenderise");
        ingredientSteps.Add("Salt");
        ingredientSteps.Add("Cook");
        ingredientSteps.Add("Complete");
    }

}
