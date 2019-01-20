using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TooltipMessage : MonoBehaviour
{
    private Text tooltip;

	void Awake()
    {
        tooltip = GetComponent<Text>();
	}

    public void SetText(string textToDisplay)
    {
        tooltip.text = textToDisplay.Replace("\\n", "\n");
    }

    public void ClearText()
    {
        tooltip.text = "";
    }

    public void AddText(string text)
    {
        tooltip.text += text;
    }
}
