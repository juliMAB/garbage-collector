using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private InputAction moveAction;
    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (moveAction == null)
        {
            // Acción de movimiento (vector2) compatible con teclado y gamepad
            moveAction = new InputAction(
                name: "Move",
                type: InputActionType.Value,
                expectedControlType: "Vector2"
            );

            // Gamepad: stick izquierdo
            moveAction.AddBinding("<Gamepad>/leftStick");

            // Teclado: WASD + Flechas como 2DVector
            var composite = moveAction.AddCompositeBinding("2DVector");
            composite.With("Up", "<Keyboard>/w");
            composite.With("Up", "<Keyboard>/upArrow");
            composite.With("Down", "<Keyboard>/s");
            composite.With("Down", "<Keyboard>/downArrow");
            composite.With("Left", "<Keyboard>/a");
            composite.With("Left", "<Keyboard>/leftArrow");
            composite.With("Right", "<Keyboard>/d");
            composite.With("Right", "<Keyboard>/rightArrow");
        }

        moveAction.Enable();
    }

    private void OnDisable()
    {
        moveAction?.Disable();
    }

    private void Update()
    {
        // Lee el input del New Input System como Vector2 (-1..1)
        moveInput = moveAction.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        // Aplica la velocidad en física (FixedUpdate)
        // Nota: si tu proyecto no tiene las propiedades linearVelocityX/Y,
        // reemplaza por: rb.velocity = moveInput * moveSpeed;
        rb.linearVelocityX = moveInput.x * moveSpeed;
        rb.linearVelocityY = moveInput.y * moveSpeed;
    }
}