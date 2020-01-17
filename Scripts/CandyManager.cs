using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyManager : MonoBehaviour
{
    public static CandyManager instance;
    // GameObject to be spawned
    public GameObject candy;
    // Tracks time until next spawn
    private float timer;
    // The time between spawns
    public float timeToSpawn;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            // A unique case where the Singleton exists but not in this scene
            if (instance.gameObject.scene.name == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // Set up timer
        timer = timeToSpawn;
    }

    // Update is called once per frame
    void Update()
    {
        // If timer is over, spawn candy
        if (timer <= 0){
            Instantiate (candy, new Vector2(transform.position.x, Random.Range(1,4)), Quaternion.identity);
            timer = timeToSpawn;
        }else{
            // Reduce time on timer
            timer -= Time.deltaTime;
        }
    }
}
