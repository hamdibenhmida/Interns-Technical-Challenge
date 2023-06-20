using UnityEngine;
using TMPro;

public class DashAbility : MonoBehaviour
{
    public float dashSpeed = 10f;           // Speed multiplier during dash
    public float dashDuration = 0.5f;       // Duration of the dash in seconds
    public float cooldownTime = 5f;         // Cooldown time in seconds

    private bool isDashing = false;         // Flag to track if dash is active
    private bool isCooldown = false;        // Flag to track if cooldown is active

    private float originalSpeed;            // Original movement speed
    private float dashTimer;                // Timer for dash duration
    private float cooldownTimer;            // Timer for cooldown period

    public TMP_Text cooldownText;
    public GameObject cooldownBackground;

    private InputManager inputManager;
    playerController playercontroller;

    private void Start()
    {
        inputManager = InputManager.instance;
        playercontroller = GetComponent<playerController>();
        originalSpeed = playercontroller.playerSpeed; // Store the original movement speed

        cooldownBackground.SetActive(false);
        cooldownText.text = "";
    }

    private void Update()
    {
        // Check for dash activation input and if not on cooldown or already dashing
        if (inputManager.PlayerDashAbility() && !isCooldown && !isDashing)
        {
            ActivateDash();
        }

        // Check if currently dashing
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
            {
                EndDash();
            }
        }
        // Check if on cooldown
        else if (isCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownBackground.SetActive(true);
            cooldownText.text = cooldownTimer.ToString("0");
            if (cooldownTimer <= 0)
            {
                cooldownBackground.SetActive(false);
                cooldownText.text = "";
                isCooldown = false;
            }
        }
    }

    private void ActivateDash()
    {
        isDashing = true;
        dashTimer = dashDuration;

        // Multiply movement speed by dashSpeed during dash
        playercontroller.playerSpeed *= dashSpeed;

        // Apply visual effects for invisibility 

       

        
    }

    private void EndDash()
    {
        isDashing = false;

        // Reset movement speed to original value
        playercontroller.playerSpeed = originalSpeed;

        // Remove any visual effects for invisibility or particle effects

        // Optionally re-enable player input after dash

        StartCooldown();
    }

    private void StartCooldown()
    {
        isCooldown = true;
        cooldownTimer = cooldownTime;
        
    }
}
