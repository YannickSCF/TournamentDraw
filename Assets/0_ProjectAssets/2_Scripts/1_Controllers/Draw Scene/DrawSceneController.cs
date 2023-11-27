
// Dependencies
using System.Collections.Generic;
using UnityEngine;
// Custom dependencies
using YannickSCF.LSTournaments.Common;
using YannickSCF.LSTournaments.Common.Controllers;
using YannickSCF.LSTournaments.Common.Models.Athletes;
using YannickSCF.LSTournaments.Common.Models.Poules;
using YannickSCF.LSTournaments.Common.Scriptables.Data;
using YannickSCF.LSTournaments.Common.Tools.Poule;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Popups;
using YannickSCF.TournamentDraw.Views.DrawScene;
using YannickSCF.TournamentDraw.Views.DrawScene.Events;

namespace YannickSCF.TournamentDraw.Controllers.DrawScene {
    public class DrawSceneController : MonoBehaviour {
        // TODO: Change place (?) To a script related with draw settings
        public enum AthleteRevealOrder { NextInNextPoule, NextInPoule, NextInAllPoules, CompletePoule, Custom };

        [SerializeField] private DrawSceneView _view;
        [SerializeField] private SpriteRenderer _backgroundImage;

        private TournamentData _data;

        private List<PouleDataModel> _allPoules;

        [SerializeField] private AthleteRevealOrder _cRevealOrder = AthleteRevealOrder.NextInNextPoule;

        private int _cPouleIndex = -1;
        private int _cAthleteIndex = -1;
        private bool _isDrawAlreadyStarted = false;
        private bool _isDrawFinished = false;

        private GameManager _gameManager;

        public AthleteRevealOrder CRevealOrder { get => _cRevealOrder; set => _cRevealOrder = value; }

        #region Mono
        private void Awake() {
            _allPoules = new List<PouleDataModel>();
        }

        private void OnEnable() {
            DrawPanelViewEvents.OnStartButtonClicked += StartButtonPressed;
            DrawPanelViewEvents.OnNextButtonClicked += OnNextButtonClicked;
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
            DrawPanelViewEvents.OnNextButtonClicked -= OnNextButtonClicked;
            DrawPanelViewEvents.OnSaveButtonClicked -= SaveDataPressed;

            DrawPanelViewEvents.OnSettingsButtonClicked -= ShowMenu;
        }
        #endregion

        public void Init() {
            _gameManager = GameManager.Instance;
            _data = DataManager.Instance.AppData;

            int participantsAlreadyRevealed = InitPouleModels();

            if (participantsAlreadyRevealed > 0) {
                _isDrawAlreadyStarted = true;

                Randomizer.SetSeed(_data.Seed);
                _allPoules = PouleUtils.CreatePoules(_data.GetNamingData().Value, _data.Athletes, _data.FillerTypeInfo, _data.FillerSubtypeInfo, _data.GetPouleMaxSize());
            }

            _view.Init(_data.TournamentName, _data.GetPouleCount(), _data.GetPouleMaxSize(), participantsAlreadyRevealed);

            if (participantsAlreadyRevealed > 0) {
                for (int i = 0; i < participantsAlreadyRevealed; ++i) {
                    RevealNewParticipant(true);
                }
            }
        }

        private int InitPouleModels() {
            if (_data.Poules.Count <= 0) {
                _allPoules = new List<PouleDataModel>();
                List<string> names = PouleUtils.GetPoulesNames(_data.GetNamingData().Value);
                for (int i = 0; i < names.Count; ++i) {
                    _allPoules.Add(new PouleDataModel(names[i]));
                }

                _data.Poules = _allPoules;
                return 0;
            } else {
                _allPoules = _data.Poules;
            }

            int athletesAlreadyRevealed = 0;
            _data.Poules.ForEach(x => athletesAlreadyRevealed += x.AthletesIds.Count);
            // TODO: Review if this is needed
            if (athletesAlreadyRevealed > 0) {
                _allPoules.ForEach(x => x.AthletesIds.Clear());
                _data.Poules.ForEach(x => x.AthletesIds.Clear());
            }

            return athletesAlreadyRevealed;
        }

