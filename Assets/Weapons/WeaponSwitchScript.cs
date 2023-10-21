using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitchScript : MonoBehaviour
{
    public Sprite axe_img;
    public Sprite candelabra_img;
    public Image img_renderer;

    static public string current_weapon_equipped = "candelabra";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            Switch_Weapon();
        }
    }

    private void Switch_Weapon(){
        switch(current_weapon_equipped){
            case "candelabra":
                current_weapon_equipped = "sword";
                img_renderer.sprite = axe_img;
                break;
            case "sword":
                current_weapon_equipped = "candelabra";
                img_renderer.sprite = candelabra_img;
                break;
            default:
                current_weapon_equipped = "candelabra";
                img_renderer.sprite = candelabra_img;
                break;
        }
    }
}
