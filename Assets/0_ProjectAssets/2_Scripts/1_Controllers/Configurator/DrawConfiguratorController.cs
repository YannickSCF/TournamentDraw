using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.TournamentDraw.Importers;
using YannickSCF.TournamentDraw.Models;
// Custom Dependencies
using YannickSCF.TournamentDraw.Models.Configurator;
using YannickSCF.TournamentDraw.Views.Configurator;
using YannickSCF.TournamentDraw.Views.Configurator.Events;

namespace YannickSCF.TournamentDraw.Controllers.Configurator {

    public class DrawConfiguratorController : MonoBehaviour {

        [SerializeField] private DrawConfiguratorView view;

        private DrawConfiguration configuration;

        private List<string> errorsList = new List<string>();
        private int numberOfParticipantsError = 0;

        #region Mono
        private void OnEnable() {
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
        }

        private void OnDisable() {
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
        }
        #endregion

        #region Event listeners methods
        private void LoadParticipantsFromFile() {
            string filePath = FileImporter.SelectFileWithBrowser();
            if (!string.IsNullOrEmpty(filePath)) {
                // Get all participants
                List<Participant> participants = FileImporter.ImportParticipantsFromFile(filePath);
                if (participants.Count > 0) {
                    view.SetFilePath(filePath);
                    // Fulfill table (reseting it first)
                    view.ResetAllTable();
                    foreach (Participant participant in participants) {
                        view.LoadParticipantOnTable(participant.Country, participant.Surname,
                            participant.Name, participant.Rank, participant.Styles,
                            participant.AcademyName, participant.SchoolName,
                            participant.TierLevel);
                    }
                }
            }
        }

        private void ParticipantAdded() {
            configuration.Participants.Add(new Participant());

            CheckMinParticipants();
            UpdateErrorsList();
        }

        private void ParticipantRemoved() {
            configuration.Participants.RemoveAt(configuration.Participants.Count - 1);

            CheckMinParticipants();
            UpdateErrorsList();
        }

        private void ParticipantDataUpdated(
            ParticipantBasicInfo infoType, string dataUpdated, int participantIndex) {


            CheckNamesAndSurnames();

            UpdateErrorsList();
        }

        private void DrawNameChanged(string strValue) {
            if (CheckDrawName(strValue)) {
                configuration.DrawName = strValue;
            }

            UpdateErrorsList();
        }

        private void NumberOfPoulesChanged(string strValue) {
            CheckNumberOfPoules(strValue, out int numberOfPoules);

            configuration.NumberOfPoules = numberOfPoules;
            UpdateErrorsList();
        }

        private void MaxPouleSizeChanged(string strValue) {
            CheckMaxPouleSize(strValue, out int maxPouleSize);

            configuration.MaxPouleSize = maxPouleSize;
            UpdateErrorsList();
        }

        private void PouleAssignChanged(int indexSelection) {
            if (indexSelection < 0 ||
                indexSelection > Enum.GetValues(typeof(PouleAssignType)).Length) {
                Debug.LogError("PouleAssignChanged wrong! -> " + indexSelection);
                return;
            }

            configuration.PouleAssign = (PouleAssignType)indexSelection;
        }

        private void ParticipantSelectionChanged(int indexSelection) {
            if (indexSelection < 0 ||
                indexSelection > Enum.GetValues(typeof(ParticipantSelectionType)).Length) {
                Debug.LogError("ParticipantSelectionChanged wrong! -> " + indexSelection);
                return;
            }

            configuration.ParticipantSelection = (ParticipantSelectionType)indexSelection;
        }

        private void ParticipantInfoCheckboxToggle(ParticipantBasicInfo checkboxInfo, bool isChecked) {
            configuration.ParticipantInfoSelected[(int)checkboxInfo] = isChecked;
        }
        #endregion

        private void UpdateErrorsList() {
            view.UpdateErrors(errorsList);
        }

