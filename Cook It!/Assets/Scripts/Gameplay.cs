using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    private float gameTimer = 30.0f; // the game timer
    private float customerSpawnTimer = 100000f; // the spawn time between customers
    public GameObject customerPrefab; // a customer
    private GameObject currentCustomer; // the current (active) customer
    private int customersServed; // number of customers served
    [SerializeField]
    private Text nextIngredient;
    private bool gameOver;

    void Start() {
        currentCustomer = Instantiate(customerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity); // create a custome
        nextIngredient.text = "";
        customersServed = 0;
        gameOver = false;
    }

    private void Update() {

        // update the text
        if (!gameOver) {
            nextIngredient.text = currentCustomer.GetComponent<CustomerControl>().currentIngredient.name + "\n Customers Served: " + customersServed;
        }
        else {
            nextIngredient.text = "Game Over! \n Customers Served: \n" + customersServed;
        }
        
        if (gameTimer < 0) {
            gameOver = true;
        }

        // lower the timers
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
            cookIngredient(currentCustomer);
        }

        // if the order has been completed move on to the next customer
        if (currentCustomer.GetComponent<CustomerControl>().currentIngredient.name == "completed") {

            currentCustomer.GetComponent<CustomerControl>().ingredientTracker = 0;
            customersServed++;
            currentCustomer = Instantiate(customerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity); // if we get to the end of the customers order, bring in a new customer

        }

        // if the ingredient is bun, just 'add' the bun and move on to the next ingredient
        if (currentCustomer.GetComponent<CustomerControl>().currentIngredient.tag == "bun") {

            // add the bun
            currentCustomer.GetComponent<CustomerControl>().ingredientTracker += 1;
            Debug.Log("Reaches");
        }
    }

    private void cookIngredient(GameObject customer) {


        bool isSuccessful = false; // if the wrong key is pressed set to false
        string tag = customer.GetComponent<CustomerControl>().currentIngredient.tag; // find the tag
        switch (tag) {
            case  "meat": 
                if (Input.GetKeyDown(KeyCode.R)) {
                    isSuccessful = true; // if the R key is pressed, log success

                }
                
                break;

            case "vegetable" :
                if (Input.GetKeyDown(KeyCode.Y)) {
                    isSuccessful = true; // if the T key is pressed, log success

                }
                break;
        }
        if (isSuccessful == true) {
            customer.GetComponent<CustomerControl>().ingredientTracker += 1; // move up the array to the next active ingredient

        }
        if (isSuccessful == false) {
            // failure mechanics
        }
    }
}




