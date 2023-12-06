using TMPro;
using UnityEngine;
using static YannickSCF.GeneralApp.CommonEventsDelegates;

namespace YannickSCF.TournamentDraw.Views.Draw.Panel.Poules.CompetitorRow {
    public class BasicCompetitorRow : MonoBehaviour {

        public static event StringEventDelegate PouleNameRevealed;

        [SerializeField] private Animator _revealAnimator;
        [Header("Basic Competitor INFO")]
        [SerializeField] private TextMeshProUGUI _competitorNameText;
        [SerializeField] private TextMeshProUGUI _competitorAcademyText;

        private bool rowFilled = false;
        private bool _showWithoutSound = false;

        public bool IsRowFilled { get => rowFilled; }

        public virtual void InitRowEmpty() {
            _competitorNameText.text = string.Empty;
            _competitorAcademyText.text = string.Empty;
            rowFilled = false;
        }

        public void SetNameAndAcademy(string competitorName, string academyName, bool revealMuted = false) {
            _showWithoutSound = revealMuted;

            _competitorNameText.text = competitorName;
            _competitorAcademyText.text = academyName;

            _revealAnimator.SetTrigger("Open");

            rowFilled = true;
        }

        public void SetTextUpper(bool toUpper) {
            bool isAlreadyUpper = (_competitorNameText.fontStyle & FontStyles.SmallCaps) == FontStyles.SmallCaps;
            if (toUpper != isAlreadyUpper) {
                _competitorNameText.fontStyle ^= FontStyles.SmallCaps;
                _competitorAcademyText.fontStyle ^= FontStyles.SmallCaps;
            }
        }

        public void ResetRow() {
            _competitorNameText.text = string.Empty;
            _competitorAcademyText.text = string.Empty;
            rowFilled = false;
        }

        public float GetRowHeight() {
            return GetComponent<RectTransform>().sizeDelta.y;
        }

        public void PlayLightsaberSFX() {
            if (!_showWithoutSound) {
                PouleNameRevealed?.Invoke("lightsaber-move");
            }
        }
    }
}
