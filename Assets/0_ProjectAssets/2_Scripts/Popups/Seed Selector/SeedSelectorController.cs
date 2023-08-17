using System;
using YannickSCF.GeneralApp.Controller.UI.Popups;

namespace YannickSCF.TournamentDraw.Popups {
    public class SeedPopupData : PopupData {
        private Action _closePopupAction;
        private Action<int> _finishSelectionAction;

        public SeedPopupData(
            string popupId,
            Action closePopupAction,
            Action<int> finishSelectionAction) : base(popupId) {
            _closePopupAction = closePopupAction;
            _finishSelectionAction = finishSelectionAction;
        }

        public Action ClosePopupAction { get => _closePopupAction; }
        public Action<int> FinishSelectionAction { get => _finishSelectionAction; }
    }

    public class SeedSelectorController : PopupController {

        private SeedSelectorView _seedView;

        private int _seed = 0;

        private Action<int> _onFinishSelection;
        private Action _onClose;

        System.Random rnd = new System.Random();

        #region Mono
        private void Awake() {
            _seedView = GetView<SeedSelectorView>();
        }

        private void Start() {
            _seed = rnd.Next();
            _seedView.SetNewOption(_seed);
        }

        protected override void OnEnable() {
            base.OnEnable();
            _seedView.OnSeedChanged += ChangeSeed;

            _seedView.OnRandomizedSeed += RandomizeSeed;
            _seedView.OnFinishedSelection += FinishSelection;
            _seedView.OnCloseSelection += CloseSelection;
        }

        protected override void OnDisable() {
            base.OnDisable();
            _seedView.OnSeedChanged -= ChangeSeed;

            _seedView.OnRandomizedSeed -= RandomizeSeed;
            _seedView.OnFinishedSelection -= FinishSelection;
            _seedView.OnCloseSelection -= CloseSelection;
        }
        #endregion

        #region Event listeners methods
        private void ChangeSeed(string strValue) {
            if (int.TryParse(strValue, out int newSeed)) {
                _seed = newSeed;
                _seedView.SetFinishButtonInteractable(true);
            } else {
                _seedView.SetEmptyOption();
                _seedView.SetFinishButtonInteractable(false);
            }
        }

        private void RandomizeSeed() {
            int newSeed = rnd.Next();
            _seedView.SetNewOption(newSeed);
        }

        private void FinishSelection() {
            _onFinishSelection?.Invoke(_seed);
        }

        private void CloseSelection() {
            _onClose?.Invoke();
        }
        #endregion

        public override void Init(PopupData popupData) {
            SeedPopupData seedPopupData = (SeedPopupData)popupData;

            _onClose = seedPopupData.ClosePopupAction;
            _onFinishSelection = seedPopupData.FinishSelectionAction;

            base.Init(popupData);
        }
    }
}
