using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadJumpManager : MonoBehaviour
{
    [SerializeField]
    AnimationCurve jumpCurve;
    [SerializeField]
    float jumpTime = 0.5f;
    float jumpTimer;

    [SerializeField]
    float gravity = 1;

    float startingHeight;
    Rigidbody2D rb;

    [SerializeField]
    int maxLives = 2; 
    int playerLives = 2;

    [SerializeField]
    int candyAvoided;

    [SerializeField]
    int candyEaten;

    // new jumping variables
    [SerializeField]
    bool isGrounded;
    [SerializeField]
    Transform feetPos;
    [SerializeField]
    float checkRadius;
    [SerializeField]
    LayerMask ground;
    [SerializeField]
    float jumpForce;

    [SerializeField]
    float jumpTimeCounter;
    [SerializeField]
    float jumpLength;

    [SerializeField]
    Vector2 jumpUpgradeRange = new Vector2(0.2f, 0.4f);

    bool isJumping;

    Animator anim;

    public static HeadJumpManager instance;

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

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInParent<Animator>();
    }

    void Start ()
    {
        ResetStats();
    }

    void Update ()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, ground);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = jumpLength;
            rb.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    public void UpdateJumpHeight()
    {
        jumpLength = Mathf.Lerp(jumpUpgradeRange.x, jumpUpgradeRange.y, (float)UpgradeManager.instance.GetUpgradeLevel(Upgrades.HeadJumpHeight) / 10f);
    }

    public void ResetStats()
    {
        playerLives = maxLives;
        candyAvoided = 0;
        candyEaten = 0;
    }

    public void UpdateMaxLives()
    {
        maxLives = UpgradeManager.instance.GetUpgradeLevel(Upgrades.AdditionalHeads) + 1;
    }

    public void HitHead()
    {
        playerLives--;
        candyAvoided--;
        anim.SetTrigger("HeadHit");
        if (playerLives == 0)
        {
            anim.SetBool("No Heads", true);
        }
    }

    public void CandyAvoided()
    {
        candyAvoided++;
    }

    public void CandyEaten()
    {
        candyEaten++;
    }

    public int GetCandyEaten()
    {
        return candyEaten;
    }

    public int GetCandyAvoided()
    {
        return candyAvoided;
    }

/* Jacky's jumping code */

/*
    // Start is called before the first frame update
    void Start()
    {
        startingHeight = transform.position.y;
        jumpTimer = 0;
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && jumpTimer <= jumpTime)
        {
            jumpTimer += Time.deltaTime;
            rb.MovePosition(new Vector3(transform.position.x, startingHeight) + new Vector3(0, jumpCurve.Evaluate(jumpTimer)));
        }
        else if (transform.position.y == startingHeight)
        {
            jumpTimer = 0;
        }
        else
        {
            rb.MovePosition(transform.position -= new Vector3(0, gravity));
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, startingHeight, transform.position.y));
        }
    }*/
}
