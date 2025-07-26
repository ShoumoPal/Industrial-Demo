using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    MiniForkLiftController controller;

    [Header("Events")]
    [SerializeField]
    UnityEvent steeringTurnedLeft;
    [SerializeField]
    UnityEvent steeringTurnedRight;

    public void CheckSteering(string direction)
    {
        StartCoroutine(CheckSteeringCR(direction));
    }

    private IEnumerator CheckSteeringCR(string direction)
    {
        if (direction == (Direction.Left).ToString())
        {
            // Check if the steering wheel is turned left
            yield return new WaitUntil(() => controller.GetYRotation() < -75f);
            steeringTurnedLeft.Invoke();
        }
        else if (direction == (Direction.Right).ToString())
        {
            // Check if the steering wheel is turned right
            yield return new WaitUntil(() => controller.GetYRotation() > 75f);
            steeringTurnedRight.Invoke();
        }
    }
}
public enum Direction
{
    Left,
    Right
}
