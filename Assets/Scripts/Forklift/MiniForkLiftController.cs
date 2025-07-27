using UnityEngine;

public class MiniForkLiftController : MonoBehaviour
{
    [SerializeField]
    Transform steeringWheel;

    public float maxSteerAngle = 80f; // Maximum rotation angle
    public float steerSpeed = 200f;   // Speed of rotation

    public float moveSpeed = 10f;
    public float turnSpeed = 50f;

    public bool engine = false;

    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        SteerWheel();

        #region Engine Input Code
        if (!engine && Input.GetKeyDown(KeyCode.E))
        {
            engine = true;
            Debug.Log("Engine started");
        }
        else if (engine && Input.GetKeyDown(KeyCode.E))
        {
            engine = false;
            Debug.Log("Engine stopped");
        } 
        #endregion

        //Debug.Log(GetYRotation());
    }

    private void FixedUpdate()
    {
        if (engine)
        {
            MoveForkLift(); 
        }
    }

    private void MoveForkLift()
    {
        float move = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow
        float turn = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow

        // Move forward/backward
        Vector3 movement = transform.forward * move * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Rotate left/right
        Quaternion rotation = Quaternion.Euler(0f, turn * turnSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * rotation);
    }

    private void SteerWheel()
    {
        float input = Input.GetAxis("Horizontal"); // Range: -1 to 1
        float targetAngle = Mathf.Clamp(input * maxSteerAngle, -maxSteerAngle, maxSteerAngle);
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0); // Assuming Z-axis rotation
        steeringWheel.localRotation = Quaternion.RotateTowards(steeringWheel.localRotation, targetRotation, steerSpeed * Time.deltaTime);
    }

    public float GetYRotation()
    {
        return ((steeringWheel.localRotation.eulerAngles.y) < 180)?
            steeringWheel.localRotation.eulerAngles.y :
            steeringWheel.localRotation.eulerAngles.y - 360;
    }
}
