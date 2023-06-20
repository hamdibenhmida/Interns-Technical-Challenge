using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class cinemachinePOVExtention : CinemachineExtension
{
    public static cinemachinePOVExtention instance;

    private InputManager inputManager;
    public Vector3 startingRotation;

    [SerializeField] private float clampAngle = 70f;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;


    protected override void Awake()
    {
        instance = this;
        inputManager = InputManager.instance;
        base.Awake();
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if(vcam.Follow)
        {
            if(stage==CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null) startingRotation = transform.rotation.eulerAngles;
                Vector2 deltaInput = inputManager.GetMouseDelta();
                startingRotation.x += deltaInput.x *verticalSpeed *Time.deltaTime; 
                startingRotation.y += deltaInput.y *horizontalSpeed *Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                state.RawOrientation = Quaternion.Euler(-startingRotation.y,startingRotation.x,0f);
            }
        }
    }
}
