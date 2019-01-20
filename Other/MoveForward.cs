using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour 
{
	public float speed;

    private bool firstFrame;

    void Awake()
    {
        firstFrame = true;
    }

	void FixedUpdate ()
    {
        if (firstFrame)
        {
            firstFrame = false;
            return;
        }
		transform.Translate(Vector3.up * speed*Time.fixedDeltaTime);
	}
}

