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

        [SerializeField] private Image bgImage;
        [SerializeField] private TextMeshProUGUI uiTitle;
        [SerializeField] private Transform mainSpace;

        [Header("UI Panels")]
        [SerializeField] private InitialPanelController initialPanelPrefab;
        [SerializeField] private DrawConfiguratorController configuratorPanelPrefab;
        [SerializeField] private DrawPanelController drawPanelPrefab;

        private Action<DrawConfiguratorController> actionOnConfiguratorOpen;
        private Action<DrawPanelController> actionOnDrawOpen;

        public void OpenInitialPanel() {
            GameObject initialPanel = Instantiate(initialPanelPrefab.gameObject, mainSpace);
        }

        public void OpenConfiguratorPanel(Action<DrawConfiguratorController> onInstantiated) {
            GameObject configPanel = Instantiate(configuratorPanelPrefab.gameObject, mainSpace);
            actionOnConfiguratorOpen = onInstantiated;

            actionOnConfiguratorOpen?.Invoke(configPanel.GetComponent<DrawConfiguratorController>());
        }

        public void OpenDrawPanel(Action<DrawPanelController> onInstantiated) {
            GameObject drawPanel = Instantiate(drawPanelPrefab.gameObject, mainSpace);
            actionOnDrawOpen = onInstantiated;

            actionOnDrawOpen?.Invoke(drawPanel.GetComponent<DrawPanelController>());
        }

        public void CloseCurrentPanel() {
            if(mainSpace.childCount > 0) {
                Destroy(mainSpace.GetChild(0).gameObject);
            }
        }

        public void ChangeTitle(string newTitle) {
            uiTitle.text = newTitle;
        }
    }
}
