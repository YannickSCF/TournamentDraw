using System;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.GeneralApp.Controller.LoadingPanel;
using YannickSCF.GeneralApp.GameManager;
using YannickSCF.GeneralApp.View.LoadingPanel.Events;
using YannickSCF.TournamentDraw.Controllers.Configurator;
using YannickSCF.TournamentDraw.Controllers.Draw;
using YannickSCF.TournamentDraw.Scriptables;
using YannickSCF.TournamentDraw.Views;
using YannickSCF.TournamentDraw.Views.InitialPanel.Events;

namespace YannickSCF.TournamentDraw.MainManagers.Controllers {

    public enum Scenes { None, Main, Draw }

    public class GameManager : BaseGameManager {

        [Header("Debug Values")]
        [SerializeField] private bool debug = false;
        [SerializeField, ConditionalHide("debug", true)] private DrawConfiguration _debugConfig;
        [SerializeField, ConditionalHide("debug", true)] private Scenes openPanelAuto = Scenes.Main;

        [Header("Main Controllers")]
        [SerializeField] private UIManager _gameUIManager;

        [Header("Settings files")]
        [SerializeField] private DrawConfiguration _config;
        [SerializeField] private Button _GoScene;

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
        private void Start() {
            SwitchState(debug ? openPanelAuto : Scenes.Main);
        }

        private void OnApplicationQuit() {
            _config.ResetConfiguration();
        }

        private void OnEnable() {
            _GoScene?.onClick.AddListener(() => SwitchState(Scenes.Draw));
        }
        private void OnDisable() {
            _GoScene?.onClick.RemoveAllListeners();
        }
        #endregion

        public void SwitchState(Scenes stateToSwitch) {
            // TODO Metodo para cambiar de cosas 
            // Ejemplos:
            //      - Initial Panel -> New Draw Button Pressed -> Open Configurator
            //      - Configurator Panel -> Draw options finished -> Change scene and open Draw Panel
            //      - etc,...

            if (sceneController.CurrentSceneIndex != (int)stateToSwitch - 1) {
                ChangeSingleScene((int)stateToSwitch - 1, false);
                c_state = stateToSwitch;
            }
        }

        protected override void SceneLoaded() {
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

            base.SceneLoaded();
        }

        // ------------------------ Initial Panel -----------------------

        private void OpenMainScene() {
            _config.ResetConfiguration();
            _gameUIManager.OpenInitialPanel();
        }

        // ---- Configurator Panel ----

        [ContextMenu("Open Configurator")]
        private void OpenConfiguratorPanel() {
            _gameUIManager.OpenConfiguratorPanel(InitializeConfiguratorData);
        }

        private void InitializeConfiguratorData(DrawConfiguratorController configuratorController) {
            configuratorController.Init(Config);
        }

        // ------------------------- Draw Panel -------------------------

        private void OpenDrawScene() {
            _gameUIManager.OpenDrawPanel(InitializeDrawPanelData);
        }

        private void InitializeDrawPanelData(DrawPanelController drawPanelController) {
            drawPanelController.Init(Config);
        }
    }
}
