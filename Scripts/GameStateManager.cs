﻿using System.Collections;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoints()
    {

    }

    public void LoseGame()
    {
        UIManager.instance.DisplayLoseUI();
    }

    public float GetHorsePoints()
    {
        return HorseManager.instance.GetDistanceTravelled() * horsePointsMultiplier;
    }
}
