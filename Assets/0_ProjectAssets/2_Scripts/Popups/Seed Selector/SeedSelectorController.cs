using System;
using YannickSCF.GeneralApp.Controller.UI.Popups;

namespace YannickSCF.TournamentDraw.Popups {
    public class SeedSelectorController : PopupController<SeedSelectorView> {

        private int _seed = 0;

        private Action<int> _onFinishSelection;
        private Action _onClose;

        System.Random rnd = new System.Random();

        #region Mono
        private void Start() {
            _seed = rnd.Next();
            View.SetNewOption(_seed);
        }

        protected override void OnEnable() {
            base.OnEnable();
            View.OnSeedChanged += ChangeSeed;

            View.OnRandomizedSeed += RandomizeSeed;
            View.OnFinishedSelection += FinishSelection;
            View.OnCloseSelection += CloseSelection;
        }

        protected override void OnDisable() {
            base.OnDisable();
            View.OnSeedChanged -= ChangeSeed;

            View.OnRandomizedSeed -= RandomizeSeed;
            View.OnFinishedSelection -= FinishSelection;
            View.OnCloseSelection -= CloseSelection;
        }
        #endregion

        public void SetCallbacks(Action<int> onFinishSelection, Action onClose) {
            _onFinishSelection = onFinishSelection;
            _onClose = onClose;
        }

        private void ChangeSeed(string strValue) {
            if (int.TryParse(strValue, out int newSeed)) {
                _seed = newSeed;
                View.SetFinishButtonInteractable(true);
            } else {
                View.SetEmptyOption();
                View.SetFinishButtonInteractable(false);
            }
        }

        private void RandomizeSeed() {
            int newSeed = rnd.Next();
            View.SetNewOption(newSeed);
        }

        private void FinishSelection() {
            _onFinishSelection?.Invoke(_seed);
        }

        private void CloseSelection() {
            _onClose?.Invoke();
        }

        public override void Close() {
            base.Close();
        }
    }
}
