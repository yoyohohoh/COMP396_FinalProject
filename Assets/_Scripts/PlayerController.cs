using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool isPlayer1;
    [SerializeField] bool isSpeedUp = false;
    [SerializeField] public bool isProtected = false;
    [SerializeField] public float health = 100f;
    [SerializeField] public float moveSpeed = 15.0f;
    float originalSpeed;
    CharacterController _controller;
    InputSystem_Actions _inputs;
    Vector2 _move;
    [SerializeField] public Text powerUpItemTxt;
    [SerializeField] public Text lifeTxt;
    [SerializeField] public Text timerTxt;
    [SerializeField] public GameObject endPoint;
    // Start is called before the first frame update
    void Awake()
    {
        if (_controller == null)
        {
            _controller = GetComponent<CharacterController>();
        }
        _inputs = new InputSystem_Actions();

        if (isPlayer1)
        {
            _inputs.Player.Player1Move.performed += context => _move = context.ReadValue<Vector2>();
            _inputs.Player.Player1Move.canceled += context => _move = Vector2.zero;

            _inputs.Player.Player1PowerUp.performed += context => UsePowerUp();
        }
        else
        {
            _inputs.Player.Player2Move.performed += context => _move = context.ReadValue<Vector2>();
            _inputs.Player.Player2Move.canceled += context => _move = Vector2.zero;

            _inputs.Player.Player2PowerUp.performed += context => UsePowerUp();
        }



    }

    private void Start()
    {
        originalSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTxt.text = $"Life: {health}";
        MovePlayer();

        if(health <= 0)
        {
            _inputs.Disable();
            if(isPlayer1)
            {
                this.transform.position = new Vector3(-2f, 0, endPoint.transform.position.z);
            }
            else
            {
                this.transform.position = new Vector3(2f, 0, endPoint.transform.position.z);
            }

        }

        if(health > 100)
        {
            health = 100;
        }
    }

    void MovePlayer()
    {
        float moveX = _move.x;
        float moveZ = _move.y;

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        _controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    void OnEnable() => _inputs.Enable();

    void OnDisable() => _inputs.Disable();

    public void UsePowerUp()
    {
        switch (powerUpItemTxt.text)
        {
            case "Protect":
                Debug.Log("Used Protect");
                powerUpItemTxt.text = "Null";
                isProtected = true;
                Invoke("ResetProtect", 5f);
                break;

            case "Speed":
                Debug.Log("Used Speed");
                powerUpItemTxt.text = "Null";
                isSpeedUp = true;
                moveSpeed *= 3f;
                Invoke("NormalSpeed", 3f);
                break;

            default:
                Debug.Log("No Power-Up");
                break;
        }
    }

    void NormalSpeed()
    {
        moveSpeed = originalSpeed;
        isSpeedUp = false;
    }

    void ResetProtect()
    {
        isProtected = false;
    }
}
