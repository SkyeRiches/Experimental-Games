using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject eventSystem;

    // gameplay variables
    private float currentHeat;
    private string currentStep;
    private Ingredient currentIngredient;
    private bool needsToStoreData;
    private Ingredient ingredientData;
    private string stepData;
    private int stepTrackerData;

    // customer management
    [SerializeField] private GameObject customerPrefab1;
    [SerializeField] private GameObject customerPrefab2;
    private float customerTimer;
    private GameObject currentCustomer;
    private int totalCustomers;

    // instantiate heat control system and gameplay
    HeatControl heatControlSystem;
    Gameplay game;

    // track the customers patience
    private float orderTime;

    private bool isReady;

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
        // initialise gameplay systems
        heatControlSystem = eventSystem.GetComponent<HeatControl>();
        game = eventSystem.GetComponent<Gameplay>();
        customerTimer = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // lower customer spawn cooldown timer
        customerTimer -= Time.deltaTime;

        // check if customer spawn cooldown is 0
        // if it is 0, spawn a new customer
        if(customerTimer <= 0.0f && gameObject.transform.childCount < 10) {
            customerTimer = 10f;
            if (currentIngredient) 
            {
                needsToStoreData = true;
            } 
            else 
            {
                needsToStoreData = false;
            }

            if (needsToStoreData) 
            {
                ingredientData = currentIngredient;
                stepData = currentIngredient.nextStep;
                stepTrackerData = currentIngredient.stepTracker;
            }

            SpawnNewCustomer();

            if (needsToStoreData) 
            {
                currentIngredient = ingredientData;
                currentIngredient.nextStep = stepData;
                currentIngredient.stepTracker = stepTrackerData;
            }
        }

        if (isReady)
        {
            orderTime += Time.deltaTime; // how long the order has taken to be made

            // update gameplay variables
            if (currentCustomer) 
            {
                game.gameplayCustomer = currentCustomer.GetComponent<CustomerControl>();
            }

            currentHeat = heatControlSystem.publicHeat;
            currentIngredient = game.gameplayCustomer.currentIngredient;
            currentStep = game.gameplayCustomer.currentIngredient.nextStep;
            game.step = currentStep;
            game.ingredient = currentIngredient;

            // check if we need to move to the next customer
            if (game.ingredient.name == "completed")
            {
                orderTime = 0;

                StartCoroutine(ReadjustDelay(currentCustomer));
                game.customersServed++;
                gameObject.GetComponent<CustomerSelect>().enabled = true;
                gameObject.GetComponent<CameraPos>().IsPrepping = false;
                gameObject.GetComponent<CameraPos>().IsCooking = false;
                gameObject.GetComponent<GUIManager>().Orders++;
            }
            // Game Loop
            game.cookIngredient(currentStep);
        }
    }

    void SpawnNewCustomer() 
    {
        // spawn a customer, there are two types of customer so pick a random one
        int customerType = Random.Range(0, 2);
        if (customerType == 1)
        {
            GameObject newCustomer = Instantiate(customerPrefab1, new Vector3(0f, -1f, 0f), Quaternion.identity); // create a customer
            newCustomer.transform.SetParent(gameObject.transform);
            totalCustomers++;
            newCustomer.name = "Customer" + totalCustomers;
        }
        else
        {
            GameObject newCustomer = Instantiate(customerPrefab2, new Vector3(0f, -1f, 0f), Quaternion.identity); // create a customer
            newCustomer.transform.SetParent(gameObject.transform);
            totalCustomers++;
            newCustomer.name = "Customer" + totalCustomers;
        }
        ReadjustCustomers();
    }

    public void ReadjustCustomers() 
    {
        for (int i = 0; i < gameObject.transform.childCount; i++) 
        {
            // line the customers up nicely
            gameObject.transform.GetChild(i).transform.position = new Vector3(1.5f * i, 0f, -7f);
        }
    }

    public IEnumerator ReadjustDelay(GameObject go)
    {
        yield return new WaitForSeconds(.1f);
        StartCoroutine(VanishDelay());
        Destroy(go);
    }

    private IEnumerator VanishDelay()
    {
        yield return new WaitForSeconds(1);
        ReadjustCustomers();
    }
}

#endregion
