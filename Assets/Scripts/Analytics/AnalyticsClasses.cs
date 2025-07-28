using System;

// All analytics classes for the forklift simulation are defined here.

namespace Analytics
{
    [System.Serializable]
    public class ForkliftData
    {
        public Module module;
        public GameData gameData;
    }

    [System.Serializable]
    public class Module
    {
        public string moduleName;
        public string moduleType;
        public string moduleVersion;
        public string moduleDescription;
    }

    [System.Serializable]
    public class GameData
    {
        public string startTime;
        public string endTime;
        public float duration;
    } 
}

