using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int HP;
    [SerializeField] private Healthbar healthbar;
    public bool invincible = false;
    public float invincibility_duration = 1;

    [SerializeField] private GameOver gameOver;

    // Start is called before the first frame update
    void Start()
    {
        healthbar.setMaxHealth(HP);
    }

    // Update is called once per frame
    void Update()
    {
        if (HP == 0)
        {
            gameOver.setUp();
        }
    }

    private void OnTriggerStay2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Enemy") && !invincible)
        {
            EnemyHP enemyScript = collision.gameObject.GetComponent<EnemyHP>();

            if(enemyScript != null && enemyScript.enemy_collider.enabled == true){
                TakeDamage(enemyScript.Enemy_dmg);
            }
        }
    }

    private void TakeDamage(int dmg){
        if (!invincible){
            HP -= dmg;
            healthbar.setHealth(HP);

            invincible = true;
            StartCoroutine(RemoveInvincibility());
        }
    }

    private IEnumerator RemoveInvincibility(){
        yield return new WaitForSeconds(invincibility_duration);
        invincible = false;
    }
}
