using UnityEngine;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Views.CommonEvents.Settings;

namespace YannickSCF.TournamentDraw.Controllers.ThreeD {
    public class FireplaceController : MonoBehaviour {

        [SerializeField] private AudioSource _audioSource;

        #region Mono
        private void Start() {
            _audioSource.mute = SettingsManager.Instance.Settings.SFXVolumeMuted;
            _audioSource.volume = SettingsManager.Instance.Settings.SFXVolume;
        }

        private void OnEnable() {
            SettingsViewsEvents.OnSFXVolumeMuted += SFXVolumeMuted;
            SettingsViewsEvents.OnSFXVolumeChanged += SFXVolumeChanged;
        }

        private void OnDisable() {
            SettingsViewsEvents.OnSFXVolumeMuted -= SFXVolumeMuted;
            SettingsViewsEvents.OnSFXVolumeChanged -= SFXVolumeChanged;
        }
        #endregion

        #region Event listeners methods
        private void SFXVolumeMuted(bool mute) {
            _audioSource.mute = mute;
        }

        private void SFXVolumeChanged(float volumeValue) {
            _audioSource.volume = volumeValue;
        }
        #endregion
    }
}
