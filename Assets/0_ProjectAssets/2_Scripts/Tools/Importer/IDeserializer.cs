using System.Collections.Generic;
using YannickSCF.TournamentDraw.Models;

namespace YannickSCF.TournamentDraw.Importers {
    public interface IDeserializer {
        public List<PouleModel> GetPoulesFromFile(string path);
        public List<ParticipantModel> GetParticipantsFromFile(string path);
    }
}
