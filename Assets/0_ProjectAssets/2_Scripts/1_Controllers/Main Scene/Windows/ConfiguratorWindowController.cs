using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.GeneralApp.Controller.UI.Windows;
using YannickSCF.TournamentDraw.Importers;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Models;
using YannickSCF.TournamentDraw.Scriptables;
using YannickSCF.TournamentDraw.Views.Configurator.Events;

namespace YannickSCF.TournamentDraw.Views.MainScene.Configurator {

    public class ConfiguratorWindowController : WindowController<ConfiguratorWindowView> {

        private DrawConfiguration _config;

        private List<string> errorsList = new List<string>();
        private int numberOfParticipantsError = 0;

        private Action _onCloseAction = null;
        private Action _onFinishAction = null;

        #region Mono
        protected override void OnEnable() {
            base.OnEnable();

            ConfiguratorViewEvents.OnLoadParticipantsFromFile += LoadParticipantsFromFile;
            ConfiguratorViewEvents.OnParticipantAdded += ParticipantAdded;
            ConfiguratorViewEvents.OnParticipantRemoved += ParticipantRemoved;
            ConfiguratorViewEvents.OnParticipantDataUpdated += ParticipantDataUpdated;

            ConfiguratorViewEvents.OnDrawNameChanged += DrawNameChanged;

            ConfiguratorViewEvents.OnNumberOfPoulesChanged += NumberOfPoulesChanged;
            ConfiguratorViewEvents.OnMaxPouleSizeChanged += MaxPouleSizeChanged;

            ConfiguratorViewEvents.OnPouleAssignChanged += PouleAssignChanged;
            ConfiguratorViewEvents.OnParticipantSelectionChanged += ParticipantSelectionChanged;

            ConfiguratorViewEvents.OnParticipantInfoCheckboxToggle += ParticipantInfoCheckboxToggle;

            ConfiguratorViewEvents.OnConfiguratorExited += CloseAction;
            ConfiguratorViewEvents.OnConfiguratorFinished += FinishAction;
        }

        protected override void OnDisable() {
            base.OnDisable();

            ConfiguratorViewEvents.OnLoadParticipantsFromFile -= LoadParticipantsFromFile;
            ConfiguratorViewEvents.OnParticipantAdded -= ParticipantAdded;
            ConfiguratorViewEvents.OnParticipantRemoved -= ParticipantRemoved;
            ConfiguratorViewEvents.OnParticipantDataUpdated -= ParticipantDataUpdated;

            ConfiguratorViewEvents.OnDrawNameChanged -= DrawNameChanged;

            ConfiguratorViewEvents.OnNumberOfPoulesChanged -= NumberOfPoulesChanged;
            ConfiguratorViewEvents.OnMaxPouleSizeChanged -= MaxPouleSizeChanged;

            ConfiguratorViewEvents.OnPouleAssignChanged -= PouleAssignChanged;
            ConfiguratorViewEvents.OnParticipantSelectionChanged -= ParticipantSelectionChanged;

            ConfiguratorViewEvents.OnParticipantInfoCheckboxToggle -= ParticipantInfoCheckboxToggle;

            ConfiguratorViewEvents.OnConfiguratorExited -= CloseAction;
            ConfiguratorViewEvents.OnConfiguratorFinished -= FinishAction;
        }
        #endregion

        public override void Init(string windowId) {
            base.Init(windowId);

            _config = GameManager.Instance.Config;
            View.InitData(_config);
        }

        public void SetAllCallback(Action closeAction, Action finishAction) {
            _onCloseAction = closeAction;
            _onFinishAction = finishAction;
        }

        private void CloseAction() {
            _onCloseAction?.Invoke();
        }
        private void FinishAction() {
            _onFinishAction?.Invoke();
        }

        #region Event listeners methods
        private void LoadParticipantsFromFile() {
            string filePath = FileImporter.SelectFileWithBrowser();
            if (!string.IsNullOrEmpty(filePath)) {
                // Get all participants
                List<ParticipantModel> participants = FileImporter.ImportParticipantsFromFile(filePath);
                if (participants.Count > 0) {
                    View.SetFilePath(filePath);
                    // Fulfill table (reseting it first)
                    View.ResetAllTable();
                    _config.Participants.Clear();
                    foreach (ParticipantModel participant in participants) {
                        View.LoadParticipantOnTable(participant.Country, participant.Surname,
                            participant.Name, participant.Rank, participant.Styles,
                            participant.AcademyName, participant.SchoolName,
                            participant.TierLevel);

                        _config.Participants.Add(new ParticipantModel(participant.Country,
                            participant.Name, participant.Surname, participant.Rank,
                            participant.Styles, participant.AcademyName, participant.SchoolName,
                            participant.TierLevel));
                    }
                }
            }

            View.SetTotalParticipantsText(_config.Participants.Count.ToString());
            AllChecks();
        }

