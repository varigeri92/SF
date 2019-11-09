using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Shockwave : Ability
{

	public override void FireAbility()
	{
		if (charges > 0) {
			base.FireAbility();
			charges--;
			Instantiate(ability.effect, transform);
		}
	}
}
