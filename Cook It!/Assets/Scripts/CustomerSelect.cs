using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class responsible for allowing the player to select which customer they serve next
/// </summary>
public class CustomerSelect : MonoBehaviour
{
    private int currentSelction;

    [SerializeField]
    private GameplayManager gManager;

    private void Awake()
    {
        currentSelction = 0;
        gManager.readyOrNot = false;
    }

    private void OnEnable()
    {
        currentSelction = 0;
        gManager.readyOrNot = false;
    }

    private void Update()
    {
        if (gManager.transform.childCount != 0)
        {
            // Sets the indicator active on the customer that is selected
            gManager.transform.GetChild(currentSelction).gameObject.GetComponent<CustomerControl>().ActiveIndicator(true);

            //// Increase the selection and remove the indicator from previously selected customer
            //if (Input.GetKeyDown(KeyCode.W))
            //{
            //    // increase selection
            //    currentSelction++;

            //    gManager.transform.GetChild(currentSelction - 1).gameObject.GetComponent<CustomerControl>().ActiveIndicator(false);

            //}

            //// Decrease the selection and remove the indicator from previously selected customer
            //if (Input.GetKeyDown(KeyCode.Y))
            //{
            //    // decrease selection
            //    currentSelction--;

            //    gManager.transform.GetChild(currentSelction + 1).gameObject.GetComponent<CustomerControl>().ActiveIndicator(false);
            //}

            // Confirm selection.
            // The customer scripts are then triggered and normal gameplay is resumed
            if (Input.GetKeyDown(KeyCode.T))
            {
                // finalise selection
                gManager.readyOrNot = true;
                GameObject selectedCustomer = gManager.transform.GetChild(currentSelction).gameObject;
                gManager.customer = selectedCustomer;
                selectedCustomer.GetComponent<CustomerControl>().isActiveCustomer = true;

                selectedCustomer.GetComponent<CustomerControl>().GenerateCustomer();

                gameObject.GetComponent<CustomerSelect>().enabled = false;
            }
        }
    }
}
