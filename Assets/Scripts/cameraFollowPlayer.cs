using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollowPlayer : MonoBehaviour
{
    public Camera mainCam;
    public Transform player;
    //Ensures camera is on a different layer than game
    public Vector3 offset = new Vector3(0,0,-1);
    public Vector3 minBound, maxBound;
    [Range(1,5)]
    public float followSpeed;

    void Start()
    {
        transform.position = player.position;
        
    }

    void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 playerPos = player.position + offset;
        Vector3 boundPos = new Vector3(
            Mathf.Clamp(playerPos.x, minBound.x, maxBound.x),
            Mathf.Clamp(playerPos.y, minBound.y, maxBound.y),
            Mathf.Clamp(playerPos.z, -1, -10));

        transform.position = Vector3.Lerp(transform.position, boundPos, followSpeed * Time.deltaTime);

    }
}

