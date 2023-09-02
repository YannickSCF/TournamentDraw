using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YannickSCF.TournamentDraw.Scriptables {
    [CreateAssetMenu(fileName = "Settings Configuration", menuName = "Scriptable Objects/YannickSCF/Tournament Draw/Settings", order = 0)]
    public class SettingsConfiguration : ScriptableObject {

        [Header("VOLUME SETTINGS")]
        [SerializeField] private bool _generalVolumeMuted;
        [SerializeField] private float _generalVolume;

        [Space]
        [SerializeField] private bool _musicVolumeMuted;
        [SerializeField] private float _musicVolume;

        [Space]
        [SerializeField] private bool _sfxVolumeMuted;
        [SerializeField] private float _sfxVolume;

        public bool GeneralVolumeMuted { get => _generalVolumeMuted; set => _generalVolumeMuted = value; }
        public float GeneralVolume { get => _generalVolume; set => _generalVolume = value; }
        public bool MusicVolumeMuted { get => _musicVolumeMuted; set => _musicVolumeMuted = value; }
        public float MusicVolume { get => _musicVolume; set => _musicVolume = value; }
        public bool SFXVolumeMuted { get => _sfxVolumeMuted; set => _sfxVolumeMuted = value; }
        public float SFXVolume { get => _sfxVolume; set => _sfxVolume = value; }

        public void ResetConfiguration() {
            _generalVolumeMuted = false;
            _generalVolume = 1f;

            _musicVolumeMuted = false;
            _musicVolume = 1f;

            _sfxVolumeMuted = false;
            _sfxVolume = 1f;
        }
    }
}
