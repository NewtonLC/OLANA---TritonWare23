using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnScript : MonoBehaviour
{
    static public int current_wave = 0;
    static public bool intermission_active = false;
    static public int num_enemies_spawned = 0;

    //Enemies killed & Enemy point values
    static public int num_ghosts_killed = 0;
    static public int num_knights_killed = 0;
    static public int num_gargoyles_killed = 0;
    static public int ghost_points = 5;
    static public int knight_points = 10;
    static public int gargoyle_points = 15;

    public float base_enemy_spawn_interval;

    public GameObject ghost_prefab;
    public GameObject knight_prefab;
    public GameObject gargoyle_prefab;
    public GameObject player;

    private float x_left_bound = -28;    // Enemies can spawn anywhere from x_left_bound to x_right_bound
    private float x_right_bound = 28;    
    private float y_upper_bound = 3;     // Enemies can spawn anywhere from y_upper_bound to y_lower_bound
    private float y_lower_bound = -14;    

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if this object's scene is the newly loaded scene
        if (scene.name == gameObject.scene.name)
        {
            // The object's scene was loaded
            current_wave = 0;
            num_enemies_spawned = 0;
            num_ghosts_killed = 0;
            num_knights_killed = 0;
            num_gargoyles_killed = 0;
            intermission_active = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!intermission_active){
            current_wave++;
            StartCoroutine(SpawnWaveEnemies());
            intermission_active = true;
        }
        if(intermission_active && num_Enemies_Killed() >= num_enemies_spawned){
            intermission_active = false;
        }
        Debug.Log(num_Enemies_Killed() + " dead. " + num_enemies_spawned + " spawned.");
    }

    private IEnumerator SpawnWaveEnemies(){
        num_enemies_spawned += (current_wave*2)+5;
        for(int i = 0;i < (current_wave*2)+5;i++){
            yield return new WaitForSeconds(1);     //Problem code: exits program for some reason?
            Spawn();
        }
    }

    void Spawn()
    {
        // Randomly decides the spawn position.
        float xPos = Random.Range(x_left_bound, x_right_bound);
        float yPos = Random.Range(y_lower_bound, y_upper_bound);

        // sets spawn position
        Vector3 SpawnPos = new Vector3(xPos, yPos, 0);

        // Spawns the enemy
        GameObject Prefab = Choose_Enemy();
        GameObject spawnedEnemy = Instantiate(Prefab, SpawnPos, Quaternion.identity);
        spawnedEnemy.GetComponent<EnemyMovement>().Player = player;
        spawnedEnemy.GetComponent<EnemyMovement>().player = player.transform;
    }

    private GameObject Choose_Enemy(){
        float randomValue = Random.Range(0.0f,1.0f);                    //Returns random float from 0 to 1
        if (randomValue < 0.5f){                                        //50% chance of ghost spawn
            return ghost_prefab;
        } 
        else if (randomValue >= 0.5f && randomValue < 0.80f){           //30% chance of knight spawn
            return knight_prefab;
        } 
        else{                                                           //20% chance of gargoyle spawn
            return gargoyle_prefab;
        } 
    }

    private int num_Enemies_Killed(){
        return num_ghosts_killed + num_knights_killed + num_gargoyles_killed;
    }
}
