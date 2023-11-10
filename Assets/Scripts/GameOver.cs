using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private string mainMenu;
    [SerializeField] private string game;

    [SerializeField] private AudioSource battleMusic;
    [SerializeField] private AudioSource gameOverMusic;

    public TMP_Text ScoreText;


    /*
    Ghosts killed:	0	x      5		total


    Knights killed:	0	x      10	total


    Gargoyles killed:	0	x      15	total


    Total killed:		0			total

    */
    public void setUp()
    {
        gameObject.SetActive(true);
        ScoreText.text = "Ghosts killed:\t" + EnemySpawnScript.num_ghosts_killed + "\tx      " + EnemySpawnScript.ghost_points + "\t\t" + (EnemySpawnScript.ghost_points*EnemySpawnScript.num_ghosts_killed) + "\n\n\n";
        ScoreText.text += "Knights killed:\t" + EnemySpawnScript.num_knights_killed + "\tx      " + EnemySpawnScript.knight_points + "\t" + (EnemySpawnScript.knight_points*EnemySpawnScript.num_knights_killed) + "\n\n\n";
        ScoreText.text += "Gargoyles killed:\t" + EnemySpawnScript.num_gargoyles_killed + "\tx      " + EnemySpawnScript.gargoyle_points + "\t" + (EnemySpawnScript.gargoyle_points*EnemySpawnScript.num_gargoyles_killed) + "\n\n\n";
        ScoreText.text += "Total killed:\t\t" + (EnemySpawnScript.num_ghosts_killed+EnemySpawnScript.num_knights_killed+EnemySpawnScript.num_gargoyles_killed) + "\t\t\t" + ((EnemySpawnScript.ghost_points*EnemySpawnScript.num_ghosts_killed)+(EnemySpawnScript.knight_points*EnemySpawnScript.num_knights_killed)+(EnemySpawnScript.gargoyle_points*EnemySpawnScript.num_gargoyles_killed));
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
