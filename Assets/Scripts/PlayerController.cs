using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float baseSpeed = 3f;              // Always-on forward movement
    public float acceleration = 2f;           // Extra speed when pressing up
    public float brakeSpeed = 5f;             // How fast braking slows you down

    private float currentSpeed;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool energyDrinkActive = false;
    private float originalBaseSpeed;
    private float energyTimer = 0f;

    public bool hasHelmet = false;
    public float helmetInvulDuration = 3f; // how long to ignore collisions
    private float helmetInvulTimer = 0f;

    [Header("Lane Check")]
    public BikeLaneChecker laneChecker;

    [Header("Boost Blink Settings")]
    public float minAlpha = 0.5f;    // Min visibility
    public float maxAlpha = 0.8f;    // Max visibility
    public float blinkSpeed = 1f; 

    public BoostUI boostUI;
    private float boostDuration = 0f;

    [Header("UI")]
    public GameObject helmetObject;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentSpeed = baseSpeed; // Always moving forward
    }

    void Update()
    {
        float targetSpeed = baseSpeed; // Default forward movement

        // Pedal forward (up)
        if (moveInput.y > 0f)
        {
            targetSpeed = baseSpeed + acceleration;
        }
        // Brake (down)
        else if (moveInput.y < 0f)
        {
            targetSpeed = 0f;
        }

        // Slow down if off-lane
        if (laneChecker != null && !laneChecker.onLane)
        {
            targetSpeed *= laneChecker.offLaneSpeedMultiplier;
        }

        // Smooth speed interpolation
        float lerpSpeed = (moveInput.y < 0f) ? brakeSpeed : 3f;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * lerpSpeed);

        // Prevent tiny leftover values
        if (currentSpeed < 0.01f)
            currentSpeed = 0f;

        // Animation speed
        float speedPercent = currentSpeed / (baseSpeed + acceleration);
        float targetAnimSpeed = Mathf.Clamp(speedPercent, 0.5f, 2f);

        animator.speed = Mathf.Lerp(animator.speed, targetAnimSpeed, Time.deltaTime * 5f);

        if (energyDrinkActive)
        {
            energyTimer -= Time.deltaTime;

            if (boostUI != null)
            {
                float progress = Mathf.Clamp01(energyTimer / boostDuration);
                boostUI.SetBoostProgress(progress); // update lightning icon fill
            }

            if (spriteRenderer != null)
            {
                // Pulse with PingPong
                float alpha = Mathf.PingPong(Time.time * blinkSpeed, maxAlpha - minAlpha) + minAlpha;
                Color c = spriteRenderer.color;
                c.a = alpha;
                spriteRenderer.color = c;
            }

            if (energyTimer <= 0f)
            {
                // Reset to normal
                baseSpeed = originalBaseSpeed;
                energyDrinkActive = false;
                DisableObstacles(false);

                if (boostUI != null)
                    boostUI.Hide();

                if (spriteRenderer != null)
                {
                    Color c = spriteRenderer.color;
                    c.a = 1f;
                    spriteRenderer.color = c;
                }
            }
        }

        if (helmetInvulTimer > 0f)
        {
            helmetInvulTimer -= Time.deltaTime;

            Debug.Log("Obstacle deactivated for: " + Mathf.Ceil(helmetInvulTimer) + " seconds left");

            if (helmetInvulTimer <= 0f)
            {
                hasHelmet = false;
                DisableObstacles(false);
            }
        }
    }

    void FixedUpdate()
    {
        // Left/right movement is based on input
        float horizontal = moveInput.x;

        // Forward movement uses currentSpeed
        Vector2 movement = new Vector2(horizontal, 1f).normalized * currentSpeed;

        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
    }

    public void ActivateEnergyDrink(float boostSpeed, float boostDuration)
    {
        if (!energyDrinkActive)
        {
            SFXPlayer.Instance.PlaySpeed();
            energyDrinkActive = true;
            originalBaseSpeed = baseSpeed;

            baseSpeed = boostSpeed;
            currentSpeed = boostSpeed;

            DisableObstacles(true);

            energyTimer = boostDuration;
            this.boostDuration = boostDuration; // store for progress calculation

            if (boostUI != null)
                boostUI.Show();
        }
        else
        {
            // Extend the timer if picking up another boost
            energyTimer = boostDuration;
        }
    }

    private void DisableObstacles(bool disable)
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");

        foreach (GameObject obj in obstacles)
        {
            Collider2D col = obj.GetComponent<Collider2D>();
            if (col != null)
                col.enabled = !disable;
        }
    }

    public void ActivateHelmet()
    {
        hasHelmet = true;
        SFXPlayer.Instance.PlayUpgrade();
        helmetObject.SetActive(true);
        Debug.Log("has helmet!");
        animator.SetBool("hasHelmet", true);
    }

    public bool TryUseHelmet()
    {
        if (hasHelmet)
        {
            hasHelmet = false;
            SFXPlayer.Instance.PlayCrash();
            helmetObject.SetActive(false);
            Debug.Log("helmet lost!");
            animator.SetBool("hasHelmet", false);
            helmetInvulTimer = helmetInvulDuration; // start temporary invulnerability
            DisableObstacles(true);
            return true;  // Helmet absorbed the hit
        }

        return false; // No helmet available
    }

    void OnMove(InputValue _value)
    {
        moveInput = _value.Get<Vector2>();
    }

    void OnRing()
    {
        Debug.Log("Ringring!");
    }
}