        private void ParticipantAdded() {
            _config.Participants.Add(new ParticipantModel());

            CheckMinParticipants();
            CheckNamesAndSurnames();

            View.SetTotalParticipantsText(_config.Participants.Count.ToString());
        }

        private void ParticipantRemoved() {
            _config.Participants.RemoveAt(_config.Participants.Count - 1);

            CheckMinParticipants();
            CheckNamesAndSurnames();

            View.SetTotalParticipantsText(_config.Participants.Count.ToString());
        }

        private void ParticipantDataUpdated(
            ParticipantBasicInfo infoType, string dataUpdated, int participantIndex) {

            switch (infoType) {
                case ParticipantBasicInfo.Country:
                    _config.Participants[participantIndex].Country = dataUpdated;
                    break;
                case ParticipantBasicInfo.Surname:
                    _config.Participants[participantIndex].Surname = dataUpdated;
                    CheckNamesAndSurnames();
                    break;
                case ParticipantBasicInfo.Name:
                    _config.Participants[participantIndex].Name = dataUpdated;
                    CheckNamesAndSurnames();
                    break;
                case ParticipantBasicInfo.Rank:
                    Ranks newRank = (Ranks)int.Parse(dataUpdated);
                    _config.Participants[participantIndex].Rank = newRank;
                    break;
                case ParticipantBasicInfo.Styles:
                    List<Styles> newStyles = new List<Styles>();
                    string[] dataSeparated = dataUpdated.Split(",");
                    foreach (string styleIntStr in dataSeparated) {
                        Styles newStyle = (Styles)int.Parse(styleIntStr);
                        newStyles.Add(newStyle);
                    }
                    _config.Participants[participantIndex].Styles = newStyles;
                    break;
                case ParticipantBasicInfo.Academy:
                    _config.Participants[participantIndex].AcademyName = dataUpdated;
                    break;
                case ParticipantBasicInfo.School:
                    _config.Participants[participantIndex].SchoolName = dataUpdated;
                    break;
                case ParticipantBasicInfo.Tier:
                    _config.Participants[participantIndex].TierLevel = int.Parse(dataUpdated);
                    break;
                default:
                    Debug.LogWarning("ParticipantDataUpdated: Participant Basic Info NOT found!");
                    break;
            }
        }

        private void DrawNameChanged(string strValue) {
            if (CheckDrawName(strValue)) {
                _config.DrawName = strValue;
            }
        }

        private void NumberOfPoulesChanged(string strValue) {
            CheckNumberOfPoules(strValue, out int numberOfPoules);

            _config.NumberOfPoules = numberOfPoules;
            CheckPoulesAndParticipants();
        }

        private void MaxPouleSizeChanged(string strValue) {
            CheckMaxPouleSize(strValue, out int maxPouleSize);

            _config.MaxPouleSize = maxPouleSize;
            CheckPoulesAndParticipants();
        }

        private void PouleAssignChanged(int indexSelection) {
            if (indexSelection < 0 ||
                indexSelection > Enum.GetValues(typeof(PouleAssignType)).Length) {
                Debug.LogError("PouleAssignChanged wrong! -> " + indexSelection);
                return;
            }

            _config.PouleAssign = (PouleAssignType)indexSelection;
        }

        private void ParticipantSelectionChanged(int indexSelection) {
            if (indexSelection < 0 ||
                indexSelection > Enum.GetValues(typeof(ParticipantSelectionType)).Length) {
                Debug.LogError("ParticipantSelectionChanged wrong! -> " + indexSelection);
                return;
            }

            _config.ParticipantSelection = (ParticipantSelectionType)indexSelection;
        }

        private void ParticipantInfoCheckboxToggle(ParticipantBasicInfo checkboxInfo, bool isChecked) {
            _config.ParticipantInfoSelected[(int)checkboxInfo] = isChecked;
        }
        #endregion

        #region CHECKERS
        private void AllChecks() {
            CheckMinParticipants();

            CheckNamesAndSurnames();

            CheckDrawName();

            CheckNumberOfPoules();
            CheckMaxPouleSize();
            CheckPoulesAndParticipants();
        }

        private bool CheckMinParticipants() {
            bool res = false;
            if (_config.Participants.Count < 4) {
                AddErrorToList(LocalizationKeys.MIN_OF_PARTICIPANTS);
            } else {
                RemoveErrorFromList(LocalizationKeys.MIN_OF_PARTICIPANTS);
                res = true;
            }

            View.UpdateErrors(errorsList);
            return res;
        }

        private bool CheckNamesAndSurnames() {
            bool res = false;
            numberOfParticipantsError = 0;

            foreach (ParticipantModel participant in _config.Participants) {
                if (string.IsNullOrEmpty(participant.Surname) || string.IsNullOrEmpty(participant.Name)) {
                    ++numberOfParticipantsError;
                }
            }

            if (numberOfParticipantsError > 0) {
                AddErrorToList(LocalizationKeys.X_PARTICIPANT_INCOMPLETE_NAME);
            } else {
                RemoveErrorFromList(LocalizationKeys.X_PARTICIPANT_INCOMPLETE_NAME);
                res = true;
            }

            View.UpdateErrors(errorsList);
            return res;
        }

