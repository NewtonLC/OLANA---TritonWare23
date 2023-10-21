using System.Collections;
using System.Collections.Generic;
using UnityEditor.Recorder;
using UnityEngine;
using UnityEngine.UI;

public class GUICooldown : MonoBehaviour
{
    [SerializeField] private Image basicAttackIcon;
    [SerializeField] private Image specialAbilityIcon;

    // For accessing cooldown variables in weapon scripts
    [SerializeField] private CandleAttackScript candelabraBasicAttack;
    [SerializeField] private CandleShieldScript candelabraSpecialAbility;
    [SerializeField] private SlashScript swordBasicAttack;
    [SerializeField] private SwordSpinScript swordSpecialAbility;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (System.String.Equals(WeaponSwitchScript.current_weapon_equipped, "candelabra"))
        {
            if (candelabraBasicAttack.can_attack)
            {
                basicAttackIcon.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                basicAttackIcon.color = new Color32(121, 121, 121, 255);
            }
            if (candelabraSpecialAbility.can_shield)
            {
                specialAbilityIcon.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                specialAbilityIcon.color = new Color32(121, 121, 121, 255);
            }
        }
        else
        {
            if (swordBasicAttack.can_slash)
            {
                basicAttackIcon.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                basicAttackIcon.color = new Color32(121, 121, 121, 255);
            }
            if (swordSpecialAbility.can_spin)
            {
                specialAbilityIcon.color = new Color32(255, 255, 255, 255);
            }
            else
            {
                specialAbilityIcon.color = new Color32(121, 121, 121, 255);
            }
        }
    }
}
