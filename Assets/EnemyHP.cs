using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    public int HP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int dmg){
        HP -= dmg;
        if(HP <= 0){
            //Death anim
            EnemyDeath();
        }
        else{
            //Damage anim
            EnemyHurt();
        }
    }

    private void EnemyDeath(){

    }

    private void EnemyHurt(){

    }
}
