using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.GeneralApp.View.UI.Windows;
using YannickSCF.TournamentDraw.Views.InitialPanel.Events;

namespace YannickSCF.TournamentDraw.Views.InitialPanel {
    public class InitialPanelView : WindowView {

        [SerializeField] private Button newDrawButton;
        [SerializeField] private Button loadDrawButton;
        [SerializeField] private Button settingsButton;

        #region Mono
        private void OnEnable() {
            newDrawButton.onClick.AddListener(NewDrawButtonClicked);
            loadDrawButton.onClick.AddListener(LoadDrawButtonClicked);
            settingsButton.onClick.AddListener(SettingsButtonClicked);
        }

        private void OnDisable() {
            newDrawButton.onClick.RemoveAllListeners();
            loadDrawButton.onClick.RemoveAllListeners();
            settingsButton.onClick.RemoveAllListeners();
        }
        #endregion

        #region Events listeners methods
        private void NewDrawButtonClicked() {
            InitialPanelViewEvents.ThrowOnNewDrawButtonPressed();
        }

        private void LoadDrawButtonClicked() {
            InitialPanelViewEvents.ThrowOnLoadDrawButtonPressed();
        }

        private void SettingsButtonClicked() {
            InitialPanelViewEvents.ThrowOnSettingsButtonPressed();

        }
        #endregion
    }
}
