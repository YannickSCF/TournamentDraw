using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
// Custom dependencies
using YannickSCF.TournamentDraw.Views.Configurator.DrawOptions.Components;
using YannickSCF.TournamentDraw.Views.Configurator.Events;

namespace YannickSCF.TournamentDraw.Views.Configurator.DrawOptions {

    public class DrawOptionsView : MonoBehaviour {

        [Header("Tournament draw Name")]
        [SerializeField] private TMP_InputField drawNameInputField;

        [Header("Total competitors (auto filled)")]
        [SerializeField] private TextMeshProUGUI totalCompetitorsTMP;

        [Header("Number of poules and max poule size")]
        [SerializeField] private TMP_InputField numberOfPoulesInputField;
        [SerializeField] private TMP_InputField maxPouleSizeInputField;

        [Header("Draw options")]
        [SerializeField] private TMP_Dropdown pouleAssignDropdown;
        [SerializeField] private TMP_Dropdown participantSelectionDropdown;

        [Header("Participants information")]
        [SerializeField] private List<ParticipantInfoCheckbox> allInfoCheckmarks;

        #region Mono
        private void OnEnable() {
            drawNameInputField.onValueChanged.AddListener(OnDrawNameChanged);

            numberOfPoulesInputField.onValueChanged.AddListener(OnNumberOfPoulesChanged);
            maxPouleSizeInputField.onValueChanged.AddListener(OnMaxPouleSizeChanged);

            pouleAssignDropdown.onValueChanged.AddListener(OnPouleAssignChanged);
            participantSelectionDropdown.onValueChanged.AddListener(OnParticipantSelectionChanged);
        }

        private void OnDisable() {
            drawNameInputField.onValueChanged.RemoveAllListeners();

            numberOfPoulesInputField.onValueChanged.RemoveAllListeners();
            maxPouleSizeInputField.onValueChanged.RemoveAllListeners();

            pouleAssignDropdown.onValueChanged.RemoveAllListeners();
            participantSelectionDropdown.onValueChanged.RemoveAllListeners();
        }
        #endregion

        #region Events listeners methods
        private void OnDrawNameChanged(string newName) {
            ConfiguratorViewEvents.ThrowOnDrawNameChanged(newName);
        }
        private void OnNumberOfPoulesChanged(string newNumber) {
            ConfiguratorViewEvents.ThrowOnNumberOfPoulesChanged(newNumber);
        }
        private void OnMaxPouleSizeChanged(string newSize) {
            ConfiguratorViewEvents.ThrowOnMaxPouleSizeChanged(newSize);
        }
        private void OnPouleAssignChanged(int indexSelected) {
            ConfiguratorViewEvents.ThrowOnPouleAssignChanged(indexSelected);
        }
        private void OnParticipantSelectionChanged(int indexSelected) {
            ConfiguratorViewEvents.ThrowOnParticipantSelectionChanged(indexSelected);
        }
        #endregion

        #region GETTERS
        public string GetTournamentDrawName() {
            if (string.IsNullOrEmpty(drawNameInputField.text))
                return string.Empty;
            return drawNameInputField.text;
        }

        public int GetNumberOfPoules() {
            if (string.IsNullOrEmpty(numberOfPoulesInputField.text))
                return 0;

            int.TryParse(numberOfPoulesInputField.text, out int res);

            return res;
        }

        public int GetMaxPouleSize() {
            if (string.IsNullOrEmpty(maxPouleSizeInputField.text))
                return 0;

            int.TryParse(maxPouleSizeInputField.text, out int res);

            return res;
        }

        public PouleAssignType GetPouleAssign() {
            return (PouleAssignType)pouleAssignDropdown.value;
        }
        
        public ParticipantSelectionType GetParticipantSelection() {
            return (ParticipantSelectionType)participantSelectionDropdown.value;
        }

        public bool[] GetParticipantInfoSelected() {
            bool[] res = new bool[Enum.GetValues(typeof(ParticipantBasicInfo)).Length];

            for (int i = 0; i < allInfoCheckmarks.Count; ++i) {
                res[i] = allInfoCheckmarks[i].GetCheckboxIsOn();
            }

            return res;
        }
        #endregion

        #region SETTERS
        public void SetDrawName(string newText) {
            drawNameInputField.SetTextWithoutNotify(newText);
        }

        public void SetTotalParticipantsText(string newText) {
            totalCompetitorsTMP.text = newText;
        }

        public void SetNumberOfPoules(string newText) {
            numberOfPoulesInputField.SetTextWithoutNotify(newText);
        }

        public void SetMaxPouleSize(string newText) {
            maxPouleSizeInputField.SetTextWithoutNotify(newText);
        }

        public void SetPouleAssign(int indexSelection) {
            pouleAssignDropdown.SetValueWithoutNotify(indexSelection);
        }

        public void SetParticipantSelection(int indexSelection) {
            participantSelectionDropdown.SetValueWithoutNotify(indexSelection);
        }

        public void SetParticipantInfoSelected(bool[] infoSelected) {
            int length = infoSelected.Length;
            if (infoSelected.Length != allInfoCheckmarks.Count) {
                Debug.LogWarning("SetParticipantInfoSelected: Wrong info selected array size: " + infoSelected.Length);
                length = infoSelected.Length < allInfoCheckmarks.Count ? infoSelected.Length : allInfoCheckmarks.Count;
            }

            for (int i = 0; i < length; ++i) {
                allInfoCheckmarks[i].SetCheckboxValue(infoSelected[i]);
            }
        }
        #endregion

        public void ResetDrawOptions() {
            SetDrawName("");

            SetTotalParticipantsText("Total: Empty");

            SetNumberOfPoules("0");
            SetMaxPouleSize("0");

            SetPouleAssign(0);
            SetParticipantSelection(0);

            SetParticipantInfoSelected(AppConstants.ParticipantInfoDefault);
        }
    }
}