        #region Event listeners methods
        private void StartButtonPressed() {
            _view.SwitchDrawPhaseView(DrawSceneView.DrawScenePhaseView.OnGoing);

            if (!_isDrawAlreadyStarted) {
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
            _data.Seed = newSeed;
            Randomizer.SetSeed(_data.Seed);

            _allPoules = PouleUtils.CreatePoules(_data.GetNamingData().Value, _data.Athletes, _data.FillerTypeInfo, _data.FillerSubtypeInfo, _data.GetPouleMaxSize());

            _gameManager.BaseUIController.HidePopup("SeedSelector");
        }

        private void OnNextButtonClicked() {
            RevealNewParticipant(false);
        }

        private void RevealNewParticipant(bool revealMuted) {
            // TODO: Add full name as a setting
            // TODO: Add show academy as a setting
            switch (_cRevealOrder) {
                case AthleteRevealOrder.Custom: RevealCustom(revealMuted); break;
                case AthleteRevealOrder.CompletePoule: RevealCompletePoule(revealMuted); break;
                case AthleteRevealOrder.NextInAllPoules: RevealNextInAllPoules(revealMuted); break;
                case AthleteRevealOrder.NextInPoule: RevealNextInPoule(revealMuted); break;
                case AthleteRevealOrder.NextInNextPoule:
                default: RevealNextInNextPoule(revealMuted); break;
            }

            if (_isDrawFinished) {
                _view.SwitchDrawPhaseView(DrawSceneView.DrawScenePhaseView.Finished);
            }

            _gameManager.SaveDrawData();
        }
        #region RevealNewParticipant methods
        private void RevealCustom(bool revealMuted) {
            // TODO: Add Custom reveal option
            RevealNextInNextPoule(revealMuted);
        }
        private void RevealCompletePoule(bool revealMuted) {
            ++_cPouleIndex;

            foreach (string athleteId in _allPoules[_cPouleIndex].AthletesIds) {
                RevealAthlete(_data.GetAthleteById(athleteId), revealMuted);
            }

            CheckIfDrawIsFinished();
        }
        private void RevealNextInAllPoules(bool revealMuted) {
            _cPouleIndex = 0;
            ++_cAthleteIndex;

            foreach (PouleDataModel pouleData in _allPoules) {
                if (_cAthleteIndex >= 0 && _cAthleteIndex < pouleData.AthletesIds.Count) {
                    RevealAthlete(_data.GetAthleteById(pouleData.AthletesIds[_cAthleteIndex]), revealMuted);
                }
                ++_cPouleIndex;
            }

            CheckIfDrawIsFinished();
        }
        private void RevealNextInPoule(bool revealMuted) {
            if (_cPouleIndex < 0 || _cPouleIndex >= _allPoules.Count) _cPouleIndex = 0;
            ++_cAthleteIndex;

            if (_cAthleteIndex >= _allPoules[_cPouleIndex].AthletesIds.Count) {
                _cAthleteIndex = 0;
                ++_cPouleIndex;
            }

            RevealAthlete(_data.GetAthleteById(_allPoules[_cPouleIndex].AthletesIds[_cAthleteIndex]), revealMuted);

            CheckIfDrawIsFinished();
        }
        private void RevealNextInNextPoule(bool revealMuted) {
            ++_cPouleIndex;
            if (_cPouleIndex >= _allPoules.Count) {
                _cPouleIndex = 0;
                ++_cAthleteIndex;
            }

            if (_cAthleteIndex < 0 || _cAthleteIndex >= _allPoules[_cPouleIndex].AthletesIds.Count) _cAthleteIndex = 0;
            RevealAthlete(_data.GetAthleteById(_allPoules[_cPouleIndex].AthletesIds[_cAthleteIndex]), revealMuted);

            CheckIfDrawIsFinished();
        }

        private void CheckIfDrawIsFinished() {
            int athletesAlreadyRevealed = 0;
            _data.Poules.ForEach(x => athletesAlreadyRevealed += x.AthletesIds.Count);

            _isDrawFinished = athletesAlreadyRevealed >= _data.PouleCountAndSizes[0, 0] * _data.PouleCountAndSizes[0, 1] + _data.PouleCountAndSizes[1, 0] * _data.PouleCountAndSizes[1, 1];
        }
        #endregion

        private void RevealAthlete(AthleteInfoModel athlete, bool revealMuted) {
            _view.AddParticipantToPoule(athlete.GetFullName(), athlete.Academy,
                _cPouleIndex, revealMuted);

            _data.Poules[_cPouleIndex].AthletesIds.Add(athlete.Id);
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
            _gameManager.SaveSettingsData();
            _gameManager.BaseUIController.HidePopup("SettingsPopup");
        }

        private void ExitMenuClicked() {
            ExitPopupData exitPopupData = new ExitPopupData("AskExit",
                false, CloseAskExit, Exit);

            _gameManager.BaseUIController.ShowPopup(exitPopupData);
        }
        private void CloseAskExit() {
            _gameManager.BaseUIController.HidePopup("AskExit");
        }
        private void Exit(bool saveBefore) {
            _gameManager.SaveAndExit(saveBefore);
        }
    }
}
