using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
// Custom dependencies
using YannickSCF.TournamentDraw.Models;
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

        #region Events listeners methods
        private void LoadFile() {
            ConfiguratorViewEvents.ThrowOnLoadParticipantsFromFile();
        }

        private void ModifyTableByCheckBox(ParticipantBasicInfo checkBoxType, bool isActive) {
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
                tableScrollRect.content.childCount - 1).GetComponent<TableRow>();
            if (row != null) {
                row.SetCountryField(country);
                row.SetSurnameField(surname);
                row.SetNameField(name);
                row.SetRankField(rank.ToString());
                row.SetStylesField(styles);
                row.SetAcademyField(academy);
                row.SetSchoolField(school);
                row.SetTierField(tier.ToString());
            }
        }
        #endregion

        #region Table view management
        private void AddNewRow(bool throwEvent = true) {
            GameObject newRow = Instantiate(tableRowPrefab, tableScrollRect.content);
            newRow.transform.SetSiblingIndex(tableScrollRect.content.childCount - 2);
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

        public void CleanTable(int participantsNumber) {
            for (int i = participantsNumber + 1; i < tableScrollRect.content.childCount - 1; ++i) {
                if (i != tableScrollRect.content.childCount - 1) {
                    Destroy(tableScrollRect.content.GetChild(i).gameObject);
                }
            }
        }

        public void SetInfoSelectedColumns(bool[] checkBoxesMarked) {
            // Reset table columns removed by checkboxes
            Array allCheckboxes = Enum.GetValues(typeof(ParticipantBasicInfo));
            for (int i = 0; i < checkBoxesMarked.Length; ++i) {
                if (checkBoxesMarked[i]) {
                    ModifyTableByCheckBox((ParticipantBasicInfo)allCheckboxes.GetValue(i), false);
                }
            }
        }
        #endregion

        public void ResetParticipantsPanel() {
            tableScrollRect.horizontalScrollbar.value = 1;
            tableScrollRect.verticalScrollbar.value = 1;

            // Reset table loaded
            filePathText.text = string.Empty;
            // Reset table values
            CleanTable(0);
            AddNewRow();
        }
    }
}
