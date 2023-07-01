using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.TournamentDraw.Models;

namespace YannickSCF.TournamentDraw.Controllers.Draw.ParticipantSelectors {
    public class RankSelector : BaseSelector {

        private Dictionary<Ranks, Queue<ParticipantModel>> allQueues;

        public override void InitializeSelector(List<ParticipantModel> allParticipants, int seed) {
            base.InitializeSelector(allParticipants, seed);

            Dictionary<Ranks, List<ParticipantModel>> allLists = SeparateByRanks(allParticipants);

            allQueues = new Dictionary<Ranks, Queue<ParticipantModel>>();
            foreach (KeyValuePair<Ranks, List<ParticipantModel>> list in allLists) {
                List<ParticipantModel> toEnqueue = ShuffleParticipants(list.Value);

                Queue<ParticipantModel> enqueuedParticipants = new Queue<ParticipantModel>();
                foreach (ParticipantModel participant in toEnqueue) {
                    enqueuedParticipants.Enqueue(participant);
                }

                allQueues.Add(toEnqueue[0].Rank, enqueuedParticipants);
            }
        }

        private Dictionary<Ranks, List<ParticipantModel>> SeparateByRanks(List<ParticipantModel> allParticipants) {
            Dictionary<Ranks, List<ParticipantModel>> res = new Dictionary<Ranks, List<ParticipantModel>>();

            foreach (ParticipantModel participant in allParticipants) {
                if (!res.ContainsKey(participant.Rank)) {
                    List<ParticipantModel> newQueue = new List<ParticipantModel>();
                    newQueue.Add(participant);
                    res.Add(participant.Rank, newQueue);
                } else {
                    res.GetValueOrDefault(participant.Rank).Add(participant);
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
            foreach (KeyValuePair<Ranks, Queue<ParticipantModel>> queue in allQueues) {
                res |= queue.Value.Count > 0;
            }
            return res;
        }

        private Queue<ParticipantModel> GetCorrectQueue() {
            Array allRanks = Enum.GetValues(typeof(Ranks));
            for (int i = allRanks.Length - 1; i >= 0; --i) {
                if (allQueues.ContainsKey((Ranks)allRanks.GetValue(i)) &&
                    allQueues.GetValueOrDefault((Ranks)allRanks.GetValue(i)).Count > 0) {
                    return allQueues.GetValueOrDefault((Ranks)allRanks.GetValue(i));
                }
            }

            return null;
        }
    }
}
