using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using YannickSCF.GeneralApp.Controller.UI.Popups;

namespace YannickSCF.TournamentDraw.Popups {
    public class SeedSelectorController : PopupController<SeedSelectorView> {

        private int _seed = 0;

        private Action<int> _onFinishSelection;

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
        }

        protected override void OnDisable() {
            base.OnDisable();
            View.OnSeedChanged -= ChangeSeed;

            View.OnRandomizedSeed -= RandomizeSeed;
            View.OnFinishedSelection -= FinishSelection;
        }
        #endregion

        public void SetCallbacks(Action<int> onFinishSelection) {
            _onFinishSelection = onFinishSelection;
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

        public override void Close() {
            base.Close();
        }
    }
}
