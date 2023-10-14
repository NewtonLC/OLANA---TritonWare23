using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //An EnemyObject's ID will determine whether it is a ghost, suit of armor, vampire, gargoyle.
    //Each enemy has a different movement/behavior pattern
    public string ID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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

    }

    // Method that handle's armor movement patterns
    // Armors will ... (Lunge at player?)
    private void ArmorMovement(){

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
