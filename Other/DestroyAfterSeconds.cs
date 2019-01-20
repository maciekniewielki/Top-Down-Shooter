using UnityEngine;
using System.Collections;

public class DestroyAfterSeconds : MonoBehaviour 
{

    public float timeToDie;

	void Start () 
	{
        Invoke("DestroyMe", timeToDie);   
	}

    void DestroyMe()
    {
        Destroy(this.gameObject);
    }
}

