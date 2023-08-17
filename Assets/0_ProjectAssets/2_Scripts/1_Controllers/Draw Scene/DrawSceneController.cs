
using System;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.GeneralApp.Controller.UI.Popups;
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

        private MenuPopupController menuPopup;

        private GameManager _gameManager;

        #region Mono
        private void OnEnable() {
            DrawPanelViewEvents.OnStartButtonClicked += StartButtonPressed;
            DrawPanelViewEvents.OnNextButtonClicked += RevealNewParticipant;
            DrawPanelViewEvents.OnSaveButtonClicked += SaveDataPressed;

            DrawPanelViewEvents.OnSettingsButtonClicked += ShowMenu;
        }


        private void Update() {
            if (Input.GetKeyUp(KeyCode.Escape)) {
                // WHEN USER PRESS (UP) ESCAPE KEY
                if (_gameManager.BaseUIController.PopupShowing().Equals("")) { // If user has NO popup active...
                    ShowMenu();             // ... show the menu
                } else if (_gameManager.BaseUIController.PopupShowing().Equals("SettingsPopup")) {       // ... if not AND user is at settings popup...
                    SettingsBackToMenu();   // ... return to menu
                } else if (_gameManager.BaseUIController.PopupShowing().Equals("Menu")) {   // ... if not AND menu is active...
                    CloseMenu();            // ... close the menu
                } // ... in any other case, don't do anything
            }
        }

        private void OnDisable() {
            DrawPanelViewEvents.OnStartButtonClicked -= StartButtonPressed;
            DrawPanelViewEvents.OnNextButtonClicked -= RevealNewParticipant;
            DrawPanelViewEvents.OnSaveButtonClicked -= SaveDataPressed;

            DrawPanelViewEvents.OnSettingsButtonClicked -= ShowMenu;
        }
        #endregion

        public void Init() {
            _gameManager = GameManager.Instance;
            _config = _gameManager.Config;

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
                SeedPopupData seedPopupData = new SeedPopupData("SeedSelector",
                    CloseSeedSelector, ChangeSeedAndStart);

                _gameManager.BaseUIController.ShowPopup(seedPopupData);
            }
        }

        private void CloseSeedSelector() {
            _view.SwitchDrawPhaseView(DrawSceneView.DrawScenePhaseView.Start);
            _gameManager.BaseUIController.HidePopup("SeedSelector");
        }

        private void ChangeSeedAndStart(int newSeed) {
            _config.Seed = newSeed;

            participantSelector = BaseSelector.GetBaseSelector(_config.ParticipantSelection);
            participantSelector.InitializeSelector(_config.Participants, newSeed);

            _gameManager.BaseUIController.HidePopup("SeedSelector");
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
            SaveDataPopupData saveDataPopupData = new SaveDataPopupData("SaveData", CloseSaveDataPopup);
            _gameManager.BaseUIController.ShowPopup(saveDataPopupData);
        }

        private void CloseSaveDataPopup() {
            _gameManager.BaseUIController.HidePopup("SaveData");
        }
        #endregion

        [ContextMenu("Show Menu")]
        private void ShowMenu() {
            MenuPopupData menuPopupData = new MenuPopupData("Menu",
                CloseMenu, SettingsMenuClicked, ExitMenuClicked);

            _gameManager.BaseUIController.ShowPopup(menuPopupData);
        }

        private void CloseMenu() {
            _gameManager.BaseUIController.HidePopup("Menu");
        }

        private void SettingsMenuClicked() {
            SettingsPopupData settingsPopupData = new SettingsPopupData("SettingsPopup", SettingsBackToMenu);
            _gameManager.BaseUIController.ShowPopup(settingsPopupData);
        }

        private void SettingsBackToMenu() {
            _gameManager.BaseUIController.HidePopup("SettingsPopup");
        }

        private void ExitMenuClicked() {
            ExitPopupData exitPopupData = new ExitPopupData("AskExit",
                true, CloseAskExit, Exit);

            _gameManager.BaseUIController.ShowPopup(exitPopupData);
        }

        private void CloseAskExit() {
            _gameManager.BaseUIController.HidePopup("AskExit");
        }

        private void Exit(bool saveBefore) {
            _gameManager.SaveAndExit(saveBefore);
        }

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
