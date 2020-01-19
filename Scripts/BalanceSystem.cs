using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceSystem : MonoBehaviour
{
    [Header("Physics Lol")]

    /// <summary>
    /// Will be a quadratic function -(trueBalance = mbalance^2) where trueBalance is between -1 and 1 and m is stability factor
    /// </summary>
    [Range (-1, 1)]
    [SerializeField]
    float trueBalance = 0;

    float balance = 0;

    /// <summary>
    /// The m in y = mx^2
    /// </summary>
    [SerializeField]
    [Range(0.1f, 2)]
    float stabilityFactor = 1;

    [SerializeField]
    Vector2 stabilityUpgrade = new Vector2(1, 0.5f);

    /// <summary>
    /// Changes your balance based on key press
    /// </summary>
    [SerializeField]
    float influence = 0.5f;

    /// <summary>
    /// To prevent bad game feel in the form of slingshotting
    /// </summary>
    [SerializeField]
    float forceLimit = 0.5f;

    /// <summary>
    /// Force being applied to Sekibanki's body
    /// </summary>
    [SerializeField]
    float force = 0;

    [SerializeField]
    float forceDecay = 0.5f;

    /// <summary>
    /// Magic number lol
    /// </summary>
    [Range(0.01f, 1f)]
    [SerializeField]
    float gravity = 0.25f;

    /// <summary>
    /// If you are unstable, gravity is changed by this much
    /// </summary>
    [SerializeField]
    float unstableGravityModifier = 2;

    /// <summary>
    /// Stable between 0 and this absolute value
    /// </summary>
    [SerializeField]
    float stableTerritory = 0.3f;

    /// <summary>
    /// Unstable between this and stableTerritory
    /// </summary>
    [SerializeField]
    float unstableTerritory = 0.6f;

    bool updateBalance;

    Animator anim;

    public static BalanceSystem instance;

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

        anim = GetComponentInParent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetBalance();
    }

    public void UpdateStability(int requestedLevel)
    {
        float newStability = (float)requestedLevel / 10f;
        stabilityFactor = Mathf.Lerp(stabilityUpgrade.x, stabilityUpgrade.y, newStability);
    }

    public void ResetBalance()
    {
        balance = 0.01f; // NO BALANCING
        updateBalance = true;
    }

    // Update is called once per frame  
    void Update()
    {
        if (updateBalance)
        {
            if (balance <= 0)
            {
                if (balance > -stableTerritory)
                {
                    balance -= gravity * Time.deltaTime;
                }
                else if (balance < -stableTerritory)
                {
                    balance -= gravity * unstableGravityModifier * Time.deltaTime;
                }
            }

            if (balance > 0) //Right side
            {
                if (balance < stableTerritory)
                {
                    balance += gravity * Time.deltaTime;
                }
                else if (balance > stableTerritory)
                {
                    balance += gravity * unstableGravityModifier * Time.deltaTime;
                }
            }

            balance += force * Time.deltaTime;

            trueBalance = stabilityFactor * Mathf.Sin(balance);
            float animVal = Mathf.InverseLerp(-1, 1, trueBalance);
            anim.SetFloat("Balance", animVal);
            UIManager.instance.UpdateBalanceBar(animVal);

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                force = Mathf.Clamp(force += -influence * Time.deltaTime, -forceLimit, force);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                force = Mathf.Clamp(force += influence * Time.deltaTime, force, forceLimit);
            }
            if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                force = Mathf.MoveTowards(force, 0, forceDecay * Time.deltaTime);
            }

            if (Mathf.Abs(trueBalance) >= 0.95f)
            {
                if (trueBalance > 0) // Is positive
                {
                    anim.SetTrigger("Fall Forward");
                }
                else if (trueBalance < 0) // Is negative
                {
                    anim.SetTrigger("Fall Backward");
                }
                updateBalance = false;
            }
        }
    }

    //public void ChangeBalance(float direction)
    //{
    //    force += direction * influence * Time.deltaTime;
    //}
}
