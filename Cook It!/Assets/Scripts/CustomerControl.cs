using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerControl : MonoBehaviour
{
    [SerializeField]
    private List<string> orderItems = new List<string>(); // the items in a customers order
    private bool customerActive; // whether the customer is active
    private int ingredientInt; // which ingredient of the order is the player on
    [SerializeField]
    private GameObject[] ingredients; // the list of possible ingredients
    [SerializeField]
    private int maxNumberOfIngredientsPerOrder = 8; // maximum ingredients per order (+2 for buns on either side)

    // getters and setters
    public string currentIngredient {
        get { return orderItems[ingredientInt]; }
        set { orderItems[ingredientInt] = value; }
    }

    public bool isActiveCustomer {
        get { return customerActive; }
        set { customerActive = value; }
    }

    public int ingredientTracker {
        get { return ingredientInt; }
        set { ingredientInt = value; }
    }



    void Start() {
        orderItems.Add("bun"); // start with a bun
        GenerateCustomer(); // generate the customers order
        ingredientInt = 0; // go the start of their order
        Debug.Log("New Customer Has Entered The Arena, first ingredient is: " + orderItems[ingredientInt]); // for testing

        orderItems.Add("bun"); // end with a bun
        orderItems.Add("completed"); // to track completeness
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateCustomer() {
        bool continueAdding = true; // whether we should keep adding
        float randomNumberForContinuation = (1/(float)maxNumberOfIngredientsPerOrder); // a float for probability
        int randomNumberForIngredient = Random.Range(0, ingredients.Length); // a float for randomizing ingredients
        while (continueAdding == true) // while we sould keep adding
        {
            orderItems.Add(ingredients[randomNumberForIngredient].name); // add an ingredient
            float testNumber = Random.Range(0.0f, 1.0f); // create a random number between 0 and 1
            // test whether random number is bigger than our test value squared (squaring weights it towards bigger numbers)
            if (testNumber <= randomNumberForContinuation * randomNumberForContinuation) { 
                continueAdding = false; // stop adding
            }
            else {
                randomNumberForContinuation += (1 / (float)maxNumberOfIngredientsPerOrder); // increase our test number
                randomNumberForIngredient = Random.Range(0, ingredients.Length); // choose a new test value
            }

        }
    }
}
