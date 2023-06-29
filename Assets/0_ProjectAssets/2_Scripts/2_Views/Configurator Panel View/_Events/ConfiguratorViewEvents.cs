// Custom dependencies
using static YannickSCF.TournamentDraw.CommonEventsDelegates;

namespace YannickSCF.TournamentDraw.Views.Configurator.Events {
    public static class ConfiguratorViewEvents {

        // ------------------------- Specific Delegates -------------------------

        public delegate void ParticipantDataEvent(ParticipantBasicInfo infoType, string dataUpdated, int participantIndex);
        public delegate void ParticipantInfoCheckboxEvent(ParticipantBasicInfo checkboxInfo, bool isChecked);

        // ------------------------------- Events -------------------------------

        #region --------------- Participants panel events ---------------
        public static event SimpleEvent OnLoadParticipantsFromFile;
        public static void ThrowOnLoadParticipantsFromFile() {
            OnLoadParticipantsFromFile?.Invoke();
        }

        public static event SimpleEvent OnParticipantAdded;
        public static void ThrowOnParticipantAdded() {
            OnParticipantAdded?.Invoke();
        }

        public static event SimpleEvent OnParticipantRemoved;
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
        public static event StringEvent OnDrawNameChanged;
        public static void ThrowOnDrawNameChanged(string newDrawName) {
            OnDrawNameChanged?.Invoke(newDrawName);
        }

        public static event StringEvent OnNumberOfPoulesChanged;
        public static void ThrowOnNumberOfPoulesChanged(string newNumberOfPoules) {
            OnNumberOfPoulesChanged?.Invoke(newNumberOfPoules);
        }

        public static event StringEvent OnMaxPouleSizeChanged;
        public static void ThrowOnMaxPouleSizeChanged(string newMaxPouleSize) {
            OnMaxPouleSizeChanged?.Invoke(newMaxPouleSize);
        }

        public static event IntegerEvent OnPouleAssignChanged;
        public static void ThrowOnPouleAssignChanged(int indexSelected) {
            OnPouleAssignChanged?.Invoke(indexSelected);
        }

        public static event IntegerEvent OnParticipantSelectionChanged;
        public static void ThrowOnParticipantSelectionChanged(int indexSelected) {
            OnParticipantSelectionChanged?.Invoke(indexSelected);
        }

        public static event ParticipantInfoCheckboxEvent OnParticipantInfoCheckboxToggle;
        public static void ThrowOnParticipantInfoCheckboxToggle(ParticipantBasicInfo checkboxInfo, bool isChecked) {
            OnParticipantInfoCheckboxToggle?.Invoke(checkboxInfo, isChecked);
        }
        #endregion

        #region --------------- Confirmation panel events ---------------
        public static event StringEvent OnOpenConfirmationPanel;
        public static void ThrowOpenConfirmationPanel(string _confirmationStr) {
            OnOpenConfirmationPanel?.Invoke(_confirmationStr);
        }

        public static event BooleanEvent OnConfirmationPanelAction;
        public static void ThrowConfirmationPanelAction(bool _clickedYes) {
            OnConfirmationPanelAction?.Invoke(_clickedYes);
        }

        public static event SimpleEvent OnCloseConfirmationPanel;
        public static void ThrowCloseConfirmationPanel() {
            OnCloseConfirmationPanel?.Invoke();
        }

        public static event StringEvent OnConditionsToContinueChanged;
        public static void ThrowOnConditionsToContinueChanged(string conditionsToContinue) {
            OnConditionsToContinueChanged?.Invoke(conditionsToContinue);
        }
        #endregion

        // --------------- EXITED ---------------

        public static event SimpleEvent OnConfiguratorExited;
        public static void ThrowConfiguratorExited() {
            OnConfiguratorExited?.Invoke();
        }

        public static event SimpleEvent OnConfiguratorFinished;
        public static void ThrowConfiguratorFinished() {
            OnConfiguratorFinished?.Invoke();
        }
    }
}
