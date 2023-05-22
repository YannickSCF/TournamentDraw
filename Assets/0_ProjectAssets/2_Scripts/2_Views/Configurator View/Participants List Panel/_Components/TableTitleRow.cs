using UnityEngine;

namespace YannickSCF.TournamentDraw.Views.Configurator.ParticipantList.Components {
    public class TableTitleRow : MonoBehaviour {

        [SerializeField] private GameObject countryField;
        [SerializeField] private GameObject surnameField;
        [SerializeField] private GameObject nameField;
        [SerializeField] private GameObject rankField;
        [SerializeField] private GameObject stylesField;
        [SerializeField] private GameObject academyField;
        [SerializeField] private GameObject schoolField;
        [SerializeField] private GameObject tierField;

        public void ShowColumn(ParticipantBasicInfo column, bool show) {
            switch (column) {
                case ParticipantBasicInfo.Country:
                    countryField.SetActive(show);
                    break;
                case ParticipantBasicInfo.Rank:
                    rankField.SetActive(show);
                    break;
                case ParticipantBasicInfo.Styles:
                    stylesField.SetActive(show);
                    break;
                case ParticipantBasicInfo.Academy:
                    academyField.SetActive(show);
                    break;
                case ParticipantBasicInfo.School:
                    schoolField.SetActive(show);
                    break;
                case ParticipantBasicInfo.Tier:
                    tierField.SetActive(show);
                    break;
                case ParticipantBasicInfo.Surname:
                case ParticipantBasicInfo.Name:
                default:
                    break;
            }
        }
    }
}
