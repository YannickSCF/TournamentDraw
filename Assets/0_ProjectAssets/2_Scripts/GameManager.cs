using System;
using UnityEngine;
using YannickSCF.GeneralApp.Controller.LoadingPanel;
using YannickSCF.TournamentDraw.Controllers.Configurator;
using YannickSCF.TournamentDraw.Controllers.Draw;
using YannickSCF.TournamentDraw.Scriptables;
using YannickSCF.TournamentDraw.Views;
using YannickSCF.TournamentDraw.Views.InitialPanel.Events;

namespace YannickSCF.TournamentDraw.MainManagers.Controllers {

    public enum States { None, Initial, Configurator, Draw }

    public class GameManager : GlobalSingleton<GameManager> {

        [Header("Debug Values")]
        [SerializeField] private bool debug = false;
        [SerializeField, ConditionalHide("debug", true)] private DrawConfiguration _debugConfig;
        [SerializeField, ConditionalHide("debug", true)] private States openPanelAuto = States.Initial;

        [Header("Main Controllers")]
        [SerializeField] private UIManager gameUIManager;
        [SerializeField] private LoadingPanelController loadingController;

        [Header("Settings files")]
        [SerializeField] private DrawConfiguration _config;

        private States c_state = States.None;

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
            SwitchState(debug ? openPanelAuto : States.Initial);
        }

        private void OnApplicationQuit() {
            _config.ResetConfiguration();
        }
        #endregion

        public void SwitchState(States stateToSwitch) {
            // TODO Metodo para cambiar de cosas 
            // Ejemplos:
            //      - Initial Panel -> New Draw Button Pressed -> Open Configurator
            //      - Configurator Panel -> Draw options finished -> Change scene and open Draw Panel
            //      - etc,...

            if (ClosePreviousPanel(stateToSwitch)) {
                switch (stateToSwitch) {
                    case States.Initial:
                        OpenInitialPanel();
                        break;
                    case States.Configurator:
                        OpenConfiguratorPanel();
                        break;
                    case States.Draw:
                        OpenDrawPanel();
                        break;
                    default:
                        break;
                }

                c_state = stateToSwitch;
            }
        }

        private bool ClosePreviousPanel(States stateToSwitch) {
            if (stateToSwitch == c_state) return false;
            gameUIManager.CloseCurrentPanel();
            return true;
        }

        // ------------------------ Initial Panel -----------------------

        private void OpenInitialPanel() {
            _config.ResetConfiguration();
            gameUIManager.OpenInitialPanel();
        }

        // --------------------- Configurator Panel ---------------------

        [ContextMenu("Open Configurator")]
        private void OpenConfiguratorPanel() {
            gameUIManager.OpenConfiguratorPanel(InitializeConfiguratorData);
        }

        private void InitializeConfiguratorData(DrawConfiguratorController configuratorController) {
            configuratorController.Init(Config);
        }

        // ------------------------- Draw Panel -------------------------

        private void OpenDrawPanel() {
            gameUIManager.OpenDrawPanel(InitializeDrawPanelData);
        }

        private void InitializeDrawPanelData(DrawPanelController drawPanelController) {
            gameUIManager.ChangeTitle(Config.DrawName);

            drawPanelController.Init(Config);
        }
    }
}
