using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerControl : MonoBehaviour 
{
    [SerializeField]
    private bool customerActive; // whether the customer is active
    private int ingredientInt; // which ingredient of the order is the player on
    [SerializeField]
    private GameObject[] ingredients; // the list of possible ingredients
    [SerializeField]
    private GameObject bun;
    [SerializeField]
    private int maxNumberOfIngredientsPerOrder = 8; // maximum ingredients per order (+2 for buns on either side)

    [SerializeField]
    private List<GameObject> items = new List<GameObject>();
    [SerializeField]
    private Ingredient ingredient;
    [SerializeField]
    private GameObject completed;

    private float impatience = 100; // value tbd
    private GameplayManager gManager;

    // getters and setters
    public Ingredient currentIngredient 
    {
        get { return ingredient; }
        set { ingredient = value; }
    }

    public bool isActiveCustomer
    {
        get { return customerActive; }
        set { customerActive = value; }
    }

    public int ingredientTracker 
    {
        get { return ingredientInt; }
        set { ingredientInt = value; }
    }

    private void Awake() 
    {
        ingredientInt = 0;
        impatience = 100; //value tbd
    }

    void Start() 
    {
        customerActive = true;
        gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameplayManager>();
        ingredientInt = 0; // go the start of their order
    }

    // Update is called once per frame
    void Update()
    {
        impatience -= Time.deltaTime;
        if (impatience <= 0)
        {
            StartCoroutine(gManager.ReadjustDelay()); // Triggers the function to wait before readjusting the customers as it wont sort them otherwise as this customer wont have been removed yet
            Destroy(gameObject);
        }


        if (items.Count != 0)
        {
            ingredient = items[ingredientInt].GetComponent<Ingredient>();
        }

        if (ingredient != null)
        {
            ingredient.Update();
        }
        else
        {
            customerActive = false;
        }
    }

    public void GenerateCustomer() 
    {
        bool continueAdding = true; // whether we should keep adding
        float randomNumberForContinuation = (1/(float)maxNumberOfIngredientsPerOrder); // a float for probability
        int randomNumberForIngredient = Random.Range(0, ingredients.Length); // a float for randomizing ingredients


        GameObject bunToAdd = Instantiate(bun, new Vector3(-10f, -10f, -10f), Quaternion.identity); ;
        bunToAdd.transform.parent = gameObject.transform;
        bunToAdd.GetComponent<Ingredient>().Generate();

        items.Add(bunToAdd);

        while (continueAdding == true) // while we should keep adding
        {
            GameObject objectToAdd = Instantiate(ingredients[randomNumberForIngredient], new Vector3(-10f, -10f, -10f), Quaternion.identity);
            objectToAdd.transform.parent = gameObject.transform;
            objectToAdd.GetComponent<Ingredient>().Generate();
            items.Add(objectToAdd); // add an ingredient
            float testNumber = Random.Range(0.0f, 1.0f); // create a random number between 0 and 1

            // test whether random number is bigger than our test value squared (squaring weights it towards bigger numbers)
            if (testNumber <= randomNumberForContinuation * randomNumberForContinuation) 
            { 
                continueAdding = false; // stop adding
            }
            else 
            {
                randomNumberForContinuation += (1 / (float)maxNumberOfIngredientsPerOrder); // increase our test number
                randomNumberForIngredient = Random.Range(0, ingredients.Length); // choose a new test value
            }

        }


        completed.name = "completed";
        items.Add(bunToAdd);

        items.Add(completed);

        foreach (gameObject orderItem in items) {
            Ingredient ingredientInOrder = orderItem.GetComponent<ingredient>();
            foreach (string step in ingredientInOrder.ingredientSteps) {
                
            }
        }

        // items[0].transform.position = new Vector3(0.5f, 1.8f, -9f);

        // Debug.Log(items[0].name);
       //  Debug.Log(items[0].transform.position.x + items[0].transform.position.y + items[0].transform.position.z);
    }
}


