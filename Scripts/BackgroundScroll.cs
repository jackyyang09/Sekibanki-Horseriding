using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    // speed of bg
    float speed = 1f;
    Renderer bgRenderer;

    // Start is called before the first frame update
    void Start()
    {
        bgRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // change offset of texture
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0f);
    }
}
