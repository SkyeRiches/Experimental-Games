using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Meat : Ingredient {
    private float sideTemp1;
    private int temp1Bucket;

    private float sideTemp2;
    private int temp2Bucket;

    private float tenderise;
    private int tenderiseStage;

    private float salt;
    private int saltStage;

    private bool stepIsComplete;

    // Start is called before the first frame update
    void Start() {
        GenerateMeat();
    }

    // Update is called once per frame
    void Update() {
        temp1Bucket = (int)sideTemp1;
        temp2Bucket = (int)sideTemp2;
        tenderiseStage = (int)tenderise;
        saltStage = (int)salt;

        if (stepIsComplete) {

        }
    }

    void GenerateMeat() {
        ingredientSteps.Add("Tenderise");
        ingredientSteps.Add("Salt");
        ingredientSteps.Add("Cook");
        ingredientSteps.Add("Complete");
    }

}
