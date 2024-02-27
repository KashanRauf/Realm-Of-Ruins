using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // 0 = sword, 1 = shield, 2 = staff
    public float weaponType {  get; protected set; }
    public float baseDamage { get; protected set; }
    public float cooldownTime { get; protected set; }
    public float altCooldownTime { get; protected set; }
    public float wait { get; protected set; }


    public abstract void Attack();

    public abstract void AltAttack();

    public float CooldownProgress()
    {
        float time;
        if (cooldownTime < wait)
        {
            time = altCooldownTime;
        } 
        else
        {
            time = cooldownTime;
        }
        return ((time - wait) / time) * 100;
    }
}
