using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    static public int current_wave = 0;
    static public bool intermission_active = false;

    static public int num_enemies_spawned = 0;
    static public int num_enemies_killed = 0;

    public float base_enemy_spawn_interval;

    public GameObject ghost_prefab;
    public GameObject knight_prefab;
    public GameObject gargoyle_prefab;
    public GameObject player;

    private float x_left_bound = -17;    // Enemies can spawn anywhere from x_left_bound to x_right_bound
    private float x_right_bound = 17;    
    private float y_upper_bound = 4;     // Enemies can spawn anywhere from y_upper_bound to y_lower_bound
    private float y_lower_bound = -6;     

    // Update is called once per frame
    void Update()
    {
        if(!intermission_active){
            current_wave++;
            StartCoroutine(SpawnWaveEnemies());
            intermission_active = true;
        }
    }

    private IEnumerator SpawnWaveEnemies(){
        for(int i = 0;i < (current_wave*2)+5;i++){
            yield return new WaitForSeconds(base_enemy_spawn_interval * Random.Range(0.7f, 1.3f));
            Spawn();
            num_enemies_spawned++;
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
        if (Random.value < 0.5f){           //50% chance of ghost spawn
            return ghost_prefab;
        } 
        else if (Random.value >= 0.5f && Random.value < 0.80f){           //30% chance of knight spawn
            return knight_prefab;
        } 
        else{           //20% chance of gargoyle spawn
            return gargoyle_prefab;
        } 
    }
}
