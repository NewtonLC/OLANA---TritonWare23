using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleShieldScript : MonoBehaviour
{
    //Variables for handling shield ability
    public float shield_duration;
    public float shield_cooldown;
    private bool can_shield = true;

    //Objects for handling the shield sprite
    public Color shield_active;
    public Color shield_inactive;
    public SpriteRenderer shield_renderer;
    public CircleCollider2D shield_collider;

    // Start is called before the first frame update
    void Start()
    {
        shield_renderer = GetComponent<SpriteRenderer>();
        shield_collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && string.Equals(WeaponSwitchScript.current_weapon_equipped, "candelabra")){
            Candle_Shield();
        }
    }

    private void Candle_Shield(){
        if (can_shield){
            Debug.Log("Shield!");
            can_shield = false;
            StartCoroutine(Shield_Duration());
        }
    }

    private IEnumerator Shield_Duration(){
        //Show the sword and turn on the collider
        shield_renderer.color = shield_active;
        shield_collider.enabled = true;
        yield return new WaitForSeconds(shield_duration);
        //Hide the sword and turn off the collider
        shield_renderer.color = shield_inactive;
        shield_collider.enabled = false;
        StartCoroutine(Shield_Cooldown());
    }

    private IEnumerator Shield_Cooldown(){
        yield return new WaitForSeconds(shield_cooldown);
        can_shield = true;
    }
}
