using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject eventSystem;

    private float currentHeat;
    private string currentStep;
    private Ingredient currentIngredient;

    HeatControl heatControlSystem;
    Gameplay game;

    public float gameheat 
    {
        get { return currentHeat; }
        set { currentHeat = value; }
    }

    public string gameState 
    {
        get { return currentStep; }
        set { currentStep = value; }
    }

    public Ingredient gameIngredient 
    {
        get { return currentIngredient; }
        set { currentIngredient = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        heatControlSystem = eventSystem.GetComponent<HeatControl>();
        game = eventSystem.GetComponent<Gameplay>();
    }

    // Update is called once per frame
    void Update()
    {
        // Getting info

        currentHeat = heatControlSystem.publicHeat;
        currentIngredient = game.currentCustomer.currentIngredient;
        currentStep = game.currentCustomer.currentIngredient.nextStep;

        // Publishing info
        game.step = currentStep;
        game.ingredient = currentIngredient;

        // Game Loop
        game.cookIngredient(currentStep);
    }
}
