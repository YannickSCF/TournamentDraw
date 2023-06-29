using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.GeneralApp.Controller.UI.Windows;
using YannickSCF.GeneralApp.View.UI.Windows;
using YannickSCF.TournamentDraw.Controllers.Configurator;
using YannickSCF.TournamentDraw.Controllers.MainScene.InitialPanel;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Views.Configurator;
using YannickSCF.TournamentDraw.Views.InitialPanel;

namespace YannickSCF.TournamentDraw.Controllers.MainScene {
    public class MainSceneController : MonoBehaviour {

        [Header("Scene Objects")]
        [SerializeField] private SpriteRenderer _backgroundImage;
        [SerializeField] private WindowsController _sceneCanvas;

        public void Init() {
            InitialPanelController window = _sceneCanvas.ShowWindow<InitialPanelController, InitialPanelView>("Initial");
            window.SetAllCallbacks(NewDrawButtonPressed, LoadDrawButtonPressed, SettingsButtonPressed);
        }

        #region Methods to manage initial buttons
        #endregion

        #region Methods to manage draw configuration from NEW
        private void NewDrawButtonPressed() {
            _sceneCanvas.HideWindow<InitialPanelController, InitialPanelView>("Initial");
            DrawConfiguratorController draw = _sceneCanvas.ShowWindow<DrawConfiguratorController, DrawConfiguratorView>("Config");
            draw.SetAllCallback(OnConfiguratorClosed, OnConfiguratorFinished);
        }

        private void OnConfiguratorClosed() {
            _sceneCanvas.CloseWindow<DrawConfiguratorController, DrawConfiguratorView>("Config");
            _ = _sceneCanvas.ShowWindow<InitialPanelController, InitialPanelView>("Initial");
        }

        private void OnConfiguratorFinished() {
            GameManager.Instance.SwitchState(States.Draw);
        }
        #endregion

        #region Methods to manage draw configuration from LOAD
        #endregion

        #region Methods to manage settings panel
        #endregion

        private void LoadDrawButtonPressed() {
            Debug.Log("LoadDrawButtonPressed");
        }

        private void SettingsButtonPressed() {
            Debug.Log("SettingsButtonPressed");
        }
    }
}
