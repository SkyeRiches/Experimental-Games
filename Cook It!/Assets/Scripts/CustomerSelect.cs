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
            //gManager.transform.GetChild(currentSelction).transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.red;

            if (Input.GetKeyDown(KeyCode.W))
            {
                // increase selection
                currentSelction++;

                //gManager.transform.GetChild(currentSelction - 1).transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.white;

            }

            if (Input.GetKeyDown(KeyCode.Y))
            {
                // decrease selection
                currentSelction--;

                //gManager.transform.GetChild(currentSelction + 1).transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.white;
            }

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
