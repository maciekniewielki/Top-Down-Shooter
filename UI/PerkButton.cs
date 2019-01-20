using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[RequireComponent(typeof(Image))]
public class PerkButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public PerkType type;
    public PerksUIController controller;

	void Start ()
    {
        controller.RegisterPerkButton(type, GetComponent<Image>());
	}


    public void ShowThisPerkOnTooltip()
    {
        controller.SetTooltipTextToPerk(type);
    }

	void Update ()
    {
	
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowThisPerkOnTooltip();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        controller.ClearTooltipText();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        controller.ClickedOnPerk(type);
        ShowThisPerkOnTooltip();
    }
}
