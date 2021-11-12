using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    
    private float customerSpawnTimer = 100000f; // the spawn time between customers
    [SerializeField]
    private GameObject customerPrefab; // a customer
    private int customersServed; // number of customers served
    private string currentState;
    private string currentIngredient;
    [SerializeField]
    private Text nextIngredient;
    private CustomerControl customer;
    GameObject newCustomer;


    public string gameplayState {
        get { return currentState; }
        set { currentState = value; }
    }

    public CustomerControl currentCustomer {
        get { return customer; }
        set { customer = value; }
    }

    public string ingredient {
        get { return currentIngredient; }
        set { currentIngredient = value; }
    }

    public string step {
        get { return currentState; }
        set { currentState = value; }
    }

    void Start() {
        newCustomer = Instantiate(customerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity); // create a customer
        nextIngredient.text = "";
        customersServed = 0;
        customer = newCustomer.GetComponent<CustomerControl>();
    }

    private void Update() {

        // if the ingredient is bun, just 'add' the bun and move on to the next ingredient
        if (customer.currentIngredient.tag == "bun") {
            // add the bun
            customer.ingredientTracker += 1;
        }

        nextIngredient.text = "Current Ingredient is: " + currentIngredient + "\n Current Step is: " + currentState;

        if (customer.currentIngredient.name == "completed") {

            newCustomer = Instantiate(customerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            customer = newCustomer.GetComponent<CustomerControl>();
        }
    }

    public void cookIngredient(string currentState) {
        switch (currentState) {
            case "PeelLeaves":
                PeelLeaves();
                break;
            case "Chop":
                Chop();
                break;
            case "Tenderise":
                Tenderise();
                break;
            case "Salt":
                Salt();
                break;
            case "Cook":
                Cook();
                break;
            case "Complete":
                Complete();
                break;
        }
    }

    public void PeelLeaves() {
        if (Input.GetKeyDown(KeyCode.Y)) {
            // do the animation
            // add whatever score you want
            customer.currentIngredient.stepTracker++;
            Debug.Log("Enters");
        }
    }

    public void Chop() {
        if (Input.GetKeyDown(KeyCode.T)) {
            // do the animation
            // add whatever score you want
            customer.currentIngredient.stepTracker++;
            Debug.Log("Enters" + customer.currentIngredient.stepTracker);
        }
    }

    public void Tenderise() {
        if (Input.GetKeyDown(KeyCode.R)) {
            customer.currentIngredient.stepTracker++;
            Debug.Log("Enters" + customer.currentIngredient.stepTracker);
        }
    }

    public void Salt() {
        if (Input.GetKeyDown(KeyCode.E)) {
            // do the animation
            // add whatever score you want
            customer.currentIngredient.stepTracker++;
            Debug.Log("Enters" + customer.currentIngredient.stepTracker);
        }
    }

    public void Cook() {
        if (Input.GetKeyDown(KeyCode.W)) {
            customer.currentIngredient.stepTracker++;
            Debug.Log("Enters" + customer.currentIngredient.stepTracker);
        }
    }

    public void Complete() {
        customer.ingredientTracker++; // move on to the next ingredient
        Debug.Log("Enters" + customer.currentIngredient.stepTracker);
    }
}




