using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //MainMenu GameObject
    public GameObject Main_MenuUI;

    //Input the name of the first level
    public string Start_Level_Name;

    public void StartGame()
    {
        SceneManager.LoadScene(Start_Level_Name);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
