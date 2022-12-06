using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public float scrollSpeed;

    private Renderer matRenderer;
    private Vector2 savedOffset;

    void Start()
    {
        matRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        float x = Mathf.Repeat(Time.time * scrollSpeed, 1);
        Vector2 offset = new Vector2(x, 0);
        matRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}