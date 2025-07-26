using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateManager : MonoBehaviour
{
    [SerializeField]
    List<InstructionState> instructionStates;

    private int currentIndex = 0;

    public void StartInstructions()
    {
        StartCoroutine(StartInstructionsCR());
    }
    private IEnumerator StartInstructionsCR()
    {
        while (currentIndex < instructionStates.Count)
        {
            InstructionState currentState = instructionStates[currentIndex];
            currentState.InitiateState();
            if (currentState.TriggerType == StateType.Timed)
            {
                yield return new WaitForSeconds(currentState.GetTimetoWait());
            }
            else if (currentState.TriggerType == StateType.Trigger)
            {
                // Wait for trigger
            }
            currentState.ExitState();
            currentIndex++;
        }
    }
}
public enum StateType
{
    Timed,
    Trigger
}
