using System;
using System.Collections.Generic;
using TMPro;
using YannickSCF.TournamentDraw.Importers;
using TournamentMaker.Settings.View.Componets;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.TournamentDraw.Model;

namespace YannickSCF.TournamentDraw.Settings.View.Componets {
    public enum ParticipantBasicInfo { Country, Surname, Name, Rank, Styles, Academy, School, Tier };

    public class ParticipantsOptionView : MonoBehaviour {

        [Header("Load File panel")]
        [SerializeField] private Button loadFileButton;
        [SerializeField] private TextMeshProUGUI filePathText;

        [Header("Table Values")]
        [SerializeField] private ScrollRect tableScrollRect;
        [SerializeField] private GameObject tableRowPrefab;
        [SerializeField] private Button addRowButton;
        [SerializeField] private Button removeRowButton;

        private bool[] checkBoxesMarked = new bool[Enum.GetValues(typeof(ParticipantBasicInfo)).Length];

        #region Mono
        private void Start() {
            removeRowButton.interactable = false;
        }

        private void OnEnable() {
            loadFileButton.onClick.AddListener(LoadParticipantsFromFile);

            ConfiguratorViewEvents.OnCheckBoxClicked += ModifyTableByCheckBox;

            addRowButton.onClick.AddListener(AddNewRow);
            removeRowButton.onClick.AddListener(RemoveNewRow);
        }

        private void OnDisable() {
            loadFileButton.onClick.RemoveAllListeners();

            ConfiguratorViewEvents.OnCheckBoxClicked -= ModifyTableByCheckBox;

            addRowButton.onClick.RemoveAllListeners();
            removeRowButton.onClick.RemoveAllListeners();
        }
        #endregion

        #region Methods to control confirmation panel
        private void FinishedParticipantsTable() {
            // TODO: Cambiar texto por LocalizeStringEvent
            ConfiguratorViewEvents.ThrowOpenConfirmationPanel("Are you sure you have finished editing the table?");
            ConfiguratorViewEvents.OnConfirmationPanelAction += FinishParticipantsTable;
        }
        private void FinishParticipantsTable(bool clickedYes) {
            if (clickedYes) {
                ConfiguratorViewEvents.ThrowTournamentParticipantsSetted(GetParticipantsFromTable());
                ConfiguratorViewEvents.ThrowTournamentParticipantDataSetted(GetParticipantData());
                ConfiguratorViewEvents.ThrowParticipantPanelStateSetted(true);
            }

            ConfiguratorViewEvents.ThrowCloseConfirmationPanel();
            ConfiguratorViewEvents.OnConfirmationPanelAction -= FinishParticipantsTable;
        }

        private void ModifyParticipantsTable() {
            // TODO: Cambiar texto por LocalizeStringEvent
            ConfiguratorViewEvents.ThrowOpenConfirmationPanel("Are you sure you want to re-edit the table? You will lose the configuration you had so far.");
            ConfiguratorViewEvents.OnConfirmationPanelAction += ModifyParticipantsTable;
        }
        private void ModifyParticipantsTable(bool clickedYes) {
            if (clickedYes) {
                ConfiguratorViewEvents.ThrowTournamentParticipantsSetted(null);
                ConfiguratorViewEvents.ThrowTournamentParticipantDataSetted(null);
                ConfiguratorViewEvents.ThrowParticipantPanelStateSetted(false);
            }

            ConfiguratorViewEvents.ThrowCloseConfirmationPanel();
            ConfiguratorViewEvents.OnConfirmationPanelAction -= ModifyParticipantsTable;
        }
        #endregion

        #region Table Controller
        private void LoadParticipantsFromFile() {
            string filePath = FileImporter.SelectFileWithBrowser();
            if (!string.IsNullOrEmpty(filePath)) {
                List<Participant> participants = FileImporter.ImportParticipantsFromFile(filePath);
                if (participants.Count > 0) {
                    filePathText.text = filePath;
                    LoadParticipantsOnTable(participants);
                }
            }
        }

        private void LoadParticipantsOnTable(List<Participant> participants) {
            for (int i = 0; i < participants.Count; ++i) {
                if (i == tableScrollRect.content.childCount - 2) {
                    AddNewRow();
                }
                TableRow row = tableScrollRect.content.GetChild(i + 1).GetComponent<TableRow>();
                if (row != null) {
                    row.SetCountryField(participants[i].Country);
                    row.SetSurnameField(participants[i].Surname);
                    row.SetNameField(participants[i].Name);
                    row.SetRankField(participants[i].Rank.ToString());
                    row.SetStylesField(participants[i].Styles);
                    row.SetAcademyField(participants[i].AcademyName);
                    row.SetSchoolField(participants[i].SchoolName);
                    row.SetTierField(participants[i].TierLevel.ToString());
                }
            }
            CleanTable(participants.Count);
        }

