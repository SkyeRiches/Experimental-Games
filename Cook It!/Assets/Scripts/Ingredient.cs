using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    // ingredient data
    private string currentStep;
    protected bool isComplete;
    public List<string> ingredientSteps = new List<string>();
    protected int stepInt = 0;
    protected Vector3 idealPosition;
    protected Vector3 idealCookPos;

    // Getters and setters
    public Vector3 cookPos
    {
        get { return idealCookPos; }
    }

    public Vector3 bestPos {
        get { return idealPosition; }
        set { idealPosition = value; }
    }

    public string nextStep {
        get { return currentStep; }
        set { currentStep = value; }
    }

    public int stepTracker {
        get { return stepInt; }
        set { stepInt = value; }
    }

    public virtual void Generate() {

    }

    private void Awake() {

    }

    void Start() {


    }

    virtual public void Update() {


    }


}
