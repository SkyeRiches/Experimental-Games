using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timer : MonoBehaviour
{
    private float timer;
    private bool timeUp;
    private bool timeBegin;

    public void Start()
    {
        timeBegin = true;
        timeUp = false;
        timer = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBegin)
        {
            // lower the timers
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timeUp = true;
                Finish();
            }
        }

    }
    
    public void Finish()
    {
        // End of round system will get triggered here
        float finalScore = gameObject.GetComponent<ScoreManager>().CalculateFinalScore();
        Debug.Log(finalScore);
    }
}
