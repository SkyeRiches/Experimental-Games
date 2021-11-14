using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Timer : MonoBehaviour
{
    private float timer;
    private bool timeUp;
    private bool timeBegin;

    [SerializeField]
    private GameplayManager gameManager;

    public void Initialize(float a_timer)
    {
        timeBegin = true;
        timer = a_timer;
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
    
    public bool Finish()
    {
        return true;
    }
}
