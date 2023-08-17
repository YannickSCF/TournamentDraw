using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.GeneralApp.View.UI.Popups;
using static YannickSCF.GeneralApp.CommonEventsDelegates;

namespace YannickSCF.TournamentDraw.Popups {
    public class SaveDataPopupView : PopupView {

        public event SimpleEventDelegate OnCloseButtonPressed;
        public event SimpleEventDelegate OnSaveJSONButtonPressed;
        public event SimpleEventDelegate OnSavePDFButtonPressed;

        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _saveInJSON;
        [SerializeField] private Button _saveInPDF;

        [SerializeField] private Animator _popupAnimator;

        #region Mono
        private void OnEnable() {
            _closeButton.onClick.AddListener(() => OnCloseButtonPressed?.Invoke());
            _saveInJSON.onClick.AddListener(() => OnSaveJSONButtonPressed?.Invoke());
            _saveInPDF.onClick.AddListener(() => OnSavePDFButtonPressed?.Invoke());
        }

        private void OnDisable() {
            _closeButton.onClick.RemoveAllListeners();
            _saveInJSON.onClick.RemoveAllListeners();
            _saveInPDF.onClick.RemoveAllListeners();
        }
        #endregion

        public override void Show() {
            base.Show();
            _popupAnimator.SetBool("Show", true);
        }

        public override void Hide() {
            _popupAnimator.SetBool("Show", false);
            StartCoroutine(WaitToHideCoroutine());
        }

        private IEnumerator WaitToHideCoroutine() {
            yield return new WaitUntil(() => _popupAnimator.GetCurrentAnimatorStateInfo(0).IsName("popup_out_idle"));
            base.Hide();
        }
    }
}
