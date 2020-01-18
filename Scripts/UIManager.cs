using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Lose UI References")]
    [SerializeField]
    TextMeshProUGUI distanceTravelledText;
    [SerializeField]
    TextMeshProUGUI candiesEatenText;
    [SerializeField]
    TextMeshProUGUI headsRemainingText;

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
        
    }

    public void DisplayLoseUI()
    {
        Time.timeScale = 0;

        anim.SetTrigger("LoseUI");
    }

    #region LoseUI

    public void SetDistanceTravelled()
    {
        distanceTravelledText.text = 
            "Distance Travelled: " + 
            HorseManager.instance.GetDistanceTravelled() + 
            "m (+" + GameStateManager.instance.GetHorsePoints() +
            "xp)";
    }

    #endregion
}
