/**
 * Author:      Yannick Santa Cruz Feuillias
 * Created:     03/09/2023
 **/

// Dependencies
using UnityEngine;

namespace YannickSCF.TournamentDraw.Scriptables {
    [CreateAssetMenu(fileName = "Settings Configuration", menuName = "YannickSCF/LS Tournaments/Tournament Draw/Settings", order = 0)]
    public class SettingsModel : ScriptableObject {

        public enum SettingType {
            GeneralVolumeMute, GeneralVolume,
            MusicVolumeMute, MusicVolume,
            SFXVolumeMute, SFXVolume
        };

        public delegate void SettingsChangedEventDelegate(SettingType type);
        public event SettingsChangedEventDelegate SettingsChanged;

        [Header("VOLUME SETTINGS")]
        [SerializeField] private bool _generalVolumeMuted;
        [SerializeField] private float _generalVolume;

        [Space]
        [SerializeField] private bool _musicVolumeMuted;
        [SerializeField] private float _musicVolume;

        [Space]
        [SerializeField] private bool _sfxVolumeMuted;
        [SerializeField] private float _sfxVolume;

        public bool GeneralVolumeMuted {
            get => _generalVolumeMuted;
            set {
                SettingsChanged?.Invoke(SettingType.GeneralVolumeMute);
                _generalVolumeMuted = value;
            }
        }

        public float GeneralVolume {
            get => _generalVolume;
            set {
                SettingsChanged?.Invoke(SettingType.GeneralVolume);
                _generalVolume = value;
            }
        }

        public bool MusicVolumeMuted {
            get => _musicVolumeMuted;
            set {
                SettingsChanged?.Invoke(SettingType.MusicVolumeMute);
                _musicVolumeMuted = value;
            }
        }

        public float MusicVolume {
            get => _musicVolume;
            set {
                SettingsChanged?.Invoke(SettingType.MusicVolume);
                _musicVolume = value;
            }
        }

        public bool SFXVolumeMuted {
            get => _sfxVolumeMuted;
            set {
                SettingsChanged?.Invoke(SettingType.SFXVolumeMute);
                _sfxVolumeMuted = value;
            }
        }

        public float SFXVolume {
            get => _sfxVolume;
            set {
                SettingsChanged?.Invoke(SettingType.SFXVolume);
                _sfxVolume = value;
            }
        }

        public void Reset() {
            _generalVolumeMuted = false;
            _generalVolume = 1f;

            _musicVolumeMuted = false;
            _musicVolume = 1f;

            _sfxVolumeMuted = false;
            _sfxVolume = 1f;
        }
    }
}
