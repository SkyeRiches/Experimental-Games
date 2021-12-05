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
    GameObject knife;
    [SerializeField]
    GameObject hand;
    [SerializeField]
    GameObject van;

    [SerializeField]
    private Vector3 VegetableIngredientPos;
    [SerializeField]
    private Vector3 completedIngredientPos;
    [SerializeField]
    private Vector3 meatIngredientPos;

    private bool needsToSetKnife;

    Vector3 knifePosition = new Vector3(0f, 0f, 0f);

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
        customer.currentIngredient.gameObject.transform.position = customer.currentIngredient.bestPos;
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
                    }
                    break;

                case "Chop":

                    currentVeg = (Vegetable)currentIngredient;
                    if (currentVeg.idealChops != currentVeg.chops)
                    {

                        // if chop is complete, return the knife back to its original state
                        knife.transform.SetParent(van.transform);
                        knife.transform.position = new Vector3(-0.01107125f, 0.05332142f, 7.143625e-05f);
                    }
                    break;

                case "Tenderise":
                    currentMeat = (Meat)currentIngredient;
                    if (currentMeat.IdealTenderisation != currentMeat.tenderiseStage)
                    {
                    }
                    break;

                case "Salt":
                    currentMeat = (Meat)currentIngredient;
                    if (currentMeat.IdealSalt != currentMeat.SaltAmount)
                    {
                    }
                    break;

                case "Cook":
                    currentMeat = (Meat)currentIngredient;
                    if (currentMeat.TempSide1 != currentMeat.IdealTempSide1 || currentMeat.TempSide2 != currentMeat.TempSide2)
                    {
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
        Debug.Log("In Chop");
        if (needsToSetKnife) {
            // put the knife in the hand
            knife.transform.parent = hand.transform;
            knife.transform.position = knifePosition;
            needsToSetKnife = false;


        }
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
        // send it to narnia
        customer.currentIngredient.gameObject.transform.position = completedIngredientPos;
        customer.ingredientTracker++; // move on to the next ingredient
        needsToSetKnife = true;
        
    }
}