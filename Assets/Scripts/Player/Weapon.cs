using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // 0 = sword, 1 = shield, 2 = staff
    public float weaponType {  get; protected set; }
    public float baseDamage { get; protected set; }
    public float cooldownTime { get; protected set; }
    public float wait { get; protected set; }


    public abstract void Attack();

    public float CooldownProgress()
    {
        return ((cooldownTime - wait) / cooldownTime) * 100;
    }
}
