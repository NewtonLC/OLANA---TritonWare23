using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*

    NOT YET IMPLEMENTED, WIP

*/

public class CandleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && string.Equals(WeaponSwitchScript.current_weapon_equipped, "candelabra")){
            Debug.Log("Candelabra attack!");
        }
    }
}