        private void CleanTable(int participantsNumber) {
            for (int i = participantsNumber + 1; i < tableScrollRect.content.childCount - 1; ++i) {
                if (i != tableScrollRect.content.childCount - 1) {
                    Destroy(tableScrollRect.content.GetChild(i).gameObject);
                }
            }
        }

        private void ModifyTableByCheckBox(ParticipantBasicInfo checkBoxType, bool isActive) {
            checkBoxesMarked[(int)checkBoxType] = isActive;

            foreach (Transform row in tableScrollRect.content) {
                TableRow tableRow = row.GetComponent<TableRow>();
                if (tableRow != null) {
                    tableRow.ShowColumn(checkBoxType, !isActive);
                } else {
                    TableTitleRow tableTitleRow = row.GetComponent<TableTitleRow>();
                    if (tableTitleRow != null) {
                        tableTitleRow.ShowColumn(checkBoxType, !isActive);
                    }
                }
            }
        }

        private void AddNewRow() {
            GameObject newRow = Instantiate(tableRowPrefab, tableScrollRect.content);
            newRow.transform.SetSiblingIndex(tableScrollRect.content.childCount - 2);
            if (tableScrollRect.content.childCount > 2) {
                removeRowButton.interactable = true;
            }
        }

        private void RemoveNewRow() {
            Destroy(tableScrollRect.content.GetChild(tableScrollRect.content.childCount - 2).gameObject);
            if (tableScrollRect.content.childCount <= 3) {
                removeRowButton.interactable = false;
            }
        }
        #endregion


        #region Overrided Methods
        public void ResetPanel() {
            // Reset table loaded
            filePathText.text = string.Empty;
            // Reset table values
            CleanTable(0);
            AddNewRow();
            // Reset checkboxes to ignore columns
            ResetIgnoreCheckboxValues();
        }

        private void ResetIgnoreCheckboxValues() {
            // Unmarck all checkboxes
            //foreach (IgnoreCheckbox checkbox in checkboxes) {
            //    checkbox.Reset();
            //}
            // Reset table columns removed by checkboxes
            Array allCheckboxes = Enum.GetValues(typeof(ParticipantBasicInfo));
            for (int i = 0; i < checkBoxesMarked.Length; ++i) {
                if (checkBoxesMarked[i]) {
                    ModifyTableByCheckBox((ParticipantBasicInfo)allCheckboxes.GetValue(i), false);
                }
            }
            // Remove ignore checkboxes markers
            checkBoxesMarked = new bool[Enum.GetValues(typeof(ParticipantBasicInfo)).Length];
        }

        public void OpenPanel() {
            gameObject.SetActive(true);
        }

        public void ClosePanel() {
            tableScrollRect.horizontalScrollbar.value = 1;
            tableScrollRect.verticalScrollbar.value = 1;

            gameObject.SetActive(false);
        }

        #region Methods to update participants Data
        public List<Participant> GetParticipantsFromTable() {
            List<Participant> res = new List<Participant>();

            for (int i = 1; i < tableScrollRect.content.childCount - 1; ++i) {
                TableRow row = tableScrollRect.content.GetChild(i).GetComponent<TableRow>();
                Participant participant = new Participant(
                    row.GetCountryField(),
                    row.GetNameField(),
                    row.GetSurnameField(),
                    row.GetRankField(),
                    row.GetStylesField(),
                    row.GetSchoolField(),
                    row.GetAcademyField()
                );
                res.Add(participant);
            }

            return res;
        }

        public List<ParticipantBasicInfo> GetParticipantData() {
            List<ParticipantBasicInfo> res = new List<ParticipantBasicInfo>();

            for (int i = 0; i < checkBoxesMarked.Length; ++i) {
                if (!checkBoxesMarked[i]) {
                    res.Add((ParticipantBasicInfo)i);
                }
            }

            return res;
        }
        #endregion

        #endregion


        #region Table Checkers
        private bool CheckMinParticipants(out string errorStr) {
            errorStr = "Configure at least 4 participants";

            if (tableScrollRect.content.transform.childCount < 6) return false;

            errorStr = string.Empty;
            return true;
        }

        private bool CheckNamesAndSurnames(out string errorStr) {
            errorStr = "Name and surname are mandatory fields";

            for (int i = 1; i < tableScrollRect.content.transform.childCount; ++i) {
                TableRow row = tableScrollRect.content.GetChild(i).GetComponent<TableRow>();
                if (row != null) {
                    if (string.IsNullOrEmpty(row.GetSurnameField()) || string.IsNullOrEmpty(row.GetNameField())) {
                        return false;
                    }
                }
            }

            errorStr = string.Empty;
            return true;
        }
        #endregion
    }
}
