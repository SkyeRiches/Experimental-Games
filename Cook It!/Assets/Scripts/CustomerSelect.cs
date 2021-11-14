using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSelect : MonoBehaviour
{
    private int currentSelction;

    [SerializeField]
    private GameplayManager gManager;

    private void Awake()
    {
        currentSelction = 0;
        
    }

    private void Update()
    {
        gManager.transform.GetChild(currentSelction).gameObject.GetComponent<Material>().color = Color.red;

        if (Input.GetKeyDown(KeyCode.W))
        {
            // increase selection
            currentSelction++;

            gManager.transform.GetChild(currentSelction - 1).gameObject.GetComponent<Material>().color = Color.white;

        }
        
        if (Input.GetKeyDown(KeyCode.Y))
        {
            // decrease selection
            currentSelction--;
            gManager.transform.GetChild(currentSelction + 1).gameObject.GetComponent<Material>().color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            // finalise selection
            gManager.readyOrNot = true;
            GameObject selectedCustomer = gManager.transform.GetChild(currentSelction).gameObject;
            gManager.readyOrNot = true;
            gManager.customer = selectedCustomer;
            selectedCustomer.GetComponent<CustomerControl>().isActiveCustomer = true;

            gameObject.GetComponent<CustomerSelect>().enabled = false;
        }
    }
}
