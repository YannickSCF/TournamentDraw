using System.Collections.Generic;
using System.IO;
using UnityEngine;
using YannickSCF.TournamentDraw.Models;
using YannickSCF.TournamentDraw.Scriptables;

namespace YannickSCF.TournamentDraw.Importers {
    public class JSONDeserializer : IDeserializer {

        public List<PouleModel> GetPoulesFromFile(string path) {
            throw new System.NotImplementedException();
        }

        public List<ParticipantModel> GetParticipantsFromFile(string path) {
            string jsonText = File.ReadAllText(path);

            List<ParticipantModel> participants = JsonUtility.FromJson<List<ParticipantModel>>(jsonText);

            if (participants.Count == 0) return null;
            else return participants;
        }

        public DrawConfiguration ImportDrawFormJSON(string filePath) {
            string jsonText = File.ReadAllText(filePath);
            DrawConfiguration drawConfig = JsonUtility.FromJson<DrawConfiguration>(jsonText);
            return drawConfig;
        }
    }
}
