using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// State manager for instruction states in a simulation.
public class StateManager : MonoBehaviour
{
    [Header("Instruction States")]  
    [SerializeField]
    List<InstructionState> instructionStates;

    [Header("Instruction Text")]
    [SerializeField]
    TextMeshProUGUI instructionText;

    private int currentIndex = 0;
    private InstructionState currentState;
    private bool forceMoveNext = false;

    private void Start()
    {
        StartInstructions();
    }

    public void StartInstructions()
    {
        StartCoroutine(StartInstructionsCR());
    }
    private IEnumerator StartInstructionsCR()
    {
        while (currentIndex < instructionStates.Count)
        {
            currentState = instructionStates[currentIndex];
            currentState.InitiateState();
            ChangeInstructionText(currentState);

            if (currentState.TriggerType == StateType.Timed)
            {
                yield return new WaitForSeconds(currentState.GetTimetoWait());
            }
            else if (currentState.TriggerType == StateType.Trigger)
            {
                // Wait for trigger
                yield return new WaitUntil(() => forceMoveNext);
                forceMoveNext = false; // Reset the flag after moving to the next state
            }
            currentState.ExitState();
            currentIndex++;
        }
    }
    public void ChangeInstructionText(InstructionState state)
    {
        //Debug.Log("Called change instructions");
        instructionText.text = state.GetInstructionText();
    }
    public void MoveNextState(int index)
    {
        Debug.Log("Called MoveNextState with index: " + index);
        if (currentState && currentState.TriggerType == StateType.Trigger && index == currentIndex)
        {
            forceMoveNext = true;
        }
    }
}
public enum StateType
{
    Timed,
    Trigger
}
