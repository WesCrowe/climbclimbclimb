/* MusicPlayer.cs
 * Author: Wesley Crowe
 * Script to control the behavior of a musicplaying gameobject
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    
    /*private AudioSource src;

    public AudioClip[] music;
    

    void Awake(){
        DontDestroyOnLoad(transform.gameObject);
        src = gameObject.GetComponent<AudioSource>();
    }

    void PlayMusic(){
        src.Play();
    }

    void StopMusic(){
        src.Stop();
    }

    public void SwapMusic(int num){
        StopMusic();
        src.clip = music[num];
        PlayMusic();
    }

    public void FindCam(){
        transform.SetParent(GameObject.Find("Main Camera").transform);
        transform.SetLocalPositionAndRotation(new Vector3(0,0,0), Quaternion.identity);
    }*/

    private static MusicPlayer instance; //the current instance of the MusicPlayer script
    private AudioSource audioSource; //the audioscource compoment
    public AudioClip[] musicClips; // Array to hold different music clips
    private int currentSceneIndex = -1;

    void Awake()
    {
        // Singleton pattern implementation
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            gameObject.AddComponent<AudioListener>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //When the MusicPlayer object is enabled
    void OnEnable()
    {
        // Subscribe to scene loaded and unloaded events
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    //When the MusicPlayer object is disabled
    void OnDisable()
    {
        // Unsubscribe from scene loaded and unloaded events
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    //When a scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //get the index
        int sceneIndex = scene.buildIndex;

        // Check if the scene has a corresponding music clip
        if (sceneIndex < musicClips.Length && musicClips[sceneIndex] != null)
        {
            if (currentSceneIndex != sceneIndex)
            {
                currentSceneIndex = sceneIndex;
                audioSource.clip = musicClips[sceneIndex];
                audioSource.Play();
            }
        }
    }

    void OnSceneUnloaded(Scene scene)
    {
        // Stop playing music when the scene is unloaded
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
