/* VolumeAdjuster.cs
 * Author: Wesley Crowe
 * Script that allows the player to change how loud game audio is
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeAdjuster : MonoBehaviour
{
    
    public AudioSource musicPlayer;

    void Awake()
    {
        //find the audioplayer
        musicPlayer = GameObject.Find("MusicPlayer").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        musicPlayer.volume = gameObject.GetComponent<Slider>().value;
    }
}
