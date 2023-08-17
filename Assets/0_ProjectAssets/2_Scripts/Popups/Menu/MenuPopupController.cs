using System;
using YannickSCF.GeneralApp.Controller.UI.Popups;

namespace YannickSCF.TournamentDraw.Popups {
    public class MenuPopupData : PopupData {
        private Action _closePopupAction;
        private Action _settingsClickAction;
        private Action _exitClickAction;

        public MenuPopupData(
            string popupId,
            Action closePopupAction,
            Action settingsClickAction,
            Action exitClickAction) : base(popupId) {
            _closePopupAction = closePopupAction;
            _settingsClickAction = settingsClickAction;
            _exitClickAction = exitClickAction;
        }

        public Action ClosePopupAction { get => _closePopupAction; }
        public Action SettingsClickAction { get => _settingsClickAction; }
        public Action ExitClickAction { get => _exitClickAction; }
    }

    public class MenuPopupController : PopupController {

        private MenuPopupView _view;

        private Action _closePopup;
        private Action _settingsClick;
        private Action _exitClick;

        #region Mono
        private void Awake() {
            _view = GetView<MenuPopupView>();
        }

        protected override void OnEnable() {
            base.OnEnable();

            _view.CloseButton += OnClose;
            _view.SettingsButton += OnSettings;
            _view.ExitButton += OnExit;
        }

        protected override void OnDisable() {
            base.OnDisable();

            _view.CloseButton -= OnClose;
            _view.SettingsButton -= OnSettings;
            _view.ExitButton -= OnExit;
        }
        #endregion

        #region Event listeners methods
        private void OnClose() {
            _closePopup?.Invoke();
        }

        private void OnSettings() {
            _settingsClick?.Invoke();
        }

        private void OnExit() {
            _exitClick?.Invoke();
        }
        #endregion

        public override void Init(PopupData popupData) {
            MenuPopupData menuData = (MenuPopupData)popupData;
            _closePopup = menuData.ClosePopupAction;
            _settingsClick = menuData.SettingsClickAction;
            _exitClick = menuData.ExitClickAction;

            base.Init(popupData);
        }
    }
}
