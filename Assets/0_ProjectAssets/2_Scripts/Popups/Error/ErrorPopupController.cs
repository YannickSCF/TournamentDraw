using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.GeneralApp.Controller.UI.Popups;

namespace YannickSCF.TournamentDraw.Popups {
    public class ErrorPopupController : PopupController<ErrorPopupView> {

        private Action _onClosePopup;

        #region Mono
        protected override void OnEnable() {
            base.OnEnable();
            View.OnCloseButtonPressed += CloseButtonPressed;
        }

        protected override void OnDisable() {
            base.OnDisable();
            View.OnCloseButtonPressed -= CloseButtonPressed;
        }
        #endregion

        public void SetCallback(Action closeCallback) {
            _onClosePopup = closeCallback;
        }

        public void SetErrorTexts(string title, string description) {
            View.SetErrorTexts(title, description);
        }

        private void CloseButtonPressed() {
            _onClosePopup?.Invoke();
        }
    }
}
