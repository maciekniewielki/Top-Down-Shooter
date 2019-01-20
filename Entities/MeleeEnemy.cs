using UnityEngine;
using System.Collections;
using System.Linq;

public class MeleeEnemy : Enemy, IDamageable
{
    public GameObject loot;
    public float lootDropChance;
    public FadingText scorePopUp;
    public float spotDistance;

    private Animator anim;

    protected override void Awake()
    {
        base.Awake();
        count++;
    }

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        InvokeRepeating("TryGetTarget", 0f, 0.3f);
    }

    void FixedUpdate()
    {
        if (target == null||dead)
            return;
        if (!IsInDamageCircle() && !IsAttacking())
        {
            MoveToPos((Vector2)target.transform.position);
            RotateToTarget((Vector2)target.transform.position);
        }
        else if (!IsAttacking())
        {
            BeginSwingAttack();
        }
        else
        {
            RotateToTarget((Vector2)target.transform.position);
        }
    }

    public override bool IsMoving()
    {
        return !IsAttacking() && !IsDying() &&anim.GetCurrentAnimatorStateInfo(0).length>0.3f;
    }

    bool IsAttacking()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
    }

    bool IsDying()
    {
        return anim.GetCurrentAnimatorStateInfo(0).IsTag("Death");
    }

    #region IDamageable implementation

    public void TakeDamage(float damage)
    {
        health -= damage;
        CheckForDeath();
    }

    public void Die()
    {
        dead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        string[] deaths = new string[] { "death1", "death2" };
        anim.Play(deaths[Random.Range(0, deaths.Length)]);
        count--;
        ScoreAndCashManager.Score += (int)scoreForKilling;
        ScoreAndCashManager.Cash += scoreForKilling / 10f;
        FadingText popUp = Instantiate(scorePopUp, transform.position, Quaternion.identity) as FadingText;
        string message = string.Format("+{0}", scoreForKilling);
        popUp.SetText(message);
    }

    public void DestroySelf()
    {
        if (Random.value < lootDropChance)
            Instantiate(loot, transform.position, Quaternion.identity);
        Destroy(gameObject); 
    }

    #endregion

    void CheckForDeath()
    {
        if (health <= 0f && !dead)
            Die();
    }

    bool IsInDamageCircle()
    {
        if (target == null)
            return true;
        else
            return (target.transform.position - transform.position).magnitude < stopMovingDistance;
    }


    void LookAtTarget()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
    }


    void TryGetTarget()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, spotDistance, 1<<10);
        if (cols.Length!=0)
        {
            target = cols.OrderBy(c => Vector2.Distance(transform.position, c.transform.position)).First().gameObject;
        }
        else
        {
            SwitchTargetToPlayer();
        }
    }

    public void BeginSwingAttack()
    {
        string[] attacks = new string[] { "attack1", "attack2", "attack3" };
        anim.Play(attacks[Random.Range(0, attacks.Length)]);
    }

    public void InflictDamageInFront()
    {
        var hits = Physics2D.RaycastAll(transform.position, transform.up, stopMovingDistance + 1.4f).Where(r => !r.collider.CompareTag("Enemy") && r.collider.GetComponent<IDamageable>() != null);
        foreach (RaycastHit2D hit in hits)
            hit.collider.GetComponent<IDamageable>().TakeDamage(damage);
    }
}
