using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YannickSCF.TournamentDraw.Views.Configurator.ParticipantList.Components.InputType.StyleDropdown {
    public class StyleCheckbox : MonoBehaviour {

        public delegate void CheckboxClicked(Styles styleCheckbox, bool checkboxValue, bool withoutNotify = false);
        public event CheckboxClicked OnCheckboxClicked;

        [SerializeField] private Styles styleCheckbox;
        [SerializeField] private TextMeshProUGUI styleLabel;
        [SerializeField] private Button checkboxButton;
        [SerializeField] private Image checkboxSelectedImage;

        #region Mono
        private void OnEnable() {
            checkboxButton.onClick.AddListener(CheckStyleBox);
        }
        private void OnDisable() {
            checkboxButton.onClick.RemoveAllListeners();
        }
        #endregion

        public void SetInitValue(bool checkboxValue = false) {
            //styleLabel.text = Enum.GetName(typeof(Styles), styleCheckbox);
            checkboxSelectedImage.gameObject.SetActive(checkboxValue);
        }

        private void CheckStyleBox() {
            checkboxSelectedImage.gameObject.SetActive(!checkboxSelectedImage.gameObject.activeSelf);
            OnCheckboxClicked?.Invoke(styleCheckbox, checkboxSelectedImage.gameObject.activeSelf);
        }
    }
}
