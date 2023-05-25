using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Views.InitialPanel;
using YannickSCF.TournamentDraw.Views.InitialPanel.Events;

namespace YannickSCF.TournamentDraw.Controllers.InitialPanel {
    public class InitialPanelController : MonoBehaviour {

        [SerializeField] private InitialPanelView initialPanelView;

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
            GameManager.Instance.SwitchState(States.Configurator);
        }

        private void LoadDrawButtonPressed() {

        }

        private void SettingsButtonPressed() {

        }
        #endregion

    }
}
