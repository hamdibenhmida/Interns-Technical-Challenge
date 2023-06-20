using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{


    public static InputManager instance;
    

    private PlayerControls playerControls;

    private void Awake()
    {
        
        instance = this;
        playerControls = new PlayerControls();
    }

    public void OnEnable()
    {
        playerControls.Enable();
    }

    public void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.player.Movement.ReadValue<Vector2>();
    }
    
    public Vector2 GetMouseDelta()
    {
        return playerControls.player.Look.ReadValue<Vector2>();
    }

    public bool PlayerJumped()
    {
        return playerControls.player.Jump.triggered;
    }

    public bool PlayerBlinkAbility()
    {
        return playerControls.player.blink.triggered;
    }

    public bool PlayerDashAbility()
    {
        return playerControls.player.dash.triggered;
    }

    public bool playerShoot()
    {
        return playerControls.player.shoot.triggered;
    }

    public bool playerReload()
    {
        return playerControls.player.reload.triggered;
    }

    public bool playerInteracted()
    {
        return playerControls.player.interact.triggered;
    }
}
