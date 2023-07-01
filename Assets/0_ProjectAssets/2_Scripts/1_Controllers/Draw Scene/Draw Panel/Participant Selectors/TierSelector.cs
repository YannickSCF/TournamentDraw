using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.TournamentDraw.Models;

namespace YannickSCF.TournamentDraw.Controllers.Draw.ParticipantSelectors {
    public class TierSelector : BaseSelector {

        private Dictionary<int, Queue<ParticipantModel>> allQueues;
        private int maxTierLevel = 0;

        public override void InitializeSelector(List<ParticipantModel> allParticipants, int seed) {
            base.InitializeSelector(allParticipants, seed);

            Dictionary<int, List<ParticipantModel>> allLists = SeparateByTier(allParticipants);

            allQueues = new Dictionary<int, Queue<ParticipantModel>>();
            foreach (KeyValuePair<int, List<ParticipantModel>> list in allLists) {
                List<ParticipantModel> toEnqueue = ShuffleParticipants(list.Value);

                Queue<ParticipantModel> enqueuedParticipants = new Queue<ParticipantModel>();
                foreach (ParticipantModel participant in toEnqueue) {
                    enqueuedParticipants.Enqueue(participant);
                }

                allQueues.Add(toEnqueue[0].TierLevel, enqueuedParticipants);
            }
        }

        private Dictionary<int, List<ParticipantModel>> SeparateByTier(List<ParticipantModel> allParticipants) {
            Dictionary<int, List<ParticipantModel>> res = new Dictionary<int, List<ParticipantModel>>();

            foreach (ParticipantModel participant in allParticipants) {
                if (!res.ContainsKey(participant.TierLevel)) {
                    List<ParticipantModel> newQueue = new List<ParticipantModel>();
                    newQueue.Add(participant);
                    res.Add(participant.TierLevel, newQueue);
                } else {
                    res.GetValueOrDefault(participant.TierLevel).Add(participant);
                }

                if (participant.TierLevel > maxTierLevel) {
                    maxTierLevel = participant.TierLevel;
                }
            }

            return res;
        }

        public override ParticipantModel GetNextParticipant() {
            Queue<ParticipantModel> correctQueue = GetCorrectQueue();
            if (correctQueue != null) {
                return correctQueue.Dequeue();
            } else {
                Debug.LogError("There are no more participants in queue!");
                return null;
            }
        }

        public override bool IsAnyParticipantToReveal() {
            bool res = false;
            foreach (KeyValuePair<int, Queue<ParticipantModel>> queue in allQueues) {
                res |= queue.Value.Count > 0;
            }
            return res;
        }

        private Queue<ParticipantModel> GetCorrectQueue() {
            for (int i = 0; i <= maxTierLevel; ++i) {
                if (allQueues.ContainsKey(i) &&
                    allQueues.GetValueOrDefault(i).Count > 0) {
                    return allQueues.GetValueOrDefault(i);
                }
            }

            return null;
        }
    }
}
