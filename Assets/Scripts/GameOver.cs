using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private string mainMenu;
    [SerializeField] private string game;

    [SerializeField] private AudioSource battleMusic;
    [SerializeField] private AudioSource gameOverMusic;
    public void setUp()
    {
        gameObject.SetActive(true);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(game);
    }

}
