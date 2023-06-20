
using System.Collections;
using System.Collections.Concurrent;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkAbility : MonoBehaviour
{
    public float cooldownDuration;
    private float cooldownTimer;
    private bool marked = false;
    private Vector3 markedLocation ;

    public TMP_Text cooldownText;
    public GameObject cooldownBackground;
    public Image abiltyImage;
    public Sprite markingSprite;
    public Sprite abilitySprite;
    
    
    private InputManager inputManager;
    private playerController playercontroller;

    
    // Start is called before the first frame update
    void Start()
    {
        playercontroller = gameObject.GetComponent<playerController>();
        inputManager = InputManager.instance;

        markedLocation = transform.position;
        cooldownTimer = 0f;
        cooldownBackground.SetActive(false);
        cooldownText.text = "";
    }


    // Update is called once per frame
    void Update()
    {

        //enemyPosition();
        // Check for input to mark the location
        if (inputManager.PlayerBlinkAbility() && cooldownTimer <= 0f)
        {
            if (!marked)
            {
                MarkLocation();

            }
            else
            {

                StartCoroutine("Blink");

            }
        }
        // Update the cooldown timer
        if (cooldownTimer > 0f)
        {
            cooldownBackground.SetActive(true) ;
            cooldownText.text = cooldownTimer.ToString("0");
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            cooldownBackground.SetActive(false) ;
            cooldownText.text = "";
        }


    }

    void MarkLocation()
    {
        if (enemyPosition() != Vector3.zero)
        {
            markedLocation = enemyPosition();  // Store the current position as the marked location
            marked = true;
            abiltyImage.sprite = abilitySprite;
        }
        
    }


    // Method to perform the blink
    IEnumerator Blink()
    {
        playercontroller.disabled = true;
        yield return new WaitForSeconds(0.01f);
        transform.position = markedLocation;
        cooldownTimer = cooldownDuration;
        marked = false;

        yield return new WaitForSeconds(0.01f);
        playercontroller.disabled = false;
        abiltyImage.sprite = markingSprite;
    }

    public Vector3 enemyPosition()
    {

       
        Vector3 rayOrigin = transform.position; // Starting position of the ray
        Vector3 rayDirection = transform.TransformDirection(Vector3.forward); // Direction along the z-axis

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(rayOrigin, rayDirection * hit.distance, Color.red);
            if (hit.collider.CompareTag("Enemy"))
            {
                Vector3 tpposition = hit.collider.transform.position ;
                tpposition.z -= hit.collider.transform.localScale.z;
                return tpposition ;
            }
        }
        else
        {
            Debug.DrawRay(rayOrigin, rayDirection * 8, Color.blue);
        }


        // Return a default position if no enemy is hit
        return Vector3.zero;

    }
}
