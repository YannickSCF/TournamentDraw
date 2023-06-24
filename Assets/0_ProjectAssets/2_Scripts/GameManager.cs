
using UnityEngine;
using YannickSCF.GeneralApp.Controller;
using YannickSCF.GeneralApp.Controller.Audio;
using YannickSCF.GeneralApp.Controller.Scenes;
using YannickSCF.GeneralApp.View.UI.LoadingPanel.Events;
using YannickSCF.TournamentDraw.Controllers.Configurator;
using YannickSCF.TournamentDraw.Controllers.Draw;
using YannickSCF.TournamentDraw.Scriptables;
using YannickSCF.TournamentDraw.Views;

namespace YannickSCF.TournamentDraw.MainManagers.Controllers {

    public enum Scenes { None, Main, Draw }

    public class GameManager : GlobalSingleton<GameManager> {

        [Header("Other Main Controllers")]
        [SerializeField] private BaseAudioController _audioController;

        [Header("Debug Values")]
        [SerializeField] private bool debug = false;
        [SerializeField, ConditionalHide("debug", true)] private DrawConfiguration _debugConfig;
        [SerializeField, ConditionalHide("debug", true)] private Scenes openPanelAuto = Scenes.Main;

        [Header("Settings files")]
        [SerializeField] private DrawConfiguration _config;

        private UIController _uIController;
        private Scenes c_state = Scenes.None;

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
        private void Awake() {
            _uIController = UIController.GetComponent<UIController>();
        }

        private void Start() {
            SwitchState(debug ? openPanelAuto : Scenes.Main);
        }

        private void OnApplicationQuit() {
            _config.ResetConfiguration();
        }
        #endregion

        public void SwitchState(Scenes stateToSwitch) {
            // TODO Metodo para cambiar de cosas 
            // Ejemplos:
            //      - Initial Panel -> New Draw Button Pressed -> Open Configurator
            //      - Configurator Panel -> Draw options finished -> Change scene and open Draw Panel
            //      - etc,...

            if (SceneController.CurrentSceneIndex != (int)stateToSwitch - 1) {
                ChangeSingleScene((int)stateToSwitch - 1, false);
                c_state = stateToSwitch;
            }
        }

        // ------------------------ Initial Panel -----------------------

        private void OpenMainScene() {
            _config.ResetConfiguration();
            _uIController.OpenInitialPanel();
        }

        // ---- Configurator Panel ----

        [ContextMenu("Open Configurator")]
        public void OpenConfiguratorPanel() {
            _uIController.OpenConfiguratorPanel(InitializeConfiguratorData);
        }

        private void InitializeConfiguratorData(DrawConfiguratorController configuratorController) {
            configuratorController.Init(Config);
        }

        // ------------------------- Draw Panel -------------------------

        private void OpenDrawScene() {
            _uIController.OpenDrawPanel(InitializeDrawPanelData);
        }

        private void InitializeDrawPanelData(DrawPanelController drawPanelController) {
            drawPanelController.Init(Config);
        }


        [SerializeField] protected UIController UIController;
        [SerializeField] protected SceneController SceneController;

        protected int _sceneToGo = 0;
        protected bool _showProgress = false;

        #region Load single scenes methods
        public virtual void ChangeSingleScene(int sceneToGo, bool showProgress = false) {
            _sceneToGo = sceneToGo;
            _showProgress = showProgress;

            UIController.LoadingController.FadeIn();

            LoadingPanelViewEvents.OnFadeInFinished += ChangeSingleSceneOnFadeInFinished;
        }

        protected virtual void ChangeSingleSceneOnFadeInFinished() {
            UIController.LoadingController.ShowLoadingValues(true, _showProgress);
            if (_showProgress) SceneController.OnSceneLoadProgress += UIController.LoadingController.UpdateProgressBar;

            SceneController.LoadSceneByIndex(_sceneToGo);

            LoadingPanelViewEvents.OnFadeInFinished -= ChangeSingleSceneOnFadeInFinished;
            SceneController.OnSceneLoaded += SceneLoaded;
        }

        protected virtual void SceneLoaded() {
            switch (c_state) {
                case Scenes.Main:
                    OpenMainScene();
                    break;
                case Scenes.Draw:
                    OpenDrawScene();
                    break;
                default:
                    break;
            }

            UIController.LoadingController.FadeOut();

            if (_showProgress) SceneController.OnSceneLoadProgress -= UIController.LoadingController.UpdateProgressBar;
            SceneController.OnSceneLoaded -= SceneLoaded;
            _sceneToGo = 0;
        }
        #endregion

        #region Load/Unload additive scenes
        public virtual void AddAdditiveScene(int c_sceneToGo) {
            SceneController.LoadSceneByIndex(c_sceneToGo, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }

        public virtual void RemoveAdditiveScene(int c_sceneToGo) {
            SceneController.UnloadSceneByIndex(c_sceneToGo);
        }
        #endregion
    }
}
