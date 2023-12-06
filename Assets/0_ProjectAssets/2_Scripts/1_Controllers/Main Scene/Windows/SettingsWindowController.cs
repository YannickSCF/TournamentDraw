using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.GeneralApp.Controller.UI.Windows;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Scriptables;
using YannickSCF.TournamentDraw.Views.CommonEvents.Settings;
using YannickSCF.TournamentDraw.Views.MainScene.Windows.Settings;

namespace YannickSCF.TournamentDraw.Controllers.MainScene.Windows.Settings {
    public class SettingsWindowController : WindowController<SettingsWindowView> {

        private Action _closeAction;

        #region Mono
        protected override void OnEnable() {
            base.OnEnable();

            View.OnCloseSettings += CloseWindow;

            SettingsManager.Instance.Settings.SettingsChanged += OnSettingChanged;

            SettingsViewsEvents.OnGeneralVolumeMuted += GeneralVolumeMuted;
            SettingsViewsEvents.OnGeneralVolumeChanged += GeneralVolumeChanged;
            SettingsViewsEvents.OnMusicVolumeMuted += MusicVolumeMuted;
            SettingsViewsEvents.OnMusicVolumeChanged += MusicVolumeChanged;
            SettingsViewsEvents.OnSFXVolumeMuted += SFXVolumeMuted;
            SettingsViewsEvents.OnSFXVolumeChanged += SFXVolumeChanged;
        }

        protected override void OnDisable() {
            base.OnDisable();

            View.OnCloseSettings -= CloseWindow;

            SettingsManager.Instance.Settings.SettingsChanged -= OnSettingChanged;

            SettingsViewsEvents.OnGeneralVolumeMuted -= GeneralVolumeMuted;
            SettingsViewsEvents.OnGeneralVolumeChanged -= GeneralVolumeChanged;
            SettingsViewsEvents.OnMusicVolumeMuted -= MusicVolumeMuted;
            SettingsViewsEvents.OnMusicVolumeChanged -= MusicVolumeChanged;
            SettingsViewsEvents.OnSFXVolumeMuted -= SFXVolumeMuted;
            SettingsViewsEvents.OnSFXVolumeChanged -= SFXVolumeChanged;
        }
        #endregion

        #region Event listeners methods
        private void CloseWindow() {
            _closeAction?.Invoke();
        }

        private void OnSettingChanged(SettingsModel.SettingType type) {
            switch (type) {
                case SettingsModel.SettingType.GeneralVolumeMute:
                    View.SetGeneralVolumeSliderInteractable(!SettingsManager.Instance.Settings.GeneralVolumeMuted);
                    break;
                case SettingsModel.SettingType.MusicVolumeMute:
                    View.SetMusicVolumeSliderInteractable(!SettingsManager.Instance.Settings.MusicVolumeMuted);
                    break;
                case SettingsModel.SettingType.SFXVolumeMute:
                    View.SetSFXVolumeSliderInteractable(!SettingsManager.Instance.Settings.SFXVolumeMuted);
                    break;
            }
        }

        private void GeneralVolumeMuted(bool boolValue) {
            SettingsManager.Instance.Settings.GeneralVolumeMuted = boolValue;
        }

        private void GeneralVolumeChanged(float floatValue) {
            SettingsManager.Instance.Settings.GeneralVolume = floatValue;
        }

        private void MusicVolumeMuted(bool boolValue) {
            SettingsManager.Instance.Settings.MusicVolumeMuted = boolValue;
        }

        private void MusicVolumeChanged(float floatValue) {
            SettingsManager.Instance.Settings.MusicVolume = floatValue;
        }

        private void SFXVolumeMuted(bool boolValue) {
            SettingsManager.Instance.Settings.SFXVolumeMuted = boolValue;
        }

        private void SFXVolumeChanged(float floatValue) {
            SettingsManager.Instance.Settings.SFXVolume = floatValue;
        }
        #endregion

        public void SetCallback(Action closeAction) {
            _closeAction = closeAction;
        }

        public override void Init(string windowId) {
            base.Init(windowId);

            SettingsModel settings = SettingsManager.Instance.Settings;
            View.SetGeneralVolume(settings.GeneralVolumeMuted, settings.GeneralVolume);
            View.SetMusicVolume(settings.MusicVolumeMuted, settings.MusicVolume);
            View.SetSFXVolume(settings.SFXVolumeMuted, settings.SFXVolume);
        }
    }
}
