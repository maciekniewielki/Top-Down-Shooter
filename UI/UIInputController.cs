using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIInputController : MonoBehaviour
{
    public RectTransform perksParent;
    public Text perkAvailabilityTip;

    private bool perksVisible;
    private bool showOrHideCoroutineRunning;
	
	void Start ()
    {
        
	}
	
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
            TogglePerks();
        if (!showOrHideCoroutineRunning && !perksVisible)
            InvokeRepeating("UpdatePerkAvailabilityTip", 1f, 1f);

    }

    IEnumerator ShowPerksWindow()
    {
        CancelInvoke("UpdatePerkAvailabilityTip");
        perkAvailabilityTip.text = "";

        showOrHideCoroutineRunning = true;
        perksVisible = true;

        for (int ii = 0; ii < 60; ii++)
        {
            perksParent.anchoredPosition += new Vector2(0, 4);
            yield return null;
        }
        showOrHideCoroutineRunning = false;
    }

    IEnumerator HidePerksWindow()
    {
        showOrHideCoroutineRunning = true;
        perksVisible = false;

        for (int ii = 0; ii < 60; ii++)
        {
            perksParent.anchoredPosition += new Vector2(0, -4);
            yield return null;
        }
        showOrHideCoroutineRunning = false;
    }

    void UpdatePerkAvailabilityTip()
    {
        if (PlayerPerksManager.IsAnyPerkAvailableToBuy())
            perkAvailabilityTip.text = "You have some perks to buy. Press \"p\" to open perk menu";
        else
            perkAvailabilityTip.text = "";
    }

    void TogglePerks()
    {
        if (showOrHideCoroutineRunning)
            return;

        if (perksVisible)
            StartCoroutine(HidePerksWindow());
        else
            StartCoroutine(ShowPerksWindow());
    }
}
