using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YannickSCF.TournamentDraw.Models;

namespace YannickSCF.TournamentDraw.Controllers.Draw.ParticipantSelectors {
    public class RandomSelector : BaseSelector {

        private Queue<ParticipantModel> participantQueue;

        public override void InitializeSelector(List<ParticipantModel> allParticipants, int seed) {
            base.InitializeSelector(allParticipants, seed);

            allParticipants = ShuffleParticipants(allParticipants);

            participantQueue = new Queue<ParticipantModel>();
            foreach (ParticipantModel participant in allParticipants) {
                participantQueue.Enqueue(participant);
            }
        }

        public override ParticipantModel GetNextParticipant() {
            if (participantQueue.Count > 0) {
                return participantQueue.Dequeue();
            } else {
                Debug.LogError("There are no more participants in queue!");
                return null;
            }
        }
    }
}
