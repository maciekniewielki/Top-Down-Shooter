using UnityEngine;
using System.Collections;

public abstract class Enemy : EntityMove
{
    public static int count;
	public float health;
	public float damage;
	public float stopMovingDistance;	//Distance to the target at which the enemy should stop
	public bool attackTheTarget;
    public bool dead;
    public float scoreForKilling;

    protected GameObject target;

	protected void SwitchTargetToPlayer()
	{
        if (Game.state != GameState.IN_PROGRESS)
            return;

        if (Player.instance != null)
            target = Player.instance.gameObject;
        else
            target = null;
	}

    public abstract bool IsMoving();
}

