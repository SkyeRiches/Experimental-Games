using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Meat : Ingredient {
    private float sideTemp1;
    private int temp1;
    private int idealTemp1;

    private float sideTemp2;
    private int temp2;
    private int idealTemp2;

    private int tenderise;
    private int idealTenderise;

    private float salt;
    private int saltStage;
    private int idealSalt;

    private bool stepIsComplete;

    public float cookingSideOne {
        get { return sideTemp1; }
        set { sideTemp1 = value; }
    }

    public float cookingSideTwo{
        get { return sideTemp2; }
        set { sideTemp2 = value; }
    }

    public int TempSide1
    {
        get { return temp1; }
        set { temp1 = value; }
    }
    public int TempSide2
    {
        get { return temp2; }
        set { temp2 = value; }
    }

    public int IdealTempSide1
    {
        get { return idealTemp1; }
        set { idealTemp1 = value; }
    }
    public int IdealTempSide2
    {
        get { return idealTemp2; }
        set { idealTemp2 = value; }
    }


    public int tenderiseStage {
        get { return tenderise; }
        set { tenderise = value; }
    }

    public int IdealTenderisation
    {
        get { return idealTenderise; }
        set { idealTenderise = value; }
    }

    public int SaltAmount
    {
        get { return saltStage; }
        set { saltStage = value; }
    }

    public int IdealSalt
    {
        get { return idealSalt; }
        set { idealSalt = value; }
    }

    // Update is called once per frame
    public override void Update() {
        temp1 = (int)sideTemp1;
        temp2 = (int)sideTemp2;
        tenderiseStage = (int)tenderise;
        saltStage = (int)salt;

        if (ingredientSteps.Count != 0) {
            
            nextStep = ingredientSteps[stepInt];
        }
    }

    public override void Generate() {
        stepInt = 0;
        sideTemp1 = 0;
        sideTemp2 = 0;

        idealTenderise = Random.Range(1, 6);
        idealSalt = Random.Range(1, 6);
        idealTemp1 = Random.Range(0, 10);
        idealTemp2 = Random.Range(0, 10);

        ingredientSteps.Add("Tenderise");
        ingredientSteps.Add("Salt");
        ingredientSteps.Add("Cook");
        ingredientSteps.Add("Complete");
    }

}
