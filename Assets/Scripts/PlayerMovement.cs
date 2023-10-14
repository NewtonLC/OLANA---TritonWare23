using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 direction;
    private float playerSpeed = 0;
    public float MAX_PLAYER_SPEED = 5.0f;
    public float slowRate = 0.25f;
    private KeyCode[] MovementKeys = {KeyCode.W,KeyCode.A,KeyCode.S,KeyCode.D};

    void FixedUpdate()
    {
        //Code that lowers speed if no movement keys are pressed, so the player stops smoothly.
        if(is_Movement_Key_Pressed()){
            playerSpeed = MAX_PLAYER_SPEED;
        }
        else{
            playerSpeed -= slowRate;
        }

        //Code that causes movement
        direction = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        direction.Normalize();
        transform.Translate(direction*Time.deltaTime*playerSpeed);
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
