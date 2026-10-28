using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Camera playerCamera;

    [Header("Ajustes de Movimiento")]
    public float moveSpeed = 5f;
    public float gravity = -9.81f;

    [Header("Sensibilidad del Mouse")]
    public float mouseSensitivity = 100f;

    private Vector3 velocity;
    private float xRotation = 0f;

    void Start()
    {
        // Oculta y bloquea el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 1. ROTACIÓN CON EL MOUSE (Mirar alrededor)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f); // Evita giros extraños con la cabeza

        if (playerCamera != null)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
        
        transform.Rotate(Vector3.up * mouseX);

        // 2. MOVIMIENTO CON TECLAS (WASD)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (controller != null)
        {
            controller.Move(move * moveSpeed * Time.deltaTime);

            // 3. GRAVEDAD BÁSICA
            if (controller.isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}