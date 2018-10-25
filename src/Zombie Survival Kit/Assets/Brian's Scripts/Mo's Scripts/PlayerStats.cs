using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{

	// Use this for initialization
	void Start ()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
	}

    void OnEquipmentChanged(EquipmentItem oldItem, EquipmentItem newItem)
    {
        if (newItem != null)
        {
            armour.AddToStat(newItem.defenceModifier);
            armour.AddToStat(newItem.attackModifier);
        }

        if (oldItem != null)
        {
            armour.RemoveFromStat(oldItem.defenceModifier);
            armour.RemoveFromStat(oldItem.attackModifier);
        }

    }

    public override void Die()
    {
        base.Die();
        //Kill the player, game over screen, reset
        HealthManager.instance.KillPlayer();
    }

}
