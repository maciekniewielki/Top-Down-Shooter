using UnityEngine;
using System.Collections;

public abstract class PlayerWeapon : MonoBehaviour 
{
	public float damage;

	protected abstract void Shoot();
	public void LookAtMouse()
	{
		Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.rotation=Quaternion.LookRotation(Vector3.forward, new Vector3(target.x, target.y, transform.position.z)-transform.position);
	}
}

