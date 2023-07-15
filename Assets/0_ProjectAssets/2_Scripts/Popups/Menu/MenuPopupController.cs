using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.GeneralApp.Controller.UI.Popups;

namespace YannickSCF.TournamentDraw.Popups {
    public class MenuPopupController : PopupController<MenuPopupView> {

        private Action _closePopup;
        private Action _settingsClick;
        private Action _exitClick;

        #region Mono
        protected override void OnEnable() {
            base.OnEnable();

            View.CloseButton += OnClose;
            View.SettingsButton += OnSettings;
            View.ExitButton += OnExit;
        }

        protected override void OnDisable() {
            base.OnDisable();

            View.CloseButton -= OnClose;
            View.SettingsButton -= OnSettings;
            View.ExitButton -= OnExit;
        }
        #endregion

        public void SetCallbacks(Action onClose, Action onSettings, Action onExit) {
            _closePopup = onClose;
            _settingsClick = onSettings;
            _exitClick = onExit;
        }

        private void OnClose() {
            _closePopup?.Invoke();
        }

        private void OnSettings() {
            _settingsClick?.Invoke();
        }

        private void OnExit() {
            _exitClick?.Invoke();
        }
    }
}
