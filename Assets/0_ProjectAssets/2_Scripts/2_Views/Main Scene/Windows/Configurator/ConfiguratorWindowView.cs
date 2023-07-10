using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.GeneralApp.View.UI.Windows;
using YannickSCF.TournamentDraw.Scriptables;
using YannickSCF.TournamentDraw.Views.Configurator.DrawOptions;
using YannickSCF.TournamentDraw.Views.Configurator.Events;
using YannickSCF.TournamentDraw.Views.Configurator.ParticipantList;

namespace YannickSCF.TournamentDraw.Views.MainScene.Configurator {
    public class ConfiguratorWindowView : WindowView {

        [SerializeField] private Animator _animator;

        [Header("Configurator sub-views")]
        [SerializeField] private ParticipantsListView participantsPanel;
        [SerializeField] private DrawOptionsView drawOptionsPanel;

        [Header("Configurator control values")]
        [SerializeField] private Button closeButton;
        [SerializeField] private Button endButton;
        [SerializeField] private TextMeshProUGUI errorsPreview;

        private DrawConfiguration _configuration;

        #region Mono
        private void OnEnable() {
            closeButton.onClick.AddListener(() => ConfiguratorViewEvents.ThrowConfiguratorExited());
            endButton.onClick.AddListener(() => ConfiguratorViewEvents.ThrowConfiguratorFinished());
        }

        private void OnDisable() {
            closeButton.onClick.RemoveAllListeners();
            endButton.onClick.RemoveAllListeners();
        }
        #endregion

        public void InitData(DrawConfiguration configuration) {
            _configuration = configuration;

            participantsPanel.Init(_configuration);
            drawOptionsPanel.SetParticipantInfoSelected(_configuration.ParticipantInfoSelected);
        }

        public override void Open() {
            base.Open();
            _animator.SetBool("Show", true);

            // Set view related with info selected checkboxes
            participantsPanel.SetTableInfoSelectedColumns(AppConstants.ParticipantInfoDefault);
            drawOptionsPanel.SetParticipantInfoSelected(AppConstants.ParticipantInfoDefault);
        }

        public override void Show() {
            base.Show();
            _animator.SetBool("Show", true);

            // Set view related with info selected checkboxes
            participantsPanel.SetTableInfoSelectedColumns(AppConstants.ParticipantInfoDefault);
            drawOptionsPanel.SetParticipantInfoSelected(AppConstants.ParticipantInfoDefault);
        }

        public override void Hide() {
            _animator.SetBool("Show", false);
            StartCoroutine(WaitToAnimationsEnds(base.Hide));
        }

        public override void Close() {
            _animator.SetBool("Show", false);
            StartCoroutine(WaitToAnimationsEnds(base.Close));
        }

        private IEnumerator WaitToAnimationsEnds(Action actionToDo) {
            yield return new WaitUntil(() => _animator.GetCurrentAnimatorStateInfo(0).IsName("configurator_hidden"));
            participantsPanel.ResetParticipantsPanel();
            drawOptionsPanel.ResetDrawOptions();
            actionToDo?.Invoke();
        }

        public void UpdateErrors(List<string> errorsList) {
            if (errorsList.Count > 0) {
                endButton.interactable = false;
                errorsPreview.text = errorsList[0];
            } else {
                endButton.interactable = true;
                errorsPreview.text = LocalizationKeys.ALL_READY;
            }
        }

        #region Getters
        public string GetDrawName() {
            return drawOptionsPanel.GetTournamentDrawName();
        }

        public int GetNumberOfPoules() {
            return drawOptionsPanel.GetNumberOfPoules();
        }

        public int GetMaxPouleSize() {
            return drawOptionsPanel.GetMaxPouleSize();
        }
        #endregion

        #region Setters
        public void SetFilePath(string filePath) {
            participantsPanel.SetFilePathText(filePath);
        }

        public void SetTotalParticipantsText(string newValue) {
            drawOptionsPanel.SetTotalParticipantsText(newValue);
        }

        public void ResetAllTable() {
            participantsPanel.CleanTable();
        }

        public void LoadParticipantOnTable(string country, string surname, string name,
            Ranks rank, List<Styles> styles, string academy, string school, int tier) {

            participantsPanel.LoadParticipantOnTable(country, surname, name,
                rank, styles, academy, school, tier);
        }
        #endregion
    }
}
