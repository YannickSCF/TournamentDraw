using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.TournamentDraw.Scriptables;
using YannickSCF.TournamentDraw.Views.DrawPanel.Poules;

namespace YannickSCF.TournamentDraw.Views.Draw {
    public class DrawPanelView : MonoBehaviour {

        [SerializeField] private ScrollRect pouleContent;

        [Header("Temp")]
        [SerializeField] private GridLayoutGroup layout;
        [SerializeField] private PouleView poulePrefab;

        #region Mono
        #endregion

        public void Init(DrawConfiguration config) {
            CreatePoules(config.NumberOfPoules, config.MaxPouleSize);

            SetContentSize();
        }

        private void CreatePoules(int numberOfPoules, int maxPouleSize) {
            float cellHeight = -1f;

            for (int i = 0; i < numberOfPoules; i++) {
                PouleView inputField = Instantiate(poulePrefab.gameObject, pouleContent.content).GetComponent<PouleView>();
                inputField.InitPoule("Poule " + (i + 1), maxPouleSize);

                if (cellHeight < 0) {
                    cellHeight = inputField.GetHeight();
                }
            }

            layout.cellSize = new Vector2(layout.cellSize.x, cellHeight);
        }

        public void SetContentSize() {
            pouleContent.content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 760);
        }
    }
}
