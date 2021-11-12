using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Ingredient : MonoBehaviour
{

    public string nextStep;
    protected bool isComplete;
    [SerializeField]
    protected List<string> ingredientSteps = new List<string>();
    protected int stepInt = 0;

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
