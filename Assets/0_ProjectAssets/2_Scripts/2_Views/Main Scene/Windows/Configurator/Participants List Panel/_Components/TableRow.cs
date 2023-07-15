using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
// Custom dependencies
using YannickSCF.TournamentDraw.Views.Configurator.Events;
using YannickSCF.TournamentDraw.Views.Configurator.ParticipantList.Components.InputType.AutoCompleteCountry;
using YannickSCF.TournamentDraw.Views.Configurator.ParticipantList.Components.InputType.StyleDropdown;

namespace YannickSCF.TournamentDraw.Views.Configurator.ParticipantList.Components {
    public class TableRow : MonoBehaviour {

        [SerializeField] private AutoCompleteInputField countryField;
        [SerializeField] private TMP_InputField surnameField;
        [SerializeField] private TMP_InputField nameField;
        [SerializeField] private TMP_Dropdown rankField;
        [SerializeField] private StylesDropdown stylesField;
        [SerializeField] private TMP_InputField academyField;
        [SerializeField] private TMP_InputField schoolField;
        [SerializeField] private TMP_InputField tierField;

        private int rowIndex;

        #region Mono
        private void OnEnable() {
            countryField.OnFinalValueSetted += CountryFieldChanged;

            surnameField.onValueChanged.AddListener(SurnameFieldChanged);
            nameField.onValueChanged.AddListener(NameFieldChanged);
            rankField.onValueChanged.AddListener(RankFieldChanged);

            stylesField.OnStylesSetted += StylesFieldChanged;

            academyField.onValueChanged.AddListener(AcademyFieldChanged);
            schoolField.onValueChanged.AddListener(SchoolFieldChanged);
            tierField.onValueChanged.AddListener(TierFieldChanged);
        }

        private void OnDisable() {
            countryField.OnFinalValueSetted -= CountryFieldChanged;

            surnameField.onValueChanged.RemoveAllListeners();
            nameField.onValueChanged.RemoveAllListeners();
            rankField.onValueChanged.RemoveAllListeners();

            stylesField.OnStylesSetted -= StylesFieldChanged;

            academyField.onValueChanged.RemoveAllListeners();
            schoolField.onValueChanged.RemoveAllListeners();
            tierField.onValueChanged.RemoveAllListeners();
        }
        #endregion

        #region Properties
        public int RowIndex { get => rowIndex; set => rowIndex = value; }
        #endregion

        #region GETTERS
        public string GetCountryField() { return countryField?.GetValue(); }
        public string GetSurnameField() { return surnameField.text; }
        public string GetNameField() { return nameField.text; }
        public Ranks GetRankField() {
            return (Ranks)Enum.Parse(typeof(Ranks), rankField.captionText.text);
        }
        public List<Styles> GetStylesField() { return stylesField.GetValues(); }
        public string GetAcademyField() { return academyField.text; }
        public string GetSchoolField() { return schoolField.text; }
        public string GetTierField() { return tierField.text; }
        #endregion

        #region SETTERS
        public void SetCountryField(string _countryCode, bool withoutNotify = false) {
            countryField?.SetInitValue(_countryCode, withoutNotify);
        }
        public void SetSurnameField(string _surname, bool withoutNotify = false) {
            if (!withoutNotify) {
                surnameField.text = _surname;
            } else {
                surnameField.SetTextWithoutNotify(_surname);
            }
        }
        public void SetNameField(string _name, bool withoutNotify = false) {
            if (!withoutNotify) {
                nameField.text = _name;
            } else {
                nameField.SetTextWithoutNotify(_name);
            }
        }
        public void SetRankField(string _rankTxt, bool withoutNotify = false) {
            if (!withoutNotify) {
                rankField.value = rankField.options.FindIndex(x => x.text == _rankTxt);
            } else {
                rankField.SetValueWithoutNotify(rankField.options.FindIndex(x => x.text == _rankTxt));
            }
        }
        public void SetStylesField(List<Styles> _styles, bool withoutNotify = false) {
            if (stylesField != null) {
                stylesField.SetInitValues(_styles, withoutNotify);
            }
        }
        public void SetAcademyField(string _academy, bool withoutNotify = false) {
            if (!withoutNotify) {
                academyField.text = _academy;
            } else {
                academyField.SetTextWithoutNotify(_academy);
            }
        }
        public void SetSchoolField(string _school, bool withoutNotify = false) {
            if (!withoutNotify) {
                schoolField.text = _school;
            } else {
                schoolField.SetTextWithoutNotify(_school);
            }
        }
        public void SetTierField(string _tier, bool withoutNotify = false) {
            if (!withoutNotify) {
                tierField.text = _tier;
            } else {
                tierField.SetTextWithoutNotify(_tier);
            }
        }
        #endregion

        #region Event listeners methods
        private void CountryFieldChanged(string finalValue) {
            ConfiguratorViewEvents.ThrowOnParticipantDataUpdated(
                ParticipantBasicInfo.Country,
                finalValue, rowIndex);
        }

        private void SurnameFieldChanged(string strValue) {
            ConfiguratorViewEvents.ThrowOnParticipantDataUpdated(
                ParticipantBasicInfo.Surname,
                strValue, rowIndex);
        }

        private void NameFieldChanged(string strValue) {
            ConfiguratorViewEvents.ThrowOnParticipantDataUpdated(
                ParticipantBasicInfo.Name,
                strValue, rowIndex);
        }

        private void RankFieldChanged(int intValue) {
            ConfiguratorViewEvents.ThrowOnParticipantDataUpdated(
                ParticipantBasicInfo.Rank,
                intValue.ToString(), rowIndex);
        }

        private void StylesFieldChanged(List<Styles> styles) {
            string stylesInList = string.Empty;

            foreach(Styles style in styles) {
                stylesInList += (int)style + ",";
            }

            ConfiguratorViewEvents.ThrowOnParticipantDataUpdated(
                ParticipantBasicInfo.Styles,
                stylesInList, rowIndex);
        }

        private void AcademyFieldChanged(string strValue) {
            ConfiguratorViewEvents.ThrowOnParticipantDataUpdated(
                ParticipantBasicInfo.Academy,
                strValue, rowIndex);
        }

        private void SchoolFieldChanged(string strValue) {
            ConfiguratorViewEvents.ThrowOnParticipantDataUpdated(
                ParticipantBasicInfo.School,
                strValue, rowIndex);
        }

        private void TierFieldChanged(string strValue) {
            ConfiguratorViewEvents.ThrowOnParticipantDataUpdated(
                ParticipantBasicInfo.Tier,
                strValue, rowIndex);
        }
        #endregion

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
                    surnameField.transform.parent.gameObject.SetActive(show);
                    break;
                case ParticipantBasicInfo.Name:
                    nameField.transform.parent.gameObject.SetActive(show);
                    break;
                default:
                    Debug.LogWarning("ShowColumn: Column not added on Table Row Script");
                    break;
            }
        }
    }
}
