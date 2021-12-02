using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public int Difficulty = 1;//Sets a Default Value For The Difficulty
    private void Awake()//This Makes This Object Unable To Be Destroyed On Scene Change Meaning Variables Can Be Transfered Between Scenes
    {
        DontDestroyOnLoad(this);
    }
}
