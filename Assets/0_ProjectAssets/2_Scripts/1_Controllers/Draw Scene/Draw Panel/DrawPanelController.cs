using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YannickSCF.TournamentDraw.Controllers.Draw.ParticipantSelectors;
using YannickSCF.TournamentDraw.Models;
using YannickSCF.TournamentDraw.Scriptables;
using YannickSCF.TournamentDraw.Views.Draw.Panel;
using YannickSCF.TournamentDraw.Views.Draw.Events;

namespace YannickSCF.TournamentDraw.Controllers.Draw {
    public class DrawPanelController : MonoBehaviour {

        [SerializeField] private DrawPanelView drawPanelView;

        private DrawConfiguration _config;

        private BaseSelector participantSelector;

        private List<PouleModel> allPoules;
        private int c_pouleIndex = -1;

        #region Mono
        private void OnEnable() {
            DrawPanelViewEvents.OnNextButtonClicked += RevealNewParticipant;
        }

        private void OnDisable() {
            DrawPanelViewEvents.OnNextButtonClicked -= RevealNewParticipant;
        }
        #endregion

        public void Init(DrawConfiguration configuration) {
            _config = configuration;
            drawPanelView.Init(_config);

            participantSelector = BaseSelector.GetBaseSelector(_config.ParticipantSelection);
            participantSelector.InitializeSelector(_config.Participants, _config.Seed);

            allPoules = new List<PouleModel>();
            for (int i = 0; i < _config.NumberOfPoules; ++i) {
                allPoules.Add(new PouleModel((i + 1).ToString()));
            }
        }

        private void RevealNewParticipant() {
            ParticipantModel revealedParticipant = participantSelector.GetNextParticipant();

            int pouleIndex = GetPouleIndex();
            drawPanelView.AddParticipantToPoule(
                revealedParticipant.FullName,
                revealedParticipant.AcademyName,
                pouleIndex);

            allPoules[pouleIndex].PouleParticipants.Add(revealedParticipant);
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
