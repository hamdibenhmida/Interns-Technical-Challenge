using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    
    private float health;
    private float lerpTimer;
    [Header("health bar")]
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    [Header("damage overlay")]
    public Image overlay;
    public float duration;
    public float fadeSpeed;

    private float durationTimer;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        
        if (health == 0)
        {
            //Debug.Log("you dead");
        }

        updateHealthUI();
        if(overlay.color.a>0)
        {
            if (health < 30)
                return;
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);

            }
        }

    }
    public void updateHealthUI()
    {
        //Debug.Log(health);
        float fillFront = frontHealthBar.fillAmount;
        float fillback = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if(fillback > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillback, hFraction, percentComplete);
        }
        if(fillFront <hFraction)
        { 
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
        }

    }
    public void takeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
        durationTimer = 0f;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);
    }
    public void restoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
}
