using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class PlayerPerksManager : MonoBehaviour
{
    public static PlayerPerksManager instance;
    public static Dictionary<PerkType, Perk> perkList;

    public static float bonusPercentAttackSpeed;
    public static float bonusPercentDamage;
    public static float bonusPercentMovementSpeed;

    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        ResetPerks();
    }

    static PlayerPerksManager()
    {
        perkList = new Dictionary<PerkType, Perk>();

        perkList.Add(PerkType.ATTACK_SPEED_100, new Perk(PerkType.ATTACK_SPEED_100, 40, "Attack speed +100%"));
        perkList.Add(PerkType.DAMAGE_50, new Perk(PerkType.DAMAGE_50, 50, "Damage +50%"));
        perkList.Add(PerkType.MOVEMENT_SPEED_30, new Perk(PerkType.MOVEMENT_SPEED_30, 100, "Movement Speed +30%"));
    }

    public static void ResetPerks()
    {
        foreach (PerkType perk in Enum.GetValues(typeof(PerkType)))
            perkList[perk].Applied = false;
        bonusPercentAttackSpeed = 0f;
        bonusPercentDamage = 0f;
        bonusPercentMovementSpeed = 0f;
    }

    public static void ApplyPerk(PerkType perk)
    {

        if (perkList[perk].Applied == true)
            return;

        perkList[perk].Applied = true;
        if (perk == PerkType.ATTACK_SPEED_100)
            bonusPercentAttackSpeed += 1f;
        else if (perk == PerkType.DAMAGE_50)
            bonusPercentDamage += 0.5f;
        else if (perk == PerkType.MOVEMENT_SPEED_30)
            bonusPercentMovementSpeed += 0.3f;

    }

    public static bool IsAnyPerkAvailableToBuy()
    {
        return perkList.Any(p => !p.Value.Applied && ScoreAndCashManager.Cash >= p.Value.cost);
    }
	
}
