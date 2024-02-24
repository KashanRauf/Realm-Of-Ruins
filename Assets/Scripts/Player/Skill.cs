using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
	public float cooldownTime { get; private set; } = 0.0f;
	public float wait { get; private set; }

	public void Invoke()
	{
		if (wait > 0.0f)
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
}
