using UnityEngine;
using System.Collections;

public class MachinegunBullet : Bullet
{
    void Awake()
    {
        GetComponent<MoveForward>().speed = speed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            return;

        IDamageable target = collision.GetComponent<IDamageable>();
        if (target == null)
            Vanish();
        else
        {
            target.TakeDamage(damage);
            Vanish();
        }
    }

    void Vanish()
    {
        Destroy(gameObject);
    }
}

