using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Ingredient : MonoBehaviour
{

    private string currentStep;
    protected bool isComplete;
    protected List<string> ingredientSteps = new List<string>();
    protected int stepInt = 0;

    public string nextStep {
        get { return currentStep; }
        set { currentStep = value; }
    }

    public virtual void Generate() {

    }

    public int stepTracker {
        get { return stepInt; }
        set { stepInt = value; }
    }

    private void Awake() {

    }

    void Start() {


    }

    virtual public void Update() {


    }


}
