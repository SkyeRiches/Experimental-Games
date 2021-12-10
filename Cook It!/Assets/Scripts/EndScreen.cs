using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Text rating;
    [SerializeField] private Text score;
    [SerializeField] private Text leavers;

    // Start is called before the first frame update
    void Start()
    {
        score.text = PlayerPrefs.GetFloat("Score").ToString();
        leavers.text = PlayerPrefs.GetInt("Leavers").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }
}
