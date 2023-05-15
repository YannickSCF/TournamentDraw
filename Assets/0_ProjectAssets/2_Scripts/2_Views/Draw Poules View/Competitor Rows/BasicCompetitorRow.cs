using TMPro;
using UnityEngine;

namespace YannickSCF.TournamentDraw.Views.Poules.CompetitorRow {
    public class BasicCompetitorRow : MonoBehaviour {

        [Header("Basic Competitor INFO")]
        [SerializeField] private TextMeshProUGUI _competitorNameText;
        [SerializeField] private TextMeshProUGUI _competitorAcademyText;

        public virtual void InitRowEmpty() {
            _competitorNameText.text = "";
            _competitorAcademyText.text = "";
        }

        public void SetNameAndAcademy(string competitorName, string academyName) {
            _competitorNameText.text = competitorName;
            _competitorAcademyText.text = academyName;
        }
    }
}
