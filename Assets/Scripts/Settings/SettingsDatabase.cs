using UnityEngine;

namespace CharacterControl.Settings
{
    [System.Serializable]
    public class SettingsData
    {
        public float MasterVolume = 0f;
        public float MouseSensitivity = 8f;
        public int GraphicsQuality = 1;
        public bool SubtitlesEnabled = true;
    }

    public static class SettingsDatabase
    {
        private static SettingsData _currentSettings = new SettingsData();

        public static SettingsData CurrentSettings => _currentSettings;

        public static void SaveSettings(SettingsData newSettings)
        {
            _currentSettings = newSettings;
        }

        public static SettingsData LoadSettings()
        {
            return _currentSettings;
        }

        public static void ResetToDefault()
        {
            _currentSettings = new SettingsData();
        }
    }
}
