using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = transform.root.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) // Parry
        {
            anim.SetTrigger("Parry");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("INCOMING");
        if (transform.root != collision.transform.root)
        {
            HeadJumpManager.instance.CandyEaten();
            Destroy(collision.gameObject);
        }
    }
}
