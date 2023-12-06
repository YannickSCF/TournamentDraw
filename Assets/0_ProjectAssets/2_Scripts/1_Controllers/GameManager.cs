
// Dependencies
using System;
using UnityEngine;
// Custom dependencies
using YannickSCF.GeneralApp;
using YannickSCF.GeneralApp.Controller.Scenes;
using YannickSCF.GeneralApp.Controller.UI;
using YannickSCF.GeneralApp.View.UI.LoadingPanel.Events;
using YannickSCF.TournamentDraw.Controllers.DrawScene;
using YannickSCF.TournamentDraw.Controllers.MainScene;
using YannickSCF.TournamentDraw.Scriptables;
using YannickSCF.LSTournaments.Common.Scriptables.Data;
using YannickSCF.LSTournaments.Common.Controllers;
using YannickSCF.GeneralApp.Controller.Audio;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace YannickSCF.TournamentDraw.MainManagers.Controllers {

    public enum States { None, Initial, Draw }

    public class GameManager : GlobalSingleton<GameManager> {

        [Header("Main Controllers")]
        [SerializeField] private BaseUIController _baseUIController;
        [SerializeField] private BaseAudioController _audioController;
        [SerializeField] private SceneController _sceneController;

        [Header("Debug Values")]
        [SerializeField] private bool _debug = false;
        [SerializeField, ConditionalHide("debug", true)] private States openPanelAuto = States.Initial;

        private DataManager _dataManager;
        private SettingsManager _settingsManager;

        private States c_state = States.None;

        public BaseUIController BaseUIController { get => _baseUIController; }

        #region Mono
        protected override void Awake() {
            base.Awake();

            _dataManager = DataManager.Instance;
            _settingsManager = SettingsManager.Instance;
            if (_settingsManager.HasSettingsSaved()) {
                _settingsManager.LoadSettings();
            }

            if (_settingsManager.HasDrawSettingsSaved()) {
                _settingsManager.LoadDrawSettings();
            }

            if (_baseUIController != null && _baseUIController.LoadingController != null) {
                _baseUIController.LoadingController.gameObject.SetActive(true);
            }
        }

        private void OnEnable() {
            _settingsManager.Settings.SettingsChanged += OnSettingChanged;
        }

        private void Start() {
            SwitchState(_debug ? openPanelAuto : States.Initial);

            SetGameToSettings();
        }

        private void OnDisable() {
            _settingsManager.Settings.SettingsChanged -= OnSettingChanged;
        }

        private void OnApplicationQuit() {
            _settingsManager.SaveSettings();
            _settingsManager.SaveDrawSettings();
#if UNITY_EDITOR
            if (!_debug) {
                _dataManager.ResetData();
                _settingsManager.ResetSettings();
                _settingsManager.ResetDrawSettings();
            }
#endif
        }
        #endregion

        #region Events Listeners
        private void OnSettingChanged(SettingsModel.SettingType type) {
            switch (type) {
                case SettingsModel.SettingType.GeneralVolumeMute:
                    _audioController.MuteSource(AudioSources.General, _settingsManager.Settings.GeneralVolumeMuted);
                    break;
                case SettingsModel.SettingType.GeneralVolume:
                    _audioController.SetGeneralVolume(AudioSources.General, _settingsManager.Settings.GeneralVolume);
                    break;
                case SettingsModel.SettingType.MusicVolumeMute:
                    _audioController.MuteSource(AudioSources.Music, _settingsManager.Settings.MusicVolumeMuted);
                    break;
                case SettingsModel.SettingType.MusicVolume:
                    _audioController.SetGeneralVolume(AudioSources.Music, _settingsManager.Settings.MusicVolume);
                    break;
                case SettingsModel.SettingType.SFXVolumeMute:
                    _audioController.MuteSource(AudioSources.SFX, _settingsManager.Settings.SFXVolumeMuted);
                    break;
                case SettingsModel.SettingType.SFXVolume:
                    _audioController.SetGeneralVolume(AudioSources.SFX, _settingsManager.Settings.SFXVolume);
                    break;
                default:
                    Debug.LogWarning($"Setting type '{Enum.GetName(typeof(SettingsModel.SettingType), type)}' not included on switch!");
                    break;
            }
        }
        #endregion


        public void SwitchState(States stateToSwitch) {
            if (c_state == States.None) {
                c_state = stateToSwitch;
                InitSceneData();
            } else if (_sceneController.CurrentSceneIndex != (int)stateToSwitch - 1) {
                ChangeSingleScene((int)stateToSwitch - 1, false);
                c_state = stateToSwitch;
            }
        }

        private void SetGameToSettings() {
            SettingsModel settings = _settingsManager.Settings;

            _audioController.MuteSource(AudioSources.General, settings.GeneralVolumeMuted);
            _audioController.SetGeneralVolume(AudioSources.General, settings.GeneralVolume);

            _audioController.MuteSource(AudioSources.Music, settings.MusicVolumeMuted);
            _audioController.SetGeneralVolume(AudioSources.Music, settings.MusicVolume);

            _audioController.MuteSource(AudioSources.SFX, settings.SFXVolumeMuted);
            _audioController.SetGeneralVolume(AudioSources.SFX, settings.SFXVolume);

            if (settings.GeneralVolumeMuted && settings.MusicVolumeMuted) {
                _audioController.PlayBackground("Suspense_Rises");
            } else {
                _audioController.SoftPlayBackground("Suspense_Rises");
            }
        }

        private void InitSceneData() {
            switch (c_state) {
                case States.Initial:
                    MainSceneController mainSceneController = FindObjectOfType<MainSceneController>();
                    mainSceneController.Init();
                    break;
                case States.Draw:
                    DrawSceneController drawSceneController = FindObjectOfType<DrawSceneController>();
                    drawSceneController.Init();
                    break;
                default:
                    Debug.LogError("Error on state given!");
                    break;
            }
        }

        public void SaveAndExit(bool saveAndExit) {
            if (!saveAndExit) {
                _dataManager.ResetData();
            } else {
                SaveDrawData();
            }

#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        public void SaveDrawData() {
            _dataManager.SaveData();
        }

        public void SaveSettingsData() {
            SettingsManager.Instance.SaveSettings();
        }

        #region Scene management
        private int _sceneToGo = 0;
        private bool _showProgress = false;

        #region Load single scenes methods
        public void ChangeSingleScene(int sceneToGo, bool showProgress = false) {
            _sceneToGo = sceneToGo;
            _showProgress = showProgress;

            _baseUIController.LoadingController.FadeIn();

            LoadingPanelViewEvents.OnFadeInFinished += ChangeSingleSceneOnFadeInFinished;
        }

        private void ChangeSingleSceneOnFadeInFinished() {
            _baseUIController.LoadingController.ShowLoadingValues(true, _showProgress);
            if (_showProgress) {
                _sceneController.OnSceneLoadProgress +=
                    _baseUIController.LoadingController.UpdateProgressBar;
            }

            _sceneController.LoadSceneByIndex(_sceneToGo);

            LoadingPanelViewEvents.OnFadeInFinished -= ChangeSingleSceneOnFadeInFinished;
            _sceneController.OnSceneLoaded += SceneLoaded;
        }

        private void SceneLoaded() {
            InitSceneData();

            _baseUIController.LoadingController.FadeOut();

            if (_showProgress) {
                _sceneController.OnSceneLoadProgress -=
                    _baseUIController.LoadingController.UpdateProgressBar;
            }

            _sceneController.OnSceneLoaded -= SceneLoaded;
            _sceneToGo = 0;
        }
        #endregion

        #region Load/Unload additive scenes
        public void AddAdditiveScene(int c_sceneToGo) {
            _sceneController.LoadSceneByIndex(c_sceneToGo, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }

        public void RemoveAdditiveScene(int c_sceneToGo) {
            _sceneController.UnloadSceneByIndex(c_sceneToGo);
        }
        #endregion
        #endregion
    }
}
