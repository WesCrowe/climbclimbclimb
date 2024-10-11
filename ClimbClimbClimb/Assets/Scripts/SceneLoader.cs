/* SceneLoader.cs
 * Author: Wesley Crowe
 * Script that invokes scene loading
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public MusicPlayer musicPlayer;

    public void Awake(){
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
    }

    /*public void loadSkiJump(){
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        SceneManager.LoadScene("ski_jump");
        //musicPlayer.SwapMusic(1);
    }*/

    //loads the next scene in the build order
    public void loadNextScene(){
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    //loads the scene given a given index of the build order
    public void loadScene(int sceneIndex){
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        SceneManager.LoadScene(sceneIndex);
    }

    //loads a tutorial scene specifically
    public void loadTutorial(){
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        SceneManager.LoadScene("tutorial");
        //musicPlayer.SwapMusic(1);
    }

    //quits the game
    public void quitGame(){
        Application.Quit();
    }

    //loads the main menu scene specifically
    public void backToMainMenu(){
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        SceneManager.LoadScene("title_screen");
    }

    public void optionsMenu(){
        SceneManager.LoadScene("Options");
    }

    public void toCredits(){
        SceneManager.LoadScene("Credits");
    }
}