        #region CHECKERS
        private bool CheckMinParticipants() {
            bool res = false;
            if (configuration.Participants.Count < 4) {
                AddErrorToList(LocalizationKeys.MIN_OF_PARTICIPANTS);
                res = true;
            } else {
                RemoveErrorFromList(LocalizationKeys.MIN_OF_PARTICIPANTS);
            }

            UpdateErrorsList();
            return res;
        }

        private bool CheckNamesAndSurnames() {
            bool res = false;
            numberOfParticipantsError = 0;

            foreach(Participant participant in configuration.Participants) {
                if (string.IsNullOrEmpty(participant.Surname) || string.IsNullOrEmpty(participant.Name)) {
                    ++numberOfParticipantsError;
                }
            }

            if (numberOfParticipantsError > 0) {
                AddErrorToList(LocalizationKeys.X_PARTICIPANT_INCOMPLETE_NAME);
                res = true;
            } else {
                RemoveErrorFromList(LocalizationKeys.X_PARTICIPANT_INCOMPLETE_NAME);
            }

            UpdateErrorsList();
            return res;
        }

        private bool CheckDrawName(string newDrawName) {
            if (string.IsNullOrEmpty(newDrawName)) {
                AddErrorToList(LocalizationKeys.DRAW_NAME_NEEDED);
                return true;
            } else {
                RemoveErrorFromList(LocalizationKeys.DRAW_NAME_NEEDED);
                return false;
            }
        }

        private bool CheckNumberOfPoules(string newNumberOfPoulesStr, out int numberOfPoules) {
            if (string.IsNullOrEmpty(newNumberOfPoulesStr)) {
                numberOfPoules = 0;
                return false;
            }

            if (!int.TryParse(newNumberOfPoulesStr, out numberOfPoules)) {
                AddErrorToList(LocalizationKeys.INCORRECT_VALUE_FOR_INPUT);
            } else {
                if (numberOfPoules <= 0) {
                    AddErrorToList(LocalizationKeys.NUMBER_OF_POULES_NEEDED);
                } else {
                    RemoveErrorFromList(LocalizationKeys.NUMBER_OF_POULES_NEEDED);
                }
            }

            return true;
        }

        private bool CheckMaxPouleSize(string newMaxPouleSizeStr, out int maxPouleSize) {
            if (string.IsNullOrEmpty(newMaxPouleSizeStr)) {
                maxPouleSize = 0;
                return false;
            }

            if (!int.TryParse(newMaxPouleSizeStr, out maxPouleSize)) {
                AddErrorToList(LocalizationKeys.INCORRECT_VALUE_FOR_INPUT);
            } else {
                if (maxPouleSize <= 0) {
                    AddErrorToList(LocalizationKeys.MAX_POULE_SIZE_NEEDED);
                } else {
                    RemoveErrorFromList(LocalizationKeys.MAX_POULE_SIZE_NEEDED);
                }
            }

            return true;
        }

        private void CheckPoulesAndParticipants() {
            int numberOfParticipants = configuration.Participants.Count;
            if (configuration.Participants != null && configuration.Participants.Count <= 4) {
                return;
            }

            int numberOfPoules = configuration.NumberOfPoules;
            int maxPouleSize = configuration.MaxPouleSize;

            if (numberOfPoules > 0 && maxPouleSize > 0) {
                int minTournamentSizeByPoules = (numberOfPoules * (maxPouleSize - 1)) + 1;
                int maxTournamentSizeByPoules = numberOfPoules * maxPouleSize;

                if (numberOfParticipants < minTournamentSizeByPoules) {
                    AddErrorToList(LocalizationKeys.INVALID_POULES_MIN);
                } else {
                    RemoveErrorFromList(LocalizationKeys.INVALID_POULES_MIN);
                }

                if (numberOfParticipants > maxTournamentSizeByPoules) {
                    AddErrorToList(LocalizationKeys.INVALID_POULES_MAX);
                } else {
                    RemoveErrorFromList(LocalizationKeys.INVALID_POULES_MAX);
                }
            }
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
