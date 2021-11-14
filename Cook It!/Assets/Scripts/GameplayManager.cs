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
    private List<GameObject> customersInScene;
    private float customerTimer;
    private GameObject currentCustomer;
    private int totalCustomers;

    
    
    

    HeatControl heatControlSystem;
    Gameplay game;

    public GameObject customer {
        get { return currentCustomer; }
        set { currentCustomer = value; }
    }

    public float gameheat 
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

    private void Awake() {
        customersInScene = new List<GameObject>();

    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewCustomer();
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

            customerTimer = 15.0f;
            SpawnNewCustomer();

        }

        currentCustomer = customersInScene[0];

        foreach (GameObject customer in customersInScene) {
            if (customer == customersInScene[0]) {
                customer.GetComponent<CustomerControl>().isActiveCustomer = true;
            } else {
                customer.GetComponent<CustomerControl>().isActiveCustomer = false;
            }
        }

        game.gameplayCustomer = currentCustomer.GetComponent<CustomerControl>();

        currentHeat = heatControlSystem.publicHeat;
        currentIngredient = game.gameplayCustomer.currentIngredient;
        currentStep = game.gameplayCustomer.currentIngredient.nextStep;

        // Publishing info
        game.step = currentStep;
        game.ingredient = currentIngredient;
        
        if (game.ingredient.name == "completed") {
            customersInScene.Remove(currentCustomer);

            Destroy(currentCustomer);
            ReadjustCustomers();
            game.customersServed++;
            currentCustomer = customersInScene[0];
        }


        // Game Loop
        game.cookIngredient(currentStep);
        Debug.Log(currentCustomer);
    }

    void SpawnNewCustomer() {
        GameObject newCustomer = Instantiate(customerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity); // create a customer
        totalCustomers++;
        newCustomer.name = "Customer" + totalCustomers;
        customersInScene.Add(newCustomer);
        ReadjustCustomers();
    }

    void ReadjustCustomers() {
        for (int i = 0; i < customersInScene.Count; i++) {
            // line the customers up nicely
            customersInScene[i].transform.position = new Vector3(0, 0, 2.5f * i);
        }
    }
}
