using UnityEngine;
using System.Collections;

public class PostRenderEffect : MonoBehaviour
{
    public Material effect;

    public void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        Graphics.Blit(src, dest, effect);
    }
}
