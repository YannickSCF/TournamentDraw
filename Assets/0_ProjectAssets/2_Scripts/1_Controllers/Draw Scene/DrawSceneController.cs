
using System;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.TournamentDraw.Controllers.Draw.ParticipantSelectors;
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
        private List<PouleModel> allPoules;
        private int c_pouleIndex = -1;

        #region Mono
        private void OnEnable() {
            DrawPanelViewEvents.OnStartButtonClicked += ShowSeedSelector;
            DrawPanelViewEvents.OnNextButtonClicked += RevealNewParticipant;
        }

        private void OnDisable() {
            DrawPanelViewEvents.OnStartButtonClicked -= ShowSeedSelector;
            DrawPanelViewEvents.OnNextButtonClicked -= RevealNewParticipant;
        }
        #endregion

        public void Init() {
            _config = GameManager.Instance.Config;

            _view.Init(_config.DrawName, _config.NumberOfPoules, _config.MaxPouleSize);

            allPoules = new List<PouleModel>();
            for (int i = 0; i < _config.NumberOfPoules; ++i) {
                allPoules.Add(new PouleModel((i + 1).ToString()));
            }

            // TODO Move to popup functionality
            participantSelector = BaseSelector.GetBaseSelector(_config.ParticipantSelection);
            participantSelector.InitializeSelector(_config.Participants, _config.Seed);
        }

        #region Event listeners methods
        private void ShowSeedSelector() {
            _view.SetStartPressed();

            SeedSelectorController seedSelector = GameManager.Instance.BaseUIController.ShowPopup<SeedSelectorController, SeedSelectorView>("SeedSelector");
            seedSelector.SetCallbacks(ChangeSeedAndStart);

            _view.ShowPoules();
        }

        private void ChangeSeedAndStart(int newSeed) {
            _config.Seed = newSeed;

            participantSelector = BaseSelector.GetBaseSelector(_config.ParticipantSelection);
            participantSelector.InitializeSelector(_config.Participants, newSeed);

            GameManager.Instance.BaseUIController.ClosePopup<SeedSelectorController, SeedSelectorView>("SeedSelector");

            _view.StartDraw();
        }

        private void RevealNewParticipant() {
            ParticipantModel revealedParticipant = participantSelector.GetNextParticipant();

            int pouleIndex = GetPouleIndex();
            _view.AddParticipantToPoule(
                revealedParticipant.FullName,
                revealedParticipant.AcademyName,
                pouleIndex);

            allPoules[pouleIndex].PouleParticipants.Add(revealedParticipant);
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
