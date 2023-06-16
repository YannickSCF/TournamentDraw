using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// Custom dependencies
using YannickSCF.TournamentDraw.Controllers.Configurator;
using YannickSCF.TournamentDraw.Controllers.Draw;
using YannickSCF.TournamentDraw.Controllers.InitialPanel;

namespace YannickSCF.TournamentDraw.Views {
    public class UIManager : MonoBehaviour {
        
        private InitialPanelController _initialPanel;
        private DrawConfiguratorController _configuratorPanel;
        private DrawPanelController _drawPanel;

        private Action<DrawConfiguratorController> actionOnConfiguratorOpen;
        private Action<DrawPanelController> actionOnDrawOpen;

        public void OpenInitialPanel() {
            _initialPanel = FindObjectOfType<InitialPanelController>();
        }

        public void OpenConfiguratorPanel(Action<DrawConfiguratorController> onInstantiated) {
            _configuratorPanel = FindObjectOfType<DrawConfiguratorController>();
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
