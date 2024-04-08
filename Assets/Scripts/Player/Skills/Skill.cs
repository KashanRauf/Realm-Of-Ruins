using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
	public bool isActive;
	public float cooldownTime;
	public float wait;
	public int stacks;
	public int usages;
	// Combat: true, Purification: false
	public bool combat;
	public PlayerState state;
	public PlayerMovement player;
	public LayerMask targets;
	public Image fillBar;
	public Image icon;

	//public virtual void Initialize() { }

	public virtual void Invoke()
	{
		if (!isActive) return;
		if (usages >= stacks)
		{
			wait -= Time.deltaTime;
			return;
        }

		Debug.Log("Used a skill!");
        wait = cooldownTime;
	}

	public float CooldownProgress()
	{
		return ((cooldownTime - wait) / cooldownTime) * 100;
	}

	// Name this sm better
	public float CooldownFillBar()
	{
		// Debug.Log("Mod: " + cooldownTime % wait);
		return 1 - ((cooldownTime - wait % cooldownTime) / cooldownTime);
	}
}
