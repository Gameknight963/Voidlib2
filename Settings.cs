using Il2Cpp;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace VoidLib2
{
    public static class Settings
    {
        private static readonly System.Reflection.FieldInfo? _colorGradingField;

        public static readonly SettingsManager SettingsInstance = Il2Cpp.Void.instance.settings;

        static Settings()
        {
            _colorGradingField = typeof(SettingsManager).GetField("colorGrading",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        }

        private static ColorGrading? GetColorGrading() =>
            _colorGradingField?.GetValue(SettingsManager.Instance) as ColorGrading;

        /// <summary>Master volume in the range (0, 1].</summary>
        public static float MasterVolume
        {
            get => SettingsInstance.settings.masterVolume;
            set
            {
                SettingsInstance.settings.masterVolume = value;
                SettingsInstance.mixer.SetFloat("Master", Mathf.Log10(value) * 20f);
            }
        }

        /// <summary>Music volume in the range (0, 1].</summary>
        public static float MusicVolume
        {
            get => SettingsInstance.settings.musicVolume;
            set
            {
                SettingsInstance.settings.musicVolume = value;
                SettingsInstance.mixer.SetFloat("Music", Mathf.Log10(value) * 20f);
            }
        }

        /// <summary>SFX volume in the range (0, 1].</summary>
        public static float SFXVolume
        {
            get => SettingsInstance.settings.fxVolume;
            set
            {
                SettingsInstance.settings.fxVolume = value;
                SettingsInstance.mixer.SetFloat("SFX", Mathf.Log10(value) * 20f);
            }
        }

        /// <summary>Vocal volume in the range (0, 1].</summary>
        public static float VocalVolume
        {
            get => SettingsInstance.settings.vocalVolume;
            set
            {
                SettingsInstance.settings.vocalVolume = value;
                SettingsInstance.mixer.SetFloat("Vocals", Mathf.Log10(value) * 20f);
            }
        }

        /// <summary>Ambience volume in the range (0, 1].</summary>
        public static float AmbienceVolume
        {
            get => SettingsInstance.settings.ambient;
            set
            {
                SettingsInstance.settings.ambient = value;
                SettingsInstance.mixer.SetFloat("Ambience", Mathf.Log10(value) * 20f);
            }
        }

        /// <summary>Whether the game runs in fullscreen mode.</summary>
        public static bool Fullscreen
        {
            get => SettingsInstance.settings.fullscreen;
            set
            {
                SettingsInstance.settings.fullscreen = value;
                Screen.fullScreen = value;
            }
        }

        /// <summary>Whether VSync is enabled.</summary>
        public static bool VSync
        {
            get => SettingsInstance.settings.VSync;
            set
            {
                SettingsInstance.settings.VSync = value;
                QualitySettings.vSyncCount = value ? 1 : 0;
            }
        }

        /// <summary>Field of view in degrees. Also applies to the player camera if available.</summary>
        public static float FOV
        {
            get => SettingsInstance.settings.fov;
            set
            {
                SettingsInstance.settings.fov = value;
                SettingsInstance.fov = value;
                if (PlayerManager.instance && PlayerManager.instance.playerCam)
                    PlayerManager.instance.playerCam.fieldOfView = value;
            }
        }

        /// <summary>Mouse sensitivity. Also applies to the player look component if available.</summary>
        public static float Sensitivity
        {
            get => SettingsInstance.settings.sensitivity;
            set
            {
                SettingsInstance.settings.sensitivity = value;
                SettingsInstance.sensitivity = value;
                if (PlayerManager.instance && PlayerManager.instance.look)
                    PlayerManager.instance.look.sensitivity = value;
            }
        }

        /// <summary>Screen brightness via post-processing color grading.</summary>
        public static float Brightness
        {
            get => SettingsInstance.settings.brightness;
            set
            {
                SettingsInstance.settings.brightness = value;
                ColorGrading? cg = GetColorGrading();
                if (cg != null)
                    cg.brightness.value = value;
            }
        }

        /// <summary>Quality level index as defined in Unity's Quality Settings.</summary>
        public static int Quality
        {
            get => (int)SettingsInstance.settings.qualityIndex;
            set
            {
                SettingsInstance.settings.qualityIndex = value;
                QualitySettings.SetQualityLevel(value);
            }
        }

        /// <summary>Anti-aliasing level as a 0-3 index into { 0, 2, 4, 8 } sample counts.</summary>
        public static int AntiAliasing
        {
            get => Mathf.RoundToInt(SettingsInstance.settings.aaLevel);
            set
            {
                int[] aaOptions = { 0, 2, 4, 8 };
                value = Mathf.Clamp(value, 0, aaOptions.Length - 1);
                SettingsInstance.settings.aaLevel = value;
                QualitySettings.antiAliasing = aaOptions[value];
            }
        }
    }
}