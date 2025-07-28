using UnityEngine;
using UnityEngine.Events;

// Script to check if the player has reached a certain distance from a target object.
public class GapCheck : MonoBehaviour
{
    [SerializeField]
    float threshold;

    [SerializeField]
    Transform target;

    [Space(10)]
    [Header("Events")]
    [SerializeField]
    UnityEvent onReached;

    public bool triggerd = false;

    private void Update()
    {
        if (target && Vector3.Distance(transform.position, target.position) < threshold && !triggerd)
        {
            onReached?.Invoke();
            triggerd = true; // Prevent multiple invocations
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetThreshold(float threshold)
    {
        this.threshold = threshold;
    }
}
