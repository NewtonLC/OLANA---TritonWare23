using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool Options_Opened = false;
    public static bool other_OptionsOpened = false;

    //Put PauseMenu GameObject
    public GameObject Pause_MenuUI;
    public GameObject GUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused && !Options_Opened && !other_OptionsOpened)
            {
                Resume();
            }
            else if(GameIsPaused && Options_Opened && !other_OptionsOpened)
            {
                Options_Opened = false;
            }
            else if(GameIsPaused && other_OptionsOpened)
            {
                other_OptionsOpened = false;
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Za Warudo");
            Time.timeScale = 0f;
        }
    }

    public void Resume()
    {
        //Resume Back To Normal
        Pause_MenuUI.SetActive(false);
        GUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        //Freeze Everything
        Pause_MenuUI.SetActive(true);
        GUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void OptionsOpen()
    {
        Options_Opened = true;
    }

    public void OptionsClose()
    {
        Options_Opened = false;
    }

    public void otherOptionsOpen()
    {
        other_OptionsOpened = true;
    }

    public void MainMenu()
    {
        //Hardcoded MainMenu
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
