using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int HP;
    public bool invincible = false;
    public float invincibility_duration = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision){
        if (collision.gameObject.CompareTag("Enemy") && !invincible)
        {
            EnemyHP enemyScript = collision.gameObject.GetComponent<EnemyHP>();

            if(enemyScript != null){
                TakeDamage(enemyScript.Enemy_dmg);
                Debug.Log(enemyScript.ID);
                Debug.Log(HP);
            }
        }
    }

    private void TakeDamage(int dmg){
        if (!invincible){
            HP -= dmg;

            invincible = true;
            StartCoroutine(RemoveInvincibility());
        }
    }

    private IEnumerator RemoveInvincibility(){
        yield return new WaitForSeconds(invincibility_duration);
        invincible = false;
    }
}
