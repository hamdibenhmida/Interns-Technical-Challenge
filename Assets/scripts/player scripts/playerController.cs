using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class playerController : MonoBehaviour
{

    public bool disabled = false;

    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    public Transform cameraTransform;
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;    
    private cinemachinePOVExtention cinemachinepov;


    private void Start()
    {

        cinemachinepov = cinemachinePOVExtention.instance;
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.instance;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (!disabled)
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector2 movement = inputManager.GetPlayerMovement();
            Vector3 move = new Vector3(movement.x, 0f, movement.y);
            move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
            move.y = 0f;
            controller.Move(move * Time.deltaTime * playerSpeed);
            transform.rotation = Quaternion.Euler(0f, cinemachinepov.startingRotation.x, 0f);



            // Changes the height position of the player..
            if (inputManager.PlayerJumped() && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }
        
    }
}
