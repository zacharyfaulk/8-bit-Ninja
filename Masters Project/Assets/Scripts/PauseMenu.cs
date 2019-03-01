using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This is used to manage the pause menu - opened with the esc key
/// </summary>

public class PauseMenu : MonoBehaviour
{
    public bool GamePaused = false;     //determines if the game is paused
    public GameObject pauseUI;          //UI object
    public Transform global;            //global object

    void Start()
    {
        global = GameObject.FindWithTag("Global").transform;    //get the global object
    }


    void Update ()
    {
        //opens and closes the pause menu with the esc key
		if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    //open the pause UI and set the game time to 0
    void Pause()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    //close the pause UI and resume the game time back to normal (1)
    public void Resume ()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }

    //restarts the game
    public void Restart()
    {
        global.transform.GetComponent<GlobalVar>().easyMode = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Courtyard");
    }

    //restarts the game in easymode - the player can't die
    public void EasyRestart()
    {
        global.transform.GetComponent<GlobalVar>().easyMode = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Courtyard");
    }

    //closes the application
    public void QuitGame()
    {
        Application.Quit();
    }


    /// This was supposed to be used for a toggle that would switch between easy and regualr mode
    /// there was a unity bug that activated the toggle when the menu opened that caused this to not work
    /// as intended so the easy mode button was implemented instead
    /*public void EasyMode(bool toggle)
     {
         if (toggle == true)
         {

             //Debug.Log("easymode on");
             global.transform.GetComponent<GlobalVar>().easyMode = true;
             Time.timeScale = 1f;
             SceneManager.LoadScene("Courtyard");

         }
         else
         {
             //Debug.Log("easymode off");
             global.transform.GetComponent<GlobalVar>().easyMode = false;
         }


     }*/
}
