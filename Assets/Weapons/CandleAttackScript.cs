using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleAttackScript : MonoBehaviour
{
    //Variables for handling attacks
    public float attack_duration = 0.1f;
    private float attack_cooldown = 0.5f;
    private float attack_distance_to_player = 1f;
    public bool can_attack = true;
    public int attack_dmg;

    //Variables for handling burn DoT
    public int burn_dmg;
    public float burn_duration;
    public float burn_cooldown;

    public Color attack_active;
    public Color attack_inactive;
    public SpriteRenderer attack_renderer;
    public CircleCollider2D attack_collider;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        attack_renderer = GetComponent<SpriteRenderer>();
        attack_collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && string.Equals(WeaponSwitchScript.current_weapon_equipped, "candelabra")){
            Candle_Attack();
        }
    }

    //Manages candle attack
    private void Candle_Attack(){
        if (can_attack){
            Debug.Log("Candelabra attack!");
            //animator.Play("Player_Candelabra_Attack", -1, 0f);
            can_attack = false;
            Face_Cursor();
            StartCoroutine(Attack_Duration());
            StartCoroutine(Attack_Cooldown());
        }
    }

    // Faces the attack hitbox towards the direction of the mouse
    private void Face_Cursor(){
        transform.localPosition = new Vector3(0,0,0);

        // Get the mouse position in world space.
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Turns the sprite left or right, depending on the mouse's position
        if(mousePosition.x < transform.position.x){
            transform.rotation = Quaternion.Euler(new Vector3(0,0,180));        //Turn to face the left
        }
        else{
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));          //Turn to face the right
        }

        Vector3 newPosition = transform.position + (transform.right * attack_distance_to_player);
        transform.position = newPosition;
    }

    private IEnumerator Attack_Duration(){
        //Show the sword and turn on the collider
        //attack_renderer.color = attack_active;
        attack_collider.enabled = true;
        animator.SetBool("isAttack", true);
        yield return new WaitForSeconds(attack_duration);
        //Hide the sword and turn off the collider
        //attack_renderer.color = attack_inactive;
        animator.SetBool("isAttack", false);
        attack_collider.enabled = false;
    }

    private IEnumerator Attack_Cooldown(){
        yield return new WaitForSeconds(attack_cooldown);
        can_attack = true;
    }
}
