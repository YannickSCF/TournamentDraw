using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Custom dependencies
using YannickSCF.TournamentDraw.Models;

namespace YannickSCF.TournamentDraw.Scriptables {
    [CreateAssetMenu(fileName = "Draw Configuration", menuName = "Scriptable Objects/YannickSCF/Tournament Draw/Configuration", order = 0)]
    public class DrawConfiguration : ScriptableObject {
        [SerializeField] private string drawName;

        [Header("PARTICIPANTS DATA")]
        [SerializeField] private List<ParticipantModel> participants = new List<ParticipantModel>();
        [SerializeField] private List<PouleModel> poules = new List<PouleModel>();

        [SerializeField] private bool[] participantInfoSelected = (bool[])AppConstants.ParticipantInfoDefault.Clone();

        [Header("DRAW SETTINGS")]
        [SerializeField] private int numberOfPoules;
        [SerializeField] private int maxPouleSize;

        [SerializeField] private PouleAssignType pouleAssign;
        [SerializeField] private ParticipantSelectionType participantSelection;

        [SerializeField] private int seed;

        #region Getters/Setters
        public string DrawName { get => drawName; set => drawName = value; }
        public List<ParticipantModel> Participants { get => participants; set => participants = value; }
        public List<PouleModel> Poules { get => poules; set => poules = value; }
        public int NumberOfPoules { get => numberOfPoules; set => numberOfPoules = value; }
        public int MaxPouleSize { get => maxPouleSize; set => maxPouleSize = value; }
        public PouleAssignType PouleAssign { get => pouleAssign; set => pouleAssign = value; }
        public ParticipantSelectionType ParticipantSelection { get => participantSelection; set => participantSelection = value; }
        public bool[] ParticipantInfoSelected { get => participantInfoSelected; set => participantInfoSelected = value; }
        public int Seed { get => seed; set => seed = value; }
        #endregion

        public void ResetConfiguration() {
            drawName = string.Empty;

            participants.Clear();
            poules.Clear();

            numberOfPoules = 0;
            maxPouleSize = 0;

            pouleAssign = PouleAssignType.OneByOne;
            participantSelection = ParticipantSelectionType.Random;

            participantInfoSelected = (bool[])AppConstants.ParticipantInfoDefault.Clone();

            seed = 0;
        }
    }
}
