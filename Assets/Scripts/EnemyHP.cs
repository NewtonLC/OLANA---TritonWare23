using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int HP;
    public int Enemy_dmg;
    public string ID;
    public bool is_spawning_in;

    //Currently. burning cannot stack.
    private bool is_burning;

    public GameObject burn_flame;

    public Animator enemyAnimator;
    public PolygonCollider2D enemy_collider;

    //To prevent the enemy from getting damaged on every frame in which it collides with the player's attack
    private bool invincible = false;

    // For accessing onGround boolean in EnemyMovement script
    [SerializeField] private EnemyMovement gargoyleMovement;

    void Start()
    {
        gargoyleMovement = this.GetComponent<EnemyMovement>();
        StartCoroutine(SpawnEnemyIn());
    }

    private IEnumerator SpawnEnemyIn(){
        enemy_collider.enabled = false;
        is_spawning_in = true;
        yield return new WaitForSeconds(1.2f);
        enemyAnimator.SetBool("enemySpawned", true);
        yield return new WaitForSeconds(0.6f);
        enemy_collider.enabled = true;
        is_spawning_in = false;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!System.String.Equals(ID, "gargoyle") || (System.String.Equals(ID, "gargoyle") && gargoyleMovement.onGround))
        { 
            if (collision.gameObject.CompareTag("PlayerSlash") && !invincible)
            {
                SlashScript slashScript = collision.gameObject.GetComponent<SlashScript>();

                if (slashScript != null) 
                {
                    TakeDamage(slashScript.slash_dmg, slashScript.slash_duration);
                    //Debug.Log("Enemy: " + ID + " | HP: " + HP);
                }
            }
            if (collision.gameObject.CompareTag("PlayerCandleAttack") && !invincible)
            {
                CandleAttackScript candleScript = collision.gameObject.GetComponent<CandleAttackScript>();

                if (candleScript != null)
                {
                    TakeDamage(candleScript.attack_dmg, candleScript.attack_duration);
                    if(!is_burning){
                        StartCoroutine(TakeBurnDamage(candleScript.burn_dmg, candleScript.burn_duration, candleScript.burn_cooldown));
                    }
                    //Debug.Log("Enemy: " + ID + " | HP: " + HP);
                }
            }
            if (collision.gameObject.CompareTag("PlayerSwordSpin") && !invincible)
            {
                SwordSpinScript spinScript = collision.gameObject.GetComponent<SwordSpinScript>();

                if (spinScript != null) 
                {
                    TakeDamage(spinScript.spin_dmg, spinScript.spin_attack_duration);
                    //Debug.Log("Enemy: " + ID + " | HP: " + HP);
                }
            }
        }
    }

    private void TakeDamage(int dmg, float duration){
        if(!invincible){
            invincible = true;
            HP -= dmg;
            StartCoroutine(DamageCooldown(duration));
            if(HP <= 0){
                StartCoroutine(EnemyDeath());
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

    private IEnumerator TakeBurnDamage(int dmg, float duration, float cooldown){
        is_burning = true;
        for(float i = 0;i < duration;i += cooldown){
            yield return new WaitForSeconds(cooldown);
            BurnFlash();
            HP -= dmg;
            if(HP <= 0){
                StartCoroutine(EnemyDeath());
            }
            else{
                EnemyHurt(dmg);
            }
        }
        is_burning = false;
    }

    private void BurnFlash(){
        GameObject burn_sprite = Instantiate(burn_flame, gameObject.transform.position, Quaternion.identity);
        Destroy(burn_sprite, 0.3f);
    }

    private IEnumerator EnemyDeath(){
        Debug.Log("Enemy " + ID + " died");
        enemy_collider.enabled = false;
        enemyAnimator.SetTrigger("enemyIsDead");
        yield return new WaitForSeconds(1);
        Increment_Enemies_Killed();
        Destroy(gameObject);
    }

    private void Increment_Enemies_Killed(){
        switch(ID){
            case "ghost":
                EnemySpawnScript.num_ghosts_killed++;
                break;
            case "knight":
                EnemySpawnScript.num_knights_killed++;
                break;
            case "gargoyle":
                EnemySpawnScript.num_gargoyles_killed++;
                break;
        }
    }

    private void EnemyHurt(int dmg){
        Debug.Log("Enemy " + ID + " took " + dmg + " damage.");
    }
}
