using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script controls how long the game goes on for. 
/// It is constantly counting down using the change in time between frames 
/// When it hits zero the game ends
/// </summary>
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
        timer = 180;
    }

    // Update is called once per frame
    void Update()
    {

        if (timeBegin && !timeUp)
        {
            // lower the timers
            timer -= Time.deltaTime;

            // An escape from the main game incase you need to return to main menu as we have no pause menu
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                timer = 0;
            }

            // End the game and load up the endScreen
            if (timer <= 0)
            {
                timeUp = true;
                gameObject.GetComponent<ScoreSystem>().CalculateTotalScore();
                SceneManager.LoadScene(2);
            }
        }

    }
}
