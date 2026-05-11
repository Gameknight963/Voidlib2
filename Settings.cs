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

        /// <summary>Sets the master volume.</summary>
        /// <param name="volume">Volume level in the range (0, 1].</param>
        public static void SetMasterVolume(float volume)
        {
            SettingsManager.Instance.settings.masterVolume = volume;
            SettingsManager.Instance.mixer.SetFloat("Master", Mathf.Log10(volume) * 20f);
        }

        /// <summary>Sets the music volume.</summary>
        /// <param name="volume">Volume level in the range (0, 1].</param>
        public static void SetMusicVolume(float volume)
        {
            SettingsManager.Instance.settings.musicVolume = volume;
            SettingsManager.Instance.mixer.SetFloat("Music", Mathf.Log10(volume) * 20f);
        }

        /// <summary>Sets the SFX volume.</summary>
        /// <param name="volume">Volume level in the range (0, 1].</param>
        public static void SetSFXVolume(float volume)
        {
            SettingsManager.Instance.settings.fxVolume = volume;
            SettingsManager.Instance.mixer.SetFloat("SFX", Mathf.Log10(volume) * 20f);
        }

        /// <summary>Sets the vocal volume.</summary>
        /// <param name="volume">Volume level in the range (0, 1].</param>
        public static void SetVocalVolume(float volume)
        {
            SettingsManager.Instance.settings.vocalVolume = volume;
            SettingsManager.Instance.mixer.SetFloat("Vocals", Mathf.Log10(volume) * 20f);
        }

        /// <summary>Sets the ambience volume.</summary>
        /// <param name="volume">Volume level in the range (0, 1].</param>
        public static void SetAmbienceVolume(float volume)
        {
            SettingsManager.Instance.settings.ambient = volume;
            SettingsManager.Instance.mixer.SetFloat("Ambience", Mathf.Log10(volume) * 20f);
        }

        /// <summary>Sets whether the game runs in fullscreen mode.</summary>
        /// <param name="fullscreen">True for fullscreen, false for windowed.</param>
        public static void SetFullscreen(bool fullscreen)
        {
            SettingsManager.Instance.settings.fullscreen = fullscreen;
            Screen.fullScreen = fullscreen;
        }

        /// <summary>Sets whether VSync is enabled.</summary>
        /// <param name="vsync">True to enable VSync, false to disable.</param>
        public static void SetVSync(bool vsync)
        {
            SettingsManager.Instance.settings.VSync = vsync;
            QualitySettings.vSyncCount = vsync ? 1 : 0;
        }

        /// <summary>Sets the field of view and applies it to the player camera if available.</summary>
        /// <param name="fov">Field of view in degrees.</param>
        public static void SetFOV(float fov)
        {
            SettingsManager.Instance.settings.fov = fov;
            SettingsManager.Instance.fov = fov;
            if (PlayerManager.instance && PlayerManager.instance.playerCam)
                PlayerManager.instance.playerCam.fieldOfView = fov;
        }

        /// <summary>Sets the mouse sensitivity and applies it to the player look component if available.</summary>
        /// <param name="sensitivity">Sensitivity value.</param>
        public static void SetSensitivity(float sensitivity)
        {
            SettingsManager.Instance.settings.sensitivity = sensitivity;
            SettingsManager.Instance.sensitivity = sensitivity;
            if (PlayerManager.instance && PlayerManager.instance.look)
                PlayerManager.instance.look.sensitivity = sensitivity;
        }

        /// <summary>Sets the screen brightness via post-processing color grading.</summary>
        /// <param name="brightness">Brightness value.</param>
        public static void SetBrightness(float brightness)
        {
            SettingsManager.Instance.settings.brightness = brightness;
            ColorGrading cg = GetColorGrading()!;
            cg.brightness.value = brightness;
        }

        /// <summary>Sets the overall quality level.</summary>
        /// <param name="index">Quality level index as defined in Unity's Quality Settings.</param>
        public static void SetQuality(int index)
        {
            SettingsManager.Instance.settings.qualityIndex = index;
            QualitySettings.SetQualityLevel(index);
        }

        /// <summary>
        /// 0-3 index into { 0, 2, 4, 8 }. Does not match the actual AA sample count.
        /// </summary>
        /// <param name="level"></param>
        public static void SetAntiAliasing(int level)
        {
            int[] aaOptions = { 0, 2, 4, 8 };
            level = Mathf.Clamp(level, 0, aaOptions.Length - 1);
            SettingsManager.Instance.settings.aaLevel = level;
            QualitySettings.antiAliasing = aaOptions[level];
        }
    }
}
