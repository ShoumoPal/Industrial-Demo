using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniForkLiftController : MonoBehaviour
{
    [SerializeField]
    Transform steeringWheel;

    public float maxSteerAngle = 80f; // Maximum rotation angle
    public float steerSpeed = 200f;   // Speed of rotation

    void Update()
    {
        SteerWheel();
    }

    private void SteerWheel()
    {
        float input = Input.GetAxis("Horizontal"); // Range: -1 to 1
        float targetAngle = Mathf.Clamp(input * maxSteerAngle, -maxSteerAngle, maxSteerAngle);
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0); // Assuming Z-axis rotation
        steeringWheel.localRotation = Quaternion.RotateTowards(steeringWheel.localRotation, targetRotation, steerSpeed * Time.deltaTime);
    }
}
