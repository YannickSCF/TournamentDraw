using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// Custom dependencies
using YannickSCF.TournamentDraw.Views.Configurator.DrawOptions;
using YannickSCF.TournamentDraw.Views.Configurator.ParticipantList;

namespace YannickSCF.TournamentDraw.Views.Configurator {
    public class DrawConfiguratorView : MonoBehaviour {
        
        [SerializeField] private ParticipantsListView participantsPanel;
        [SerializeField] private DrawOptionsView drawOptionsPanel;

        [SerializeField] private Button endButton;
        [SerializeField] private TextMeshProUGUI errorsPreview;

        #region Mono
        #endregion

        #region Public control methods
        public void OpenPanel() {
            gameObject.SetActive(true);
        }

        public void ClosePanel() {
            gameObject.SetActive(false);

            participantsPanel.ResetParticipantsPanel();
            drawOptionsPanel.ResetDrawOptions();

            // Set view related with info selected checkboxes
            bool[] infoSelected = new bool[Enum.GetValues(typeof(ParticipantBasicInfo)).Length];

            participantsPanel.SetInfoSelectedColumns(infoSelected);
            drawOptionsPanel.SetParticipantInfoSelected(infoSelected);
        }

        public void UpdateErrors(List<string> errorsList) {
            if (errorsList.Count > 0) {
                endButton.interactable = false;
                errorsPreview.text = errorsList[0];
            } else {
                endButton.interactable = true;
                errorsPreview.text = LocalizationKeys.ALL_READY;
            }
        }
        #endregion

        #region SETTERS
        public void SetFilePath(string filePath) {
            participantsPanel.SetFilePathText(filePath);
        }

        public void ResetAllTable() {
            participantsPanel.CleanTable(0);
        }

        public void LoadParticipantOnTable(string country, string surname, string name,
            Ranks rank, List<Styles> styles, string academy, string school, int tier) {

            participantsPanel.LoadParticipantOnTable(country, surname, name,
                rank, styles, academy, school, tier);
        }
        #endregion
    }
}
