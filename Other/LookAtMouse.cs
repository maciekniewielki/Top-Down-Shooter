using UnityEngine;

public class LookAtMouse : MonoBehaviour 
{
	void FixedUpdate () 
	{
		Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation=Quaternion.LookRotation(Vector3.forward, new Vector3(target.x, target.y, transform.position.z)-transform.position);
	}
}

