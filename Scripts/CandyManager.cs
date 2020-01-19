using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyManager : MonoBehaviour
{
    public static CandyManager instance;
    // GameObject to be spawned
    public GameObject[] candy;

    public Vector2 candySpawnRange;

    // GameObject for player
    public GameObject head;

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
        Invoke("SpawnCandy", Random.Range(candySpawnRange.x, candySpawnRange.y));
    }

    // Update is called once per frame
    //void Update()
    //{
    //
    //}

    public void SpawnCandy()
    {
        Instantiate(candy[Random.Range(0, candy.Length)], new Vector2(transform.position.x, /*Random.Range(1,4)*/1), Quaternion.identity);
        Invoke("SpawnCandy", Random.Range(candySpawnRange.x, candySpawnRange.y));
    }

    public void DestroyAllCandies()
    {
        foreach (CandyScript c in FindObjectsOfType<CandyScript>())
        {
            Destroy(c.gameObject);
        }
    }
}
