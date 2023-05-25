using System;
using UnityEngine;
using YannickSCF.GeneralApp.Controller.LoadingPanel;
using YannickSCF.TournamentDraw.Controllers.Configurator;
using YannickSCF.TournamentDraw.Scriptables;
using YannickSCF.TournamentDraw.Views;
using YannickSCF.TournamentDraw.Views.InitialPanel.Events;

namespace YannickSCF.TournamentDraw.MainManagers.Controllers {

    public enum States { None, Initial, Configurator, Draw }

    public class GameManager : GlobalSingleton<GameManager> {

        [SerializeField] private UIManager gameUIManager;
        [SerializeField] private LoadingPanelController loadingController;

        [Header("Settings files")]
        [SerializeField] private DrawConfiguration configuration;

        private States c_state = States.None;

        #region Mono
        private void Start() {
            SwitchState(States.Initial);
        }

        private void OnApplicationQuit() {
            configuration.ResetConfiguration();
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
            gameUIManager.OpenInitialPanel();
        }

        // --------------------- Configurator Panel ---------------------

        [ContextMenu("Open Configurator")]
        private void OpenConfiguratorPanel() {
            gameUIManager.OpenConfiguratorPanel(InitializeConfiguratorData);
        }

        private void InitializeConfiguratorData(DrawConfiguratorController configuratorController) {
            configuratorController.Init(configuration);
        }

        // ------------------------- Draw Panel -------------------------

        private void OpenDrawPanel() {
            //gameUIManager.OpenDrawPanel(configuration);
        }
    }
}
