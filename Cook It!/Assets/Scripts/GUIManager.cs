using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private Text numbers;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text ordersText;
    [SerializeField] private Image orderImage;

    private int counter;
    private float score;
    private int orders;

    public int Counter
    {
        get { return counter; }
        set { counter = value; }
    }
    public float Score
    {
        get { return score; }
        set { score = value; }
    }
    public int Orders
    {
        get { return orders; }
        set { orders = value; }
    }

    // Update is called once per frame
    void Update()
    {
        numbers.text = "X" + counter;
        scoreText.text = "Score: " + score;
        ordersText.text = "Served: " + orders;
    }
}
