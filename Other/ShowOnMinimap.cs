using UnityEngine;
using System.Collections;

public class ShowOnMinimap : MonoBehaviour 
{
    public Transform minimap;
    public GameObject prefab;
    public Color boxColor;

    void Start () 
	{
        GameObject sprite = (GameObject)Instantiate(prefab ,transform.position, transform.rotation);
        sprite.GetComponent<SpriteRenderer>().color = boxColor;
        sprite.GetComponent<FollowAnObject>().target = transform;
        sprite.transform.SetParent(minimap);
    }
}

