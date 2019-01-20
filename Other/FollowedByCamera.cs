using UnityEngine;
using System.Collections;

public class FollowedByCamera : MonoBehaviour 
{
	
	void FixedUpdate () 
	{
        Camera.main.transform.position = transform.position + new Vector3(0, 0, -10f);
	}
}

