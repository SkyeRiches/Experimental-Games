using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject eventSystem;

    private float currentHeat;
    private string currentStep;
    private Ingredient currentIngredient;

    // customer management
    [SerializeField]
    private GameObject customerPrefab;
    private float customerTimer;
    private GameObject currentCustomer;
    private int totalCustomers;

    HeatControl heatControlSystem;
    Gameplay game;

    private float orderTime;
    private float customerPatience = 100; // THis is placeholder for now

    private bool isReady;

    private bool needsToStoreData;
    private Ingredient ingredientData;
    private string stepData;
    private int stepTrackerData;

    #region Getters/Setters
    public bool readyOrNot
    {
        get { return isReady; }
        set { isReady = value; }
    }


    public GameObject customer {
        get { return currentCustomer; }
        set { currentCustomer = value; }
    }

    public float gameHeat 
    {
        get { return currentHeat; }
        set { currentHeat = value; }
    }

    public string gameState 
    {
        get { return currentStep; }
        set { currentStep = value; }
    }

    public Ingredient gameIngredient 
    {
        get { return currentIngredient; }
        set { currentIngredient = value; }
    }
    #endregion

    #region Functions

    // Start is called before the first frame update
    void Start()
    {
        heatControlSystem = eventSystem.GetComponent<HeatControl>();
        game = eventSystem.GetComponent<Gameplay>();
        customerTimer = 5.0f;

    }

    // Update is called once per frame
    void Update()
    {
        // Getting info
        customerTimer -= Time.deltaTime;



        if(customerTimer <= 0.0f) {

            customerTimer = 10.0f;
            if (currentIngredient) {
                needsToStoreData = true;


            } else {
                needsToStoreData = false;
            }

            if (needsToStoreData) {
                ingredientData = currentIngredient;
                stepData = currentIngredient.nextStep;
                stepTrackerData = currentIngredient.stepTracker;
            }

            SpawnNewCustomer();

            if (needsToStoreData) {
                currentIngredient = ingredientData;
                currentIngredient.nextStep = stepData;
                currentIngredient.stepTracker = stepTrackerData;
            }



        }

        // this is placeholder for how long they have waited for their order
        customerPatience -= Time.deltaTime;

        if (isReady)
        {
            orderTime += Time.deltaTime; // how long the order has taken to be made

            if (currentCustomer) {
                game.gameplayCustomer = currentCustomer.GetComponent<CustomerControl>();
            }

            currentHeat = heatControlSystem.publicHeat;
            currentIngredient = game.gameplayCustomer.currentIngredient;
            currentStep = game.gameplayCustomer.currentIngredient.nextStep;

            // Publishing info
            game.step = currentStep;
            game.ingredient = currentIngredient;




            if (game.ingredient.name == "completed")
            {
                gameObject.GetComponent<ScoreManager>().CalculateOrderScore(orderTime, customerPatience);
                orderTime = 0;
                // This is placeholder, we need a proper customer patience system, im using this purely for testing values lol - skylar
                customerPatience = 100;

                Destroy(currentCustomer);
                ReadjustCustomers();
                game.customersServed++;
                gameObject.GetComponent<CustomerSelect>().enabled = true;

            }

            // Game Loop
            game.cookIngredient(currentStep);
        }

    }

    void SpawnNewCustomer() {

        GameObject newCustomer = Instantiate(customerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity); // create a customer
        newCustomer.transform.SetParent(gameObject.transform);
        totalCustomers++;
        newCustomer.name = "Customer" + totalCustomers;
        ReadjustCustomers();


    }

    void ReadjustCustomers() {
        for (int i = 0; i < gameObject.transform.childCount; i++) {
            // line the customers up nicely
            gameObject.transform.GetChild(i).transform.position = new Vector3(1.5f * i, 1f, -7f);
        }
    }
}

#endregion
