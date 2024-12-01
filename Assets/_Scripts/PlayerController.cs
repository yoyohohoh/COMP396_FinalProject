using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector3 startPosition;
    [SerializeField] Vector3 startRotation;

    [SerializeField] Vector3 finishPosition;
    [SerializeField] Vector3 finishRotation;
    [SerializeField] bool isPlayer1;
    [SerializeField] bool isSpeedUp = false;
    [SerializeField] public bool isProtected = false;
    [SerializeField] public int health = 100;
    //[SerializeField] public float moveSpeed = 15.0f;
    [SerializeField] public Image powerUpItem;
    [SerializeField] public Text powerUpItemTxt;
    [SerializeField] public Text lifeTxt;
    [SerializeField] public Text timerTxt;

    private float originalSpeed;
    private float currentSpeed = 0f;
    private float inputAcceleration;
    private float inputSteering;
    private CharacterController _controller;
    private InputSystem_Actions _inputs;
    private Vector2 _move;
    private Vector3 verticalVelocity; // Vertical velocity for grounding and gravity

    [SerializeField] private float acceleration = 20f;
    [SerializeField] public float maxSpeed = 50f;
    [SerializeField] private float turnSpeed = 100f;
    [SerializeField] private float brakeForce = 50f;
    [SerializeField] private float gravity = -9.8f; // Gravity force
    [SerializeField] private float groundCheckOffset = 0.1f; // Offset to ensure grounding
    Color notInUseColor;
    Sprite background;
    [SerializeField] GameObject carSound;
    void Awake()
    {
        if (_controller == null)
        {
            _controller = GetComponent<CharacterController>();
        }
        _inputs = new InputSystem_Actions();

        Invoke("InputSetup", 3.0f);
    }

    void InputSetup()
    {
        if (isPlayer1)
        {
            carSound = GameObject.Find("Audio Source").transform.Find("CarSound1").gameObject;

            _inputs.Player.Player1Move.performed += context =>
            {
                _move = context.ReadValue<Vector2>();

                carSound.SetActive(true);
            };
            _inputs.Player.Player1Move.canceled += context =>
            {
                _move = Vector2.zero;
                carSound.SetActive(false);
            };

            _inputs.Player.Player1PowerUp.performed += context => UsePowerUp();
        }
        else
        {
            carSound = GameObject.Find("Audio Source").transform.Find("CarSound2").gameObject;
            _inputs.Player.Player2Move.performed += context =>
            {
                _move = context.ReadValue<Vector2>();
                carSound.SetActive(true);
            };
            _inputs.Player.Player2Move.canceled += context =>
            {
                _move = Vector2.zero;
                carSound.SetActive(false);
            };

            _inputs.Player.Player2PowerUp.performed += context => UsePowerUp();
        }
    }

    private void Start()
    {
        originalSpeed = maxSpeed;
        transform.position = startPosition;
        transform.rotation = Quaternion.Euler(startRotation);
        notInUseColor = powerUpItem.color;
        background = powerUpItem.sprite;

    }

    void Update()
    {

        lifeTxt.text = $"Life: {health}";

        // Check if player is grounded
        if (_controller.isGrounded)
        {
            verticalVelocity.y = -groundCheckOffset; // Slight downward force to stay grounded
        }
        else
        {
            verticalVelocity.y += gravity * Time.deltaTime; // Apply gravity if in the air
        }

        if (health > 100)
        {
            health = 100;
        }

        HandleInput();
        MovePlayer();
    }

    void HandleInput()
    {
        // Input from _move (mapped by InputSystem_Actions) is normalized to car-like controls
        inputAcceleration = _move.y; // Forward/Backward
        inputSteering = _move.x;     // Left/Right

        // Apply braking if moving backward while moving forward
        if (inputAcceleration < 0 && currentSpeed > 0)
        {
            currentSpeed -= brakeForce * Time.deltaTime;
        }
    }

    void MovePlayer()
    {
        // Accelerate/decelerate based on input
        if (Mathf.Abs(inputAcceleration) > 0.01f)
        {
            // Apply acceleration based on input
            currentSpeed += acceleration * inputAcceleration * Time.deltaTime;
        }
        else
        {
            // Natural deceleration when no input is given
            if (currentSpeed > 0) // Moving forward
            {
                currentSpeed -= brakeForce * Time.deltaTime; // Gradual reduction
                currentSpeed = Mathf.Max(currentSpeed, 0);  // Clamp to zero to avoid going backward unintentionally
            }
            else if (currentSpeed < 0) // Moving backward
            {
                currentSpeed += brakeForce * Time.deltaTime; // Gradual increase toward zero
                currentSpeed = Mathf.Min(currentSpeed, 0);  // Clamp to zero to avoid overshooting
            }
        }

        // Clamp speed within range
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed / 2, maxSpeed);

        // Adjust turning sensitivity based on speed
        float speedFactor = Mathf.Clamp01(Mathf.Abs(currentSpeed) / maxSpeed); // Use absolute speed for consistent turning
        float adjustedTurnSpeed = turnSpeed * speedFactor * 0.5f;              // Reduce turn speed further for smoother control

        // Reverse steering direction when moving backward
        float turnDirection = currentSpeed < 0 ? -1 : 1;

        // Turn the player using inputSteering
        float turn = inputSteering * adjustedTurnSpeed * Time.deltaTime * turnDirection;
        transform.Rotate(0, turn, 0);

        // Calculate forward movement
        Vector3 forwardMovement = transform.forward * currentSpeed * Time.deltaTime;

        // Combine forward movement with vertical velocity
        Vector3 finalMovement = forwardMovement + verticalVelocity * Time.deltaTime;

        // Use CharacterController to move the player
        _controller.Move(finalMovement);
    }

    public void ResetPlayerPosition()
    {
        transform.position = finishPosition;
        transform.rotation = Quaternion.Euler(finishRotation);
        this.enabled = false;
    }

    void OnEnable() => _inputs.Enable();
    void OnDisable() => _inputs.Disable();

    public void UsePowerUp()
    {
        switch (powerUpItemTxt.text)
        {
            case "Protected":
                Debug.Log("Used Protect");
                powerUpItem.color = Color.white;
                //powerUpItemTxt.text = "Proctect in use";
                isProtected = true;
                Invoke("ResetProtect", 5f);
                break;

            case "Speed":
                Debug.Log("Used Speed");
                powerUpItem.color = Color.white;
                //powerUpItemTxt.text = "Speed in use";
                isSpeedUp = true;
                maxSpeed *= 50f;
                Invoke("NormalSpeed", 5f);
                break;

            default:
                Debug.Log("No Power-Up");
                break;
        }
    }

    void NormalSpeed()
    {
        powerUpItem.color = notInUseColor;
        //powerUpItemTxt.text = "Null";
        powerUpItem.sprite = background;
        maxSpeed = originalSpeed;
        isSpeedUp = false;
    }

    void ResetProtect()
    {
        powerUpItem.color = notInUseColor;
        //powerUpItemTxt.text = "Null";
        powerUpItem.sprite = background;
        isProtected = false;
    }
}
