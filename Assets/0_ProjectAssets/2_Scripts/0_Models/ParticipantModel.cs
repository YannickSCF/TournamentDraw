using System;
using System.Collections.Generic;
using UnityEngine;

namespace YannickSCF.TournamentDraw.Models {
    [System.Serializable]
    public class ParticipantModel {
        [SerializeField] private string country;

        [SerializeField] private string name;
        [SerializeField] private string surname;

        [SerializeField] private Ranks rank;
        [SerializeField] private List<Styles> styles;

        [SerializeField] private string schoolName;
        [SerializeField] private string academyName;

        [SerializeField] private int tierLevel;

        public string Country { get => country; set => country = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public Ranks Rank { get => rank; set => rank = value; }
        public List<Styles> Styles { get => styles; set => styles = value; }
        public string SchoolName { get => schoolName; set => schoolName = value; }
        public string AcademyName { get => academyName; set => academyName = value; }
        public int TierLevel { get => tierLevel; set => tierLevel = value; }

        public string FullName { get => Surname + ", " + Name; }

        public ParticipantModel() { }

        public ParticipantModel(
            string country,
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
