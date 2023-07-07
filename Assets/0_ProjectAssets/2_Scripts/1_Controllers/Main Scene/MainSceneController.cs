using UnityEngine;

using YannickSCF.GeneralApp.Controller.UI.Windows;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Controllers.Configurator;
using YannickSCF.TournamentDraw.Views.Configurator;
using YannickSCF.TournamentDraw.Controllers.MainScene.Initial;
using YannickSCF.TournamentDraw.Views.MainScene.Initial;

namespace YannickSCF.TournamentDraw.Controllers.MainScene {
    public class MainSceneController : MonoBehaviour {

        [Header("Scene Objects")]
        [SerializeField] private SpriteRenderer _backgroundImage;
        [SerializeField] private WindowsController _sceneCanvas;

        public void Init() {
            InitialWindowController window = _sceneCanvas.ShowWindow<InitialWindowController, InitialWindowView>("Initial");
            window.SetCallbacks(OpenConfigurationWindow, OpenSettingsWindow);
        }

        #region Methods to manage draw configuration from NEW
        private void OpenConfigurationWindow() {
            _sceneCanvas.HideWindow<InitialWindowController, InitialWindowView>("Initial");
            DrawConfiguratorController draw = _sceneCanvas.ShowWindow<DrawConfiguratorController, DrawConfiguratorView>("Config");
            draw.SetAllCallback(OnConfiguratorClosed, OnConfiguratorFinished);
        }

        private void OnConfiguratorClosed() {
            _sceneCanvas.CloseWindow<DrawConfiguratorController, DrawConfiguratorView>("Config");
            InitialWindowController window = _sceneCanvas.ShowWindow<InitialWindowController, InitialWindowView>("Initial");
            window.GoBack();
        }

        private void OnConfiguratorFinished() {
            GameManager.Instance.SwitchState(States.Draw);
        }
        #endregion


        #region Methods to manage draw configuration from NEW
        private void OpenSettingsWindow() {
            //_sceneCanvas.HideWindow<InitialWindowController, InitialWindowView>("Initial");
            //DrawConfiguratorController draw = _sceneCanvas.ShowWindow<DrawConfiguratorController, DrawConfiguratorView>("Config");
            //draw.SetAllCallback(OnConfiguratorClosed, OnConfiguratorFinished);
        }

        private void OnSettingsClosed() {
            //_sceneCanvas.CloseWindow<DrawConfiguratorController, DrawConfiguratorView>("Config");
            //InitialWindowController window = _sceneCanvas.ShowWindow<InitialWindowController, InitialWindowView>("Initial");
            //window.GoBack();
        }
        #endregion
    }
}
