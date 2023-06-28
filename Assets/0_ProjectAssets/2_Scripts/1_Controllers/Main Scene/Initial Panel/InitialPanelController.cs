using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.GeneralApp.Controller.UI.Windows;
using YannickSCF.TournamentDraw.Views.InitialPanel;
using YannickSCF.TournamentDraw.Views.InitialPanel.Events;

namespace YannickSCF.TournamentDraw.Controllers.MainScene.InitialPanel {
    public class InitialPanelController : WindowController<InitialPanelView> {

        private Action _newDrawCallback;
        private Action _loadDrawCallback;
        private Action _settingsCallback;

        #region Mono
        protected override void OnEnable() {
            base.OnEnable();

            InitialPanelViewEvents.OnNewDrawButtonPressed += NewDrawButtonPressed;
            InitialPanelViewEvents.OnLoadDrawButtonPressed += LoadDrawButtonPressed;
            InitialPanelViewEvents.OnSettingsButtonPressed += SettingsButtonPressed;
        }

        protected override void OnDisable() {
            base.OnDisable();

            InitialPanelViewEvents.OnNewDrawButtonPressed -= NewDrawButtonPressed;
            InitialPanelViewEvents.OnLoadDrawButtonPressed -= LoadDrawButtonPressed;
            InitialPanelViewEvents.OnSettingsButtonPressed -= SettingsButtonPressed;
        }
        #endregion

        #region Events listeners methods
        private void NewDrawButtonPressed() {
            _newDrawCallback?.Invoke();
        }

        private void LoadDrawButtonPressed() {
            _loadDrawCallback?.Invoke();
        }

        private void SettingsButtonPressed() {
            _settingsCallback?.Invoke();
        }
        #endregion

        public void SetAllCallbacks(Action newDrawCallback, Action loadDrawCallback, Action settingsCallback) {
            _newDrawCallback = newDrawCallback;
            _loadDrawCallback = loadDrawCallback;
            _settingsCallback = settingsCallback;
        }
    }
}
