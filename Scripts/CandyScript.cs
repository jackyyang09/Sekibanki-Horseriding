using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyScript : MonoBehaviour
{
    private Rigidbody2D candyBody;
    public GameObject head;

    // Start is called before the first frame update
    void Start()
    {
        candyBody = GetComponent<Rigidbody2D>();
        head = GameObject.Find("Head");
    }

    // Update is called once per frame
    void Update()
    {
        // Move candy to left
        candyBody.velocity = new Vector2(-5f, 0f);
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject == CandyManager.instance.head){
        Debug.Log("touch head");
        }
    }
}
