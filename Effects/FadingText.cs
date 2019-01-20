using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(TextMesh))]
[RequireComponent(typeof(MeshRenderer))]
public class FadingText : MonoBehaviour
{
    public float transparency;

    private TextMesh text;
    private Vector2 initialPosition;
    private MeshRenderer mRend;
    
    public float Transparency
    {
        get
        {
            return transparency;
        }

        set
        {
            transparency = value;
        }
    }

	void Awake ()
    {
        initialPosition = transform.position;
        text = GetComponent<TextMesh>();
        mRend = GetComponent<MeshRenderer>();
	}
	
	void LateUpdate ()
    {
        transform.position += (Vector3)initialPosition;
        mRend.material.color = new Color(mRend.material.color.r, mRend.material.color.g, mRend.material.color.b, Transparency);
    }

    public void SetText(string text)
    {
        this.text.text = text;  //I'm not proud of this...
    }

    public void SetSize(float size)
    {

    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
