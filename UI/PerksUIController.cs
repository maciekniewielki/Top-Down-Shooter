using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PerksUIController : MonoBehaviour
{
    public TooltipMessage tooltip;

    private Dictionary<PerkType, Image> perkButtons;

    void Awake()
    {
        perkButtons = new Dictionary<PerkType, Image>();
    }

    void Start()
    {
        ClearTooltipText();
    }

    public void ClickedOnPerk(PerkType type)
    {
        if (PlayerPerksManager.perkList[type].Applied)
            return;
        else
        {
            if (PlayerPerksManager.perkList[type].cost > ScoreAndCashManager.Cash)
                return;

            BuyPerk(type);
        }
    }

    void BuyPerk(PerkType perk)
    {
        ScoreAndCashManager.Cash -= PlayerPerksManager.perkList[perk].cost;
        PlayerPerksManager.ApplyPerk(perk);
        SetPerkActiveInUI(perk);
    }

    void SetPerkActiveInUI(PerkType type)
    {
        perkButtons[type].color = new Color(1f, 1f, 1f, 1f);
    }

    public void RegisterPerkButton(PerkType typeToRegister, Image reference)
    {
        perkButtons.Add(typeToRegister, reference);
    }

    public void SetTooltipTextToPerk(PerkType type)
    {
        tooltip.SetText(PlayerPerksManager.perkList[type].Description);
    }

    public void ClearTooltipText()
    {
        tooltip.SetText("Press \"p\" again to close the window");
    }
}
