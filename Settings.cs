using Il2Cpp;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace VoidLib2
{
    public static class Settings
    {
        private static readonly System.Reflection.FieldInfo? _colorGradingField;

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
            get => SettingsManager.Instance.settings.masterVolume;
            set
            {
                SettingsManager.Instance.settings.masterVolume = value;
                SettingsManager.Instance.mixer.SetFloat("Master", Mathf.Log10(value) * 20f);
            }
        }

        /// <summary>Music volume in the range (0, 1].</summary>
        public static float MusicVolume
        {
            get => SettingsManager.Instance.settings.musicVolume;
            set
            {
                SettingsManager.Instance.settings.musicVolume = value;
                SettingsManager.Instance.mixer.SetFloat("Music", Mathf.Log10(value) * 20f);
            }
        }

        /// <summary>SFX volume in the range (0, 1].</summary>
        public static float SFXVolume
        {
            get => SettingsManager.Instance.settings.fxVolume;
            set
            {
                SettingsManager.Instance.settings.fxVolume = value;
                SettingsManager.Instance.mixer.SetFloat("SFX", Mathf.Log10(value) * 20f);
            }
        }

        /// <summary>Vocal volume in the range (0, 1].</summary>
        public static float VocalVolume
        {
            get => SettingsManager.Instance.settings.vocalVolume;
            set
            {
                SettingsManager.Instance.settings.vocalVolume = value;
                SettingsManager.Instance.mixer.SetFloat("Vocals", Mathf.Log10(value) * 20f);
            }
        }

        /// <summary>Ambience volume in the range (0, 1].</summary>
        public static float AmbienceVolume
        {
            get => SettingsManager.Instance.settings.ambient;
            set
            {
                SettingsManager.Instance.settings.ambient = value;
                SettingsManager.Instance.mixer.SetFloat("Ambience", Mathf.Log10(value) * 20f);
            }
        }

        /// <summary>Whether the game runs in fullscreen mode.</summary>
        public static bool Fullscreen
        {
            get => SettingsManager.Instance.settings.fullscreen;
            set
            {
                SettingsManager.Instance.settings.fullscreen = value;
                Screen.fullScreen = value;
            }
        }

        /// <summary>Whether VSync is enabled.</summary>
        public static bool VSync
        {
            get => SettingsManager.Instance.settings.VSync;
            set
            {
                SettingsManager.Instance.settings.VSync = value;
                QualitySettings.vSyncCount = value ? 1 : 0;
            }
        }

        /// <summary>Field of view in degrees. Also applies to the player camera if available.</summary>
        public static float FOV
        {
            get => SettingsManager.Instance.settings.fov;
            set
            {
                SettingsManager.Instance.settings.fov = value;
                SettingsManager.Instance.fov = value;
                if (PlayerManager.instance && PlayerManager.instance.playerCam)
                    PlayerManager.instance.playerCam.fieldOfView = value;
            }
        }

        /// <summary>Mouse sensitivity. Also applies to the player look component if available.</summary>
        public static float Sensitivity
        {
            get => SettingsManager.Instance.settings.sensitivity;
            set
            {
                SettingsManager.Instance.settings.sensitivity = value;
                SettingsManager.Instance.sensitivity = value;
                if (PlayerManager.instance && PlayerManager.instance.look)
                    PlayerManager.instance.look.sensitivity = value;
            }
        }

        /// <summary>Screen brightness via post-processing color grading.</summary>
        public static float Brightness
        {
            get => SettingsManager.Instance.settings.brightness;
            set
            {
                SettingsManager.Instance.settings.brightness = value;
                ColorGrading? cg = GetColorGrading();
                if (cg != null)
                    cg.brightness.value = value;
            }
        }

        /// <summary>Quality level index as defined in Unity's Quality Settings.</summary>
        public static int Quality
        {
            get => (int)SettingsManager.Instance.settings.qualityIndex;
            set
            {
                SettingsManager.Instance.settings.qualityIndex = value;
                QualitySettings.SetQualityLevel(value);
            }
        }

        /// <summary>Anti-aliasing level as a 0-3 index into { 0, 2, 4, 8 } sample counts.</summary>
        public static int AntiAliasing
        {
            get => Mathf.RoundToInt(SettingsManager.Instance.settings.aaLevel);
            set
            {
                int[] aaOptions = { 0, 2, 4, 8 };
                value = Mathf.Clamp(value, 0, aaOptions.Length - 1);
                SettingsManager.Instance.settings.aaLevel = value;
                QualitySettings.antiAliasing = aaOptions[value];
            }
        }
    }
}