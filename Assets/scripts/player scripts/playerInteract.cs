using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteract : MonoBehaviour
{
    private Transform cameraTransform;
    [SerializeField]private float distance= 3f;
    [SerializeField]private LayerMask mask;

    private playerUI playerUI;
    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        playerUI = GetComponent<playerUI>();
        inputManager = InputManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.updateText(string.Empty);

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance , Color.blue);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray,out hitInfo, distance, mask))
        {
            if(hitInfo.collider.GetComponent<Interactable>()!= null )
            {
                Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                playerUI.updateText(interactable.promptMessage);
                if (inputManager.playerInteracted())
                {
                    interactable.BeseInteract();
                }
            }
        }
    }
}
