using System;
using System.Collections.Generic;
using TMPro;
using TournamentMaker.Settings.View.Componets;
using UnityEngine;

namespace YannickSCF.TournamentDraw.Settings.View.Componets {
    public class TableRow : MonoBehaviour {

        [SerializeField] private AutoCompleteInputField countryField;
        [SerializeField] private TMP_InputField surnameField;
        [SerializeField] private TMP_InputField nameField;
        [SerializeField] private TMP_Dropdown rankField;
        [SerializeField] private StylesDropdown stylesField;
        [SerializeField] private TMP_InputField academyField;
        [SerializeField] private TMP_InputField schoolField;
        [SerializeField] private TMP_InputField tierField;

        #region Mono
        private void OnEnable() {
            surnameField.onValueChanged.AddListener(OnMandatoryInputFilled);
            nameField.onValueChanged.AddListener(OnMandatoryInputFilled);
        }
        private void OnDisable() {
            surnameField.onValueChanged.RemoveAllListeners();
            nameField.onValueChanged.RemoveAllListeners();
        }
        #endregion

        private void OnMandatoryInputFilled(string text) {
            TournamentSettingsViewEvents.ThrowOnMandatoryInputFieldUpdated(!string.IsNullOrEmpty(text));
        }

        public void SetCountryField(string _countryCode) {
            countryField?.SetInitValue(_countryCode);
        }
        public string GetCountryField() {
            return countryField?.GetValue();
        }

        public void SetSurnameField(string _surname) {
            surnameField.text = _surname;
        }
        public string GetSurnameField() {
            return surnameField.text;
        }


        public void SetNameField(string _name) {
            nameField.text = _name;
        }
        public string GetNameField() {
            return nameField.text;
        }


        public void SetRankField(string _rankTxt) {
            rankField.SetValueWithoutNotify(rankField.options.FindIndex(x => x.text == _rankTxt));
        }
        public Ranks GetRankField() {
            return (Ranks)Enum.Parse(typeof(Ranks), rankField.captionText.text);
        }


        public void SetStylesField(List<Styles> _styles) {
            if (stylesField != null) {
                stylesField.SetInitValues(_styles);
            }
        }
        public List<Styles> GetStylesField() {
            return stylesField.GetValues();
        }


        public void SetAcademyField(string _academy) {
            academyField.text = _academy;
        }
        public string GetAcademyField() {
            return academyField.text;
        }


        public void SetSchoolField(string _school) {
            schoolField.text = _school;
        }
        public string GetSchoolField() {
            return schoolField.text;
        }


        public void SetTierField(string _tier) {
            tierField.text = _tier;
        }
        public string GetTierField() {
            return tierField.text;
        }


        public void ShowColumn(ParticipantBasicInfo column, bool show) {
            switch (column) {
                case ParticipantBasicInfo.Country:
                    countryField.transform.parent.gameObject.SetActive(show);
                    break;
                case ParticipantBasicInfo.Rank:
                    rankField.transform.parent.gameObject.SetActive(show);
                    break;
                case ParticipantBasicInfo.Styles:
                    stylesField.transform.parent.gameObject.SetActive(show);
                    break;
                case ParticipantBasicInfo.Academy:
                    academyField.transform.parent.gameObject.SetActive(show);
                    break;
                case ParticipantBasicInfo.School:
                    schoolField.transform.parent.gameObject.SetActive(show);
                    break;
                case ParticipantBasicInfo.Tier:
                    tierField.transform.parent.gameObject.SetActive(show);
                    break;
                case ParticipantBasicInfo.Surname:
                case ParticipantBasicInfo.Name:
                default:
                    break;
            }
        }
    }
}
