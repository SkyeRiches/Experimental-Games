using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Meat : Ingredient {
    // data variables for meat
    private float cook;
    private int temp1;
    private int idealCook;
    private int tenderise;
    private int idealTenderise;
    private float salt;
    private int saltStage;
    private int idealSalt;

    private bool stepIsComplete;

    [SerializeField] private Material[] burgerMaterials = new Material[5];

    // Getters and setters
    public float cooking {
        get { return cook; }
        set { cook = value; }
    }

    public int TempSide1
    {
        get { return temp1; }
        set { temp1 = value; }
    }

    public int IdealCook
    {
        get { return idealCook; }
        set { idealCook = value; }
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
        temp1 = (int)cook;
        tenderiseStage = (int)tenderise;
        saltStage = (int)salt;

        if (ingredientSteps.Count != 0) {
            
            nextStep = ingredientSteps[stepInt];
        }

        // set the material of the burger based on how cooked it is
        switch (temp1)
        {
            case 0:
                // do nothing
                break;

            case 1:
                gameObject.GetComponent<Renderer>().material = burgerMaterials[0];
                break;

            case 2:
                gameObject.GetComponent<Renderer>().material = burgerMaterials[1];
                break;

            case 3:
                gameObject.GetComponent<Renderer>().material = burgerMaterials[2];
                break;

            case 4:
                gameObject.GetComponent<Renderer>().material = burgerMaterials[3];
                break;

            case 5:
                gameObject.GetComponent<Renderer>().material = burgerMaterials[4];
                break;

            default:
                break;
        }
    }

    // generate the meat
    public override void Generate() {
        // initialise valus
        stepInt = 0;
        cook = 0;

        gameObject.GetComponent<Renderer>().material = burgerMaterials[0];
        gameObject.transform.rotation = Quaternion.Euler(new Vector3(-90, 0, 0));

        idealCookPos = new Vector3(1.106f, 1.896f, -2.039f);
        idealPosition = new Vector3(-0.441f, 1.8f, -2.4f);

        idealTenderise = Random.Range(1, 6);
        idealSalt = Random.Range(1, 6);
        idealCook = Random.Range(0, 5);

        // add the steps
        ingredientSteps.Add("Tenderise");
        ingredientSteps.Add("Salt");
        ingredientSteps.Add("Cook");
        ingredientSteps.Add("Complete");
    }

}
