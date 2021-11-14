using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    
    [SerializeField]
    private GameObject customerPrefab; // a customer
    private int customersServed; // number of customers served
    private string currentState;

    [SerializeField]
    private Text nextIngredient;
    private CustomerControl customer;
    GameObject newCustomer;

    private bool isCompleted;
    private string completedText;

    private Ingredient currentIngredient;

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

    public CustomerControl currentCustomer 
    {
        get { return customer; }
        set { customer = value; }
    }

    public string step 
    {
        get { return currentState; }
        set { currentState = value; }
    }

    void Start() 
    {
        newCustomer = Instantiate(customerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity); // create a customer
        nextIngredient.text = "";
        customersServed = 0;
        customer = newCustomer.GetComponent<CustomerControl>();
        isCompleted = false;
    }

    private void Update() 
    {
        if (customer != null)
        {
            // if the ingredient is bun, just 'add' the bun and move on to the next ingredient
            if (customer.currentIngredient.tag == "bun")
            {
                // add the bun
                customer.ingredientTracker += 1;
            }

            // these needs to change to being part of a ui manager
            if (isCompleted)
            {
                completedText = "Completed!";
            }
            else
            {
                completedText = "NotCompleted :(";
            }

            nextIngredient.text = "Current Ingredient is: " + currentIngredient.name + "\n Current Step is: " + currentState + "\nCompletion Status is: " + completedText;
        }


        Debug.Log(customer.isActiveCustomer);

        // check to see if customer is done
        if (!customer.isActiveCustomer)
        {
            NextCustomer();
        }
    }

    public void cookIngredient(string currentState) 
    {

        if ((Input.GetKeyDown(KeyCode.T))) 
        {
            if (isCompleted) 
            {
                customer.currentIngredient.stepTracker++;
                isCompleted = false;
            }
            
        } 
        else 
        {
            switch (currentState) 
            {
                case "PeelLeaves":
                    PeelLeaves();
                    break;
                case "Chop":
                    Chop();
                    break;
                case "Tenderise":
                    Tenderise((Meat)currentIngredient);
                    break;
                case "Salt":
                    Salt();
                    break;
                case "Cook":
                    Cook((Meat)currentIngredient);
                    break;
                case "Complete":
                    Complete();
                    break;
            }
        }
    }

    public void PeelLeaves() 
    {
        if (Input.GetKeyDown(KeyCode.Y)) 
        {
            // do the animation
            // add whatever score you want
            isCompleted = true;
        }
    }

    public void Chop() 
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            // do the animation
            // add whatever score you want
            isCompleted = true;
        }
    }

    public void Tenderise(Meat currentIngredient) 
    {
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            currentIngredient.tenderiseStage++;
            isCompleted = true;
        }
    }

    public void Salt() 
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            // do the animation
            // add whatever score you want
            isCompleted = true;
        }
    }

    public void Cook(Meat currentIngredient) 
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            isCompleted = true;
        }
    }

    public void Complete() 
    {
        customer.ingredientTracker++; // move on to the next ingredient
    }

    private void NextCustomer()
    {
        Debug.Log("next customer");
        Destroy(customer.gameObject);
        newCustomer = Instantiate(customerPrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        customer = newCustomer.GetComponent<CustomerControl>();
    }
}




