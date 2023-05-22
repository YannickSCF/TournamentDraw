using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YannickSCF.TournamentDraw.Models;

namespace YannickSCF.TournamentDraw.Importers {
    public class JSONDeserializer : IDeserializer {

        public List<Poule> GetPoulesFromFile(string path) {
            throw new System.NotImplementedException();
        }

        public List<Participant> GetParticipantsFromFile(string path) {
            string jsonText = File.ReadAllText(path);

            List<Participant> participants = JsonUtility.FromJson<List<Participant>>(jsonText);

            if (participants.Count == 0) return null;
            else return participants;
        }
    }
}
