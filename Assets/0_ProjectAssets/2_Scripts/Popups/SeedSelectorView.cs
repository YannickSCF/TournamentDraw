using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.GeneralApp.View.UI.Popups;

namespace YannickSCF.TournamentDraw.Popups {
    public class SeedSelectorView : PopupView {

        public event CommonEventsDelegates.StringEvent OnSeedChanged;

        public event CommonEventsDelegates.SimpleEvent OnRandomizedSeed;
        public event CommonEventsDelegates.SimpleEvent OnFinishedSelection;

        [SerializeField] private TMP_InputField _seedInputField;
        [SerializeField] private Button _randomizeSeedButton;
        [SerializeField] private Button _finishButton;

        #region Mono
        private void OnEnable() {
            _seedInputField.onValueChanged.AddListener(OnInputChanged);

            _randomizeSeedButton.onClick.AddListener(() => OnRandomizedSeed?.Invoke());
            _finishButton.onClick.AddListener(() => OnFinishedSelection?.Invoke());
        }

        private void OnDisable() {
            _seedInputField.onValueChanged.RemoveAllListeners();

            _randomizeSeedButton.onClick.RemoveAllListeners();
            _finishButton.onClick.RemoveAllListeners();
        }
        #endregion

        #region Event listeners methods
        private void OnInputChanged(string inputSeed) {
            OnSeedChanged?.Invoke(inputSeed);
        }
        #endregion

        public void SetEmptyOption() {
            _seedInputField.SetTextWithoutNotify("");
        }

        public void SetNewOption(int newSeed) {
            _seedInputField.SetTextWithoutNotify(newSeed.ToString());
        }

        public void SetFinishButtonInteractable(bool isInteractable) {
            _finishButton.interactable = isInteractable;
        }

        public override void Close() {
            base.Close();
        }
    }
}
