using TMPro;
using UnityEngine;

namespace YannickSCF.TournamentDraw.Views.Draw.Panel.Poules.CompetitorRow {
    public class BasicCompetitorRow : MonoBehaviour {

        [SerializeField] private Animator _revealAnimator;
        [Header("Basic Competitor INFO")]
        [SerializeField] private TextMeshProUGUI _competitorNameText;
        [SerializeField] private TextMeshProUGUI _competitorAcademyText;

        private bool rowFilled = false;

        public bool IsRowFilled { get => rowFilled; }

        public virtual void InitRowEmpty() {
            _competitorNameText.text = string.Empty;
            _competitorAcademyText.text = string.Empty;
            rowFilled = false;
        }

        public void SetNameAndAcademy(string competitorName, string academyName) {
            _competitorNameText.text = competitorName;
            _competitorAcademyText.text = academyName;

            _revealAnimator.SetTrigger("Open");

            rowFilled = true;
        }

        public void ResetRow() {
            _competitorNameText.text = string.Empty;
            _competitorAcademyText.text = string.Empty;
            rowFilled = false;
        }
    }
}
