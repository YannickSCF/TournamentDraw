using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.GeneralApp.View.UI.Popups;
using static YannickSCF.GeneralApp.CommonEventsDelegates;

namespace YannickSCF.TournamentDraw.Popups {
    public class ErrorPopupView : PopupView {

        public event SimpleEventDelegate OnCloseButtonPressed;

        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Button _closeButton;

        #region Mono
        private void OnEnable() {
            _closeButton.onClick.AddListener(() => OnCloseButtonPressed?.Invoke());
        }

        private void OnDisable() {
            _closeButton.onClick.RemoveAllListeners();
        }
        #endregion

        public void SetErrorTexts(string title, string description) {
            _titleText.text = title;
            _descriptionText.text = description;
        }
    }
}
