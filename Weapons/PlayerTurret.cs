using UnityEngine;
using System.Collections;
using System;
using System.Linq;

[ExecuteInEditMode]
[DisallowMultipleComponent]
public class PlayerTurret : MonoBehaviour, IDamageable
{
    public static int count;

    public float bulletDamage;
    public float maxHealth;
    public bool active;
    public float visionRadius;
    public GameObject gun;
    public float targetRefreshTime;
    public float rotateSpeed;
    public MoveForward missile;
    public Transform missileSpawnLocation;
    public float timeBetweenShots;
    public float projectileSpeed;
    public SpriteRenderer rangeCircle;

    private float health;
    private Transform target;
    private float lastShootTime;

    #region IDamageable Implementation
    public void Die()
    {
        count--;
        Destroy(this.gameObject);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        CheckForDeath();
        BroadcastMessage("DisplayHealthPercent", health / maxHealth, SendMessageOptions.DontRequireReceiver);
    }
    #endregion

    void CheckForDeath()
    {
        if (health <= 0)
            Die();
    }

    void Awake()
    {
        count++;
    }

    void Start()
    {
        rangeCircle.transform.localScale = GetScaleOfRangeCircle();
        health = maxHealth;
        lastShootTime = Time.time;
        Invoke("DisableRangeCircle", 3f);
    }

    bool TryGetTarget()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll((Vector2)transform.position, visionRadius);
        if (hitColliders == null)
            return false;

        if (!hitColliders.Any(c => c.CompareTag("Enemy") && !c.GetComponent<Enemy>().dead))
            return false;

        target = hitColliders.Where(c => c.CompareTag("Enemy") && !c.GetComponent<Enemy>().dead).OrderBy(c => Vector2.Distance((Vector2)transform.position, (Vector2)c.transform.position)).First().transform;
        return true;
    }

    void Update()
    {
#if UNITY_EDITOR
        rangeCircle.transform.localScale = GetScaleOfRangeCircle();
#endif
        if (Game.state != GameState.IN_PROGRESS)
            return;
        if (!TryRotateToTarget())
            TryGetTarget();
        TryShoot();
    }

    bool TryRotateToTarget()
    {

        if (target == null)
            return false;

        if (Vector2.Distance(target.position, transform.position) > visionRadius + 1f)
        {
            target = null;
            return false;
        }

        Vector3 predictedMovement = CalculatePredictedMovement(projectileSpeed);
        Debug.DrawLine(transform.position, predictedMovement);
        RotateToTarget(predictedMovement);
        return true;
    }

    Vector2 CalculatePredictedMovement(float projectileSpeed)
    {
        if (!target.GetComponent<Enemy>().IsMoving())
            return target.position;

        EntityMove targetMove = target.GetComponent<EntityMove>();
        Vector2 currentPos = transform.position;
        Vector2 currentTargetPos = target.transform.position;
        Vector2 targetDirection = target.transform.up;
        float distance = Vector2.Distance(currentPos, currentTargetPos);
        float timeToHit = distance / projectileSpeed;
        Vector2 predictedTargetPos = currentTargetPos + targetMove.linearMovementSpeed * targetDirection.normalized * timeToHit;
        for (int ii = 0; ii < 3; ii++)
        {
            distance = Vector2.Distance(currentPos, predictedTargetPos);
            timeToHit = distance / projectileSpeed;
            predictedTargetPos = currentTargetPos + targetMove.linearMovementSpeed * targetDirection.normalized * timeToHit;
        }
        return predictedTargetPos;
    }

    void TryShoot()
    {
        if (!TryRotateToTarget())
            return;
        if (lastShootTime + timeBetweenShots < Time.time && target != null)
        {
            if (target.GetComponent<Enemy>().dead)
            {
                TryGetTarget();
                return;
            }
            Shoot();
            lastShootTime = Time.time;
        }
    }

    void Shoot()
    {
        MoveForward projectile = Instantiate(missile, missileSpawnLocation.position, missileSpawnLocation.transform.rotation) as MoveForward;
        projectile.speed = projectileSpeed;
        projectile.GetComponent<Bullet>().damage = bulletDamage;
    }

    public void RotateToTarget(Vector2 target)
    {
        Vector2 direction = new Vector3(target.x, target.y, gun.transform.position.z) - gun.transform.position;
        RotateInDirection(direction.normalized);
    }

    public void RotateInDirection(Vector2 direction)
    {
        gun.transform.localRotation = Quaternion.LookRotation(Vector3.forward, new Vector3(direction.x, direction.y, 0));
    }

    void DisableRangeCircle()
    {
        rangeCircle.gameObject.SetActive(false);
    }

    Vector3 GetScaleOfRangeCircle()
    {
        Vector3 scale = transform.lossyScale;
        Bounds spriteBounds = rangeCircle.sprite.bounds;
        return new Vector3(1 / scale.x, 1 / scale.y, 0f) / spriteBounds.extents.x * visionRadius;
    }
}

