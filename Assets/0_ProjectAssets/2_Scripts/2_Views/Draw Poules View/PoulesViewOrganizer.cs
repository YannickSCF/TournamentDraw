using UnityEngine;
using UnityEngine.UI;

namespace YannickSCF.TournamentDraw.Views.Poules {
    
    public class PoulesViewOrganizer : MonoBehaviour {
        [SerializeField] private GridLayoutGroup layout;

        [SerializeField] private int numberOfPoules;
        [SerializeField] private PouleView poulePrefab;

        private void Start() {
            RectTransform parentRect = gameObject.GetComponent<RectTransform>();
            float cellHeight = -1f;

            for (int i = 0; i < numberOfPoules; i++) {
                PouleView inputField = Instantiate(poulePrefab.gameObject, transform).GetComponent<PouleView>();
                inputField.InitPoule("Poule " + (i + 1), 11);

                if (cellHeight < 0) {
                    cellHeight = inputField.GetHeight();
                }
            }

            float numberOfRowsFloat = parentRect.rect.height / cellHeight;
            if (numberOfRowsFloat - (int)numberOfRowsFloat == 0) {
                layout.constraintCount = (int)numberOfRowsFloat - 1;
            } else {
                layout.constraintCount = (int)(parentRect.rect.height / cellHeight);
            }

            float ySpacing = (parentRect.rect.height - cellHeight * layout.constraintCount) / layout.constraintCount;
            layout.spacing = new Vector2(layout.spacing.x, ySpacing < layout.spacing.x ? ySpacing : layout.spacing.x);

            int numberOfCols = (int)System.Math.Ceiling((double)numberOfPoules / layout.constraintCount);

            float cellWidth = (parentRect.rect.width - layout.spacing.x * (numberOfCols - 1)) / numberOfCols;
            layout.cellSize = new Vector2(cellWidth, cellHeight);
        }
    }
}
