using UnityEngine;
using System.Collections;

public class ImageFloat : MonoBehaviour {

    // Use this for initialization
    public Bounds bounds;
    public float acceleration;
    public float initialSpeed;
    public float initialDistance;

    private Vector3 speed; 

	void Start ()
    {
        this.transform.position = this.bounds.center + new Vector3(Random.value, Random.value).normalized * initialDistance;
        speed = new Vector3(Random.value, Random.value).normalized * initialSpeed;
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.speed += (this.bounds.center - this.transform.position) *this.acceleration;
        if (this.bounds.Contains(this.transform.position+this.speed))
            this.transform.Translate(this.speed);
	
	}
}
