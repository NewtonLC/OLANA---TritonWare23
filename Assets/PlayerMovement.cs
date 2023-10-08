using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 direction;
    private float playerSpeed = 0;
    private const int MAX_PLAYER_SPEED = 5;
    private KeyCode[] MovementKeys = {KeyCode.W,KeyCode.A,KeyCode.S,KeyCode.D};

    void FixedUpdate()
    {
        //If a movement key is pressed, set speed to MAX_PLAYER_SPEED.
        //If a movement key is not pressed, then for each frame, lower player speed.
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){

        }
        direction = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        direction.Normalize();
        transform.Translate(direction*Time.deltaTime*playerSpeed);
    }

    bool is_Movement_Key_Pressed(){
        foreach(KeyCode movementKey in MovementKeys){

        }
        return false;
    }
}
