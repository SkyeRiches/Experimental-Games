using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Upon the game starting, it will grab the volume player pref and set the audio volume accordingly
/// </summary>
public class VolumeControl : MonoBehaviour
{
    
    void Start()
    {
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");
    }
}
