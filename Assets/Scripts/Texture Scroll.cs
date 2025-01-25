using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : MonoBehaviour
{

    public float speedX = 0.1f;
    public float speedY = 0.1f;
    private float transformX;
    private float transformY;

    // Start is called before the first frame update
    void Start()
    {
        transformX = GetComponent<Renderer>().material.mainTextureOffset.x;
        transformY = GetComponent<Renderer>().material.mainTextureOffset.y;
    }

    // Update is called once per frame
    void Update()
    {
        transformX += Time.deltaTime * speedX;
        transformY += Time.deltaTime * speedY;
        GetComponent<Renderer>().material.SetTextureOffset("_BaseMap", new Vector2(transformX, speedY));
    }
}
