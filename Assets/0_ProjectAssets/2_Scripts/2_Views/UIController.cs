/// Dependencies
using System;
using UnityEngine;
/// Custom dependencies
using YannickSCF.GeneralApp.Controller.UI;
using YannickSCF.GeneralApp.View.UI.Windows;
using YannickSCF.TournamentDraw.Controllers.Configurator;
using YannickSCF.TournamentDraw.Controllers.Draw;
using YannickSCF.TournamentDraw.Controllers.InitialPanel;

namespace YannickSCF.TournamentDraw.Views {
    public class UIController : BaseUIController {
        
        private InitialPanelController _initialPanel;
        private DrawConfiguratorController _configuratorPanel;
        private DrawPanelController _drawPanel;

        private Action<DrawConfiguratorController> actionOnConfiguratorOpen;
        private Action<DrawPanelController> actionOnDrawOpen;

        public void OpenInitialPanel() {
            ShowView("Initial");
        }

        public void OpenConfiguratorPanel(Action<DrawConfiguratorController> onInstantiated) {
            WindowsView configView = ShowView("Config");
            _configuratorPanel = configView.GetComponent<DrawConfiguratorController>();
            actionOnConfiguratorOpen = onInstantiated;

            actionOnConfiguratorOpen?.Invoke(_configuratorPanel);
        }

        public void OpenDrawPanel(Action<DrawPanelController> onInstantiated) {
            _drawPanel = FindObjectOfType<DrawPanelController>();
            actionOnDrawOpen = onInstantiated;

            actionOnDrawOpen?.Invoke(_drawPanel);
        }
    }
}
