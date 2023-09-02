using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.TournamentDraw.Views.Draw.Panel.Poules;
using YannickSCF.TournamentDraw.Views.DrawScene.Events;

namespace YannickSCF.TournamentDraw.Views.DrawScene {
    public class DrawSceneView : MonoBehaviour {

        public enum DrawScenePhaseView { Start, OnGoing, Finished };

        [Header("Main Scene Objects")]
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private Animator _animator;
        [Header("Draw flow objects")]
        [SerializeField] private Button _startDrawButton;
        [SerializeField] private Button _nextParticipantButton;
        [SerializeField] private Button _saveDrawButton;

        [Header("Settings objects")]
        [SerializeField] private Button _settingsMenuButton;

        [Header("Poules objects")]
        [SerializeField] private float _separatorPercentage = 0.03f;
        [SerializeField] private float _poulePercentage = 0.225f;
        [SerializeField] private ScrollRect _poulesSpace;
        [SerializeField] private PouleView _poulePrefab;

        private GridLayoutGroup _poluesSpaceLayout;
        private List<PouleView> _allPouleViews;

        private DrawScenePhaseView _currentState = DrawScenePhaseView.Start;
        public DrawScenePhaseView CurrentState { get => _currentState; }

        #region Mono
        private void Awake() {
            _poluesSpaceLayout = _poulesSpace.content.GetComponent<GridLayoutGroup>();
        }

        private void OnEnable() {
            _startDrawButton.onClick.AddListener(() => DrawPanelViewEvents.ThrowOnStartButtonClicked());
            _nextParticipantButton.onClick.AddListener(() => DrawPanelViewEvents.ThrowOnNextButtonClicked());
            _saveDrawButton.onClick.AddListener(() => DrawPanelViewEvents.ThrowOnSaveButtonClicked());

            _settingsMenuButton.onClick.AddListener(() => DrawPanelViewEvents.ThrowOnSettingsButtonClicked());
        }

        private void OnDisable() {
            _startDrawButton.onClick.RemoveAllListeners();
            _nextParticipantButton.onClick.RemoveAllListeners();
            _saveDrawButton.onClick.RemoveAllListeners();

            _settingsMenuButton.onClick.RemoveAllListeners();
        }
        #endregion

        public void Init(string drawTitle, int numberOfPoules, int maxPouleSize, int participantsAlreadyRevealed = 0) {
            _titleText.text = drawTitle;
            CreatePoules(numberOfPoules, maxPouleSize);

            if (participantsAlreadyRevealed > 0) {
                SwitchDrawPhaseView(DrawScenePhaseView.OnGoing);
            } else {
                SwitchDrawPhaseView(DrawScenePhaseView.Start);
            }
        }

        private void CreatePoules(int numberOfPoules, int maxPouleSize) {
            _allPouleViews = new List<PouleView>();

            PouleView pouleView = Instantiate(_poulePrefab, _poulesSpace.content).GetComponent<PouleView>();
            pouleView.InitPoule("Poule A", maxPouleSize);
            Vector2 pouleSize = new Vector2(_poulesSpace.content.rect.size.x * _poulePercentage, pouleView.GetComponent<RectTransform>().rect.size.y);

            GridLayoutGroup gridLayout = _poulesSpace.content.GetComponent<GridLayoutGroup>();
            gridLayout.cellSize = pouleSize;
            gridLayout.spacing = new Vector2(_poulesSpace.content.rect.size.x * _separatorPercentage, _poulesSpace.content.rect.size.x * _separatorPercentage);

            _allPouleViews.Add(pouleView);

            for (int i = 1; i < numberOfPoules; i++) {
                PouleView inputField = Instantiate(_poulePrefab.gameObject, _poulesSpace.content).GetComponent<PouleView>();
                inputField.InitPoule("Poule " + ((char)(65 + i)).ToString(), maxPouleSize);

                _allPouleViews.Add(inputField);
            }

            _poulesSpace.content.offsetMin = new Vector2(0, 0);

            float totalHeight = (float)(Math.Ceiling(numberOfPoules / 4.0) * gridLayout.cellSize.y + (Math.Ceiling(numberOfPoules / 4.0) - 1) * gridLayout.spacing.y) + 200;
            if (_poulesSpace.content.rect.size.y < totalHeight) {
                _poulesSpace.content.offsetMin = new Vector2(0, _poulesSpace.content.rect.size.y - totalHeight);
            }
        }

        public void AddParticipantToPoule(string completeName, string academyName, int pouleIndex, bool revealMuted) {
            _allPouleViews[pouleIndex].AddParticipantToPoule(completeName, academyName, revealMuted);
        }

        public void SwitchDrawPhaseView(DrawScenePhaseView phaseToSwitch) {
            if (_currentState != DrawScenePhaseView.Finished) {
                _currentState = phaseToSwitch;

                _animator.SetInteger("State", (int)phaseToSwitch);

                switch (phaseToSwitch) {
                    case DrawScenePhaseView.Start:
                    default:
                        break;
                    case DrawScenePhaseView.OnGoing:
                        break;
                    case DrawScenePhaseView.Finished:
                        break;
                }
            }
        }
    }
}
