using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeObject : MonoBehaviour
{
    [SerializeField]
    Upgrades upgradeType;

    [SerializeField]
    string description;

    Transform[] upgradeToggles;

    // Start is called before the first frame update
    void Start()
    {
        upgradeToggles = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            upgradeToggles[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyUpgrade(int requestedLevel)
    {
        if (requestedLevel <= UpgradeManager.instance.GetUpgradeLevel(upgradeType)) return;
        int price = UpgradeManager.instance.GetCachedUpgradePrice();
        if (GameStateManager.instance.GetPoints() > UpgradeManager.instance.GetCachedUpgradePrice())
        {
            GameStateManager.instance.SubtractPoints(price);
            UIManager ui = UIManager.instance;
            ui.SetXP();
            ui.HideCost();

            for (int i = UpgradeManager.instance.GetUpgradeLevel(upgradeType); i < requestedLevel; i++)
            {
                upgradeToggles[i].GetComponentInChildren<Image>().enabled = false;
                upgradeToggles[i].GetChild(0).GetChild(0).gameObject.SetActive(true);
            }

            UpgradeManager.instance.SetUpgradeLevel(upgradeType, requestedLevel);
        }
    }

    public void HoveringOverMe(int x)
    {
        // Can you buy this?
        if (x > UpgradeManager.instance.GetUpgradeLevel(upgradeType))
        {
            UIManager ui = UIManager.instance;
            ui.SetUpgradeDescription(description);
            ui.SetUpgradeCost(upgradeType, x);
            ui.SetXP();
        }
    }

    public void NotHovering()
    {
        //UIManager ui = UIManager.instance;
        //ui.SetUpgradeDescription("");
        //ui.HideCost();
    }
}
