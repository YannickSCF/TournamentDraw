using UnityEngine;
using YannickSCF.GeneralApp.Controller.Audio;
using YannickSCF.GeneralApp.Controller.Scenes;
using YannickSCF.GeneralApp.Controller.UI;
using YannickSCF.GeneralApp.View.UI.LoadingPanel.Events;
using YannickSCF.TournamentDraw.Controllers.DrawScene;
using YannickSCF.TournamentDraw.Controllers.MainScene;
using YannickSCF.TournamentDraw.Scriptables;

namespace YannickSCF.TournamentDraw.MainManagers.Controllers {

    public enum States { None, Initial, Draw }

    public class GameManager : GlobalSingleton<GameManager> {

        [Header("Main Controllers")]
        [SerializeField] private BaseUIController _baseUIController;
        [SerializeField] private BaseAudioController _baseAudioController;
        [SerializeField] private SceneController _sceneController;

        [Header("Debug Values")]
        [SerializeField] private bool debug = false;
        [SerializeField, ConditionalHide("debug", true)] private DrawConfiguration _debugConfig;
        [SerializeField, ConditionalHide("debug", true)] private States openPanelAuto = States.Initial;

        [Header("Settings files")]
        [SerializeField] private DrawConfiguration _config;

        private States c_state = States.None;

        public BaseUIController BaseUIController { get => _baseUIController; }

        public DrawConfiguration Config {
            get { return debug ? _debugConfig : _config; }
            set {
                if (debug) {
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
            SwitchState(debug ? openPanelAuto : States.Initial);
        }

        private void OnApplicationQuit() {
            _config.ResetConfiguration();
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
