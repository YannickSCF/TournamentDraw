using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.TournamentDraw.Scriptables;

namespace YannickSCF.TournamentDraw.MainManagers.Controllers {
    public class DataController : MonoBehaviour {
        private const string DRAW_CONFIGURATION_PLAYER_PREF = "DrawConfiguration";

        public bool HasDrawConfigurationSaved() {
            return PlayerPrefs.HasKey(DRAW_CONFIGURATION_PLAYER_PREF);
        }

        public void SaveDrawConfiguration(DrawConfiguration config) {
            PlayerPrefs.SetString(DRAW_CONFIGURATION_PLAYER_PREF, JsonUtility.ToJson(config));
            PlayerPrefs.Save();
        }

        public DrawConfiguration GetDrawConfiguration(DrawConfiguration config) {
            string drawConfigJSON = PlayerPrefs.GetString(DRAW_CONFIGURATION_PLAYER_PREF);
            JsonUtility.FromJsonOverwrite(drawConfigJSON, config);
            return config;
        }
    }
}
