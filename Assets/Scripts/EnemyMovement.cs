using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //An EnemyObject's ID will determine whether it is a ghost, suit of armor, vampire, gargoyle.
    //Each enemy has a different movement/behavior pattern
    public string ID;
    public float speed;

    //Player reference
    public GameObject Player;
    public Transform player;

    //Variable for candelabra ability
    public bool running_away = false;

    //Variable for knight's lunge
    public float lunge_distance;
    public float lunge_length;
    public float lunge_speed;
    public float lunge_startup;
    public float lunge_cooldown;
    private bool lunge_is_charging = false;
    private bool lunge_is_active = false;
    private bool lunge_is_cooled_down = true;
    private Vector3 lunge_direction;

    //Variable for ghost modifier
    public float ghost_speedRate = 1.003f;

    // Start is called before the first frame update
    void Start()
    {
        player = Player.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(ID){
            case "ghost":
                GhostMovement();
                break;
            case "armor":
                ArmorMovement();
                break;
            case "gargoyle":
                GargoyleMovement();
                break;
            case "vampire":
                VampireMovement();
                break;
        }
    }

    // Method that handle's ghost movement patterns
    // Ghosts will ... (move directly at player?)
    private void GhostMovement(){
        Vector3 directionTo = player.position - transform.position;
        
        //Speeds up to the player unless running away
        if (running_away)
        {
            //Reset speed/slow down, depends on candelabra implementation
            transform.position -= directionTo.normalized * speed;
        }
        else
        {
            //Default Movement
            speed *= ghost_speedRate;
            transform.position += directionTo.normalized * speed;
        }


    }

    // Method that handle's armor movement patterns
    // Armors will ... (Lunge at player?)
    private void ArmorMovement(){
        Vector3 directionTo = player.position - transform.position;
        
        // Moves the enemy towards the player every frame unless they're running away
        // direction = target - origin;
        if (running_away){
            transform.position -= directionTo.normalized * speed;
        }
        else{
            if(!lunge_is_active && !lunge_is_charging){
                transform.position += directionTo.normalized * speed;
            }
            else if (lunge_is_charging){ //Take the current direction towards the player
                lunge_direction = directionTo.normalized;
            }
            else if (lunge_is_active){   //Charge forward in the direction obtained while charging
                transform.position += lunge_direction * lunge_speed;
            }
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if(distanceToPlayer < lunge_distance && lunge_is_cooled_down){
            StartCoroutine(ArmorLunge(lunge_startup));
            StartCoroutine(ArmorLungeCooldown(lunge_cooldown));
        }
    }

    private IEnumerator ArmorLunge(float startup){
        lunge_is_charging = true;
        yield return new WaitForSeconds(startup);
        lunge_is_charging = false;
        lunge_is_active = true;
        yield return new WaitForSeconds(lunge_duration());
        lunge_is_active = false;
    }

    private IEnumerator ArmorLungeCooldown(float cooldown){
        lunge_is_cooled_down = false;
        yield return new WaitForSeconds(cooldown);
        lunge_is_cooled_down = true;
    }

    //Returns the distance divided by the speed divided by the number of frames per second
    private float lunge_duration(){     
        return (lunge_length / lunge_speed) / 50;
    }

    // Method that handle's gargoyle movement patterns
    // Gargoyles will ... (Begin in a "flying" state until they reach the player OR take damage. Then, move normally)
    private void GargoyleMovement(){

    }

    // Method that handle's vampires' movement patterns
    // Vampires will ... (move directly at player?)
    private void VampireMovement(){

    }
}
