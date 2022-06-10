using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{

    //Sound Effect Declarations
    public AudioSource source;
    public AudioClip Ambience;
    public AudioClip CompleteAudio;
    public AudioClip[] ChopAudio;
    public AudioClip[] SaltAudio;
    public AudioClip[] TenderiseAudio;
    public AudioClip[] PeelAudio;
    public AudioClip[] CookAudio;


    [SerializeField]
    private GameObject customerPrefab; // a customer
    [SerializeField]
    private GameObject gameplayManager;

    private int customersServed2; // number of customers served

    // current customer variables
    private string currentState;
    private CustomerControl customer;
    private Ingredient currentIngredient;

    GameObject newCustomer;

    [SerializeField] GameObject knife;
    [SerializeField] GameObject hammer;
    [SerializeField] GameObject salt;
    [SerializeField] GameObject hand;
    [SerializeField] GameObject van;

    // positions for gameObjects
    [SerializeField]
    private Vector3 VegetableIngredientPos;
    [SerializeField]
    private Vector3 completedIngredientPos;
    [SerializeField]
    private Vector3 meatIngredientPos;
    Vector3 knifePosition = new Vector3(0f, 0f, 0f);

    // trackers for if things are in correct place
    private bool needsToSetKnife;
    private bool needsToSetHammer;
    private bool needsToSetSalt;

    private bool meatOnSideOne;

    private float scoreToAdd;

    #region Getters/Setters
    public int customersServed {
        get { return customersServed2; }
        set { customersServed2 = value; }
    }

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

    public CustomerControl gameplayCustomer 
    {
        get { return customer; }
        set { customer = value; }
    }

    public string step 
    {
        get { return currentState; }
        set { currentState = value; }
    }
    #endregion

    void Start() 
    {
        customersServed = 0;
        source.PlayOneShot(Ambience);
    }

    public void cookIngredient(string currentState) 
    {
        // Work out the score accumalated when the player moves on from an ingredient
        if ((Input.GetKeyDown(KeyCode.T))) 
        {
            Vegetable currentVeg;
            Meat currentMeat;
            switch (currentState)
            {
                case "PeelLeaves":
                    currentVeg = (Vegetable)currentIngredient;
                    if (currentVeg.idealPulls != currentVeg.pulls)
                    {
                        scoreToAdd = 0;
                    }
                    break;

                case "Chop":
                    // if chop is complete, return the knife back to its original state
                    knife.transform.SetParent(van.transform);
                    knife.transform.localPosition = new Vector3(-0.011f, 0.05332142f, 7.143625e-05f);
                    knife.transform.localRotation = Quaternion.Euler(90, 0, 0);
                    currentVeg = (Vegetable)currentIngredient;

                    if (currentVeg.idealChops != currentVeg.chops)
                    {
                        scoreToAdd = 0;
                    }
                    break;

                case "Tenderise":
                    hammer.transform.SetParent(van.transform);
                    hammer.transform.localPosition = new Vector3(-0.015f, 0.05332142f, 8.9e-05f);
                    hammer.transform.localRotation = Quaternion.Euler(90, 0, 0);
                    currentMeat = (Meat)currentIngredient;
                    if (currentMeat.IdealTenderisation != currentMeat.tenderiseStage)
                    {
                        scoreToAdd = 0;
                    }
                    break;

                case "Salt":
                    salt.transform.SetParent(van.transform);
                    salt.transform.localPosition = new Vector3(0.015f, 0.03f, 0.00135f);
                    salt.transform.localRotation = Quaternion.Euler(0, -24.996f, 0);

                    currentMeat = (Meat)currentIngredient;
                    if (currentMeat.IdealSalt != currentMeat.SaltAmount)
                    {
                        scoreToAdd = 0;
                    }
                    break;

                case "Cook":
                    currentMeat = (Meat)currentIngredient;
                    if (currentMeat.TempSide1 != currentMeat.IdealCook)
                    {
                        scoreToAdd = 0;
                    }

                    break;

                default:
                    scoreToAdd = 100;
                    break;
            }

            gameplayManager.GetComponent<ScoreSystem>().IncreaseScore(scoreToAdd);
            // move to next ingredient
            customer.currentIngredient.stepTracker++;
            gameplayManager.GetComponent<GUIManager>().Counter = 0;

        } 
        else 
        {
            // set positions of the camera and ingredients based on the current game state
            switch (currentState)
            {
                case "PeelLeaves":
                    customer.currentIngredient.gameObject.transform.localPosition = customer.currentIngredient.bestPos;
                    gameplayManager.GetComponent<CameraPos>().IsPrepping = true;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    PeelLeaves((Vegetable)currentIngredient);
                    break;
                case "Chop":
                    customer.currentIngredient.gameObject.transform.localPosition = customer.currentIngredient.bestPos;
                    gameplayManager.GetComponent<CameraPos>().IsPrepping = true;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    Chop((Vegetable)currentIngredient);
                    break;
                case "Tenderise":
                    customer.currentIngredient.gameObject.transform.localPosition = customer.currentIngredient.bestPos;
                    gameplayManager.GetComponent<CameraPos>().IsPrepping = true;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    Tenderise((Meat)currentIngredient);
                    break;
                case "Salt":
                    customer.currentIngredient.gameObject.transform.localPosition = customer.currentIngredient.bestPos;
                    gameplayManager.GetComponent<CameraPos>().IsPrepping = true;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    Salt((Meat)currentIngredient);
                    break;
                case "Cook":
                    customer.currentIngredient.gameObject.transform.localPosition = customer.currentIngredient.cookPos;
                    gameplayManager.GetComponent<CameraPos>().IsPrepping = false;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = true;
                    Cook((Meat)currentIngredient);
                    break;
                case "Complete":
                    customer.currentIngredient.gameObject.transform.localPosition = customer.currentIngredient.bestPos;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    gameplayManager.GetComponent<CameraPos>().IsCooking = false;
                    Complete();
                    break;
                default: break;
            }
        }
    }

    // peel the leaves
    public void PeelLeaves(Vegetable currentIngredient) 
    {
        if (Input.GetKeyDown(KeyCode.Y)) 
        {
            SoundEffect(PeelAudio);
            currentIngredient.pulls++;
            gameplayManager.GetComponent<GUIManager>().Counter++;
            gameplayManager.GetComponent<AnimationManager>().PullLeaves();
        }
    }

    // chop a vegetable
    public void Chop(Vegetable currentIngredient) 
    {
        if (needsToSetKnife) {
            // put the knife in the hand
            knifePosition = new Vector3(0.000339999999f, 7.00000019e-05f, -9.99999975e-05f);
            knife.transform.parent = hand.transform;
            knife.transform.localPosition = knifePosition;
            knife.transform.localRotation = Quaternion.Euler(359.704926f, 233.018097f, 61.5224228f);
            needsToSetKnife = false;
        }
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            SoundEffect(ChopAudio);
            gameplayManager.GetComponent<AnimationManager>().Chop();
            currentIngredient.chops++;
            gameplayManager.GetComponent<GUIManager>().Counter++;
        }
    }

    // tenderise meat
    public void Tenderise(Meat currentIngredient) 
    {
        if (needsToSetHammer)
        {
            Vector3 hammerPos = new Vector3(0.000339999999f, 7.00000019e-05f, -0.000199999995f);
            hammer.transform.parent = hand.transform;
            hammer.transform.localPosition = hammerPos;
            hammer.transform.localRotation = Quaternion.Euler(345.764862f, 240.749176f, 66.0224838f);
            needsToSetHammer = false;
        }
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            SoundEffect(TenderiseAudio);
            currentIngredient.tenderiseStage++;
            gameplayManager.GetComponent<GUIManager>().Counter++;
            gameplayManager.GetComponent<AnimationManager>().Tenderise();
        }
    }

    // salt meat
    public void Salt(Meat currentIngredient) 
    {
        if (needsToSetSalt)
        {
            Vector3 saltPos = new Vector3(-1.99999995e-05f, -3.9999999e-05f, -0.000899999985f);
            salt.transform.parent = hand.transform;
            salt.transform.localPosition = saltPos;
            salt.transform.localRotation = Quaternion.Euler(342.41156f, 147.28978f, 154.90834f);
            needsToSetSalt = false;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SoundEffect(SaltAudio);
            currentIngredient.SaltAmount++;
            gameplayManager.GetComponent<GUIManager>().Counter++;
            gameplayManager.GetComponent<AnimationManager>().Salt();
        }
    }

    // cook meat
    public void Cook(Meat currentIngredient) 
    {
        SoundEffect(CookAudio);
        currentIngredient.cooking += gameplayManager.GetComponent<GameplayManager>().gameHeat / 10000;
        currentIngredient.GetComponentInChildren<Text>().text = ((int)currentIngredient.cooking).ToString();
    }

    // play a sound effect
    public void SoundEffect(AudioClip[] audio)
    {
        int random = Random.Range(0, 2);
        source.PlayOneShot(audio[random]);
    }

    // if your on the completion step (i.e. ingredient is complete)
    public void Complete() 
    {
        // send it to narnia
        customer.currentIngredient.gameObject.transform.position = completedIngredientPos;
        customer.ingredientTracker++; // move on to the next ingredient
        needsToSetKnife = true;
        needsToSetHammer = true;
        needsToSetSalt = true;
        
    }
}