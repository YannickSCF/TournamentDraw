using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using YannickSCF.GeneralApp.View.UI.Popups;
using static YannickSCF.GeneralApp.CommonEventsDelegates;

namespace YannickSCF.TournamentDraw.Popups {
    public class ExitAppPopupView : PopupView {

        public event SimpleEventDelegate OnCloseButtonButtonPressed;
        public event BooleanEventDelegate OnExitButtonPressed;

        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _exitAndSaveButton;

        [SerializeField] private Animator _popupAnimator;

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

        public override void Open() {
            base.Open();
            _popupAnimator.SetBool("Show", true);
        }

        public override void Show() {
            base.Show();
            _popupAnimator.SetBool("Show", true);
        }

        public override void Hide() {
            _popupAnimator.SetBool("Show", false);
            StartCoroutine(WaitToHideCoroutine());
        }

        private IEnumerator WaitToHideCoroutine() {
            yield return new WaitUntil(() => _popupAnimator.GetCurrentAnimatorStateInfo(0).IsName("popup_out_idle"));
            base.Hide();
        }

        public override void Close() {
            base.Close();
            _popupAnimator.SetBool("Show", false);
        }

        public void SetSaveAndExitOption(bool isActive) {
            _exitAndSaveButton.gameObject.SetActive(isActive);
        }
    }
}
