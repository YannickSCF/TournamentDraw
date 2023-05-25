using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// Custom dependencies
using YannickSCF.TournamentDraw.Controllers.Configurator;
using YannickSCF.TournamentDraw.Controllers.InitialPanel;

namespace YannickSCF.TournamentDraw.Views {
    public class UIManager : MonoBehaviour {

        [SerializeField] private Image bgImage;
        [SerializeField] private TextMeshProUGUI uiTitle;
        [SerializeField] private Transform mainSpace;


        [Header("UI Panels")]
        [SerializeField] private InitialPanelController initialPanelPrefab;
        [SerializeField] private DrawConfiguratorController configuratorPanelPrefab;
        [SerializeField] private GameObject drawPanelPrefab;

        private Action<DrawConfiguratorController> actionOnConfiguratorOpen;

        public void OpenInitialPanel() {
            GameObject initialPanel = Instantiate(initialPanelPrefab.gameObject, mainSpace);
        }

        public void OpenConfiguratorPanel(Action<DrawConfiguratorController> onInstantiated) {
            GameObject configPanel = Instantiate(configuratorPanelPrefab.gameObject, mainSpace);
            actionOnConfiguratorOpen = onInstantiated;

            actionOnConfiguratorOpen?.Invoke(configPanel.GetComponent<DrawConfiguratorController>());
        }

        public void OpenDrawPanel(Action onInstantiated) {
            
        }

        public void ChangePanelEvent() {

        }

        public void CloseCurrentPanel() {
            if(mainSpace.childCount > 0) {
                Destroy(mainSpace.GetChild(0).gameObject);
            }
        }
    }
}
