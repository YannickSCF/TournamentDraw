using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YannickSCF.TournamentDraw.Models {
    [System.Serializable]
    public class Poule {
        [SerializeField] private string pouleName;

        [SerializeField] private List<Participant> pouleParticipants;

        public Poule(string _pouleName, int pouleParticipantCapacity) {
            pouleName = _pouleName;
            pouleParticipants = new List<Participant>(pouleParticipantCapacity);
        }

        public Poule(string _pouleName, List<Participant> _pouleParticipants) {
            pouleName = _pouleName;

            pouleParticipants = _pouleParticipants;
        }

        public string PouleName { get => pouleName; }
        public List<Participant> PouleParticipants { get => pouleParticipants; }

    }
}
