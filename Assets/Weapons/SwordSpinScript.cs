using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpinScript : MonoBehaviour
{

    //Variables for handling attacks
    public int spin_dmg;
    public float spin_attack_duration;         //How often enemies take damage while in the attack
    public float spin_duration;
    public float spin_cooldown;
    public bool can_spin = true;

    //Objects for handling the spin sprite
    public Color spin_active;
    public Color spin_inactive;
    public SpriteRenderer spin_renderer;
    public CircleCollider2D spin_collider;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        spin_renderer = GetComponent<SpriteRenderer>();
        spin_collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && string.Equals(WeaponSwitchScript.current_weapon_equipped, "sword")){
            Spin_Attack();
        }
    }

    private void Spin_Attack(){
        if (can_spin){
            Debug.Log("Spin!");
            can_spin = false;
            animator.Play("Player_Sword_Special", -1, 0f);
            StartCoroutine(Spin_Duration());
            StartCoroutine(Spin_Cooldown());
        }
    }

    private IEnumerator Spin_Duration(){
        //Show the sword and turn on the collider
        //spin_renderer.color = spin_active;
        spin_collider.enabled = true;
        animator.SetBool("isSpecial", true);
        yield return new WaitForSeconds(spin_duration);
        //Hide the sword and turn off the collider
        //spin_renderer.color = spin_inactive;
        spin_collider.enabled = false;
    }

    private IEnumerator Spin_Cooldown(){
        animator.SetBool("isSpecial", false);
        yield return new WaitForSeconds(spin_cooldown);
        can_spin = true;
    }
}
