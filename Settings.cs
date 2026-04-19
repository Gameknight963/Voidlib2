using Il2Cpp;
using UnityEngine;

namespace VoidLib2
{
    public static class Settings
    {
        static readonly SettingsManager settingsManager;

        static Settings()
        {
            settingsManager = GameObject.Find("PlayerPrefs").GetComponent<SettingsManager>();
        }

        public enum GameSetting
        {
            fov,
            sensitivity,
        }
        public static float GetSetting(GameSetting setting)
        {
            return setting switch
            {
                GameSetting.fov => settingsManager.fov,
                GameSetting.sensitivity => settingsManager.sensitivity,
                _ => throw new ArgumentOutOfRangeException($"GameSetting {setting} is out of range")
            };
        }
        public static void ChangeSetting(GameSetting setting, float value)
        {
            switch (setting)
            {
                case GameSetting.fov:
                    settingsManager.fov = value;
                    return;
                case GameSetting.sensitivity:
                    settingsManager.sensitivity = value;
                    return;
            }
            throw new ArgumentOutOfRangeException($"GameSetting {setting} is out of range");
        }
    }
}
