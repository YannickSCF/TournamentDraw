using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.TournamentDraw.Views.Draw.Panel.Poules;

public class TempPouleColocator : MonoBehaviour {

    [SerializeField] private float _separatorPercentage = 0.03f;
    [SerializeField] private float _poulePercentage = 0.225f;

    [SerializeField, Range(1, 16)] private int _poulesToInstantiate;
    [SerializeField] private GameObject _poulePrefab;

    [SerializeField] private ScrollRect _scrollRect;

    [ContextMenu("Text Poules")]
    public void TestInstantiatePoules() {
        CleanChilds();
        InstantiatePoules(_poulesToInstantiate);
    }

    [ContextMenu("Clean Poules")]
    public void CleanChilds() {
        List<Transform> allChilds = new List<Transform>();
        foreach (Transform child in _scrollRect.content) {
            allChilds.Add(child);
        }

        for (int i = 0; i < allChilds.Count; ++i) {
            Destroy(allChilds[i].gameObject);
        }
    }

    private void InstantiatePoules(int numberOfPoules) {
        PouleView pouleView = Instantiate(_poulePrefab, _scrollRect.content).GetComponent<PouleView>();
        pouleView.InitPoule("Hola", 8);
        Vector2 pouleSize = new Vector2(_scrollRect.content.rect.size.x * _poulePercentage, pouleView.GetComponent<RectTransform>().rect.size.y);

        GridLayoutGroup gridLayout = _scrollRect.content.GetComponent<GridLayoutGroup>();
        gridLayout.cellSize = pouleSize;
        gridLayout.spacing = new Vector2(_scrollRect.content.rect.size.x * _separatorPercentage, _scrollRect.content.rect.size.x * _separatorPercentage);

        for (int i = 1; i < numberOfPoules; ++i) {
            Instantiate(_poulePrefab, _scrollRect.content).GetComponent<PouleView>().InitPoule("Hola", 8);
        }

        _scrollRect.content.offsetMin = new Vector2(0, 0);

        float totalHeight = (float)(Math.Ceiling(numberOfPoules / 4.0) * gridLayout.cellSize.y + (Math.Ceiling(numberOfPoules / 4.0) - 1) * gridLayout.spacing.y) + 200;
        if (_scrollRect.content.rect.size.y < totalHeight) {
            _scrollRect.content.offsetMin = new Vector2(0, _scrollRect.content.rect.size.y - totalHeight);
        }
    }
}
