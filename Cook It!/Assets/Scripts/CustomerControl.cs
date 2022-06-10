using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerControl : MonoBehaviour 
{
    [SerializeField]
    private GameObject[] ingredients; // the list of possible ingredients
    [SerializeField]
    private GameObject bun; // a bun ingredient
    [SerializeField]
    private GameObject completed; // the completion ingredient
    [SerializeField]
    private int maxNumberOfIngredientsPerOrder = 8; // maximum ingredients per order (+2 for buns on either side)

    // gameObjects to hold the HUD sprites
    [SerializeField]
    private GameObject panelForOrderPrefab; 
    private GameObject canvas;

    private GameObject panelForOrder;
    // private Image imageToAdd;

    // Customer variables
    [SerializeField]
    private List<GameObject> items = new List<GameObject>(); // the ingredients in the order
    [SerializeField]
    private Ingredient ingredient; // the current ingredient
    private int ingredientInt; // which ingredient of the order is the player on
    [SerializeField]
    private bool customerActive; // whether the customer is active
    private float impatience = 60; // impatience tracker for the customer

    private GameplayManager gManager; 

    // Ingredient Sprites
    [SerializeField]
    private Sprite lettuceSprite;
    [SerializeField]
    private Sprite tomatoSprite;
    [SerializeField]
    private Sprite burgerSprite;
    [SerializeField]
    private Sprite onionSprite;
    [SerializeField]
    private Sprite bunSprite;
    [SerializeField]
    private Sprite knifeSprite;
    [SerializeField]
    private Sprite fireSprite;
    [SerializeField]
    private Sprite saltSprite;

    // for updating active ingredientSprites
    private List<GameObject> ingredientSprites = new List<GameObject>();
    private GameObject currentSprite;

    // the step sprite gameObjects
    private GameObject StepSpriteObjectOne;
    private GameObject StepSpriteObjectTwo;
    private GameObject StepSpriteObjectThree;

    // the gameObjects for the text for the step sprites
    private GameObject step1Text;
    private GameObject step2Text;
    private GameObject step3Text;

    // the images to be loaded into the sprite gameObjects
    private Image StepSpriteOne;
    private Image StepSpriteTwo;
    private Image StepSpriteThree;
    private List<Image> stepSpriteList = new List<Image>();
    private GameObject[] stepNumbers = new GameObject[3];


    [SerializeField] private GameObject activeIndicator;
    private bool isChosen = false;

    // getters and setters
    public Ingredient currentIngredient 
    {
        get { return ingredient; }
        set { ingredient = value; }
    }

    public bool isActiveCustomer
    {
        get { return customerActive; }
        set { customerActive = value; }
    }

    public int ingredientTracker 
    {
        get { return ingredientInt; }
        set { ingredientInt = value; }
    }

    // steps to be run when the customer needs to be generated
    private void Initialise() 
    {
        // set the initial values
        ingredientInt = 0;
        // set up the GUI canvas
        canvas = new GameObject();
        canvas.AddComponent<Canvas>();
        canvas.name = "CustomerCanvas";
        canvas.transform.SetParent(this.gameObject.transform);
        canvas.transform.localPosition = new Vector3(-4.62f, -4.087f, -4.749f);

        // set up the sprite holder gameObjects
        StepSpriteObjectOne = new GameObject();
        StepSpriteOne = StepSpriteObjectOne.AddComponent<Image>();
        StepSpriteOne.name = "Sprite1";
        stepSpriteList.Add(StepSpriteOne);

        StepSpriteObjectTwo = new GameObject();
        StepSpriteTwo = StepSpriteObjectTwo.AddComponent<Image>();
        StepSpriteTwo.name = "Sprite2";
        stepSpriteList.Add(StepSpriteTwo);

        StepSpriteObjectThree = new GameObject();
        StepSpriteThree = StepSpriteObjectThree.AddComponent<Image>();
        StepSpriteThree.name = "Sprite3";
        stepSpriteList.Add(StepSpriteThree);

        // set up the GUI text holder gameObjects
        step1Text = new GameObject();
        step1Text.AddComponent<Text>();
        step1Text.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        step1Text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        step1Text.GetComponent<Text>().color = Color.red;
        stepNumbers[0] = step1Text;

        step2Text = new GameObject();
        step2Text.AddComponent<Text>();
        step2Text.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        step2Text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        step2Text.GetComponent<Text>().color = Color.red;
        stepNumbers[1] = step2Text;

        step3Text = new GameObject();
        step3Text.AddComponent<Text>();
        step3Text.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        step3Text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;
        step3Text.GetComponent<Text>().color = Color.red;
        stepNumbers[2] = step3Text;
    }

    void Start() 
    {
        impatience = Random.Range(30, 120);
        Debug.Log(impatience);
        customerActive = true;
        gManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameplayManager>();
        ingredientInt = 0; // go the start of their order
    }

    void Update()
    {
        // update impatience and check if customer is ready to leave
        //impatience -= Time.deltaTime;
        if (impatience <= 0)
        {
            impatience = 3f;
            gManager.GetComponent<ScoreSystem>().LeaverNumber++;
            StartCoroutine(gManager.ReadjustDelay(gameObject)); // Triggers the function to wait before readjusting the customers as it wont sort them otherwise as this customer wont have been removed yet
        }
        // update current ingredient
        if (items.Count != 0)
        {
            ingredient = items[ingredientInt].GetComponent<Ingredient>();
        }
        // update the current ingredient
        if (ingredient != null)
        {
            ingredient.Update();
        }
        else
        {
            customerActive = false;
        }
        // update the sprites
        if (customerActive) {
            UpdateSprites();
        }

    }

    public void GenerateCustomer() 
    {
        Initialise();
        // set up the panel for the GUI
        panelForOrder = Instantiate(panelForOrderPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        panelForOrder.transform.SetParent(canvas.transform);

        panelForOrder.SetActive(true);
        panelForOrder.transform.localScale = new Vector3(0.0025f, 0.0025f, 0.0025f);
        panelForOrder.transform.localPosition = new Vector3(0f,0f,0f);
        panelForOrder.transform.localPosition = new Vector3(5, 6, 5);

        // Generate the ingredient
        bool continueAdding = true; // whether we should keep adding
        float randomNumberForContinuation = (1/(float)maxNumberOfIngredientsPerOrder); // a float for probability
        int randomNumberForIngredient = Random.Range(0, ingredients.Length); // a float for randomizing ingredients
        GameObject bunImageObject = new GameObject();
        
        // set up bun GUI
        Image bunImageToAdd = bunImageObject.AddComponent<Image>(); 
        ingredientSprites.Add(bunImageObject); 
        bunImageToAdd.sprite = bunSprite; 
        bunImageToAdd.transform.SetParent(panelForOrder.transform, false);
        bunImageToAdd.transform.localPosition = new Vector3(-800, 500 - 100 * items.Count, 0);
        GameObject bunToAdd = Instantiate(bun, new Vector3(-10f, -10f, -10f), Quaternion.identity);

        // create and initialize the bun
        bunToAdd.transform.parent = gameObject.transform;
        bunToAdd.GetComponent<Ingredient>().Generate();

        items.Add(bunToAdd); // add the bun to the list

        while (continueAdding == true) // while we should keep adding
        {
            // create and add an ingredient
            GameObject objectToAdd = Instantiate(ingredients[randomNumberForIngredient], new Vector3(-10f, -10f, -10f), Quaternion.identity);
            objectToAdd.transform.parent = gameObject.transform;
            objectToAdd.GetComponent<Ingredient>().Generate();
            items.Add(objectToAdd); // add an ingredient
            float testNumber = Random.Range(0.0f, 1.0f); // create a random number between 0 and 1

            // test whether random number is bigger than our test value squared (squaring weights it towards bigger numbers)
            if (testNumber <= randomNumberForContinuation * randomNumberForContinuation) 
            { 
                continueAdding = false; // stop adding
            }
            else 
            {
                randomNumberForContinuation += (1 / (float)maxNumberOfIngredientsPerOrder); // increase our test number
                randomNumberForIngredient = Random.Range(0, ingredients.Length); // choose a new test value
            }

            // set up the gameObject GUI
            GameObject imageObject = new GameObject();
            Image imageToAdd = imageObject.AddComponent<Image>();
            imageToAdd.gameObject.SetActive(true);
            imageToAdd.enabled = true;
            ingredientSprites.Add(imageObject);
            if (objectToAdd.name == "lettuce(Clone)") 
            {
                imageToAdd.sprite = lettuceSprite;
            } 
            else if (objectToAdd.name == "Tomato(Clone)") 
            {
                imageToAdd.sprite = tomatoSprite;
            } 
            else if (objectToAdd.name == "burger(Clone)") 
            {
                imageToAdd.sprite = burgerSprite;
            } 
            else if (objectToAdd.name == "Onion(Clone)") 
            {
                imageToAdd.sprite = onionSprite;
            } 
            else 
            {
                imageToAdd.sprite = bunSprite;
            }
            imageToAdd.transform.SetParent(panelForOrder.transform, false);
            // No idea why this has to be -900 instead of -800 like the other 2 but for some reason it works
            imageToAdd.transform.localPosition = new Vector3(-900, 600 - 100 * items.Count, 0);
        }
        // add another bun at the end
        GameObject bunTwoImageObject = new GameObject();
        // Set up GUI for the second bun
        Image bunTwoImageToAdd = bunTwoImageObject.AddComponent<Image>();
        ingredientSprites.Add(bunTwoImageObject);
        bunTwoImageToAdd.sprite = bunSprite;
        bunTwoImageToAdd.transform.SetParent(panelForOrder.transform, false);
        bunTwoImageToAdd.transform.localPosition = new Vector3(-800, 500 - 100 * items.Count, 0);

        completed.name = "completed";
        items.Add(bunToAdd);

        items.Add(completed);

        // set up the positioning of the GUI sprites
        for (int i = 0; i<=stepSpriteList.Count - 1; i++) {
            stepSpriteList[i].gameObject.transform.SetParent(panelForOrder.transform, false);
            stepSpriteList[i].gameObject.transform.localPosition = new Vector3(-110, -85, -900 - i *100);
            stepSpriteList[i].gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);

            stepNumbers[i].gameObject.transform.SetParent(panelForOrder.transform, false);
            stepNumbers[i].gameObject.transform.localPosition = new Vector3(-110, -84, -900 - i * 100);
            stepNumbers[i].gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
        }        
    }

    public void UpdateSprites() {
        // find the active sprite and make it jut out so its obvious which step is active
        currentSprite = ingredientSprites[ingredientInt];
        foreach (GameObject sprite in ingredientSprites) {
            Vector3 spritePos = sprite.transform.localPosition;
            if (sprite != currentSprite) {
                spritePos.x = -800f;
            } else {
                spritePos.x = -700f;
            }
            sprite.transform.localPosition = spritePos;
        }
        // clear the sprite images and then find what sprite should be displayed
        // set active the correct sprites
        for (int i = 0; i <= currentIngredient.ingredientSteps.Count - 1; i++) 
        {
            stepSpriteList[i].sprite = null;
            stepSpriteList[i].gameObject.SetActive(true);
            stepSpriteList[i].sprite = StepImage(currentIngredient.ingredientSteps[i]);
            if (stepSpriteList[i].sprite == null)
            {
                stepSpriteList[i].gameObject.SetActive(false);
            }

            stepNumbers[i].SetActive(true);
            stepNumbers[i].GetComponent<Text>().text = StepText(currentIngredient.ingredientSteps[i]).ToString();
            if (StepText(currentIngredient.ingredientSteps[i]) == 0)
            {
                stepNumbers[i].gameObject.SetActive(false);
            }
        }
        for (int i = currentIngredient.ingredientSteps.Count; i<=2; i++) {
            stepSpriteList[i].gameObject.SetActive(false);
            stepNumbers[i].SetActive(false);
        }
    }

    // retrieve the correct image
    private Sprite StepImage(string step) {
        switch (step) {
            case "PeelLeaves":
                return lettuceSprite;
            case "Chop":
                return knifeSprite;
            case "Tenderise":
                return burgerSprite;
            case "Salt":
                return saltSprite;    
            case "Cook":
                return fireSprite;
            case "Bun Step":
                return bunSprite;
        }
        return null;
    }

    // update the text based on what the ingredient step is
    private int StepText(string step)
    {
        switch (step)
        {
            case "PeelLeaves":
                if (ingredient.GetComponent<Vegetable>())
                {
                    return ingredient.GetComponent<Vegetable>().idealPulls;
                }
                return 0;
            case "Chop":
                if (ingredient.GetComponent<Vegetable>())
                {
                    return ingredient.GetComponent<Vegetable>().idealChops;
                }
                return 0;

            case "Tenderise":
                if (ingredient.GetComponent<Meat>())
                {
                    return ingredient.GetComponent<Meat>().IdealTenderisation;
                }
                return 0;
            case "Salt":
                if (ingredient.GetComponent<Meat>())
                {
                    return ingredient.GetComponent<Meat>().IdealSalt;
                }
                return 0;
            case "Cook":
                if (ingredient.GetComponent<Meat>())
                {
                    return ingredient.GetComponent<Meat>().IdealCook;
                }
                return 0;
        }
        return 0;
    }

    public void ActiveIndicator(bool a_value)
    {
        activeIndicator.SetActive(a_value);
    }
}




