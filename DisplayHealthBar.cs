using UnityEngine;
using System.Collections;

public class DisplayHealthBar : MonoBehaviour
{
    public Transform healthBarParent;

    private Vector2 startingScale;

    void Start()
    {
        startingScale = healthBarParent.localScale;
        DisplayHealthPercent(2f);
    }

    public void DisplayHealthPercent(float percent)
    {
        if (percent >= 1f || percent<=0f)
            healthBarParent.localScale = Vector3.zero;
        else
            healthBarParent.localScale = new Vector2(startingScale.x, startingScale.y*percent);
    }
}
