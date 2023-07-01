using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YannickSCF.TournamentDraw.Models;

namespace YannickSCF.TournamentDraw.Controllers.Draw.ParticipantSelectors {
    public abstract class BaseSelector {

        protected System.Random rand;

        public virtual void InitializeSelector(List<ParticipantModel> allParticipants, int seed) {
            rand = new System.Random(seed);
        }

        public abstract ParticipantModel GetNextParticipant();
        public abstract bool IsAnyParticipantToReveal();

        protected List<ParticipantModel> ShuffleParticipants(List<ParticipantModel> participantsToSort) {
            participantsToSort = participantsToSort.OrderBy(x => x.FullName).ToList();

            int n = participantsToSort.Count;
            while (n > 1) {
                n--;
                int k = rand.Next(n + 1);
                ParticipantModel value = participantsToSort[k];
                participantsToSort[k] = participantsToSort[n];
                participantsToSort[n] = value;
            }

            return participantsToSort;
        }

        public static BaseSelector GetBaseSelector(ParticipantSelectionType selectionType) {

            switch (selectionType) {
                case ParticipantSelectionType.Random:
                    return new RandomSelector();
                case ParticipantSelectionType.ByLevel:
                    return new TierSelector();
                case ParticipantSelectionType.ByRank:
                    return new RankSelector();
            }

            return null;
        }
    }
}
