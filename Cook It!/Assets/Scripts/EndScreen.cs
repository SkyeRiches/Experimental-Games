using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// The class that manages the EndGame screen
/// </summary>
public class EndScreen : MonoBehaviour
{
    [SerializeField] private Text rating;
    [SerializeField] private Text score;
    [SerializeField] private Text leavers;

    // Upon loading the scene, the score value and number of leavers is grabbed from the player pref for each
    // And displayed on the UI text elements
    void Start()
    {
        score.text = PlayerPrefs.GetFloat("Score").ToString();
        leavers.text = PlayerPrefs.GetInt("Leavers").ToString();
    }

    // If they press R, it will take them back to the main menu
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
