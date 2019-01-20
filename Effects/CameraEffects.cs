using UnityEngine;
using System.Collections;

public class CameraEffects : MonoBehaviour {


    public float cameraShakeIntensivity = 0.1f;
    public float cameraShakeTime = 1f;
    private float cameraShakeRemainingTime = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (cameraShakeRemainingTime > 0f)
        {
            cameraShakeRemainingTime -= Time.deltaTime;
            if (cameraShakeRemainingTime > 0f)
                transform.position += new Vector3(Random.value * cameraShakeIntensivity * (cameraShakeRemainingTime / cameraShakeTime), Random.value * cameraShakeIntensivity * (cameraShakeRemainingTime / cameraShakeTime), -10);
        }
    }

    public void DoShake()
    {
		cameraShakeIntensivity=0.1f;
        cameraShakeRemainingTime = cameraShakeTime;
    }

	public void IconShake()
	{
		cameraShakeIntensivity=1.3f;
		cameraShakeRemainingTime=cameraShakeTime;
	}
}
