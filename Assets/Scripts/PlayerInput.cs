using UnityEngine;

public class PlayerInput2D : MonoBehaviour
{
    [Header("References: ")]
    [SerializeField] private Gameplay _inputActions;
    [Space]
    [SerializeField] private PlayerMovement _movementLogic;
    [Space]
    [SerializeField] private PlayerAnimator _animator;
    //[SerializeField] private PlayerCamera2D _cameraLogic;
    //[SerializeField] private CameraSpring2D _cameraSpringLogic;
    //[SerializeField] private CameraLean2D _cameraLeanLogic;
    [Space]


    [Header("Movement Parameters: ")]
    [SerializeField] private float moveAcceleration = 50.0f;
    [Space]
    [SerializeField] private float CameraSpeedThreshold = 550f;

    [Header("Fixed Camera Position: ")]
    [SerializeField] private Vector3 targetedFocusPosition;


    void Start()
    {
        //Initialize Input Actions:
        _inputActions = new Gameplay();
        _inputActions.Enable();
    }

    void OnDestroy()
    {
        //Dispose Input Actions:
        if (_inputActions != null)
        {
            _inputActions.Player.Disable();
            _inputActions.Dispose();
        }
    }

    void Update()
    {
        //Define Input and Time:
        var input = _inputActions.Player;
        var deltaTime = Time.deltaTime;

        //Define Input Data for Movement Logic:
        var characterInput = new CharacterInput2D
        {
            Move = input.Move.ReadValue<Vector2>()
        };

        //Update Movement Logic:
        _movementLogic.UpdateInput(characterInput);
        _movementLogic.UpdateBody(deltaTime);
    }

    private void LateUpdate()
    {
        //Define Time and Camera Target:
        var deltaTime = Time.deltaTime;
        var cameraTarget = _movementLogic.GetCameraTarget();
        var wideZoomCameraTarget = _movementLogic.GetWideZoomCameraTarget();
    }

    private void FixedUpdate()
    {
        //Gather Movement Input, Drag, and Gravity Accelerations Based on Input Value:
        _movementLogic.GatherIntrinsicAccelerations(moveAcceleration);

        //Gather Physics Data for Movement Logic:
        _movementLogic.GatherIntrinsicPhysics();

        //Update Movement Logic Using Semi-Implicit Euler Integration:
        _movementLogic.ApplyMovement();
    }
}
