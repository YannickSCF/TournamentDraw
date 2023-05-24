using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Views.InitialPanel.Events;

namespace YannickSCF.TournamentDraw.Controllers.InitialPanel {
    public class InitialPanelController : MonoBehaviour {

        #region Mono
        private void OnEnable() {
            InitialPanelViewEvents.OnNewDrawButtonPressed += NewDrawButtonPressed;
            InitialPanelViewEvents.OnLoadDrawButtonPressed += LoadDrawButtonPressed;
            InitialPanelViewEvents.OnSettingsButtonPressed += SettingsButtonPressed;
        }

        private void OnDisable() {
            InitialPanelViewEvents.OnNewDrawButtonPressed -= NewDrawButtonPressed;
            InitialPanelViewEvents.OnLoadDrawButtonPressed -= LoadDrawButtonPressed;
            InitialPanelViewEvents.OnSettingsButtonPressed -= SettingsButtonPressed;
        }
        #endregion

        #region Events listeners methods
        private void NewDrawButtonPressed() {
            GameManager.Instance.OpenConfiguratorPanel();
        }

        private void LoadDrawButtonPressed() {

        }

        private void SettingsButtonPressed() {

        }
        #endregion

    }
}
