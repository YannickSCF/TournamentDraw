using System;
using YannickSCF.GeneralApp.Controller.UI.Popups;

namespace YannickSCF.TournamentDraw.Popups {
    public class ExitPopupData : PopupData {
        private bool _hasSaveAndExitOption;

        private Action _closePopupAction;
        private Action<bool> _exitClickAction;

        public ExitPopupData(
            string popupId,
            bool hasSaveAndExitOption,
            Action closePopupAction,
            Action<bool> exitClickAction) : base(popupId) {
            _hasSaveAndExitOption = hasSaveAndExitOption;
            _closePopupAction = closePopupAction;
            _exitClickAction = exitClickAction;
        }

        public bool HasSaveAndExitOption { get => _hasSaveAndExitOption; }
        public Action ClosePopupAction { get => _closePopupAction; }
        public Action<bool> ExitClickAction { get => _exitClickAction; }
    }

    public class ExitAppPopupController : PopupController {

        private ExitAppPopupView _view;

        private Action _onClosePopup;
        private Action<bool> _onExitPopup;

        #region Mono
        private void Awake() {
            _view = GetView<ExitAppPopupView>();
        }

        protected override void OnEnable() {
            base.OnEnable();

            _view.OnCloseButtonButtonPressed += CloseButtonPressed;
            _view.OnExitButtonPressed += ExitPopup;
        }

        protected override void OnDisable() {
            base.OnDisable();

            _view.OnCloseButtonButtonPressed -= CloseButtonPressed;
            _view.OnExitButtonPressed -= ExitPopup;
        }
        #endregion

        #region Event listeners methods
        private void CloseButtonPressed() {
            _onClosePopup?.Invoke();
        }

        private void ExitPopup(bool boolValue) {
            _onExitPopup?.Invoke(boolValue);
        }
        #endregion

        public override void Init(PopupData popupData) {
            ExitPopupData exitPopupData = (ExitPopupData)popupData;

            _view.SetSaveAndExitOption(exitPopupData.HasSaveAndExitOption);

            _onClosePopup = exitPopupData.ClosePopupAction;
            _onExitPopup = exitPopupData.ExitClickAction;

            base.Init(popupData);
        }
    }
}
