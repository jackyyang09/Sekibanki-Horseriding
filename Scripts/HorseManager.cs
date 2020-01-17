using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseManager : MonoBehaviour
{
    /// <summary>
    /// Speed at which the horse moves AKA distance point gain in meters
    /// Lowest speed (walking) averages 6400 meters per hour -> 1.78 meters per second
    /// </summary>
    [SerializeField]
    float speed;

    [SerializeField]
    float pointsToAdd;

    public static HorseManager instance;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        pointsToAdd += speed * Time.deltaTime;
    }
}
