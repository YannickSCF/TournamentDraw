using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.GeneralApp.View.UI.Windows;

namespace YannickSCF.TournamentDraw.Views.MainScene.Initial {

    public enum ButtonType { StartDraw, Settings, Exit, ContinueDraw, NewDraw, LoadDraw, Back }

    public class InitialWindowView : WindowView {

        [SerializeField] private Animator _sideMenuAnimator;

        [SerializeField] private SideMenuButton _continueButton;

        public void SetViewState(int stateInt) {
            _sideMenuAnimator.SetInteger("State", stateInt);
        }

        public void SetContinueInteractable(bool isInteractable) {
            _continueButton.SetInteractable(isInteractable);
        }

        public override void Hide() {
            StartCoroutine(WaitToHideCoroutine());
        }

        private IEnumerator WaitToHideCoroutine() {
            yield return new WaitUntil(() => _sideMenuAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle_hide"));
            base.Hide();
        }
    }
}
