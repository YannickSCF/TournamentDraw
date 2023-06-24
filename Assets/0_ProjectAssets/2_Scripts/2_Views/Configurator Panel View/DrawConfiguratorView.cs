using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.GeneralApp.View.UI.Windows;
using YannickSCF.TournamentDraw.Scriptables;
// Custom dependencies
using YannickSCF.TournamentDraw.Views.Configurator.DrawOptions;
using YannickSCF.TournamentDraw.Views.Configurator.ParticipantList;

namespace YannickSCF.TournamentDraw.Views.Configurator {
    public class DrawConfiguratorView : WindowsView {
        
        [SerializeField] private ParticipantsListView participantsPanel;
        [SerializeField] private DrawOptionsView drawOptionsPanel;

        [SerializeField] private Button endButton;
        [SerializeField] private TextMeshProUGUI errorsPreview;

        private DrawConfiguration _configuration;

        #region Public control methods
        public void Init(DrawConfiguration configuration) {
            _configuration = configuration;

            participantsPanel.Init(_configuration);
            drawOptionsPanel.SetParticipantInfoSelected(_configuration.ParticipantInfoSelected);
        }

        public void OpenPanel() {
            gameObject.SetActive(true);

            // Set view related with info selected checkboxes
            participantsPanel.SetTableInfoSelectedColumns(AppConstants.ParticipantInfoDefault);
            drawOptionsPanel.SetParticipantInfoSelected(AppConstants.ParticipantInfoDefault);
        }

        public void ClosePanel() {
            gameObject.SetActive(false);

            participantsPanel.ResetParticipantsPanel();
            drawOptionsPanel.ResetDrawOptions();
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

        #region Getters
        public string GetDrawName() {
            return drawOptionsPanel.GetTournamentDrawName();
        }

        public int GetNumberOfPoules() {
            return drawOptionsPanel.GetNumberOfPoules();
        }

        public int GetMaxPouleSize() {
            return drawOptionsPanel.GetMaxPouleSize();
        }
        #endregion

        #region Setters
        public void SetFilePath(string filePath) {
            participantsPanel.SetFilePathText(filePath);
        }

        public void SetTotalParticipantsText(string newValue) {
            drawOptionsPanel.SetTotalParticipantsText(newValue);
        }

        public void ResetAllTable() {
            participantsPanel.CleanTable();
        }

        public void LoadParticipantOnTable(string country, string surname, string name,
            Ranks rank, List<Styles> styles, string academy, string school, int tier) {

            participantsPanel.LoadParticipantOnTable(country, surname, name,
                rank, styles, academy, school, tier);
        }

        public override void Init() {
            throw new System.NotImplementedException();
        }

        public override void Open() {
            throw new System.NotImplementedException();
        }

        public override void Close() {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
