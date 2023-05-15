using System.Collections;
using System.Collections.Generic;
using TournamentMaker.Settings.View.Componets;
using UnityEngine;
using YannickSCF.TournamentDraw.Model;
using YannickSCF.TournamentDraw.Settings.View.Componets;

namespace YannickSCF.TournamentDraw.Settings.View {
    public static class TournamentSettingsViewEvents {

        // ----------------------------------- Confirmation panel events -----------------------------------

        public delegate void OpenConfirmationPanel(string confirmationStr);
        public static event OpenConfirmationPanel OnOpenConfirmationPanel;
        public static void ThrowOpenConfirmationPanel(string _confirmationStr) {
            OnOpenConfirmationPanel?.Invoke(_confirmationStr);
        }

        public delegate void CloseConfirmationPanel();
        public static event CloseConfirmationPanel OnCloseConfirmationPanel;
        public static void ThrowCloseConfirmationPanel() {
            OnCloseConfirmationPanel?.Invoke();
        }

        public delegate void ConfirmationPanelAction(bool clickedYes);
        public static event ConfirmationPanelAction OnConfirmationPanelAction;
        public static void ThrowConfirmationPanelAction(bool _clickedYes) {
            OnConfirmationPanelAction?.Invoke(_clickedYes);
        }

        // ------------------------------------- Participants panel events -------------------------------------

        public delegate void TournamentParticipantsSetted(List<Participant> participants);
        public static event TournamentParticipantsSetted OnTournamentParticipantsSetted;
        public static void ThrowTournamentParticipantsSetted(List<Participant> _participants) {
            OnTournamentParticipantsSetted?.Invoke(_participants);
        }

        public delegate void TournamentParticipantDataSetted(List<ParticipantBasicInfo> participantData);
        public static event TournamentParticipantDataSetted OnTournamentParticipantDataSetted;
        public static void ThrowTournamentParticipantDataSetted(List<ParticipantBasicInfo> _participantData) {
            OnTournamentParticipantDataSetted?.Invoke(_participantData);
        }

        public delegate void ParticipantPanelStateSetted(bool isClosing);
        public static event ParticipantPanelStateSetted OnParticipantPanelStateSetted;
        public static void ThrowParticipantPanelStateSetted(bool _isClosing) {
            OnParticipantPanelStateSetted?.Invoke(_isClosing);
        }

        public delegate void CheckBoxClicked(ParticipantBasicInfo checkBox, bool isMarked);
        public static event CheckBoxClicked OnCheckBoxClicked;
        public static void ThrowOnCheckBoxClicked(ParticipantBasicInfo _checkBox, bool _isMarked) {
            OnCheckBoxClicked?.Invoke(_checkBox, _isMarked);
        }


        public delegate void MandatoryInputFieldUpdated(bool isFilled);
        public static event MandatoryInputFieldUpdated OnMandatoryInputFieldUpdated;
        public static void ThrowOnMandatoryInputFieldUpdated(bool _isFilled) {
            OnMandatoryInputFieldUpdated?.Invoke(_isFilled);
        }

        // ------------------------------------- General panel events -------------------------------------

        public delegate void TournamentNameSetted(string newName);
        public static event TournamentNameSetted OnTournamentNameSetted;
        public static void ThrowTournamentNameSetted(string _newName) {
            OnTournamentNameSetted?.Invoke(_newName);
        }

        // -------------------------------------- Poules panel events -------------------------------------


        public delegate void TournamentNumSizePoulesSetted(int[,] newNumSizePoules);
        public static event TournamentNumSizePoulesSetted OnTournamentNumSizePoulesSetted;
        public static void ThrowTournamentNumSizePoulesSetted(int[,] _newNumSizePoules) {
            OnTournamentNumSizePoulesSetted?.Invoke(_newNumSizePoules);
        }

        // --------------------------------------------- EXITED -------------------------------------------

        public delegate void TournamentSettingsExited();
        public static event TournamentSettingsExited OnTournamentSettingsExited;
        public static void ThrowTournamentSettingsExited() {
            OnTournamentSettingsExited?.Invoke();
        }

        // --------------------------------------------- ENDING -------------------------------------------

        public delegate void TournamentSettingsEnded();
        public static event TournamentSettingsEnded OnTournamentSettingsEnded;
        public static void ThrowTournamentSettingsEnded() {
            OnTournamentSettingsEnded?.Invoke();
        }
    }
}
