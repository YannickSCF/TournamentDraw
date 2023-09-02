using System;
using YannickSCF.GeneralApp.Controller.UI.Popups;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Views.CommonEvents.Settings;

namespace YannickSCF.TournamentDraw.Popups {
    public class SettingsPopupData : PopupData {
        private Action _backAction;

        public SettingsPopupData(
            string popupId,
            Action backAction) : base(popupId) {
            _backAction = backAction;
        }

        public Action BackAction { get => _backAction; }
    }

    public class SettingsPopupController : PopupController {

        private SettingsPopupView _view;

        private Action _backAction;

        #region Mono
        private void Awake() {
            _view = GetView<SettingsPopupView>();
        }

        protected override void OnEnable() {
            base.OnEnable();

            _view.BackButton += OnBackButton;

            SettingsViewsEvents.OnGeneralVolumeMuted += GeneralVolumeMuted;
            SettingsViewsEvents.OnMusicVolumeMuted += MusicVolumeMuted;
            SettingsViewsEvents.OnSFXVolumeMuted += SFXVolumeMuted;
        }

        protected override void OnDisable() {
            base.OnDisable();

            _view.BackButton -= OnBackButton;

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
            _view.SetGeneralVolumeSliderInteractable(!isMuted);
        }

        private void MusicVolumeMuted(bool isMuted) {
            _view.SetMusicVolumeSliderInteractable(!isMuted);
        }

        private void SFXVolumeMuted(bool isMuted) {
            _view.SetSFXVolumeSliderInteractable(!isMuted);
        }
        #endregion

        public override void Init(PopupData popupData) {
            SettingsPopupData settingsPopupData = (SettingsPopupData)popupData;
            _backAction = settingsPopupData.BackAction;

            base.Init(popupData);

            GameManager _gameManager = GameManager.Instance;

            _view.SetGeneralVolume(_gameManager.IsGeneralVolumeMuted, _gameManager.GeneralVolume);
            _view.SetMusicVolume(_gameManager.IsMusicVolumeMuted, _gameManager.MusicVolume);
            _view.SetSFXVolume(_gameManager.IsSFXVolumeMuted, _gameManager.SFXVolume);
        }
    }
}
