using System.Collections;
using System.Collections.Generic;

namespace YannickSCF.TournamentDraw.Models.Configurator {
    public class DrawConfiguration {
        private string drawName;

        private List<Participant> participants;

        private int numberOfPoules;
        private int maxPouleSize;

        private PouleAssignType pouleAssign;
        private ParticipantSelectionType participantSelection;

        private bool[] participantInfoSelected;

        public string DrawName { get => drawName; set => drawName = value; }
        public List<Participant> Participants { get => participants; set => participants = value; }
        public int NumberOfPoules { get => numberOfPoules; set => numberOfPoules = value; }
        public int MaxPouleSize { get => maxPouleSize; set => maxPouleSize = value; }
        public PouleAssignType PouleAssign { get => pouleAssign; set => pouleAssign = value; }
        public ParticipantSelectionType ParticipantSelection { get => participantSelection; set => participantSelection = value; }
        public bool[] ParticipantInfoSelected { get => participantInfoSelected; set => participantInfoSelected = value; }
    }
}
