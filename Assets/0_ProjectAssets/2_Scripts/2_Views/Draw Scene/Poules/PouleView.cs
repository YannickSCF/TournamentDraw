using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YannickSCF.TournamentDraw.Views.Draw.Panel.Poules.CompetitorRow;

namespace YannickSCF.TournamentDraw.Views.Draw.Panel.Poules {

    public class PouleView : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI _pouleNameTitle;

        [Header("Poule Object values")]
        [SerializeField] private Transform _pouleContentParent;
        [SerializeField] private BasicCompetitorRow _pouleCompetitorInfoRow;

        private RectTransform _rectTransform;
        private List<BasicCompetitorRow> _allRows;

        private float _rowHeight = 0;

        #region Mono
        private void Awake() {
            _rectTransform = GetComponent<RectTransform>();
        }
        #endregion

        public void InitPoule(string pouleName, int numberOfRows = 1) {
            _pouleNameTitle.text = pouleName;

            AddEmptyRows(numberOfRows);

            float totalHeight = transform.GetChild(0).GetComponent<RectTransform>().rect.height;
            totalHeight += numberOfRows * _rowHeight;

            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, totalHeight);
        }

        private void AddEmptyRows(int numberOfRows = 1) {
            _allRows = new List<BasicCompetitorRow>();
            for (int i = 0; i < numberOfRows; ++i) {
                BasicCompetitorRow newRow = Instantiate(_pouleCompetitorInfoRow.gameObject, _pouleContentParent).GetComponent<BasicCompetitorRow>();
                newRow.InitRowEmpty();
                _allRows.Add(newRow);

                float newRowHeight = newRow.GetRowHeight();
                if (_rowHeight < newRowHeight) {
                    _rowHeight = newRowHeight;
                }
            }
        }

        public void AddParticipantToPoule(string completeName, string academyName, bool revealMuted = false) {
            foreach (BasicCompetitorRow row in _allRows) {
                if (!row.IsRowFilled) {
                    row.SetNameAndAcademy(completeName, academyName, revealMuted);
                    break;
                }
            }
        }

        public void SetAthletesTextsToUpper(bool toUpper) {
            foreach (BasicCompetitorRow row in _allRows) {
                row.SetTextUpper(toUpper);
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