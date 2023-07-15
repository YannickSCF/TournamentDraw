// Custom dependencies
using static YannickSCF.GeneralApp.CommonEventsDelegates;

namespace YannickSCF.TournamentDraw.Views.Configurator.Events {
    public static class ConfiguratorViewEvents {

        // ------------------------- Specific Delegates -------------------------

        public delegate void ParticipantDataEvent(ParticipantBasicInfo infoType, string dataUpdated, int participantIndex);
        public delegate void ParticipantInfoCheckboxEvent(ParticipantBasicInfo checkboxInfo, bool isChecked);

        // ------------------------------- Events -------------------------------

        #region --------------- Participants panel events ---------------
        public static event SimpleEventDelegate OnLoadParticipantsFromFile;
        public static void ThrowOnLoadParticipantsFromFile() {
            OnLoadParticipantsFromFile?.Invoke();
        }

        public static event SimpleEventDelegate OnParticipantAdded;
        public static void ThrowOnParticipantAdded() {
            OnParticipantAdded?.Invoke();
        }

        public static event SimpleEventDelegate OnParticipantRemoved;
        public static void ThrowOnParticipantRemoved() {
            OnParticipantRemoved?.Invoke();
        }

        public static event ParticipantDataEvent OnParticipantDataUpdated;
        public static void ThrowOnParticipantDataUpdated(
            ParticipantBasicInfo infoType, string dataUpdated, int participantIndex) {
            OnParticipantDataUpdated?.Invoke(infoType, dataUpdated, participantIndex);
        }
        #endregion

        #region ------------------ Draw options events ------------------
        public static event StringEventDelegate OnDrawNameChanged;
        public static void ThrowOnDrawNameChanged(string newDrawName) {
            OnDrawNameChanged?.Invoke(newDrawName);
        }

        public static event StringEventDelegate OnNumberOfPoulesChanged;
        public static void ThrowOnNumberOfPoulesChanged(string newNumberOfPoules) {
            OnNumberOfPoulesChanged?.Invoke(newNumberOfPoules);
        }

        public static event StringEventDelegate OnMaxPouleSizeChanged;
        public static void ThrowOnMaxPouleSizeChanged(string newMaxPouleSize) {
            OnMaxPouleSizeChanged?.Invoke(newMaxPouleSize);
        }

        public static event IntegerEventDelegate OnPouleAssignChanged;
        public static void ThrowOnPouleAssignChanged(int indexSelected) {
            OnPouleAssignChanged?.Invoke(indexSelected);
        }

        public static event IntegerEventDelegate OnParticipantSelectionChanged;
        public static void ThrowOnParticipantSelectionChanged(int indexSelected) {
            OnParticipantSelectionChanged?.Invoke(indexSelected);
        }

        public static event ParticipantInfoCheckboxEvent OnParticipantInfoCheckboxToggle;
        public static void ThrowOnParticipantInfoCheckboxToggle(ParticipantBasicInfo checkboxInfo, bool isChecked) {
            OnParticipantInfoCheckboxToggle?.Invoke(checkboxInfo, isChecked);
        }
        #endregion

        #region --------------- Confirmation panel events ---------------
        public static event StringEventDelegate OnOpenConfirmationPanel;
        public static void ThrowOpenConfirmationPanel(string _confirmationStr) {
            OnOpenConfirmationPanel?.Invoke(_confirmationStr);
        }

        public static event BooleanEventDelegate OnConfirmationPanelAction;
        public static void ThrowConfirmationPanelAction(bool _clickedYes) {
            OnConfirmationPanelAction?.Invoke(_clickedYes);
        }

        public static event SimpleEventDelegate OnCloseConfirmationPanel;
        public static void ThrowCloseConfirmationPanel() {
            OnCloseConfirmationPanel?.Invoke();
        }

        public static event StringEventDelegate OnConditionsToContinueChanged;
        public static void ThrowOnConditionsToContinueChanged(string conditionsToContinue) {
            OnConditionsToContinueChanged?.Invoke(conditionsToContinue);
        }
        #endregion

        // --------------- EXITED ---------------

        public static event SimpleEventDelegate OnConfiguratorExited;
        public static void ThrowConfiguratorExited() {
            OnConfiguratorExited?.Invoke();
        }

        public static event SimpleEventDelegate OnConfiguratorFinished;
        public static void ThrowConfiguratorFinished() {
            OnConfiguratorFinished?.Invoke();
        }
    }
}
