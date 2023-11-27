
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

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace YannickSCF.TournamentDraw.MainManagers.Controllers {

    public enum States { None, Initial, Draw }

    public class GameManager : GlobalSingleton<GameManager> {

        [Header("Main Controllers")]
        [SerializeField] private BaseUIController _baseUIController;
        [SerializeField] private AudioController _audioController;
        [SerializeField] private SceneController _sceneController;
        [SerializeField] private DataController _dataController;

        [Header("Debug Values")]
        [SerializeField] private bool _debug = false;
        [SerializeField, ConditionalHide("debug", true)] private TournamentData _debugConfig;
        [SerializeField, ConditionalHide("debug", true)] private States openPanelAuto = States.Initial;

        [Header("Settings files")]
        [SerializeField] private TournamentData _config;
        [SerializeField] private SettingsConfiguration _settings;

        private States c_state = States.None;

        public BaseUIController BaseUIController { get => _baseUIController; }

        public TournamentData Config {
            get { return _debug ? _debugConfig : _config; }
            set {
                if (_debug) {
                    _debugConfig = value;
                } else {
                    _config = value;
                }
            }
        }

        #region Mono
        protected override void Awake() {
            base.Awake();

            if (_baseUIController != null && _baseUIController.LoadingController != null) {
                _baseUIController.LoadingController.gameObject.SetActive(true);
            }
        }

        private void Start() {
            LoadSavedData();

            SwitchState(_debug ? openPanelAuto : States.Initial);

            SetGameToSettings();
        }

        private void OnApplicationQuit() {
#if UNITY_EDITOR
            if (!_debug) {
                _config.ResetData();
                _settings.ResetConfiguration();
            }
#endif
        }
        #endregion

        private void LoadSavedData() {
            if (_dataController.HasDrawConfigurationSaved()) {
                _config = _dataController.GetDrawConfiguration(_config);
            }

            if (_dataController.HasSettingsConfigurationSaved()) {
                _settings = _dataController.GetSettingsConfiguration(_settings);
            }
        }

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
            _audioController.MuteSource(GeneralApp.AudioSources.General, IsGeneralVolumeMuted);
            _audioController.SetGeneralVolume(GeneralApp.AudioSources.General, GeneralVolume);

            _audioController.MuteSource(GeneralApp.AudioSources.Music, IsMusicVolumeMuted);
            _audioController.SetGeneralVolume(GeneralApp.AudioSources.Music, MusicVolume);

            _audioController.MuteSource(GeneralApp.AudioSources.SFX, IsSFXVolumeMuted);
            _audioController.SetGeneralVolume(GeneralApp.AudioSources.SFX, SFXVolume);

            if (IsGeneralVolumeMuted && IsMusicVolumeMuted) {
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

        public bool IsConfigToContinue() {
            return !string.IsNullOrEmpty(Config.TournamentName);
        }

        public void SaveAndExit(bool saveAndExit) {
            if (!saveAndExit) {
                _config.ResetData();
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
            _dataController.SaveDrawConfiguration(_config);
        }

        public void SaveSettingsData() {
            _dataController.SaveSettingsConfiguration(_settings);
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

        #region Audio Management
        public bool IsGeneralVolumeMuted { get => _settings.GeneralVolumeMuted; set => _settings.GeneralVolumeMuted = value; }
        public bool IsMusicVolumeMuted { get => _settings.MusicVolumeMuted; set => _settings.MusicVolumeMuted = value; }
        public bool IsSFXVolumeMuted { get => _settings.SFXVolumeMuted; set => _settings.SFXVolumeMuted = value; }

        public float GeneralVolume { get => _settings.GeneralVolume; set => _settings.GeneralVolume = value; }
        public float MusicVolume { get => _settings.MusicVolume; set => _settings.MusicVolume = value; }
        public float SFXVolume { get => _settings.SFXVolume; set => _settings.SFXVolume = value; }
        #endregion
    }
}