        private bool CheckDrawName(string newDrawName) {
            bool res = false;
            if (string.IsNullOrEmpty(newDrawName)) {
                AddErrorToList(LocalizationKeys.DRAW_NAME_NEEDED);
            } else {
                RemoveErrorFromList(LocalizationKeys.DRAW_NAME_NEEDED);
                res = true;
            }

            View.UpdateErrors(errorsList);
            return res;
        }
        private bool CheckDrawName() {
            return CheckDrawName(View.GetDrawName());
        }

        private bool CheckNumberOfPoules(string newNumberOfPoulesStr, out int numberOfPoules) {
            bool res = false;
            if (string.IsNullOrEmpty(newNumberOfPoulesStr)) {
                newNumberOfPoulesStr = "0";
            }

            if (!int.TryParse(newNumberOfPoulesStr, out numberOfPoules)) {
                AddErrorToList(LocalizationKeys.INCORRECT_VALUE_FOR_INPUT);
            } else {
                if (numberOfPoules <= 0) {
                    AddErrorToList(LocalizationKeys.NUMBER_OF_POULES_NEEDED);
                } else {
                    RemoveErrorFromList(LocalizationKeys.NUMBER_OF_POULES_NEEDED);
                    res = true;
                }
            }

            View.UpdateErrors(errorsList);
            return res;
        }
        private bool CheckNumberOfPoules() {
            return CheckNumberOfPoules(View.GetNumberOfPoules().ToString(), out int emptyAux);
        }

        private bool CheckMaxPouleSize(string newMaxPouleSizeStr, out int maxPouleSize) {
            bool res = false;
            if (string.IsNullOrEmpty(newMaxPouleSizeStr)) {
                newMaxPouleSizeStr = "0";
            }

            if (!int.TryParse(newMaxPouleSizeStr, out maxPouleSize)) {
                AddErrorToList(LocalizationKeys.INCORRECT_VALUE_FOR_INPUT);
            } else {
                if (maxPouleSize <= 0) {
                    AddErrorToList(LocalizationKeys.MAX_POULE_SIZE_NEEDED);
                } else {
                    RemoveErrorFromList(LocalizationKeys.MAX_POULE_SIZE_NEEDED);
                    res = true;
                }
            }

            View.UpdateErrors(errorsList);
            return res;
        }
        private bool CheckMaxPouleSize() {
            return CheckMaxPouleSize(View.GetMaxPouleSize().ToString(), out int emptyAux);
        }

        private bool CheckPoulesAndParticipants() {
            bool res = false;
            int numberOfParticipants = _config.Participants.Count;
            int numberOfPoules = _config.NumberOfPoules;
            int maxPouleSize = _config.MaxPouleSize;

            if (numberOfPoules > 0 && maxPouleSize > 0) {
                int minTournamentSizeByPoules = numberOfPoules == 1 ?
                    maxPouleSize : (numberOfPoules * (maxPouleSize - 1)) + 1;
                int maxTournamentSizeByPoules = numberOfPoules * maxPouleSize;

                if (minTournamentSizeByPoules == maxTournamentSizeByPoules) {
                    if (numberOfParticipants != minTournamentSizeByPoules) {
                        AddErrorToList(LocalizationKeys.INVALID_POULES_EQUAL);
                    } else {
                        RemoveErrorFromList(LocalizationKeys.INVALID_POULES_EQUAL);
                        res = true;
                    }
                } else {
                    if (numberOfParticipants < minTournamentSizeByPoules) {
                        AddErrorToList(LocalizationKeys.INVALID_POULES_MIN);
                    } else {
                        RemoveErrorFromList(LocalizationKeys.INVALID_POULES_MIN);
                        res = true;
                    }

                    if (numberOfParticipants > maxTournamentSizeByPoules) {
                        AddErrorToList(LocalizationKeys.INVALID_POULES_MAX);
                        res &= false;
                    } else {
                        RemoveErrorFromList(LocalizationKeys.INVALID_POULES_MAX);
                        res &= true;
                    }
                }
            } else {
                RemoveErrorFromList(LocalizationKeys.INVALID_POULES_EQUAL);
                RemoveErrorFromList(LocalizationKeys.INVALID_POULES_MIN);
                RemoveErrorFromList(LocalizationKeys.INVALID_POULES_MAX);
            }

            View.UpdateErrors(errorsList);
            return res;
        }

        private void AddErrorToList(string errorKeyMsg) {
            if (!errorsList.Contains(errorKeyMsg)) {
                errorsList.Add(errorKeyMsg);
            }
        }
        private void RemoveErrorFromList(string errorKeyMsg) {
            if (errorsList.Contains(errorKeyMsg)) {
                errorsList.Remove(errorKeyMsg);
            }
        }
        #endregion
    }
}
