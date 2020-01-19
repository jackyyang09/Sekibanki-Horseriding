using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Upgrades
{
    HeadJumpHeight,
    AdditionalHeads,
    Balance,
    GravityEffect
}

public class UpgradeManager : MonoBehaviour
{
    [SerializeField]
    float costScaleFactor = 1.25f;

    [SerializeField]
    int maxUpgrades = 10;
    float[] upgradeCosts;

    [SerializeField]
    int startingUpgradeCost = 50;

    [Header("Upgrades")]

    [Range(0, 5)]
    [SerializeField]
    int headJumpHeight = 1;

    [Range(0, 5)]
    [SerializeField]
    int additionalHeads = 1;

    [Range(0, 5)]
    [SerializeField]
    int balance = 1;

    [Range(0, 5)]
    [SerializeField]
    int gravityEffect = 1;

    int cachedUpgradePrice;

    public static UpgradeManager instance;

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
        upgradeCosts = new float[(int)maxUpgrades];
        upgradeCosts[0] = startingUpgradeCost;
        for (int i = 1; i < maxUpgrades; i++)
        {
            upgradeCosts[i] = upgradeCosts[i - 1] * costScaleFactor;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetUpgradeLevel(Upgrades u)
    {
        switch (u)
        {
            case Upgrades.HeadJumpHeight:
                return headJumpHeight;
            case Upgrades.AdditionalHeads:
                return additionalHeads;
            case Upgrades.Balance:
                return balance;
            case Upgrades.GravityEffect:
                return gravityEffect;
            default:
                return 0;
        }
    }

    public void SetUpgradeLevel(Upgrades u, int requestedLevel)
    {
        switch (u)
        {
            case Upgrades.HeadJumpHeight:
                headJumpHeight = requestedLevel;
                break;
            case Upgrades.AdditionalHeads:
                additionalHeads = requestedLevel;
                break;
            case Upgrades.Balance:
                balance = requestedLevel;
                break;
            case Upgrades.GravityEffect:
                gravityEffect = requestedLevel;
                break;
        }
    }

    public int GetUpgradePrice(int upgradeLevel)
    {
        return (int)upgradeCosts[upgradeLevel];
    }

    /// <summary>
    /// A hack, but we all know this will work
    /// </summary>
    /// <returns></returns>
    public int GetCachedUpgradePrice()
    {
        return cachedUpgradePrice;
    }

    public int GetRequestedUpgradeCost(Upgrades u, int requestedLevel)
    {
        float totalCosts = 0;
        for (int i = GetUpgradeLevel(u); i < requestedLevel; i++)
        {
            totalCosts += GetUpgradePrice(i);
        }
        cachedUpgradePrice = (int)totalCosts; // Save it cause we know we're gonna use it
        return cachedUpgradePrice;
    }
}
