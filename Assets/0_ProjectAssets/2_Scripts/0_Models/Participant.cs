using System.Collections.Generic;
using UnityEngine;

namespace YannickSCF.TournamentDraw.Model {
    [System.Serializable]
    public class Participant {
        [SerializeField] private Countries country;

        [SerializeField] private string name;
        [SerializeField] private string surname;

        [SerializeField] private Ranks rank;
        [SerializeField] private List<Styles> styles;

        [SerializeField] private string schoolName;
        [SerializeField] private string academyName;

        [SerializeField] private int tierLevel;

        public Countries Country { get => country; }
        public string Name { get => name; }
        public string Surname { get => surname; }
        public Ranks Rank { get => rank; }
        public List<Styles> Styles { get => styles; }
        public string SchoolName { get => schoolName; }
        public string AcademyName { get => academyName; }
        public int TierLevel { get => tierLevel; }

        public Participant(
            Countries country,
            string name,
            string surname,
            Ranks rank,
            List<Styles> styles,
            string schoolName,
            string academyName,
            int tierLevel = 0) {
            this.country = country;

            this.name = name;
            this.surname = surname;

            this.rank = rank;
            this.styles = styles;

            this.schoolName = schoolName;
            this.academyName = academyName;
            this.tierLevel = tierLevel;
        }
    }
}
