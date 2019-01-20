using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class EntityMove : MonoBehaviour
{
	public float linearMovementSpeed;

	protected Rigidbody2D rb;

	protected virtual void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	public void MoveToPos(Vector2 destination)
	{
        Vector2 direction = (destination - (Vector2)transform.position).normalized;
        MoveInDirection(direction);
	}

	public virtual void MoveInDirection(Vector2 direction)
	{
        rb.MovePosition((Vector2)transform.position+direction.normalized * linearMovementSpeed * Time.fixedDeltaTime);
    }

	public void RotateToTarget(Vector2 target)
	{
		transform.rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(target.x, target.y, transform.position.z) - transform.position);
	}

	public void RotateInDirection(Vector2 direction)
	{
		transform.rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(direction.x, direction.y, transform.position.z));
	}

    public void StopMoving()
    {
        //rb.velocity = Vector2.zero;
    }
}
