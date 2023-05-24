using UnityEngine;
using YannickSCF.GeneralApp.Controller.LoadingPanel;
using YannickSCF.TournamentDraw.Controllers.Configurator;
using YannickSCF.TournamentDraw.Scriptables;
using YannickSCF.TournamentDraw.Views;
using YannickSCF.TournamentDraw.Views.InitialPanel.Events;

namespace YannickSCF.TournamentDraw.MainManagers.Controllers {
    public class GameManager : GlobalSingleton<GameManager> {

        [SerializeField] private UIManager gameUIManager;
        [SerializeField] private LoadingPanelController loadingController;

        [Header("Settings files")]
        [SerializeField] private DrawConfiguration configuration;

        #region Mono
        private void Start() {
            OpenInitialPanel();
        }

        private void OnApplicationQuit() {
            configuration.ResetConfiguration();
        }
        #endregion

        public void SwitchState() {
            // TODO Metodo para cambiar de cosas 
            // Ejemplos:
            //      - Initial Panel -> New Draw Button Pressed -> Open Configurator
            //      - Configurator Panel -> Draw options finished -> Change scene and open Draw Panel
            //      - etc,...
        }

        // ------------------------ Initial Panel -----------------------

        public void OpenInitialPanel() {
            gameUIManager.OpenInitialPanel();
        }

        // --------------------- Configurator Panel ---------------------

        [ContextMenu("Open Configurator")]
        public void OpenConfiguratorPanel() {
            gameUIManager.OpenConfiguratorPanel(InitializeConfiguratorData);
        }

        private void InitializeConfiguratorData(DrawConfiguratorController configuratorController) {
            configuratorController.Init(configuration);
        }

        // ------------------------- Draw Panel -------------------------

        public void OpenDrawPanel() {
            //gameUIManager.OpenDrawPanel(configuration);
        }
    }
}
