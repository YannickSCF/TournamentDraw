
using System;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.TournamentDraw.Controllers.Draw.ParticipantSelectors;
using YannickSCF.TournamentDraw.FileManagement;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Models;
using YannickSCF.TournamentDraw.Popups;
using YannickSCF.TournamentDraw.Scriptables;
using YannickSCF.TournamentDraw.Views.DrawScene;
using YannickSCF.TournamentDraw.Views.DrawScene.Events;

namespace YannickSCF.TournamentDraw.Controllers.DrawScene {
    public class DrawSceneController : MonoBehaviour {

        [SerializeField] private DrawSceneView _view;
        [SerializeField] private SpriteRenderer _backgroundImage;

        private DrawConfiguration _config;

        private BaseSelector participantSelector;
        private List<PouleModel> _allPoules;
        private int c_pouleIndex = -1;
        private bool isDrawAlreadyStarted = false;

        #region Mono
        private void OnEnable() {
            DrawPanelViewEvents.OnStartButtonClicked += StartButtonPressed;
            DrawPanelViewEvents.OnNextButtonClicked += RevealNewParticipant;
            DrawPanelViewEvents.OnSaveButtonClicked += SaveDataPressed;
        }

        private void OnDisable() {
            DrawPanelViewEvents.OnStartButtonClicked -= StartButtonPressed;
            DrawPanelViewEvents.OnNextButtonClicked -= RevealNewParticipant;
            DrawPanelViewEvents.OnSaveButtonClicked -= SaveDataPressed;
        }
        #endregion

        public void Init() {
            _config = GameManager.Instance.Config;

            int participantsAlreadyRevealed = InitPouleModels();

            if (participantsAlreadyRevealed > 0) {
                isDrawAlreadyStarted = true;

                participantSelector = BaseSelector.GetBaseSelector(_config.ParticipantSelection);
                participantSelector.InitializeSelector(_config.Participants, _config.Seed);
            }

            _view.Init(_config.DrawName, _config.NumberOfPoules, _config.MaxPouleSize, participantsAlreadyRevealed);
        }

        private int InitPouleModels() {
            if (_config.Poules.Count <= 0) {
                _allPoules = new List<PouleModel>();
                for (int i = 0; i < _config.NumberOfPoules; ++i) {
                    _allPoules.Add(new PouleModel((i + 1).ToString()));
                }

                _config.Poules = _allPoules;
                return 0;
            } else {
                _allPoules = _config.Poules;
            }

            int participantsAlreadyRevealed = 0;
            foreach (PouleModel poule in _allPoules) {
                participantsAlreadyRevealed += poule.PouleParticipants.Count;
            }

            return participantsAlreadyRevealed;
        }

        #region Event listeners methods
        private void StartButtonPressed() {
            _view.SwitchDrawPhaseView(DrawSceneView.DrawScenePhaseView.OnGoing);

            if (!isDrawAlreadyStarted) {
                SeedSelectorController seedSelector = GameManager.Instance.BaseUIController.ShowPopup<SeedSelectorController, SeedSelectorView>("SeedSelector");
                seedSelector.SetCallbacks(ChangeSeedAndStart, CloseSeedSelector);
            }
        }

        private void ChangeSeedAndStart(int newSeed) {
            _config.Seed = newSeed;

            participantSelector = BaseSelector.GetBaseSelector(_config.ParticipantSelection);
            participantSelector.InitializeSelector(_config.Participants, newSeed);

            GameManager.Instance.BaseUIController.ClosePopup<SeedSelectorController, SeedSelectorView>("SeedSelector");
        }

        private void CloseSeedSelector() {
            _view.SwitchDrawPhaseView(DrawSceneView.DrawScenePhaseView.Start);
            GameManager.Instance.BaseUIController.HidePopup<SeedSelectorController, SeedSelectorView>("SeedSelector");
        }

        private void RevealNewParticipant() {
            ParticipantModel revealedParticipant = participantSelector.GetNextParticipant();

            int pouleIndex = GetPouleIndex();
            _view.AddParticipantToPoule(
                revealedParticipant.FullName,
                revealedParticipant.AcademyName,
                pouleIndex);

            _allPoules[pouleIndex].PouleParticipants.Add(revealedParticipant);

            if (!participantSelector.IsAnyParticipantToReveal()) {
                _view.SwitchDrawPhaseView(DrawSceneView.DrawScenePhaseView.Finished);
            }
        }

        private void SaveDataPressed() {
            SaveDataPopupController saveData = GameManager.Instance.BaseUIController.ShowPopup<SaveDataPopupController, SaveDataPopupView>("SaveData");
            saveData.SetClosePopupCallback(CloseSaveDataPopup);
        }

        private void CloseSaveDataPopup() {
            GameManager.Instance.BaseUIController.ClosePopup<SaveDataPopupController, SaveDataPopupView>("SaveData");
        }
        #endregion

        private int GetPouleIndex() {
            switch (_config.PouleAssign) {
                case PouleAssignType.PouleByPoule:
                    // TODO: Disabled option - Look out when poules have different sizes
                    Debug.LogError(GetType().Name + ": GetPouleIndex (PouleAssignType.PouleByPoule) -> Method not implemented");
                    throw new NotImplementedException();
                case PouleAssignType.Custom:
                    // TODO: Disabled option
                    Debug.LogError(GetType().Name + ": GetPouleIndex (PouleAssignType.Custom) -> Method not implemented");
                    throw new NotImplementedException();
                case PouleAssignType.OneByOne:
                default:
                    ++c_pouleIndex;
                    if (c_pouleIndex >= _config.NumberOfPoules) {
                        c_pouleIndex = 0;
                    }
                    return c_pouleIndex;
            }
        }
    }
}
