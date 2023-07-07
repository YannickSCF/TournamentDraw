using UnityEngine;
using UnityEngine.UI;

namespace YannickSCF.TournamentDraw.Views.MainScene.Initial {
    public class SideMenuButton : MonoBehaviour {

        [SerializeField] private ButtonType _buttonType;
        [SerializeField] private Button _button;

        #region Mono
        private void OnEnable() {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable() {
            _button.onClick.RemoveAllListeners();
        }
        #endregion

        private void OnButtonClicked() {
            InitialWindowViewEvents.ThrowOnSideButtonClicked(_buttonType);
        }

        public void SetInteractable(bool isInteractable) {
            _button.interactable = isInteractable;
        }
    }
}
