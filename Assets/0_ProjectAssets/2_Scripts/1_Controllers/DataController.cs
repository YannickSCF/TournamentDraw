using UnityEngine;
using YannickSCF.LSTournaments.Common.Scriptables.Data;
using YannickSCF.TournamentDraw.Scriptables;

namespace YannickSCF.TournamentDraw.MainManagers.Controllers {
    public class DataController : MonoBehaviour {
        private const string DRAW_CONFIGURATION_PLAYER_PREF = "DrawConfiguration";
        private const string SETTINGS_CONFIGURATION_PLAYER_PREF = "SettingsConfiguration";

        #region Draw Configuration
        public bool HasDrawConfigurationSaved() {
            return PlayerPrefs.HasKey(DRAW_CONFIGURATION_PLAYER_PREF);
        }

        public void SaveDrawConfiguration(TournamentData config) {
            PlayerPrefs.SetString(DRAW_CONFIGURATION_PLAYER_PREF, JsonUtility.ToJson(config));
            PlayerPrefs.Save();
        }

        public TournamentData GetDrawConfiguration(TournamentData config) {
            string drawConfigJSON = PlayerPrefs.GetString(DRAW_CONFIGURATION_PLAYER_PREF);
            JsonUtility.FromJsonOverwrite(drawConfigJSON, config);
            return config;
        }
        #endregion

        #region Settings Configuration
        public bool HasSettingsConfigurationSaved() {
            return PlayerPrefs.HasKey(SETTINGS_CONFIGURATION_PLAYER_PREF);
        }

        public void SaveSettingsConfiguration(SettingsConfiguration settings) {
            PlayerPrefs.SetString(SETTINGS_CONFIGURATION_PLAYER_PREF, JsonUtility.ToJson(settings));
            PlayerPrefs.Save();
        }

        public SettingsConfiguration GetSettingsConfiguration(SettingsConfiguration settings) {
            string settingsConfigJSON = PlayerPrefs.GetString(SETTINGS_CONFIGURATION_PLAYER_PREF);
            JsonUtility.FromJsonOverwrite(settingsConfigJSON, settings);
            return settings;
        }
        #endregion
    }
}
