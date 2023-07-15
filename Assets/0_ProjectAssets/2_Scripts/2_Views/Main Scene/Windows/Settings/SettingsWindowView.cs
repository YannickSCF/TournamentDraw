using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.GeneralApp.View.UI.Windows;
using YannickSCF.TournamentDraw.Views.CommonComponents.Volume;
using YannickSCF.TournamentDraw.Views.CommonEvents.Settings;
using static YannickSCF.GeneralApp.CommonEventsDelegates;

namespace YannickSCF.TournamentDraw.Views.MainScene.Windows.Settings {
    public class SettingsWindowView : WindowView {

        public event SimpleEventDelegate OnCloseSettings;

        [SerializeField] private Button _closeButton;
        [SerializeField] private VolumeView _generalVolume;
        [SerializeField] private VolumeView _musicVolume;
        [SerializeField] private VolumeView _sfxVolume;

        [SerializeField] private Animator _animator;

        #region Mono
        private void OnEnable() {
            _closeButton.onClick.AddListener(CloseSettings);

            _generalVolume.OnMuteButtonPressed += GeneralMuted;
            _generalVolume.OnVolumeSettedPressed += GeneralSetted;

            _musicVolume.OnMuteButtonPressed += MusicMuted;
            _musicVolume.OnVolumeSettedPressed += MusicSetted;

            _sfxVolume.OnMuteButtonPressed += SFXMuted;
            _sfxVolume.OnVolumeSettedPressed += SFXSetted;
        }

        private void OnDisable() {
            _closeButton.onClick.RemoveAllListeners();

            _generalVolume.OnMuteButtonPressed -= GeneralMuted;
            _generalVolume.OnVolumeSettedPressed -= GeneralSetted;

            _musicVolume.OnMuteButtonPressed -= MusicMuted;
            _musicVolume.OnVolumeSettedPressed -= MusicSetted;

            _sfxVolume.OnMuteButtonPressed -= SFXMuted;
            _sfxVolume.OnVolumeSettedPressed -= SFXSetted;
        }
        #endregion

        #region Event listeners methods
        private void CloseSettings() {
            OnCloseSettings?.Invoke();
        }

        private void GeneralMuted(bool isOn) {
            SettingsViewsEvents.ThrowOnGeneralVolumeMuted(isOn);
        }
        private void GeneralSetted(float volumeValue) {
            SettingsViewsEvents.ThrowOnGeneralVolumeChanged(volumeValue);
        }

        private void MusicMuted(bool isOn) {
            SettingsViewsEvents.ThrowOnMusicVolumeMuted(isOn);
        }
        private void MusicSetted(float volumeValue) {
            SettingsViewsEvents.ThrowOnMusicVolumeChanged(volumeValue);
        }

        private void SFXMuted(bool isOn) {
            SettingsViewsEvents.ThrowOnSFXVolumeMuted(isOn);
        }
        private void SFXSetted(float volumeValue) {
            SettingsViewsEvents.ThrowOnSFXVolumeChanged(volumeValue);
        }
        #endregion

        public override void Open() {
            base.Open();
            _animator.SetBool("Show", true);
        }

        public override void Show() {
            base.Show();
            _animator.SetBool("Show", true);
        }

        public override void Hide() {
            _animator.SetBool("Show", false);
            StartCoroutine(WaitToAnimationsEnds(base.Hide));
        }

        public override void Close() {
            _animator.SetBool("Show", false);
            StartCoroutine(WaitToAnimationsEnds(base.Close));
        }

        private IEnumerator WaitToAnimationsEnds(Action actionToDo) {
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("settings_hidden"));
            actionToDo?.Invoke();
        }

        public void SetGeneralVolume(bool isMuted, float volumeValue) {
            _generalVolume.SetMuted(isMuted, true);
            _generalVolume.SetValue(volumeValue, true);

            SetGeneralVolumeSliderInteractable(!isMuted);
        }
        public void SetGeneralVolumeSliderInteractable(bool isInteractable) {
            _generalVolume.SetSliderInteractable(isInteractable);
        }

        public void SetMusicVolume(bool isMuted, float volumeValue) {
            _musicVolume.SetMuted(isMuted, true);
            _musicVolume.SetValue(volumeValue, true);

            SetMusicVolumeSliderInteractable(!isMuted);
        }
        public void SetMusicVolumeSliderInteractable(bool isInteractable) {
            _musicVolume.SetSliderInteractable(isInteractable);
        }

        public void SetSFXVolume(bool isMuted, float volumeValue) {
            _sfxVolume.SetMuted(isMuted, true);
            _sfxVolume.SetValue(volumeValue, true);

            SetSFXVolumeSliderInteractable(!isMuted);
        }
        public void SetSFXVolumeSliderInteractable(bool isInteractable) {
            _sfxVolume.SetSliderInteractable(isInteractable);
        }
    }
}
