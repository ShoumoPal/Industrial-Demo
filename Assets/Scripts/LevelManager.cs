using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

// Level manager for handling various game events in a forklift simulation.
public class LevelManager : MonoBehaviour
{
    [SerializeField]
    MiniForkLiftController controller;

    [Header("Steering Events")]
    [SerializeField]
    UnityEvent steeringTurnedLeft;
    [SerializeField]
    UnityEvent steeringTurnedRight;

    [Header("Engine Events")]
    [SerializeField]
    UnityEvent engineStarted;

    [Header("Level Events")]
    [SerializeField]
    UnityEvent levelStarted;
    [SerializeField]
    UnityEvent onReachedTrigger;

    #region Steering Related Events
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
    #endregion

    #region Engine Related
    public void CheckEngine()
    {
        StartCoroutine(CheckEngineCR());
    }

    private IEnumerator CheckEngineCR()
    {
        yield return new WaitUntil(() => controller.engine == true);
        engineStarted.Invoke();
    }
    #endregion

    #region Level Related Events
    public void LevelStart()
    {
        levelStarted?.Invoke();
    }
    public void OnReachedTrigger()
    {
        onReachedTrigger?.Invoke();
    }
    #endregion

    // Just to end the level
    public void EndLevel()
    {
        #if UNITY_EDITOR_64 || UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }
}
public enum Direction
{
    Left,
    Right
}
