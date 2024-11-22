using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField] bool isPlayer1;
    [SerializeField] float moveSpeed = 15.0f;
    CharacterController _controller;
    InputSystem_Actions _inputs;
    Vector2 _move;
    [SerializeField] public Text powerUpItemTxt;

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

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
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

    private void UsePowerUp()
    {
        switch (powerUpItemTxt.text)
        {
            case "Protect":
                Debug.Log("Used Protect");
                powerUpItemTxt.text = "Null";
                break;

            case "Speed":
                Debug.Log("Used Speed");
                powerUpItemTxt.text = "Null";
                break;

            default:
                Debug.Log("No Power-Up");
                break;
        }


    }
}
