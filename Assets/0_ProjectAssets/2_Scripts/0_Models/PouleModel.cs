using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YannickSCF.TournamentDraw.Models {
    [System.Serializable]
    public class PouleModel {
        [SerializeField] private string pouleName;

        [SerializeField] private List<ParticipantModel> pouleParticipants;

        public PouleModel(string _pouleName, int pouleParticipantCapacity) {
            pouleName = _pouleName;
            pouleParticipants = new List<ParticipantModel>(pouleParticipantCapacity);
        }

        public PouleModel(string _pouleName, List<ParticipantModel> _pouleParticipants) {
            pouleName = _pouleName;

            pouleParticipants = _pouleParticipants;
        }

        public string PouleName { get => pouleName; }
        public List<ParticipantModel> PouleParticipants { get => pouleParticipants; }

    }
}
