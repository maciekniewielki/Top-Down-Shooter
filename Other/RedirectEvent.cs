using UnityEngine;
using System.Collections;

public class RedirectEvent : MonoBehaviour
{
    public MeleeEnemy parent;

    public void Attack()
    {
        parent.InflictDamageInFront();
    }

    public void DestroySelf()
    {
        parent.DestroySelf();
    }
}
