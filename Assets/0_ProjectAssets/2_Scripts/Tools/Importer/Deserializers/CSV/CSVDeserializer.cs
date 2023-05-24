using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using YannickSCF.GeneralApp.CountriesData;
using YannickSCF.TournamentDraw.Models;

namespace YannickSCF.TournamentDraw.Importers {
    public class CSVDeserializer : IDeserializer {
        private enum FieldsOrder {
            Name = 0,
            Surnames = 1,
            Rank = 2,
            Styles = 3,
            SchoolName = 4,
            AcademyName = 5,
            Country = 6
        }

        public List<PouleModel> GetPoulesFromFile(string path) {
            throw new NotImplementedException();
        }

        #region Methods to deserialize list of participants
        public List<ParticipantModel> GetParticipantsFromFile(string path) {
            Debug.Log("Participants by CSV: " + path);
            string jsonText = File.ReadAllText(path, System.Text.Encoding.UTF8);

            List<string[]> values = GetValues(jsonText);
            List<ParticipantModel> finalList = null;
            try {
                finalList = ToParticipantObjectList(values);
            } catch (Exception ex) {
                Debug.LogError(ex.Message);
            }

            return finalList;
        }

        private List<string[]> GetValues(string completeText) {
            List<string[]> res = new List<string[]>();
            string[] allLines = completeText.Split('\n');
            for (int i = 1; i < allLines.Length; ++i) {
                string[] separated = allLines[i].Split(';');
                if (!string.IsNullOrEmpty(separated[0])) {
                    res.Add(separated);
                }
            }
            return res;
        }

        private List<ParticipantModel> ToParticipantObjectList(List<string[]> participantStrings) {
            List<ParticipantModel> res = new List<ParticipantModel>();

            try {
                foreach (string[] participantStr in participantStrings) {
                    string name = participantStr[(int)FieldsOrder.Name];
                    string surnames = participantStr[(int)FieldsOrder.Surnames];
                    Ranks rank = ManageRank(participantStr[(int)FieldsOrder.Rank]);
                    List<Styles> styles = ManageStyles(participantStr[(int)FieldsOrder.Styles]);
                    string schoolName = participantStr[(int)FieldsOrder.SchoolName];
                    string academyName = participantStr[(int)FieldsOrder.AcademyName];
                    string country = ManageCountry(participantStr[(int)FieldsOrder.Country]);

                    ParticipantModel participant = new ParticipantModel(country, name, surnames, rank, styles, schoolName, academyName);
                    res.Add(participant);
                }
            } catch (Exception ex) {
                throw ex;
            }

            return res;
        }

        private Ranks ManageRank(string rankStr) {
            rankStr = rankStr.Replace("\r", "");

            string[] rankNames = Enum.GetNames(typeof(Ranks));
            if (rankNames.Contains(rankStr)) {
                return (Ranks)Enum.Parse(typeof(Ranks), rankStr);
            }

            throw new Exception("ERROR: No coincidence for Rank '" + rankStr + "'. Please, review your CSV");
        }

        private List<Styles> ManageStyles(string stylesStrArr) {
            List<Styles> res = new List<Styles>();
            string[] styleNames = Enum.GetNames(typeof(Styles));

            stylesStrArr = stylesStrArr.Replace(" ", "");
            stylesStrArr = stylesStrArr.Replace("\r", "");

            string[] stylesStr = stylesStrArr.Split(',');

            foreach (string style in stylesStr) {
                if (styleNames.Contains(style)) {
                    res.Add((Styles)Enum.Parse(typeof(Styles), style));
                } else {
                    throw new Exception("ERROR: No coincidence for Style '" + style + "'. Please, review your CSV");
                }
            }

            return res;
        }

        private string ManageCountry(string countryStr) {
            countryStr = countryStr.Replace("\r", "");

            if (CountriesDataUtils.IsCountryNameInList(countryStr)) {
                return CountriesDataUtils.GetCodeByName(countryStr);
            }

            if (CountriesDataUtils.IsCountryCodeInList(countryStr)) {
                return countryStr;
            }

            throw new Exception("ERROR: No coincidence for Country '" + countryStr + "'. Please, review your CSV");
        }
        #endregion

    }
}
