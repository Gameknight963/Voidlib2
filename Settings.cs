using Il2Cpp;
using UnityEngine;

namespace VoidLib2
{
    public static class Settings
    {
        static readonly SettingsManager SettingsManager = Il2Cpp.Void.instance.settings;

        public enum GameSetting
        {
            fov,
            sensitivity,
        }
        public static float GetSetting(GameSetting setting)
        {
            return setting switch
            {
                GameSetting.fov => SettingsManager.fov,
                GameSetting.sensitivity => SettingsManager.sensitivity,
                _ => throw new ArgumentOutOfRangeException($"GameSetting {setting} is out of range")
            };
        }
        public static void ChangeSetting(GameSetting setting, float value)
        {
            switch (setting)
            {
                case GameSetting.fov:
                    SettingsManager.fov = value;
                    return;
                case GameSetting.sensitivity:
                    SettingsManager.sensitivity = value;
                    return;
            }
            throw new ArgumentOutOfRangeException($"GameSetting {setting} is out of range");
        }
    }
}
