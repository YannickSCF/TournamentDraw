using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// Custom dependencies
using YannickSCF.TournamentDraw.Controllers.Configurator;

namespace YannickSCF.TournamentDraw.Views {
    public class UIManager : MonoBehaviour {

        [SerializeField] private Image bgImage;
        [SerializeField] private TextMeshProUGUI uiTitle;
        [SerializeField] private Transform mainSpace;


        [Header("UI Panels")]
        [SerializeField] private GameObject initialPanelPrefab;
        [SerializeField] private DrawConfiguratorController configuratorPanelPrefab;
        [SerializeField] private GameObject drawPanelPrefab;

        private Action<DrawConfiguratorController> actionOnConfiguratorOpen;

        public void OpenInitialPanel() {
            GameObject initialPanel = Instantiate(initialPanelPrefab, mainSpace);
        }

        public void OpenConfiguratorPanel(Action<DrawConfiguratorController> onInstantiated) {
            GameObject configPanel = Instantiate(configuratorPanelPrefab.gameObject, mainSpace);
            actionOnConfiguratorOpen = onInstantiated;

            actionOnConfiguratorOpen?.Invoke(configuratorPanelPrefab);
        }

        public void OpenDrawPanel(Action onInstantiated) {
            
        }
    }
}
