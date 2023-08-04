using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.GeneralApp.View.UI.Popups;
using static YannickSCF.GeneralApp.CommonEventsDelegates;

namespace YannickSCF.TournamentDraw.Popups {
    public class MenuPopupView : PopupView {

        public event SimpleEventDelegate CloseButton;
        public event SimpleEventDelegate SettingsButton;
        public event SimpleEventDelegate ExitButton;

        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;

        [SerializeField] private Animator _popupAnimator;

        #region Mono
        private void OnEnable() {
            _closeButton.onClick.AddListener(CloseButtonClicked);
            _settingsButton.onClick.AddListener(SettingsButtonClicked);
            _exitButton.onClick.AddListener(ExitButtonClicked);
        }

        private void OnDisable() {
            _closeButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
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

        private void CloseButtonClicked() {
            CloseButton?.Invoke();
        }

        private void SettingsButtonClicked() {
            SettingsButton?.Invoke();
        }

        private void ExitButtonClicked() {
            ExitButton?.Invoke();
        }
    }
}
