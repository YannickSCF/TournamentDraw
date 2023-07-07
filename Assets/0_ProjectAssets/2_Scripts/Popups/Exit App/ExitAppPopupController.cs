using System;

using YannickSCF.GeneralApp.Controller.UI.Popups;

namespace YannickSCF.TournamentDraw.Popups {
    public class ExitAppPopupController : PopupController<ExitAppPopupView> {

        private Action _onClosePopup;
        private Action<bool> _onExitPopup;

        #region Mono
        protected override void OnEnable() {
            base.OnEnable();

            View.OnCloseButtonButtonPressed += CloseButtonPressed;
            View.OnExitButtonPressed += ExitPopup;
        }

        protected override void OnDisable() {
            base.OnDisable();

            View.OnCloseButtonButtonPressed -= CloseButtonPressed;
            View.OnExitButtonPressed -= ExitPopup;
        }
        #endregion

        public void SetCallbacks(Action closeCallback, Action<bool> exitCallback) {
            _onClosePopup = closeCallback;
            _onExitPopup = exitCallback;
        }

        public void SetSaveAndExitOption(bool activeSaveButton) {
            View.SetSaveAndExitOption(activeSaveButton);
        }

        private void CloseButtonPressed() {
            _onClosePopup?.Invoke();
        }

        private void ExitPopup(bool boolValue) {
            _onExitPopup?.Invoke(boolValue);
        }
    }
}
