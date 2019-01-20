using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class MinimapAdjust : MonoBehaviour
{
    public Rect baseRect;

    private Camera minimapCamera;

    void AdjustCamera()
    {
        float correction = (16f / 9f) / Camera.main.aspect;
        Rect adjustedRect = new Rect(baseRect.x, baseRect.y + (1 - baseRect.y) * (1 - 1 / correction), baseRect.width, baseRect.height);
        minimapCamera.rect = adjustedRect;
    }

	void Start ()
    {
        minimapCamera = GetComponent<Camera>();
        baseRect = minimapCamera.rect;

        AdjustCamera();
	}
}