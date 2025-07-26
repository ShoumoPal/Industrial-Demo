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

    [SerializeField]
    InstructionSO instructionSO;

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

    public string GetInstructionText()
    {
        return instructionSO.instructionText;
    }
}
