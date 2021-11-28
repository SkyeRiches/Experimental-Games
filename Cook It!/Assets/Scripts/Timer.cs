using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        timer = 300;
    }

    // Update is called once per frame
    void Update()
    {

        if (timeBegin && !timeUp)
        {
            // lower the timers
            timer -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                timer = 0;
            }

            if (timer <= 0)
            {
                timeUp = true;
                gameObject.GetComponent<ScoreSystem>().CalculateTotalScore();
                SceneManager.LoadScene(2);
            }
        }

    }
}
