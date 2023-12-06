/**
 * Author:      Yannick Santa Cruz Feuillias
 * Created:     29/11/2023
 **/

// Dependencies
using UnityEngine;
// Custom dependencies
using YannickSCF.GeneralApp;
using YannickSCF.TournamentDraw.Scriptables;

namespace YannickSCF.TournamentDraw.MainManagers.Controllers {
    public class SettingsManager : GlobalSingleton<SettingsManager> {
        private const string SETTINGS_CONFIGURATION_PLAYER_PREF = "SettingsConfiguration";
        private const string DRAW_SETTINGS_CONFIGURATION_PLAYER_PREF = "DrawSettingsConfiguration";

        [SerializeField] private SettingsModel _settings;
        [SerializeField] private DrawSettingsModel _drawSettings;

        public SettingsModel Settings { get => _settings; }
        public DrawSettingsModel DrawSettings { get => _drawSettings; }

        #region Mono
        private void OnEnable() {
            if (_settings != null) _settings.SettingsChanged += OnAnySettingChanged;
            else Debug.LogWarning("SETTINGS doesn't referenced!");

            if (_settings != null) _settings.SettingsChanged += OnAnySettingChanged;
            else Debug.LogWarning("DRAW SETTINGS doesn't referenced!");
        }

        private void OnDisable() {
            if (_settings != null) _settings.SettingsChanged -= OnAnySettingChanged;
            else Debug.LogWarning("SETTINGS doesn't referenced!");

            if (_drawSettings != null) _drawSettings.DrawSettingsChanged -= OnAnyDrawSettingChanged;
            else Debug.LogWarning("DRAW SETTINGS doesn't referenced!");
        }
        #endregion

        #region Settings methods
        public bool HasSettingsSaved() {
            return PlayerPrefs.HasKey(SETTINGS_CONFIGURATION_PLAYER_PREF);
        }

        public void SaveSettings() {
            PlayerPrefs.SetString(SETTINGS_CONFIGURATION_PLAYER_PREF, JsonUtility.ToJson(_settings));
            PlayerPrefs.Save();
        }

        public SettingsModel LoadSettings() {
            string settingsConfigJSON = PlayerPrefs.GetString(SETTINGS_CONFIGURATION_PLAYER_PREF);
            
            if (!string.IsNullOrEmpty(settingsConfigJSON)) {
                JsonUtility.FromJsonOverwrite(settingsConfigJSON, _settings);
            }

            return _settings;
        }

        public void ResetSettings() {
            _settings.Reset();
        }

        #region Private Methods
        private void OnAnySettingChanged(SettingsModel.SettingType type) {
            SaveSettings();
        }
        #endregion
        #endregion

        #region Draw Settings methods
        public bool HasDrawSettingsSaved() {
            return PlayerPrefs.HasKey(DRAW_SETTINGS_CONFIGURATION_PLAYER_PREF);
        }

        public void SaveDrawSettings() {
            PlayerPrefs.SetString(DRAW_SETTINGS_CONFIGURATION_PLAYER_PREF, JsonUtility.ToJson(_drawSettings));
            PlayerPrefs.Save();
        }

        public DrawSettingsModel LoadDrawSettings() {
            string drawSettingsConfigJSON = PlayerPrefs.GetString(DRAW_SETTINGS_CONFIGURATION_PLAYER_PREF);
            
            if (!string.IsNullOrEmpty(drawSettingsConfigJSON)) {
                JsonUtility.FromJsonOverwrite(drawSettingsConfigJSON, _drawSettings);
            }

            return _drawSettings;
        }

        public void ResetDrawSettings() {
            _drawSettings.Reset();
        }

        #region Private Methods
        private void OnAnyDrawSettingChanged(DrawSettingsModel.DrawSettingType type) {
            SaveDrawSettings();
        }
        #endregion
        #endregion
    }
}
