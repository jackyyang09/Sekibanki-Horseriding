using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyScript : MonoBehaviour
{
    private Rigidbody2D candyBody;

    // Start is called before the first frame update
    void Start()
    {
        candyBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move candy to left
        candyBody.velocity = new Vector2(-5f, 0f);
    }

    void OnTriggerEnter2D(Collider2D other){
        // Get player gameobject from CandyManager
        if(other.gameObject == CandyManager.instance.head){
            HeadJumpManager.instance.playerLives -= 1;
            Debug.Log("Lives left: " + HeadJumpManager.instance.playerLives);
        }
    }
}
