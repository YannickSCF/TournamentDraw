using TMPro;
using UnityEngine;
using YannickSCF.TournamentDraw.Views.Poules.CompetitorRow;

namespace YannickSCF.TournamentDraw.Views.Poules {

    public class PouleView : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _pouleNameTitle;

        [Header("Poule Object values")]
        [SerializeField] private Transform _pouleContentParent;
        [SerializeField] private BasicCompetitorRow _pouleCompetitorInfoRow;

        private RectTransform _rectTransform;

        #region Mono
        private void Awake() {
            _rectTransform = GetComponent<RectTransform>();
        }
        #endregion

        public void InitPoule(string pouleName, int numberOfRows = 1) {
            _pouleNameTitle.text = pouleName;

            AddEmptyRows(numberOfRows);

            float totalHeight = transform.GetChild(0).GetComponent<RectTransform>().rect.height;
            totalHeight += numberOfRows * 25;

            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, totalHeight);
        }

        private void AddEmptyRows(int numberOfRows = 1) {
            for (int i = 0; i < numberOfRows; ++i) {
                BasicCompetitorRow newRow = Instantiate(_pouleCompetitorInfoRow.gameObject, _pouleContentParent).GetComponent<BasicCompetitorRow>();
                newRow.InitRowEmpty();
            }
        }

        public float GetWidth() {
            return _rectTransform.rect.width;
        }

        public float GetHeight() {
            return _rectTransform.rect.height;
        }
    }

}