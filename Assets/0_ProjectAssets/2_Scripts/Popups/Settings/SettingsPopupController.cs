using System;
using YannickSCF.GeneralApp.Controller.UI.Popups;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Views.CommonEvents.Settings;

namespace YannickSCF.TournamentDraw.Popups {
    public class SettingsPopupController : PopupController<SettingsPopupView> {

        private Action _backAction;

        #region Mono
        protected override void OnEnable() {
            base.OnEnable();

            View.BackButton += OnBackButton;

            SettingsViewsEvents.OnGeneralVolumeMuted += GeneralVolumeMuted;
            SettingsViewsEvents.OnMusicVolumeMuted += MusicVolumeMuted;
            SettingsViewsEvents.OnSFXVolumeMuted += SFXVolumeMuted;
        }

        protected override void OnDisable() {
            base.OnDisable();

            View.BackButton -= OnBackButton;

            SettingsViewsEvents.OnGeneralVolumeMuted -= GeneralVolumeMuted;
            SettingsViewsEvents.OnMusicVolumeMuted -= MusicVolumeMuted;
            SettingsViewsEvents.OnSFXVolumeMuted -= SFXVolumeMuted;
        }
        #endregion

        #region Event listeners methods
        private void OnBackButton() {
            _backAction?.Invoke();
        }

        private void GeneralVolumeMuted(bool isMuted) {
            View.SetGeneralVolumeSliderInteractable(!isMuted);
        }

        private void MusicVolumeMuted(bool isMuted) {
            View.SetMusicVolumeSliderInteractable(!isMuted);
        }

        private void SFXVolumeMuted(bool isMuted) {
            View.SetSFXVolumeSliderInteractable(!isMuted);
        }
        #endregion

        public void SetCallback(Action closeAction) {
            _backAction = closeAction;
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
