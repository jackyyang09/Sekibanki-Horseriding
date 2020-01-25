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
    Vector2 speedRange;

    [SerializeField]
    float timeAlive;

    Animator anim;

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
        timeAlive += Time.deltaTime;
    }

    public void UpdateSpeed(int level)
    {
        float newSpeed = (float)level / 10f;
        speed = Mathf.Lerp(speedRange.x, speedRange.y, newSpeed);
        anim.SetFloat("HorseSpeed", Mathf.Lerp(1, 2, newSpeed));
    }

    public void ResetTime()
    {
        timeAlive = 0;
    }

    public float GetTimeAlive()
    {
        return timeAlive;
    }

    public float GetDistanceTravelled()
    {
        return (float)System.Decimal.Round((System.Decimal)(timeAlive * speed), 2);
    }

    public float GetSpeed()
    {
        return speed;
    }
}