using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.GeneralApp.View.UI.Popups;
using static YannickSCF.GeneralApp.CommonEventsDelegates;

namespace YannickSCF.TournamentDraw.Popups {
    public class ErrorPopupView : PopupView {

        public event SimpleEventDelegate OnCloseButtonPressed;

        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Button _closeButton;

        [SerializeField] private Animator _popupAnimator;

        #region Mono
        private void OnEnable() {
            _closeButton.onClick.AddListener(() => OnCloseButtonPressed?.Invoke());
        }

        private void OnDisable() {
            _closeButton.onClick.RemoveAllListeners();
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

        public void SetErrorTexts(string title, string description) {
            _titleText.text = title;
            _descriptionText.text = description;
        }
    }
}
