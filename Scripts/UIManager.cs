﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public enum UIState
    {
        Default = 0,
        LossDisplay,
        PressToContinue,
        Upgrades
    }

    [SerializeField]
    UIState currentState;

    [Header("Lose UI References")]
    [SerializeField]
    TextMeshProUGUI distanceTravelledText;
    [SerializeField]
    TextMeshProUGUI candiesAvoidedText;
    [SerializeField]
    TextMeshProUGUI candiesEatenText;
    [SerializeField]
    TextMeshProUGUI totalPointsText;

    [Header("Upgrade UI References")]
    [SerializeField]
    GameObject upgradeWindow;
    [SerializeField]
    TextMeshProUGUI xpText;
    [SerializeField]
    TextMeshProUGUI costText;
    [SerializeField]
    TextMeshProUGUI descriptionText;

    Animator anim;

    public static UIManager instance;

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

        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown) // Skip lol
        {
            switch (currentState)
            {
                case UIState.LossDisplay:
                    SetAllLoseText();
                    anim.SetTrigger("SkipLoseUI");
                    currentState = UIState.PressToContinue;
                    break;
                case UIState.PressToContinue:
                    GameStateManager.instance.AddPoints();
                    anim.SetTrigger("PressToContinue");
                    upgradeWindow.SetActive(true);
                    currentState = UIState.Upgrades;
                    SetXP();
                    break;
            }
        }
    }

    #region LoseUI

    public void DisplayLoseUI()
    {
        currentState = UIState.LossDisplay;
        Time.timeScale = 0;
        anim.SetTrigger("LoseUI");
    }

    public void SetDistanceTravelled()
    {
        distanceTravelledText.text = 
            HorseManager.instance.GetDistanceTravelled() + 
            "m (+" + GameStateManager.instance.GetHorsePoints() +
            "xp)";
    }

    public void SetCandiesAvoided()
    {
        candiesAvoidedText.text =
                    HeadJumpManager.instance.GetCandyAvoided() +
                    "m (+" + GameStateManager.instance.GetCandyAvoidedPoints() +
                    "xp)";
    }

    public void SetCandiesEaten()
    {
        candiesEatenText.text =
            HeadJumpManager.instance.GetCandyEaten() +
            "m (+" + GameStateManager.instance.GetCandyEatenPoints() +
            "xp)";
    }

    public void SetTotalPoints()
    {
        totalPointsText.text =
            "Total Score: +" +
            GameStateManager.instance.GetPointsToBeAdded() +
            "xp";
    }

    public void SetAllLoseText()
    {
        GameStateManager.instance.ClearPointsBuffer();
        SetDistanceTravelled();
        SetCandiesAvoided();
        SetCandiesEaten();
        SetTotalPoints();
    }

    #endregion

    #region UgpradeUI

    public void SetUpgradeDescription(string desc)
    {
        descriptionText.text = desc;
    }

    public void SetUpgradeCost(Upgrades u, int requestedLevel)
    {
        costText.gameObject.SetActive(true);
        costText.text = (UpgradeManager.instance.GetRequestedUpgradeCost(u, requestedLevel)).ToString();
    }

    public void SetXP()
    {
        xpText.text = GameStateManager.instance.GetPoints().ToString();
    }

    public void HideCost()
    {
        costText.gameObject.SetActive(false);
    }

    #endregion
}
