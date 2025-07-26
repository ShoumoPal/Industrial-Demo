using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InstructionState : MonoBehaviour
{
    [Header("Trigger Type")]
    [SerializeField]
    StateType triggerType;

    [Tooltip("Does not matter if trigger type is 'trigger'")]
    [SerializeField]
    float timeToWait;

    #region Trigger Prop
    public StateType TriggerType
    {
        get { return triggerType; }
        set
        {
            if (value != triggerType)
            {
                triggerType = value;
            }
        }
    } 
    #endregion

    [Header("Next State")]
    [SerializeField]
    InstructionState nextState;

    [Header("On State Enter")]
    [SerializeField]
    private UnityEvent onStateEnter;

    [Header("On State Exit")]
    [SerializeField]
    private UnityEvent onStateExit;

    private void Update()
    {
        if (TriggerType != triggerType)
        {
            TriggerType = triggerType;
        }
    }

    #region Getters
    public InstructionState GetNextState()
    {
        return nextState;
    }
    public float GetTimetoWait()
    {
        return timeToWait;
    } 
    #endregion

    public void InitiateState()
    {
        onStateEnter?.Invoke();
    }
    public void ExitState()
    {
        onStateExit?.Invoke();
    }
}
