using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashScript : MonoBehaviour
{
    //Variables for handling attacks
    public float slash_duration = 0.1f;
    private float slash_cooldown = 0.5f;
    private float slash_distance_to_player = 1.6f;
    private bool can_slash = true;
    public int slash_dmg;

    public Color slash_active;
    public Color slash_inactive;
    public SpriteRenderer slash_renderer;
    public PolygonCollider2D slash_collider;

    void Start(){
        // Get the SpriteRenderer component attached to this GameObject.
        slash_renderer = GetComponent<SpriteRenderer>();
        slash_collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && string.Equals(WeaponSwitchScript.current_weapon_equipped, "axe")){
            //Swing axe
            Slash_Attack();
        }
    }

    private void Slash_Attack(){
        if (can_slash){
            Debug.Log("Axe slash!");
            can_slash = false;
            Face_Cursor();
            StartCoroutine(Slash_Duration());
            StartCoroutine(Slash_Cooldown());
        }
    }

    // Faces the slash hitbox towards the direction of the mouse
    private void Face_Cursor(){
        transform.localPosition = new Vector3(0,0,0);

        // Get the mouse position in world space.
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the sprite to the mouse.
        Vector3 direction = mousePosition - transform.position;

        // Calculate the angle in degrees.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the sprite to face the mouse.
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector3 newPosition = transform.position + (transform.right * slash_distance_to_player);
        transform.position = newPosition;
    }

    private IEnumerator Slash_Duration(){
        //Show the sword and turn on the collider
        slash_renderer.color = slash_active;
        slash_collider.enabled = true;
        yield return new WaitForSeconds(slash_duration);
        //Hide the sword and turn off the collider
        slash_renderer.color = slash_inactive;
        slash_collider.enabled = false;
    }

    private IEnumerator Slash_Cooldown(){
        yield return new WaitForSeconds(slash_cooldown);
        can_slash = true;
    }
}
