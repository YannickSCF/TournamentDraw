using UnityEngine;
using UnityEngine.UI;

using YannickSCF.GeneralApp.View.UI.Popups;

namespace YannickSCF.TournamentDraw.Popups {
    public class ExitAppPopupView : PopupView {

        public event CommonEventsDelegates.SimpleEvent OnCloseButtonButtonPressed;
        public event CommonEventsDelegates.BooleanEvent OnExitButtonPressed;

        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _exitAndSaveButton;

        #region Mono
        private void OnEnable() {
            _closeButton.onClick.AddListener(() => OnCloseButtonButtonPressed?.Invoke());
            _exitButton.onClick.AddListener(() => OnExitButtonPressed?.Invoke(false));
            _exitAndSaveButton.onClick.AddListener(() => OnExitButtonPressed?.Invoke(true));
        }

        private void OnDisable() {
            _closeButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
            _exitAndSaveButton.onClick.RemoveAllListeners();
        }
        #endregion

        public void SetSaveAndExitOption(bool isActive) {
            _exitAndSaveButton.gameObject.SetActive(isActive);
        }
    }
}
