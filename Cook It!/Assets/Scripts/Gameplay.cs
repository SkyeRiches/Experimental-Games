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

    private Ingredient currentIngredient;

    [SerializeField]
    private GameObject gameplayManager;

    [SerializeField]
    private Vector3 currentIngredientPos;
    [SerializeField]
    private Vector3 completedIngredientPos;

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
    }

    public void cookIngredient(string currentState) 
    {

        if ((Input.GetKeyDown(KeyCode.T))) 
        {
            Vegetable currentVeg;
            Meat currentMeat;
            switch (currentState)
            {
                case "PeelLeaves":
                    currentVeg = (Vegetable)currentIngredient;
                    if (currentVeg.idealPulls != currentVeg.pulls)
                    {
                        gameplayManager.GetComponent<TaskFailManager>().TriggerPenalty(currentState);
                    }
                    break;

                case "Chop":
                    currentVeg = (Vegetable)currentIngredient;
                    if (currentVeg.idealChops != currentVeg.chops)
                    {
                        gameplayManager.GetComponent<TaskFailManager>().TriggerPenalty(currentState);
                    }
                    break;

                case "Tenderise":
                    currentMeat = (Meat)currentIngredient;
                    if (currentMeat.IdealTenderisation != currentMeat.tenderiseStage)
                    {
                        gameplayManager.GetComponent<TaskFailManager>().TriggerPenalty(currentState);
                    }
                    break;

                //case "Salt":
                //    currentMeat = (Meat)currentIngredient;
                //    if (currentMeat.IdealSalt != currentMeat.SaltAmount)
                //    {
                //        gameplayManager.GetComponent<TaskFailManager>().TriggerPenalty(currentState);
                //    }
                //    break;

                case "Cook":
                    currentMeat = (Meat)currentIngredient;
                    if (currentMeat.TempSide1 != currentMeat.IdealTempSide1 || currentMeat.TempSide2 != currentMeat.TempSide2)
                    {
                        gameplayManager.GetComponent<TaskFailManager>().TriggerPenalty(currentState);
                    }

                    break;

                default: break;
            }

            customer.currentIngredient.stepTracker++;
            gameplayManager.GetComponent<GUIManager>().Counter = 0;

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
                    Salt((Meat)currentIngredient);
                    break;
                case "Cook":
                    gameplayManager.GetComponent<CameraPos>().IsPrepping = false;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = true;
                    Cook((Meat)currentIngredient);
                    break;
                case "Complete":
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    Complete();
                    break;

                default: break;
            }
        }
    }

    public void PeelLeaves(Vegetable currentIngredient) 
    {
        if (Input.GetKeyDown(KeyCode.Y)) 
        {
            currentIngredient.pulls++;
            gameplayManager.GetComponent<GUIManager>().Counter++;
            gameplayManager.GetComponent<AnimationManager>().PullLeaves();
        }
    }

    public void Chop(Vegetable currentIngredient) 
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            gameplayManager.GetComponent<AnimationManager>().Chop();
            currentIngredient.chops++;
            gameplayManager.GetComponent<GUIManager>().Counter++;
        }
    }

    public void Tenderise(Meat currentIngredient) 
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            currentIngredient.tenderiseStage++;
            gameplayManager.GetComponent<GUIManager>().Counter++;
            gameplayManager.GetComponent<AnimationManager>().Tenderise();
        }
    }

    public void Salt(Meat currentIngredient) 
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentIngredient.SaltAmount++;
            gameplayManager.GetComponent<GUIManager>().Counter++;
            gameplayManager.GetComponent<AnimationManager>().Salt();
        }
    }

    public void Cook(Meat currentIngredient) 
    {
        if (meatOnSideOne) 
        {
            currentIngredient.cookingSideOne += gameplayManager.GetComponent<GameplayManager>().gameHeat / 10000;

        } 
        else 
        {
            currentIngredient.cookingSideTwo += gameplayManager.GetComponent<GameplayManager>().gameHeat / 10000;
        }

        if (Input.GetKeyDown(KeyCode.W)) 
        {

            if (meatOnSideOne) 
            {
                meatOnSideOne = false;
            } 
            else 
            {
                meatOnSideOne = true;
            }
            gameplayManager.GetComponent<AnimationManager>().FlipPan();
        }
    }

    public void Complete() 
    {

        customer.currentIngredient.transform.position = completedIngredientPos;
        customer.ingredientTracker++; // move on to the next ingredient
        customer.currentIngredient.transform.position = currentIngredientPos;
    }
}