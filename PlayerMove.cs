using UnityEngine;
using System.Collections;

public class PlayerMove : EntityMove
{
	PlayerWeapon weapon;

    void Start()
    {
        weapon = GetComponentInChildren<PlayerWeapon>();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void MoveInDirection(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + direction.normalized * linearMovementSpeed * (PlayerPerksManager.bonusPercentMovementSpeed + 1) * Time.fixedDeltaTime);
    }

    void FixedUpdate()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        float verticalMove = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(horizontalMove, verticalMove).normalized;
        MoveInDirection(direction);
        RotateInDirection(direction);
        if (weapon != null)
			weapon.LookAtMouse();
	}
		
}
