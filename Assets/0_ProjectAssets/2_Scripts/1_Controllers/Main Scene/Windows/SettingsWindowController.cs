using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.GeneralApp.Controller.UI.Windows;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Views.MainScene.Windows.Settings;

namespace YannickSCF.TournamentDraw.Controllers.MainScene.Windows.Settings {
    public class SettingsWindowController : WindowController<SettingsWindowView> {

        private Action _closeAction;

        #region Mono
        protected override void OnEnable() {
            base.OnEnable();

            View.OnCloseSettings += CloseWindow;

            SettingsWindowViewEvents.OnGeneralVolumeMuted += GeneralVolumeMuted;
            SettingsWindowViewEvents.OnMusicVolumeMuted += MusicVolumeMuted;
            SettingsWindowViewEvents.OnSFXVolumeMuted += SFXVolumeMuted;
        }

        protected override void OnDisable() {
            base.OnDisable();

            View.OnCloseSettings -= CloseWindow;

            SettingsWindowViewEvents.OnGeneralVolumeMuted -= GeneralVolumeMuted;
            SettingsWindowViewEvents.OnMusicVolumeMuted -= MusicVolumeMuted;
            SettingsWindowViewEvents.OnSFXVolumeMuted -= SFXVolumeMuted;
        }
        #endregion

        #region Event listeners methods
        private void CloseWindow() {
            _closeAction?.Invoke();
        }

        private void GeneralVolumeMuted(bool isMuted) {
            View.SetGeneralVolumeSliderInteractable(isMuted);
        }

        private void MusicVolumeMuted(bool isMuted) {
            View.SetGeneralVolumeSliderInteractable(isMuted);
        }

        private void SFXVolumeMuted(bool isMuted) {
            View.SetGeneralVolumeSliderInteractable(isMuted);
        }
        #endregion

        public void SetCallback(Action closeAction) {
            _closeAction = closeAction;
        }

        public override void Init(string windowId) {
            base.Init(windowId);

            GameManager _gameManager = GameManager.Instance;

            View.SetGeneralVolume(_gameManager.IsGeneralVolumeMuted(), _gameManager.GetGeneralVolume());
            View.SetMusicVolume(_gameManager.IsMusicVolumeMuted(), _gameManager.GetMusicVolume());
            View.SetSFXVolume(_gameManager.IsSFXVolumeMuted(), _gameManager.GetSFXVolume());
        }
    }
}
