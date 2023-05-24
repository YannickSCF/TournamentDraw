using System.Collections.Generic;

namespace YannickSCF.TournamentDraw.Models.Configurator {
    [System.Serializable]
    public class DrawConfigurationModel {
        private string drawName;

        private List<ParticipantModel> participants;

        private int numberOfPoules;
        private int maxPouleSize;

        private PouleAssignType pouleAssign;
        private ParticipantSelectionType participantSelection;

        private bool[] participantInfoSelected = (bool[])AppConstants.ParticipantInfoDefault.Clone();
        
        private int seed;

        public string DrawName { get => drawName; set => drawName = value; }
        public List<ParticipantModel> Participants { get => participants; set => participants = value; }
        public int NumberOfPoules { get => numberOfPoules; set => numberOfPoules = value; }
        public int MaxPouleSize { get => maxPouleSize; set => maxPouleSize = value; }
        public PouleAssignType PouleAssign { get => pouleAssign; set => pouleAssign = value; }
        public ParticipantSelectionType ParticipantSelection { get => participantSelection; set => participantSelection = value; }
        public bool[] ParticipantInfoSelected { get => participantInfoSelected; set => participantInfoSelected = value; }
        public int Seed { get => seed; set => seed = value; }
    }
}
