using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class StartMenu : MonoBehaviour
{
    //DataManager Declarations
    public AudioMixer audioMixer;
    public DataManager dataManager;

    //Sound Effect Declarations
    public AudioSource source;
    public AudioClip MenuSelect;
    public AudioClip MenuCycle;
    public AudioClip MenuAdjust;

    //Menu Variables
    int ActiveMenu = 0;
    int CurrentButtonStart = 0;
    int StartLimit = 4;
    int CurrentButtonOptions = 0;
    int OptionsLimit = 2;
    int CurrentButtonQuit = 0;
    int QuitLimit = 2;

    //Menu Objects
    GameObject startMenu;
    GameObject optionsMenu;
    GameObject tutorialMenu;
    GameObject quitMenu;

    //Menu Buttons
    GameObject startButton1;
    GameObject startButton2;
    GameObject startButton3;
    GameObject startButton4;
    GameObject OptionsButton1;
    GameObject OptionsButton2;
    GameObject OptionsButton3;
    GameObject TutorialButton;
    GameObject QuitButton1;
    GameObject QuitButton2;
    GameObject[][] menu;

    //Options Variables
    float volume;

    //Options Objects
    GameObject volumeSlider;
    
    //Handles Setting The Menu To Its Default Values
    void Start()
    {
        //Start Menu Declarations
        startMenu = transform.GetChild(0).gameObject;
        startButton1 = startMenu.transform.GetChild(0).gameObject;
        startButton2 = startMenu.transform.GetChild(1).gameObject;
        startButton3 = startMenu.transform.GetChild(2).gameObject;
        startButton4 = startMenu.transform.GetChild(3).gameObject;

        //Options + Tutorial Menu Declarations
        optionsMenu = transform.GetChild(1).gameObject;
        OptionsButton1 = optionsMenu.transform.GetChild(0).gameObject;
        volumeSlider = OptionsButton1.transform.GetChild(0).gameObject;
        OptionsButton2 = optionsMenu.transform.GetChild(1).gameObject;

        tutorialMenu = transform.GetChild(2).gameObject;
        TutorialButton = tutorialMenu.transform.GetChild(0).gameObject;
        //Setting Up Options Sliders
        volume = 1;
        volumeSlider.GetComponent<Slider>().value = volume;//Sets Slider To Default Value


        //Quit Menu Declarations
        quitMenu = transform.GetChild(3).gameObject;
        QuitButton1 = quitMenu.transform.GetChild(0).gameObject;
        QuitButton2 = quitMenu.transform.GetChild(1).gameObject;

        //Menu Setup
        GameObject[][] menu = menuSet();
        resetMenu();
        buttonSelect(ActiveMenu, CurrentButtonStart, StartLimit, menu);//Handles Visual Button Interactivity
    }
    //Handles General Menu Navigation

    void Update()
    {
        //Start Menu
        if (ActiveMenu == 0)
        {
            if (Input.GetKeyDown("q"))//Spin It!
            {
                menu = menuSet();
                CurrentButtonStart = cycleMenu(CurrentButtonStart, StartLimit);//Handles Scrolling Limit Of Menu
                buttonSelect(ActiveMenu, CurrentButtonStart, StartLimit, menu);//Handles Visual Button Interactivity
            }
            if (Input.GetKeyDown("r"))//Bop It!
            {
                source.PlayOneShot(MenuSelect);
                if (CurrentButtonStart == 0)//Start Option
                {
                    StartGame();//Starts Game
                }
                if (CurrentButtonStart == 1)//Options Option
                {
                    ActiveMenu = 1;
                    resetMenu();
                    menu = menuSet();
                    buttonSelect(ActiveMenu, CurrentButtonOptions, OptionsLimit, menu);//Handles Visual Button Interactivity
                }
                if (CurrentButtonStart == 2)//Tutorial Option
                {
                    ActiveMenu = 2;
                    resetMenu();
                    menu = menuSet();
                    buttonSelect(ActiveMenu, 0, 1, menu);//Handles Visual Button Interactivity
                }
                if (CurrentButtonStart == 3)//Quit Option
                {
                    ActiveMenu = 3;
                    resetMenu();
                    menu = menuSet();
                    buttonSelect(ActiveMenu, CurrentButtonQuit, QuitLimit, menu);//Handles Visual Button Interactivity
                }
            }
        }
        //Options Menu
        else if (ActiveMenu == 1)
        {
            if (Input.GetKeyDown("q"))//Spin It!
            {
                menu = menuSet();
                CurrentButtonOptions = cycleMenu(CurrentButtonOptions, OptionsLimit);//Handles Scrolling Limit Of Menu
                buttonSelect(ActiveMenu, CurrentButtonOptions, OptionsLimit, menu);//Handles Visual Button Interactivity
            }
            if (Input.GetKeyDown("e"))//Twist It!
            {
                if (CurrentButtonOptions==0)//Volume Slider
                {
                    volume = sliderChange(volume);//Handles Scrolling Limit Of Slider
                    volumeSlider.GetComponent<Slider>().value = volume;//Sets Slider To Updated Value
                    SetVoulume(volume);//Sets Audio On AudioManager To The Changed Value
                }
            }
            if (Input.GetKeyDown("r"))//Bop It!
            {
                source.PlayOneShot(MenuSelect);
                if (CurrentButtonOptions == 1)
                {
                    ActiveMenu = 0;
                    resetMenu();
                    menu = menuSet();
                    buttonSelect(ActiveMenu, CurrentButtonStart, StartLimit, menu);//Handles Visual Button Interactivity
                }
            }

        }
        //Tutorial Menu
        else if (ActiveMenu == 2)
        {
            if (Input.GetKeyDown("r"))//Bop It!
            {
                source.PlayOneShot(MenuSelect);
                ActiveMenu = 0;
                resetMenu();
                menu = menuSet();
                buttonSelect(ActiveMenu, CurrentButtonStart, StartLimit, menu);//Handles Visual Button Interactivity
            }
        }
        //Quit Menu
        else if (ActiveMenu == 3)
        {
            if (Input.GetKeyDown("q"))//Spin It!
            {
                menu = menuSet();
                CurrentButtonQuit = cycleMenu(CurrentButtonQuit, QuitLimit);//Handles Scrolling Limit Of Menu
                buttonSelect(ActiveMenu, CurrentButtonQuit, QuitLimit, menu);//Handles Visual Button Interactivity
            }
            if (Input.GetKeyDown("r"))//Bop It!
            {
                source.PlayOneShot(MenuSelect);
                if (CurrentButtonQuit==0)
                {
                    ActiveMenu = 0;
                    resetMenu();
                    menu = menuSet();
                    buttonSelect(ActiveMenu, CurrentButtonStart, StartLimit, menu);//Handles Visual Button Interactivity
                }
                if(CurrentButtonQuit==1)
                {
                    CloseGame();
                }
            }
        }
    }
    //Used To Set The Menu Array
    public GameObject[][] menuSet()
    {
        GameObject[][] setup =
        {
            new GameObject[] { startButton1, startButton2, startButton3, startButton4},//Start Menu
            new GameObject[] { OptionsButton1, OptionsButton2, OptionsButton3},//Options Menu
            new GameObject[] { TutorialButton },//Tutorial Menu
            new GameObject[] { QuitButton1, QuitButton2 }//Quit Menu
        };
        return setup;
    }
    //Cycles Menus
    public int cycleMenu(int i,int limit)
    {
        i += 1;
        if (i==limit)
        {
            i = 0;
        }
        source.PlayOneShot(MenuCycle);
        return i;
    }
    //Changes Sliders
    public float sliderChange(float i)
    {
        i -= 0.05f;
        if (i < 0)
        {
            i = 1.0f;
        }
        source.PlayOneShot(MenuAdjust);
        return i;
    }
    //Handles Visual Button Interactivity
    public void buttonSelect(int menunum, int selected, int limit, GameObject[][] buttons)
    {
        for (int i = 0; i < limit; i++)
        {
            if (i == selected)
            {
                buttons[menunum][i].GetComponent<Button>().interactable = true;//Makes The Selected Button Look Interactible 
            }
            else
            {
                buttons[menunum][i].GetComponent<Button>().interactable = false;//Makes The Selected Button Look Not Interactible 
            }
        }
    }
    //Resets Every Menu's Value To Its Default Value
    public void resetMenu()
    {
        
        CurrentButtonStart = 0;
        CurrentButtonOptions = 0;
        CurrentButtonQuit = 0;
        //Disables UI Visibility  
        startMenu.SetActive(false);
        optionsMenu.SetActive(false);
        tutorialMenu.SetActive(false);
        quitMenu.SetActive(false);
        //Enables UI Visibility For Current Menu
        if (ActiveMenu == 0)
        {
            startMenu.SetActive(true);
        }
        else if (ActiveMenu == 1)
        {
            optionsMenu.SetActive(true);
        }
        else if (ActiveMenu == 2)
        {
            tutorialMenu.SetActive(true);
        }
        else if (ActiveMenu == 3)
        {
            quitMenu.SetActive(true);
        }
    }
    //Sends The Player To The Game Scene
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    //Sets The Value of Volume on the audioManager
    public void SetVoulume(float Volume)
    {
        PlayerPrefs.SetFloat("Volume", Volume);
        gameObject.GetComponent<AudioSource>().volume = Volume;
    }
    //Sets The Value of Difficulty on the DataManager
    public void SetDifficulty(int difficultySet)//Unused
    {
        dataManager.Difficulty = difficultySet;
    }
    //Closes The Game
    public void CloseGame()
    {
        Application.Quit();
    }

}