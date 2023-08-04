using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.GeneralApp.View.UI.Popups;
using static YannickSCF.GeneralApp.CommonEventsDelegates;

namespace YannickSCF.TournamentDraw.Popups {
    public class SeedSelectorView : PopupView {

        public event StringEventDelegate OnSeedChanged;

        public event SimpleEventDelegate OnRandomizedSeed;
        public event SimpleEventDelegate OnFinishedSelection;
        public event SimpleEventDelegate OnCloseSelection;

        [SerializeField] private TMP_InputField _seedInputField;
        [SerializeField] private Button _randomizeSeedButton;
        [SerializeField] private Button _finishButton;
        [SerializeField] private Button _closeButton;

        [SerializeField] private Animator _popupAnimator;

        #region Mono
        private void OnEnable() {
            _seedInputField.onValueChanged.AddListener(OnInputChanged);

            _randomizeSeedButton.onClick.AddListener(() => OnRandomizedSeed?.Invoke());
            _finishButton.onClick.AddListener(() => OnFinishedSelection?.Invoke());
            _closeButton.onClick.AddListener(() => OnCloseSelection?.Invoke());
        }

        private void OnDisable() {
            _seedInputField.onValueChanged.RemoveAllListeners();

            _randomizeSeedButton.onClick.RemoveAllListeners();
            _finishButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
        }
        #endregion

        #region Event listeners methods
        private void OnInputChanged(string inputSeed) {
            OnSeedChanged?.Invoke(inputSeed);
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

        public void SetEmptyOption() {
            _seedInputField.SetTextWithoutNotify("");
        }

        public void SetNewOption(int newSeed) {
            _seedInputField.SetTextWithoutNotify(newSeed.ToString());
        }

        public void SetFinishButtonInteractable(bool isInteractable) {
            _finishButton.interactable = isInteractable;
        }
    }
}
