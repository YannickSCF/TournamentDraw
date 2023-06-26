
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

    public class GameManager : YannickSCF.GeneralApp.Controller.GameManager {

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
            _uIController = BaseUIController as UIController;
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
                SceneUtils.ChangeSingleScene((int)stateToSwitch - 1, false);
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
    }
}
