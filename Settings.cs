using Il2Cpp;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using static MelonLoader.MelonLogger;

namespace VoidLib2
{
    public static class Settings
    {
        private static readonly System.Reflection.FieldInfo? _colorGradingField;

        private static SettingsManager? _instance;
        private static SettingsManager Instance =>
            _instance ??= GameObject.FindObjectOfType<SettingsManager>();

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
            get => Instance.settings.masterVolume;
            set
            {
                Instance.settings.masterVolume = value;
                Instance.mixer.SetFloat("Master", Mathf.Log10(value) * 20f);
            }
        }

        /// <summary>Music volume in the range (0, 1].</summary>
        public static float MusicVolume
        {
            get => Instance.settings.musicVolume;
            set
            {
                Instance.settings.musicVolume = value;
                Instance.mixer.SetFloat("Music", Mathf.Log10(value) * 20f);
            }
        }

        /// <summary>SFX volume in the range (0, 1].</summary>
        public static float SFXVolume
        {
            get => Instance.settings.fxVolume;
            set
            {
                Instance.settings.fxVolume = value;
                Instance.mixer.SetFloat("SFX", Mathf.Log10(value) * 20f);
            }
        }

        /// <summary>Vocal volume in the range (0, 1].</summary>
        public static float VocalVolume
        {
            get => Instance.settings.vocalVolume;
            set
            {
                Instance.settings.vocalVolume = value;
                Instance.mixer.SetFloat("Vocals", Mathf.Log10(value) * 20f);
            }
        }

        /// <summary>Ambience volume in the range (0, 1].</summary>
        public static float AmbienceVolume
        {
            get => Instance.settings.ambient;
            set
            {
                Instance.settings.ambient = value;
                Instance.mixer.SetFloat("Ambience", Mathf.Log10(value) * 20f);
            }
        }

        /// <summary>Whether the game runs in fullscreen mode.</summary>
        public static bool Fullscreen
        {
            get => Instance.settings.fullscreen;
            set
            {
                Instance.settings.fullscreen = value;
                Screen.fullScreen = value;
            }
        }

        /// <summary>Whether VSync is enabled.</summary>
        public static bool VSync
        {
            get => Instance.settings.VSync;
            set
            {
                Instance.settings.VSync = value;
                QualitySettings.vSyncCount = value ? 1 : 0;
            }
        }

        /// <summary>Field of view in degrees. Also applies to the player camera if available.</summary>
        public static float FOV
        {
            get => Instance.settings.fov;
            set
            {
                Instance.settings.fov = value;
                Instance.fov = value;
                if (PlayerManager.instance && PlayerManager.instance.playerCam)
                    PlayerManager.instance.playerCam.fieldOfView = value;
            }
        }

        /// <summary>Mouse sensitivity. Also applies to the player look component if available.</summary>
        public static float Sensitivity
        {
            get => Instance.settings.sensitivity;
            set
            {
                Instance.settings.sensitivity = value;
                Instance.sensitivity = value;
                if (PlayerManager.instance && PlayerManager.instance.look)
                    PlayerManager.instance.look.sensitivity = value;
            }
        }

        /// <summary>Screen brightness via post-processing color grading.</summary>
        public static float Brightness
        {
            get => Instance.settings.brightness;
            set
            {
                Instance.settings.brightness = value;
                ColorGrading? cg = GetColorGrading();
                if (cg != null)
                    cg.brightness.value = value;
            }
        }

        /// <summary>Quality level index as defined in Unity's Quality Settings.</summary>
        public static int Quality
        {
            get => (int)Instance.settings.qualityIndex;
            set
            {
                Instance.settings.qualityIndex = value;
                QualitySettings.SetQualityLevel(value);
            }
        }

        /// <summary>Anti-aliasing level as a 0-3 index into { 0, 2, 4, 8 } sample counts.</summary>
        public static int AntiAliasing
        {
            get => Mathf.RoundToInt(Instance.settings.aaLevel);
            set
            {
                int[] aaOptions = { 0, 2, 4, 8 };
                value = Mathf.Clamp(value, 0, aaOptions.Length - 1);
                Instance.settings.aaLevel = value;
                QualitySettings.antiAliasing = aaOptions[value];
            }
        }
    }
}