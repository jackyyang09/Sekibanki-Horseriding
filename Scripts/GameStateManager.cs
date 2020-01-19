using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    /// <summary>
    /// Points but it'll be truncated as an integer in game (Cheat Engine users BTFO)
    /// </summary>
    [SerializeField]
    float points;

    [SerializeField]
    float horsePointsMultiplier = 1;
    [SerializeField]
    float candiesAvoidedMultiplier = 10;
    [SerializeField]
    float candiesEatenMultiplier = 100;

    int pointsToBeAdded; // Add up our total temporarily

    public static GameStateManager instance;

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
        pointsToBeAdded = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {

    }

    public int GetPoints()
    {
        return (int)points;
    }

    public void AddPoints()
    {
        points += pointsToBeAdded;
        ClearPointsBuffer();
    }

    public void SubtractPoints(int amount)
    {
        if (amount > points) Debug.LogError("THATS REALLY EXPENSIVE BRO ARE YOU SURE????");
        points -= amount;
    }

    public void ClearPointsBuffer()
    {
        pointsToBeAdded = 0;
    }

    public int GetPointsToBeAdded()
    {
        return pointsToBeAdded;
    }

    public void LoseGame()
    {
        UIManager.instance.DisplayLoseUI();
    }

    public int GetHorsePoints()
    {
        int newPoints = Mathf.RoundToInt(HorseManager.instance.GetDistanceTravelled() * horsePointsMultiplier);
        pointsToBeAdded += newPoints;
        return newPoints;
    }

    public int GetCandyAvoidedPoints()
    {
        int newPoints = Mathf.RoundToInt(HeadJumpManager.instance.GetCandyAvoided() * candiesAvoidedMultiplier);
        pointsToBeAdded += newPoints;
        return newPoints;
    }

    public int GetCandyEatenPoints()
    {
        int newPoints = Mathf.RoundToInt(HeadJumpManager.instance.GetCandyEaten() * candiesEatenMultiplier);
        pointsToBeAdded += newPoints;
        return newPoints;
    }
}
