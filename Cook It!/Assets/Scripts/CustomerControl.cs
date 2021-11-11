using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private List<GameObject> items = new List<GameObject>();
    private GameObject completed;

    // getters and setters
    public GameObject currentIngredient {
        get { return items[ingredientInt]; }
        set { items[ingredientInt] = value; }
    }

    public bool isActiveCustomer {
        get { return customerActive; }
        set { customerActive = value; }
    }

    public int ingredientTracker {
        get { return ingredientInt; }
        set { ingredientInt = value; }
    }

    private void Awake() {
        GenerateCustomer();
        ingredientInt = 0; // this has to be called before start, or otherwise it will remain as
        // a high number from the previous customer and immediately be asked to call from an empty list
    }

    void Start() {
        completed = new GameObject();

        
        ingredientInt = 0; // go the start of their order

        completed.name = "completed";

        items.Insert(0, bun); // start with a bun

        items.Add(bun); // end with a bun

        items.Add(completed); // to track completeness

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
            items.Add(ingredients[randomNumberForIngredient]); // add an ingredient
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
