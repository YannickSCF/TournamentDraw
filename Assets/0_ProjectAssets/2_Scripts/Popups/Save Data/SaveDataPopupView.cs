using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.GeneralApp.View.UI.Popups;

namespace YannickSCF.TournamentDraw.Popups {
    public class SaveDataPopupView : PopupView {

        public event CommonEventsDelegates.SimpleEvent OnCloseButtonPressed;
        public event CommonEventsDelegates.SimpleEvent OnSaveJSONButtonPressed;
        public event CommonEventsDelegates.SimpleEvent OnSavePDFButtonPressed;

        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _saveInJSON;
        [SerializeField] private Button _saveInPDF;

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
    }
}
