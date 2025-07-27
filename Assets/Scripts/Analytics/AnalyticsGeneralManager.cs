using UnityEngine;
using Analytics;
using System;
using System.IO;

public class AnalyticsGeneralManager : MonoBehaviour
{
    [Header("Module details")]
    [SerializeField]
    string moduleName;
    [SerializeField]
    string moduleType;
    [SerializeField]
    string moduleVersion;
    [SerializeField]
    string moduleDescription;

    private float duration;
    DateTime startTime;
    DateTime endTime;
    TimeZoneInfo istZone;

    string path = Application.streamingAssetsPath + "/ForkLiftAnalyticsData.json";

    private void Start()
    {
        istZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
    }
    public void StartSimulation()
    {
        startTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, istZone);
    }
    public void EndSimulation()
    {
        endTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, istZone);
        duration = (float)(endTime - startTime).TotalSeconds;
        SendAnalyticsData();
    }

    private void SendAnalyticsData()
    {
        Module module = new Module
        {
            moduleName = moduleName,
            moduleType = moduleType,
            moduleVersion = moduleVersion,
            moduleDescription = moduleDescription
        };

        GameData gameData = new GameData
        {
            startTime = startTime.ToString("o"),
            endTime = endTime.ToString("o"),
            duration = duration
        };

        ForkliftData forkliftData = new ForkliftData
        {
            module = module,
            gameData = gameData
        };

        string newJson = JsonUtility.ToJson(forkliftData, true);
        Debug.Log("Analytics Data: " + newJson);

        // Print the json in a file
        File.WriteAllText(path, newJson);
    }
}
