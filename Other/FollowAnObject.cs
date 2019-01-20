using UnityEngine;
using System.Collections;

public class FollowAnObject : MonoBehaviour 
{
    public Transform target;

	void FixedUpdate () 
	{
        if (target != null)
            transform.position = target.position;
        else
            Destroy(this.gameObject);
	}
}

