using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyScript : MonoBehaviour
{
    private Rigidbody2D candyBody;

    bool pointRegistered;

    // Start is called before the first frame update
    void Start()
    {
        candyBody = GetComponent<Rigidbody2D>();
        // Move candy to left
        candyBody.velocity = new Vector2(-5f, 0f);
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pointRegistered)
        {
            // Magic numbers lolololol
            if (transform.position.x < -2.5f)
            {
                HeadJumpManager.instance.CandyAvoided();
                pointRegistered = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        // Get player gameobject from CandyManager
        if (other.gameObject == CandyManager.instance.head){
            HeadJumpManager.instance.HitHead();
        }
    }
}
