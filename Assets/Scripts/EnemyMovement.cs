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

    //Enemy RigidBody2D Reference
    //Rigidbody2D rb;

    //Variable for candelabra ability
    public bool running_away = false;
    private float run_away_duration = 2;

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

    // Variables for gargoyle movement
    [SerializeField] private float gargoyleHorizontalSpeed;
    [SerializeField] private float gargoyleVeritcalSpeed;
    [SerializeField] private float gargoyleHeightAbovePlayer;

    // Variables for gargoyle stomp
    private bool stompIsActive = false;
    [SerializeField] private float stompSpeed;
    private bool stompIsCharging = false;
    [SerializeField] private float stompChargeTime;
    private bool stompMovingDown = false;
    public bool onGround = false;
    [SerializeField] private float timeOnGround;
    private bool stompMovingUp = false;
    private bool stompOnCooldown = false;
    [SerializeField] private float stompCooldownTime;

    //Experimental
    //Variable for Knockback
    //public float knockback_force = 5f;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        player = Player.transform;
        //rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D collision){
        if((collision.gameObject.CompareTag("PlayerCandleShield"))){
            StartCoroutine(Run_Away());
        }
    }

    private IEnumerator Run_Away(){
        running_away = true;
        yield return new WaitForSeconds(run_away_duration);
        running_away = false;
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

    //Experimental Code for Knockback Implementation
   /* private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerSlash"))
        {
            SlashScript slashScript = collision.gameObject.GetComponent<SlashScript>();

            if (slashScript != null)
            {
                Vector3 directionTo = transform.position - player.position;
                rb.AddForce(directionTo.normalized * knockback_force, ForceMode2D.Impulse);
                knockbackDistance(0.2f);
            }
        }
    }

    private IEnumerator knockbackDistance(float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("Velocity Should Reset");
        rb.velocity = Vector3.zero;
    }*/


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
        Vector3 directionTo = player.position - transform.position;

        Vector3 flyHeight = new Vector3(0, gargoyleHeightAbovePlayer, 0);
        Vector3 horizontalSpeed = new Vector3(gargoyleHorizontalSpeed, 1, 1);
        Vector3 verticalSpeed = new Vector3(1, gargoyleVeritcalSpeed, 1);

        if (!stompIsActive)
        {
            if (running_away)
            {
                transform.position -= Vector3.Scale(Vector3.Scale((directionTo + flyHeight).normalized, horizontalSpeed), verticalSpeed);
            }
            else
            {
                // Move toward a certain height above the player's position
                transform.position += Vector3.Scale(Vector3.Scale((directionTo + flyHeight).normalized, horizontalSpeed), verticalSpeed);
                // Initiate stomp if above player and the ability is not on cooldown
                if ((Mathf.Abs(transform.position.y) <= Mathf.Abs((float)(player.position.y + flyHeight.y + 0.1))) && !stompOnCooldown)
                {
                    StartCoroutine(gargoyleStomp());
                }
            }
        }
        else
        {
            if (!stompIsCharging)
            {
                if (stompMovingDown)
                {
                    transform.position += Vector3.down * stompSpeed;
                }
                else if (stompMovingUp)
                {
                    transform.position += Vector3.up * stompSpeed;
                }
            }
        }


    }

    // Manages the time and respective booleans when a gargoyle is carrying out a stomp
    private IEnumerator gargoyleStomp()
    {
        stompIsActive = true;
        stompIsCharging = true;
        yield return new WaitForSeconds(stompChargeTime);
        stompIsCharging = false;
        stompMovingDown = true;
        yield return new WaitForSeconds(stompDuration());
        stompMovingDown = false;
        onGround = true;
        yield return new WaitForSeconds(timeOnGround);
        onGround = false;
        stompMovingUp = true;
        yield return new WaitForSeconds(stompDuration());
        stompMovingUp = false;
        stompIsActive = false;
        StartCoroutine(gargoyleStompCooldown());
    }

    // Returns the distance divided by the speed divided by the number of frames per second
    private float stompDuration()
    {
        return (gargoyleHeightAbovePlayer / stompSpeed / 50);
    }

    // Manages the time and boolean when a gargoyle's stomp ability is on cooldown
    private IEnumerator gargoyleStompCooldown()
    {
        stompOnCooldown = true;
        yield return new WaitForSeconds(stompCooldownTime);
        stompOnCooldown = false;
    }


    // Method that handle's vampires' movement patterns
    // Vampires will ... (move directly at player?)
    private void VampireMovement(){

    }
}
