using UnityEngine;
using System.Collections;

public class PlayerTurretMissile : Bullet
{
    public GameObject explosion;
    public float cameraShakeIntensivity;
    public float cameraShakeTime;
    
    private float cameraShakeRemainingTime;

    void Awake()
    {
        GetComponent<MoveForward>().speed = speed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            return;

        IDamageable target = collision.GetComponent<IDamageable>();
        if(target!=null&&Player.instance!=null)
        {
            GameObject boom=Instantiate(explosion, collision.transform.position, Quaternion.Euler(0f, 0f, Random.value*360f)) as GameObject;
            boom.GetComponent<AudioSource>().volume = 5 / Vector2.Distance(transform.position, Player.instance.transform.position);
            target.TakeDamage(damage);
            Camera.main.GetComponent<CameraEffects>().DoShake();
            Vanish();
        }
    }

    void Vanish()
    {
        Destroy(gameObject);
    }
}

