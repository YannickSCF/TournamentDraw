using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// Custom dependencies
using YannickSCF.TournamentDraw.Scriptables;
using YannickSCF.TournamentDraw.Views.Configurator.Events;
using YannickSCF.TournamentDraw.Views.Configurator.ParticipantList.Components;

namespace YannickSCF.TournamentDraw.Views.Configurator.ParticipantList {

    public class ParticipantsListView : MonoBehaviour {

        [Header("Load File panel")]
        [SerializeField] private Button loadFileButton;
        [SerializeField] private TextMeshProUGUI filePathText;

        [Header("Table Values")]
        [SerializeField] private ScrollRect tableScrollRect;
        [SerializeField] private GameObject tableRowPrefab;
        [SerializeField] private Button addRowButton;
        [SerializeField] private Button removeRowButton;

        private DrawConfiguration _configuration;

        #region Mono
        private void Start() {
            removeRowButton.interactable = false;
        }

        private void OnEnable() {
            loadFileButton.onClick.AddListener(LoadFile);

            ConfiguratorViewEvents.OnParticipantInfoCheckboxToggle += ModifyTableByCheckBox;

            addRowButton.onClick.AddListener(AddNewRowNotifying);
            removeRowButton.onClick.AddListener(RemoveLastRowNotifying);
        }

        private void OnDisable() {
            loadFileButton.onClick.RemoveAllListeners();

            ConfiguratorViewEvents.OnParticipantInfoCheckboxToggle -= ModifyTableByCheckBox;

            addRowButton.onClick.RemoveAllListeners();
            removeRowButton.onClick.RemoveAllListeners();
        }
        #endregion

        public void Init(DrawConfiguration configuration) {
            _configuration = configuration;

            SetTableInfoSelectedColumns(_configuration.ParticipantInfoSelected);
        }

        #region Events listeners methods
        private void LoadFile() {
            ConfiguratorViewEvents.ThrowOnLoadParticipantsFromFile();
        }

        private void ModifyTableByCheckBox(ParticipantBasicInfo checkBoxType, bool isActive) {
            foreach (Transform row in tableScrollRect.content) {
                TableRow tableRow = row.GetComponent<TableRow>();
                if (tableRow != null) {
                    tableRow.ShowColumn(checkBoxType, isActive);
                } else {
                    TableTitleRow tableTitleRow = row.GetComponent<TableTitleRow>();
                    if (tableTitleRow != null) {
                        tableTitleRow.ShowColumn(checkBoxType, isActive);
                    }
                }
            }
        }

        private void AddNewRowNotifying() {
            AddNewRow();
        }

        private void RemoveLastRowNotifying() {
            RemoveLastRow();
        }
        #endregion

        #region SETTERS
        public void SetFilePathText(string filePath) {
            filePathText.text = filePath;
        }

        public void LoadParticipantOnTable(string country, string surname, string name,
            Ranks rank, List<Styles> styles, string academy, string school, int tier) {

            AddNewRow(false);

            TableRow row = tableScrollRect.content.GetChild(
                tableScrollRect.content.childCount - 2).GetComponent<TableRow>();
            if (row != null) {
                row.SetCountryField(country, true);
                row.SetSurnameField(surname, true);
                row.SetNameField(name, true);
                row.SetRankField(rank.ToString(), true);
                row.SetStylesField(styles, true);
                row.SetAcademyField(academy, true);
                row.SetSchoolField(school, true);
                row.SetTierField(tier.ToString(), true);
            }
        }
        #endregion

        #region Table view management
        private void AddNewRow(bool throwEvent = true) {
            GameObject newRow = Instantiate(tableRowPrefab, tableScrollRect.content);
            newRow.transform.SetSiblingIndex(tableScrollRect.content.childCount - 2);

            TableRow rowComponent = newRow.GetComponent<TableRow>();
            rowComponent.RowIndex = tableScrollRect.content.childCount - 3;
            SetRowInfoSelectedColumns(rowComponent, _configuration.ParticipantInfoSelected);

            if (tableScrollRect.content.childCount > 2) {
                removeRowButton.interactable = true;
            }

            if (throwEvent) {
                ConfiguratorViewEvents.ThrowOnParticipantAdded();
            }
        }

        private void RemoveLastRow(bool throwEvent = true) {
            Destroy(tableScrollRect.content.GetChild(tableScrollRect.content.childCount - 2).gameObject);
            if (tableScrollRect.content.childCount <= 3) {
                removeRowButton.interactable = false;
            }

            if (throwEvent) {
                ConfiguratorViewEvents.ThrowOnParticipantRemoved();
            }
        }

        public void CleanTable() {
            List<GameObject> allRows = new List<GameObject>();
            for(int i = 1; i < tableScrollRect.content.childCount - 1; ++i) {
                allRows.Add(tableScrollRect.content.GetChild(i).gameObject);
            }

            foreach (GameObject row in allRows) {
                Destroy(row);
            }
        }

        public void SetRowInfoSelectedColumns(TableRow row, bool[] checkBoxesMarked) {
            Array allCheckboxes = Enum.GetValues(typeof(ParticipantBasicInfo));
            for (int i = 0; i < checkBoxesMarked.Length; ++i) {
                row.ShowColumn((ParticipantBasicInfo)allCheckboxes.GetValue(i), checkBoxesMarked[i]);
            }
        }

        public void SetTableInfoSelectedColumns(bool[] checkBoxesMarked) {
            // Reset table columns removed by checkboxes
            Array allCheckboxes = Enum.GetValues(typeof(ParticipantBasicInfo));
            for (int i = 0; i < checkBoxesMarked.Length; ++i) {
                ModifyTableByCheckBox((ParticipantBasicInfo)allCheckboxes.GetValue(i), checkBoxesMarked[i]);
            }
        }
        #endregion

        public void ResetParticipantsPanel() {
            tableScrollRect.horizontalScrollbar.value = 1;
            tableScrollRect.verticalScrollbar.value = 1;

            // Reset table loaded
            filePathText.text = string.Empty;
            // Reset table values
            CleanTable();
        }
    }
}
