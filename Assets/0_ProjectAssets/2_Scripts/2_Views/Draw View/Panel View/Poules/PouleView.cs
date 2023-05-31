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
        private List<BasicCompetitorRow> allRows;

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
            allRows = new List<BasicCompetitorRow>();
            for (int i = 0; i < numberOfRows; ++i) {
                BasicCompetitorRow newRow = Instantiate(_pouleCompetitorInfoRow.gameObject, _pouleContentParent).GetComponent<BasicCompetitorRow>();
                newRow.InitRowEmpty();
                allRows.Add(newRow);
            }
        }

        public void AddParticipantToPoule(string completeName, string academyName) {
            foreach (BasicCompetitorRow row in allRows) {
                if (!row.IsRowFilled) {
                    row.SetNameAndAcademy(completeName, academyName);
                    break;
                }
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