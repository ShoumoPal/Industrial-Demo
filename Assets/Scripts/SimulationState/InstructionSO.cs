using UnityEngine;

[CreateAssetMenu(fileName = "Instruction SO", menuName = "SO/Instruction")]
public class InstructionSO : ScriptableObject
{
    [TextArea(10, 10)]
    public string instructionText;
}
