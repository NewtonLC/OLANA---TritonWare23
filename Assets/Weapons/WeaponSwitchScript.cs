using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitchScript : MonoBehaviour
{
    [SerializeField] private Image equippedIcon;
    [SerializeField] private Image sheathedIcon;
    [SerializeField] private Image basicAttackIcon;
    [SerializeField] private Image specialAbilityIcon;

    [SerializeField] private Sprite candelabraIcon;
    [SerializeField] private Sprite swordIcon;
    [SerializeField] private Sprite candelabraBasicAttackIcon;
    [SerializeField] private Sprite candelabraSpecialAbilityIcon;
    [SerializeField] private Sprite swordBasicAttackIcon;
    [SerializeField] private Sprite swordSpecialAbilityIcon;

    static public string current_weapon_equipped = "candelabra";

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)){
            Switch_Weapon();
            Debug.Log("switched weapon!");
        }
    }

    private void Switch_Weapon(){
        switch(current_weapon_equipped){
            case "candelabra":
                
                animator.SetBool("SwordEquipped", true);
                Debug.Log(animator.GetBool("SwordEquipped"));
                current_weapon_equipped = "sword";
                equippedIcon.sprite = swordIcon;
                sheathedIcon.sprite = candelabraIcon;
                basicAttackIcon.sprite = swordBasicAttackIcon;
                specialAbilityIcon.sprite = swordSpecialAbilityIcon;
                break;
            case "sword":
                animator.SetBool("SwordEquipped", false);
                Debug.Log(animator.GetBool("SwordEquipped"));
                current_weapon_equipped = "candelabra";
                equippedIcon.sprite = candelabraIcon;
                sheathedIcon.sprite = swordIcon;
                basicAttackIcon.sprite = candelabraBasicAttackIcon;
                specialAbilityIcon.sprite = candelabraSpecialAbilityIcon;
                break;
            default:
                animator.SetBool("SwordEquipped", false);
                current_weapon_equipped = "candelabra";
                equippedIcon.sprite = candelabraIcon;
                sheathedIcon.sprite = swordIcon;
                basicAttackIcon.sprite = candelabraBasicAttackIcon;
                specialAbilityIcon.sprite = candelabraSpecialAbilityIcon;
                break;
        }
    }
}
