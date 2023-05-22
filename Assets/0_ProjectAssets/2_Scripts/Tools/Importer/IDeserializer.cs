using System.Collections.Generic;
using YannickSCF.TournamentDraw.Models;

namespace YannickSCF.TournamentDraw.Importers {
    public interface IDeserializer {
        public List<Poule> GetPoulesFromFile(string path);
        public List<Participant> GetParticipantsFromFile(string path);
    }
}
