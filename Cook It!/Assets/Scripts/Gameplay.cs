using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    
    [SerializeField]
    private GameObject customerPrefab; // a customer
    private int customersServed2; // number of customers served
    private string currentState;

    private CustomerControl customer;
    GameObject newCustomer;

    private bool isCompleted;

    private Ingredient currentIngredient;

    [SerializeField]
    private GameObject gameplayManager;

    private bool meatOnSideOne;

    #region Getters/Setters
    public int customersServed {
        get { return customersServed2; }
        set { customersServed2 = value; }
    }

    public Ingredient ingredient 
    {
        get { return currentIngredient; }
        set { currentIngredient = value; }
    }

    public string gameplayState 
    {
        get { return currentState; }
        set { currentState = value; }
    }

    public CustomerControl gameplayCustomer 
    {
        get { return customer; }
        set { customer = value; }
    }

    public string step 
    {
        get { return currentState; }
        set { currentState = value; }
    }
    #endregion

    void Start() 
    {
        customersServed = 0;
        isCompleted = false;
    }

    public void cookIngredient(string currentState) 
    {

        if ((Input.GetKeyDown(KeyCode.T))) 
        {
            customer.currentIngredient.stepTracker++;
            isCompleted = false;
            
        } 
        else 
        {
            switch (currentState) 
            {
                case "PeelLeaves":
                    gameplayManager.GetComponent<CameraPos>().IsPrepping = true;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    PeelLeaves((Vegetable)currentIngredient);
                    break;
                case "Chop":
                    gameplayManager.GetComponent<CameraPos>().IsPrepping = true;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    Chop((Vegetable)currentIngredient);
                    break;
                case "Tenderise":
                    gameplayManager.GetComponent<CameraPos>().IsPrepping = true;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    Tenderise((Meat)currentIngredient);
                    break;
                case "Salt":
                    gameplayManager.GetComponent<CameraPos>().IsPrepping = true;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    Salt();
                    break;
                case "Cook":
                    gameplayManager.GetComponent<CameraPos>().IsPrepping = false;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = true;
                    Cook((Meat)currentIngredient);
                    break;
                case "Bun Step":
                    gameplayManager.GetComponent<CameraPos>().IsPrepping = true;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    BunStep();
                    break;
                case "Complete":
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    Complete();
                    break;
            }
        }
    }

    public void PeelLeaves(Vegetable currentIngredient) 
    {
        if (Input.GetKeyDown(KeyCode.Y)) 
        {
            currentIngredient.pulls++;
            // do the animation
        }
    }

    public void Chop(Vegetable currentIngredient) 
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            // do the animation
            // add whatever score you want
            currentIngredient.chops++;
;
        }
    }

    public void Tenderise(Meat currentIngredient) 
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            currentIngredient.tenderiseStage++;
            isCompleted = true;
        }
    }

    public void Salt() 
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            // do the animation
            // add whatever score you want
            isCompleted = true;

        }
    }

    public void Cook(Meat currentIngredient) 
    {
        if (meatOnSideOne) {
            currentIngredient.cookingSideOne += gameplayManager.GetComponent<GameplayManager>().gameHeat / 10000;

        } else {
            currentIngredient.cookingSideTwo += gameplayManager.GetComponent<GameplayManager>().gameHeat / 10000;
        }

        if (Input.GetKeyDown(KeyCode.W)) 
        {

            if (meatOnSideOne) {
                meatOnSideOne = false;
            } else {
                meatOnSideOne = true;
            }
        }
    }

    public void BunStep() 
    {
        isCompleted = true;
    }

    public void Complete() 
    {
        customer.ingredientTracker++; // move on to the next ingredient
    }
}