using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceSystem : MonoBehaviour
{
    [Header("Physics Lol")]

    /// <summary>
    /// Will be a quadratic function -(balance = mx^2) where balance is between -2 and 2 and m is stability factor
    /// </summary>
    [Range (-1, 1)]
    [SerializeField]
    float balance = 0;

    /// <summary>
    /// The m in y = mx^2
    /// </summary>
    [SerializeField]
    float stabilityFactor = 1;

    /// <summary>
    /// Changes your balance based on key press
    /// </summary>
    [SerializeField]
    float influence = 0.5f;

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

    PlayerActions actions;

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

        actions = new PlayerActions();
        actions.Default.Jump.performed += context => Jump();
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame  
    void Update()
    {
        if (balance < 0)
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

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            force += -influence * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            force += influence * Time.deltaTime;
        }
        if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            force = Mathf.MoveTowards(force, 0, forceDecay * Time.deltaTime);
        }
    }

    //public void ChangeBalance(float direction)
    //{
    //    force += direction * influence * Time.deltaTime;
    //}

    public void Jump()
    {

    }
}
