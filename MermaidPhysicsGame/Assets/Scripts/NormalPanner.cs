using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPanner : MonoBehaviour
{
    float scrollSpeed = 0.02f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_TextureSample0", new Vector2(0, offset));
    }
}
