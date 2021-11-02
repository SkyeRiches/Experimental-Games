using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    private float gameTimer = 30.0f; // the game timer
    private float customerSpawnTimer = 100000f; // the spawn time between customers
    public GameObject customerPrefab; // a customer
    private GameObject currentCustomer; // the current (active) customer
    private bool success; // tracks if an input is met with success


    void Start() {
        currentCustomer = Instantiate(customerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity); // create a custome
    }

    private void Update() {
        gameTimer -= Time.deltaTime;
        customerSpawnTimer -= Time.deltaTime;
        if (gameTimer <= 0) {
            // end the game
        }

        if (customerSpawnTimer <= 0) {
            // spawn a customer and reset timer.
            currentCustomer = Instantiate(customerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity); // if the timer runs out, replace the current customer

            customerSpawnTimer = 1000000; // for the purposes of testing, i have a new customer created when the old one runs out
        }

        if (Input.anyKeyDown) { // if there is input, run the cook ingredient function
            success = cookIngredient(currentCustomer);
        }

        if (currentCustomer.GetComponent<CustomerControl>().currentIngredient == "completed") {
            currentCustomer = Instantiate(customerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity); // if we get to the end of the customers order, bring in a new customer
        }
    }

    private bool cookIngredient(GameObject customer) {
        bool isSuccessful = false; // if the wrong key is pressed set to false

        switch (customer.GetComponent<CustomerControl>().currentIngredient) {
            case "burger":
                if (Input.GetKeyDown(KeyCode.R)) {
                    isSuccessful = true; // if the R key is pressed, log success
                }
                break;

            case "bun":
                if (Input.GetKeyDown(KeyCode.T)) {
                    isSuccessful = true; // if the T key is pressed, log success
                }
                break;
        }
        if (isSuccessful == true) {
            customer.GetComponent<CustomerControl>().ingredientTracker += 1; // move up the array to the next active ingredient
            Debug.Log("New Ingredient Is: " + customer.GetComponent<CustomerControl>().currentIngredient); // for testing as there are no visuals
        }
        if (isSuccessful == false) {
            // failure mechanics
        }
        return isSuccessful; // return whether the right or wrong key was pressed
    }
}




