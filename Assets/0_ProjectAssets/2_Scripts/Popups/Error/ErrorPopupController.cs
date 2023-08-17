using System;
using YannickSCF.GeneralApp.Controller.UI.Popups;

namespace YannickSCF.TournamentDraw.Popups {
    public class ErrorPopupData : PopupData {
        private string _errorTitle;
        private string _errorDescription;

        private Action _closePopupAction;

        public ErrorPopupData(
            string popupId,
            string errorTitle,
            string errorDescription,
            Action closePopupAction) : base(popupId) {
            _errorTitle = errorTitle;
            _errorDescription = errorDescription;
            _closePopupAction = closePopupAction;
        }

        public string ErrorTitle { get => _errorTitle; }
        public string ErrorDescription { get => _errorDescription; }
        public Action ClosePopupAction { get => _closePopupAction; }
    }

    public class ErrorPopupController : PopupController {

        private ErrorPopupView _view;
        private Action _onClosePopup;

        #region Mono
        private void Awake() {
            _view = GetView<ErrorPopupView>();
        }

        protected override void OnEnable() {
            base.OnEnable();
            _view.OnCloseButtonPressed += CloseButtonPressed;
        }

        protected override void OnDisable() {
            base.OnDisable();
            _view.OnCloseButtonPressed -= CloseButtonPressed;
        }
        #endregion

        #region Event listeners methods
        private void CloseButtonPressed() {
            _onClosePopup?.Invoke();
        }
        #endregion

        public override void Init(PopupData popupData) {
            ErrorPopupData errorPopupData = (ErrorPopupData)popupData;

            _view.SetErrorTexts(errorPopupData.ErrorTitle, errorPopupData.ErrorDescription);

            _onClosePopup = errorPopupData.ClosePopupAction;

            base.Init(popupData);
        }
    }
}
