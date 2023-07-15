using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.GeneralApp.View.UI.Popups;

namespace YannickSCF.TournamentDraw.Popups {
    public class MenuPopupView : PopupView {

        public event CommonEventsDelegates.SimpleEvent CloseButton;
        public event CommonEventsDelegates.SimpleEvent SettingsButton;
        public event CommonEventsDelegates.SimpleEvent ExitButton;

        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;

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
