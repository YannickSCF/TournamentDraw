using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.TournamentDraw.Views.Draw.Panel.Poules;
using YannickSCF.TournamentDraw.Views.DrawScene.Events;

namespace YannickSCF.TournamentDraw.Views.DrawScene {
    public class DrawSceneView : MonoBehaviour {

        public enum DrawScenePhaseView { ToStart, OnGoing, Finished };

        [Header("Main Scene Objects")]
        [SerializeField] private TextMeshProUGUI _titleText;
        [Header("Draw flow objects")]
        [SerializeField] private Button _startDrawButton;
        [SerializeField] private Button _nextParticipantButton;
        [SerializeField] private Button _saveDrawButton;

        [Header("Settings objects")]
        [SerializeField] private Button _settingsMenuButton;

        [Header("Poules objects")]
        [SerializeField] private ScrollRect _poulesSpace;
        [SerializeField] private PouleView _poulePrefab;

        private GridLayoutGroup _poluesSpaceLayout;
        private List<PouleView> _allPouleViews;

        #region Mono
        private void Awake() {
            _poluesSpaceLayout = _poulesSpace.content.GetComponent<GridLayoutGroup>();
        }

        private void OnEnable() {
            _startDrawButton.onClick.AddListener(() => DrawPanelViewEvents.ThrowOnStartButtonClicked());
            _nextParticipantButton.onClick.AddListener(() => DrawPanelViewEvents.ThrowOnNextButtonClicked());
            _saveDrawButton.onClick.AddListener(() => DrawPanelViewEvents.ThrowOnSaveButtonClicked());
        }

        private void OnDisable() {
            _startDrawButton.onClick.RemoveAllListeners();
            _nextParticipantButton.onClick.RemoveAllListeners();
            _saveDrawButton.onClick.RemoveAllListeners();
        }
        #endregion

        public void Init(string drawTitle, int numberOfPoules, int maxPouleSize, int participantsAlreadyRevealed = 0) {
            _titleText.text = drawTitle;
            CreatePoules(numberOfPoules, maxPouleSize);

            if (participantsAlreadyRevealed > 0) {
                for (int i = 0; i < participantsAlreadyRevealed; ++i) {
                    DrawPanelViewEvents.ThrowOnNextButtonClicked();
                }
            }

            SwitchDrawPhaseView(DrawScenePhaseView.ToStart);
        }

        private void CreatePoules(int numberOfPoules, int maxPouleSize) {
            float cellHeight = -1f;

            _allPouleViews = new List<PouleView>();
            for (int i = 0; i < numberOfPoules; i++) {
                PouleView inputField = Instantiate(_poulePrefab.gameObject, _poulesSpace.content).GetComponent<PouleView>();
                inputField.InitPoule("Poule " + (i + 1), maxPouleSize);

                _allPouleViews.Add(inputField);
                if (cellHeight < 0) {
                    cellHeight = inputField.GetHeight();
                }
            }

            _poluesSpaceLayout.cellSize = new Vector2(_poluesSpaceLayout.cellSize.x, cellHeight);
        }

        public void AddParticipantToPoule(string completeName, string academyName, int pouleIndex) {
            _allPouleViews[pouleIndex].AddParticipantToPoule(completeName, academyName);
        }

        public void SwitchDrawPhaseView(DrawScenePhaseView phaseToSwitch) {
            switch (phaseToSwitch) {
                case DrawScenePhaseView.ToStart:
                default:
                    _startDrawButton.gameObject.SetActive(true);
                    
                    _nextParticipantButton.gameObject.SetActive(true);
                    _nextParticipantButton.interactable = false;

                    _saveDrawButton.gameObject.SetActive(false);

                    _poulesSpace.content.gameObject.SetActive(false);
                    break;
                case DrawScenePhaseView.OnGoing:
                    _startDrawButton.gameObject.SetActive(false);
                    
                    _nextParticipantButton.gameObject.SetActive(true);
                    _nextParticipantButton.interactable = true;
                    
                    _saveDrawButton.gameObject.SetActive(false);

                    _poulesSpace.content.gameObject.SetActive(true);
                    break;
                case DrawScenePhaseView.Finished:
                    _startDrawButton.gameObject.SetActive(false);
                    
                    _nextParticipantButton.gameObject.SetActive(false);
                    _nextParticipantButton.interactable = false;
                    
                    _saveDrawButton.gameObject.SetActive(true);

                    _poulesSpace.content.gameObject.SetActive(true);
                    break;
            }
        }
    }
}
