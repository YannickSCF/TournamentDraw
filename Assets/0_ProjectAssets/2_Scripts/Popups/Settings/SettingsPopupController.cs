using System;
using YannickSCF.GeneralApp.Controller.UI.Popups;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Scriptables;
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

            _view.BackButton -= OnBackButton;

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
        private void OnBackButton() {
            _backAction?.Invoke();
        }

        private void OnSettingChanged(SettingsModel.SettingType type) {
            switch (type) {
                case SettingsModel.SettingType.GeneralVolumeMute:
                    _view.SetGeneralVolumeSliderInteractable(!SettingsManager.Instance.Settings.GeneralVolumeMuted);
                    break;
                case SettingsModel.SettingType.MusicVolumeMute:
                    _view.SetMusicVolumeSliderInteractable(!SettingsManager.Instance.Settings.MusicVolumeMuted);
                    break;
                case SettingsModel.SettingType.SFXVolumeMute:
                    _view.SetSFXVolumeSliderInteractable(!SettingsManager.Instance.Settings.SFXVolumeMuted);
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

        public override void Init(PopupData popupData) {
            SettingsPopupData settingsPopupData = (SettingsPopupData)popupData;
            _backAction = settingsPopupData.BackAction;

            base.Init(popupData);

            SettingsModel settings = SettingsManager.Instance.Settings;
            _view.SetGeneralVolume(settings.GeneralVolumeMuted, settings.GeneralVolume);
            _view.SetMusicVolume(settings.MusicVolumeMuted, settings.MusicVolume);
            _view.SetSFXVolume(settings.SFXVolumeMuted, settings.SFXVolume);
        }
    }
}
