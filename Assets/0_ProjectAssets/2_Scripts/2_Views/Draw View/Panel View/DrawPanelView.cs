using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.TournamentDraw.Scriptables;
using YannickSCF.TournamentDraw.Views.Draw.Panel.Poules;
using YannickSCF.TournamentDraw.Views.DrawScene.Events;

namespace YannickSCF.TournamentDraw.Views.Draw.Panel {
    public class DrawPanelView : MonoBehaviour {

        [Header("Main parameters")]
        [SerializeField] private TextMeshProUGUI _drawNameText;
        [SerializeField] private Image _bgLogoImage;

        [SerializeField] private ScrollRect pouleContent;

        [Header("Temp")]
        [SerializeField] private GridLayoutGroup layout;
        [SerializeField] private PouleView poulePrefab;
        [SerializeField] private Button nextButton;

        private List<PouleView> allPouleViews;

        #region Mono
        private void OnEnable() {
            nextButton.onClick.AddListener(SelectNextParticipant);
        }

        private void OnDisable() {
            nextButton.onClick.RemoveAllListeners();
        }
        #endregion

        public void Init(DrawConfiguration config) {
            _drawNameText.text = config.DrawName;

            CreatePoules(config.NumberOfPoules, config.MaxPouleSize);

            pouleContent.content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 760);
        }

        private void CreatePoules(int numberOfPoules, int maxPouleSize) {
            float cellHeight = -1f;

            allPouleViews = new List<PouleView>();
            for (int i = 0; i < numberOfPoules; i++) {
                PouleView inputField = Instantiate(poulePrefab.gameObject, pouleContent.content).GetComponent<PouleView>();
                inputField.InitPoule("Poule " + (i + 1), maxPouleSize);

                allPouleViews.Add(inputField);
                if (cellHeight < 0) {
                    cellHeight = inputField.GetHeight();
                }
            }

            layout.cellSize = new Vector2(layout.cellSize.x, cellHeight);
        }

        #region Events listeners methods
        private void SelectNextParticipant() {
            DrawPanelViewEvents.ThrowOnNextButtonClicked();
        }
        #endregion

        public void AddParticipantToPoule(string completeName, string academyName, int pouleIndex) {
            allPouleViews[pouleIndex].AddParticipantToPoule(completeName, academyName);
        }
    }
}
