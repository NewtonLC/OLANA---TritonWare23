using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int HP;
    public int Enemy_dmg;
    public string ID;

    //To prevent the enemy from getting damaged on every frame in which it collides with the player's attack
    private bool invincible = false;

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision){
        if (collision.gameObject.CompareTag("PlayerSlash") && !invincible)
        {
            SlashScript slashScript = collision.gameObject.GetComponent<SlashScript>();

            if(slashScript != null){
                TakeDamage(slashScript.slash_dmg, slashScript.slash_duration);
                Debug.Log("Enemy: " + ID + " | HP: " + HP);
            }
        }
    }

    public void TakeDamage(int dmg, float duration){
        if(!invincible){
            invincible = true;
            HP -= dmg;
            StartCoroutine(DamageCooldown(duration));
            if(HP <= 0){
                EnemyDeath();
            }
            else{
                EnemyHurt(dmg);
            }
        }
    }

    private IEnumerator DamageCooldown(float duration){
        yield return new WaitForSeconds(duration);
        invincible = false;
    }

    private void EnemyDeath(){
        Debug.Log("Enemy " + ID + " died");
        Destroy(gameObject);
    }

    private void EnemyHurt(int dmg){
        Debug.Log("Enemy " + ID + " took " + dmg + " damage.");
    }
}
