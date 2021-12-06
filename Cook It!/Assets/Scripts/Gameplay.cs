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

    [SerializeField] GameObject knife;
    [SerializeField] GameObject hammer;
    [SerializeField] GameObject salt;
    [SerializeField] GameObject hand;
    [SerializeField] GameObject van;

    [SerializeField]
    private Vector3 VegetableIngredientPos;
    [SerializeField]
    private Vector3 completedIngredientPos;
    [SerializeField]
    private Vector3 meatIngredientPos;

    private bool needsToSetKnife;
    private bool needsToSetHammer;
    private bool needsToSetSalt;

    Vector3 knifePosition = new Vector3(0f, 0f, 0f);

    private bool meatOnSideOne;

    private float scoreToAdd;

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
                        scoreToAdd = 0;
                    }
                    break;

                case "Chop":
                    // if chop is complete, return the knife back to its original state
                    knife.transform.SetParent(van.transform);
                    knife.transform.localPosition = new Vector3(-0.011f, 0.05332142f, 7.143625e-05f);
                    knife.transform.localRotation = Quaternion.Euler(90, 0, 0);
                    currentVeg = (Vegetable)currentIngredient;

                    if (currentVeg.idealChops != currentVeg.chops)
                    {
                        scoreToAdd = 0;
                    }
                    break;

                case "Tenderise":
                    hammer.transform.SetParent(van.transform);
                    hammer.transform.localPosition = new Vector3(-0.015f, 0.05332142f, 8.9e-05f);
                    hammer.transform.localRotation = Quaternion.Euler(90, 0, 0);
                    currentMeat = (Meat)currentIngredient;
                    if (currentMeat.IdealTenderisation != currentMeat.tenderiseStage)
                    {
                        scoreToAdd = 0;
                    }
                    break;

                case "Salt":
                    salt.transform.SetParent(van.transform);
                    salt.transform.localPosition = new Vector3(0.015f, 0.03f, 0.00135f);
                    salt.transform.localRotation = Quaternion.Euler(0, -24.996f, 0);

                    currentMeat = (Meat)currentIngredient;
                    if (currentMeat.IdealSalt != currentMeat.SaltAmount)
                    {
                        scoreToAdd = 0;
                    }
                    break;

                case "Cook":
                    currentMeat = (Meat)currentIngredient;
                    if (currentMeat.TempSide1 != currentMeat.IdealCook)
                    {
                        scoreToAdd = 0;
                    }

                    break;

                default:
                    scoreToAdd = 100;
                    break;
            }

            gameplayManager.GetComponent<ScoreSystem>().IncreaseScore(scoreToAdd);

            customer.currentIngredient.stepTracker++;
            gameplayManager.GetComponent<GUIManager>().Counter = 0;

        } 
        else 
        {
            switch (currentState) 
            {
                case "PeelLeaves":
                    customer.currentIngredient.gameObject.transform.localPosition = customer.currentIngredient.bestPos;
                    gameplayManager.GetComponent<CameraPos>().IsPrepping = true;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    PeelLeaves((Vegetable)currentIngredient);
                    break;
                case "Chop":
                    customer.currentIngredient.gameObject.transform.localPosition = customer.currentIngredient.bestPos;
                    gameplayManager.GetComponent<CameraPos>().IsPrepping = true;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    Chop((Vegetable)currentIngredient);
                    break;
                case "Tenderise":
                    customer.currentIngredient.gameObject.transform.localPosition = customer.currentIngredient.bestPos;
                    gameplayManager.GetComponent<CameraPos>().IsPrepping = true;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    Tenderise((Meat)currentIngredient);
                    break;
                case "Salt":
                    customer.currentIngredient.gameObject.transform.localPosition = customer.currentIngredient.bestPos;
                    gameplayManager.GetComponent<CameraPos>().IsPrepping = true;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    Salt((Meat)currentIngredient);
                    break;
                case "Cook":
                    customer.currentIngredient.gameObject.transform.localPosition = customer.currentIngredient.cookPos;
                    gameplayManager.GetComponent<CameraPos>().IsPrepping = false;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = true;
                    Cook((Meat)currentIngredient);
                    break;
                case "Complete":
                    customer.currentIngredient.gameObject.transform.localPosition = customer.currentIngredient.bestPos;
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
        if (needsToSetKnife) {
            // put the knife in the hand#
            knifePosition = new Vector3(0f, 0f, 0f);
            knife.transform.parent = hand.transform;
            knife.transform.localPosition = knifePosition;
            knife.transform.localRotation = Quaternion.Euler(8.196f, 230.969f, 66.549f);
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
        if (needsToSetHammer)
        {
            hammer.transform.parent = hand.transform;
            hammer.transform.localPosition = knifePosition;
            hammer.transform.localRotation = Quaternion.Euler(8.196f, 230.969f, 66.549f);
            needsToSetHammer = false;
        }
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            currentIngredient.tenderiseStage++;
            gameplayManager.GetComponent<GUIManager>().Counter++;
            gameplayManager.GetComponent<AnimationManager>().Tenderise();
        }
    }

    public void Salt(Meat currentIngredient) 
    {
        if (needsToSetSalt)
        {
            salt.transform.parent = hand.transform;
            salt.transform.localPosition = knifePosition;
            salt.transform.localRotation = Quaternion.Euler(180, -24.996f, 0);
            needsToSetSalt = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentIngredient.SaltAmount++;
            gameplayManager.GetComponent<GUIManager>().Counter++;
            gameplayManager.GetComponent<AnimationManager>().Salt();
        }
    }

    public void Cook(Meat currentIngredient) 
    {
        currentIngredient.cooking += gameplayManager.GetComponent<GameplayManager>().gameHeat / 10000;
    }

    public void Complete() 
    {
        // send it to narnia
        customer.currentIngredient.gameObject.transform.position = completedIngredientPos;
        customer.ingredientTracker++; // move on to the next ingredient
        needsToSetKnife = true;
        needsToSetHammer = true;
        needsToSetSalt = true;
        
    }
}