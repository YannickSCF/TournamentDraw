using UnityEngine;
using TMPro;

using YannickSCF.GeneralApp.Controller.UI.Windows;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Controllers.MainScene.Initial;
using YannickSCF.TournamentDraw.Views.MainScene.Initial;
using YannickSCF.TournamentDraw.Controllers.MainScene.Windows.Settings;
using YannickSCF.TournamentDraw.Views.MainScene.Windows.Settings;
using YannickSCF.LSTournaments.Common.Controllers;
using YannickSCF.LSTournaments.Common.Views;

namespace YannickSCF.TournamentDraw.Controllers.MainScene {
    public class MainSceneController : MonoBehaviour {

        [Header("Scene Objects")]
        [SerializeField] private SpriteRenderer _backgroundImage;
        [SerializeField] private WindowsController _sceneCanvas;
        [SerializeField] private TextMeshProUGUI _version;

        private InitialWindowController _initialWindow;
        private SettingsWindowController _settingsWindow;
        private ConfiguratorController _configWindow;

        private void Start() {
            _version.text = "Version: " + Application.version;
        }

        public void Init() {
            _initialWindow = _sceneCanvas.ShowWindow<InitialWindowController, InitialWindowView>("Initial");
            _initialWindow.SetCallbacks(OpenConfigurationWindow, OpenSettingsWindow);
        }

        #region Methods to manage draw configuration from NEW
        private void OpenConfigurationWindow() {
            DataManager.Instance.ResetData();
            _initialWindow.OnWindowHidden += ShowConfiguratorFromInitial;
            _sceneCanvas.HideWindow<InitialWindowController, InitialWindowView>("Initial");
        }

        private void ShowConfiguratorFromInitial(WindowController<InitialWindowView> window) {
            _configWindow = _sceneCanvas.ShowWindow<ConfiguratorController, ConfiguratorView>("Config");
            _configWindow.SetCallbacks(OnConfiguratorClosed, OnConfiguratorFinished);

            _initialWindow.OnWindowHidden -= ShowConfiguratorFromInitial;
        }

        private void OnConfiguratorClosed() {
            DataManager.Instance.ResetData();
            _configWindow.OnWindowHidden += ShowInitialFromConfig;
            _sceneCanvas.HideWindow<ConfiguratorController, ConfiguratorView>("Config");
            _sceneCanvas.CloseWindow<ConfiguratorController, ConfiguratorView>("Config");
        }

        private void ShowInitialFromConfig(WindowController<ConfiguratorView> window) {
            _sceneCanvas.ShowWindow<InitialWindowController, InitialWindowView>("Initial");
            _initialWindow.GoBack();

            _configWindow.OnWindowHidden -= ShowInitialFromConfig;
        }

        private void OnConfiguratorFinished() {
            GameManager.Instance.SaveDrawData();
            GameManager.Instance.SwitchState(States.Draw);
        }
        #endregion


        #region Methods to manage draw configuration from NEW
        private void OpenSettingsWindow() {
            _initialWindow.OnWindowHidden += ShowSettingsFromInitial;
            _sceneCanvas.HideWindow<InitialWindowController, InitialWindowView>("Initial");
        }

        private void ShowSettingsFromInitial(WindowController<InitialWindowView> window) {
            _settingsWindow = _sceneCanvas.ShowWindow<SettingsWindowController, SettingsWindowView>("Settings");
            _settingsWindow.SetCallback(OnSettingsClosed);

            _initialWindow.OnWindowHidden -= ShowSettingsFromInitial;
        }

        private void ShowInitialFromSettings(WindowController<SettingsWindowView> window) {
            _sceneCanvas.ShowWindow<InitialWindowController, InitialWindowView>("Initial");
            _initialWindow.GoBack();

            _settingsWindow.OnWindowHidden -= ShowInitialFromSettings;
        }

        private void OnSettingsClosed() {
            GameManager.Instance.SaveSettingsData();
            _settingsWindow.OnWindowHidden += ShowInitialFromSettings;
            _sceneCanvas.HideWindow<SettingsWindowController, SettingsWindowView>("Settings");
        }
        #endregion
    }
}
