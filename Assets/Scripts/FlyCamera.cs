using UnityEngine;

public class FlyCamera : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float lookSensitivity = 2f;
    public bool invertY = false;

    private Vector3 rotation;
    private bool isLooking = false;

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMouseLook()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isLooking = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isLooking = false;
        }

        if (isLooking)
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivity * (invertY ? 1 : -1);

            rotation.x += mouseY;
            rotation.y += mouseX;

            transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, 0f);
        }
    }

    void HandleMovement()
    {
        Vector3 direction = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );

        Vector3 move = transform.TransformDirection(direction) * moveSpeed * Time.deltaTime;
        transform.position += move;
    }
}
