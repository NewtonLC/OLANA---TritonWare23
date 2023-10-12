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
        //Code that lowers speed if no movement keys are pressed, so the player stops smoothly.
        if(is_Movement_Key_Pressed()){
            playerSpeed = MAX_PLAYER_SPEED;
        }
        else{
            playerSpeed -= 0.25f;
        }

        //Code that causes movement
        direction = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        direction.Normalize();
        transform.Translate(direction*Time.deltaTime*playerSpeed);

        //TODO: Add sword-swinging functionality, if player clicks then run sword anim
    }

    bool is_Movement_Key_Pressed(){
        foreach(KeyCode movementKey in MovementKeys){
            if(Input.GetKey(movementKey)){
                return true;
            }
        }
        return false;
    }
}
