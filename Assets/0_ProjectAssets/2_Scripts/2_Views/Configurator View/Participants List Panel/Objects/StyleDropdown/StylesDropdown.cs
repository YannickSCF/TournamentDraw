using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static TMPro.TMP_Dropdown;

namespace YannickSCF.TournamentDraw.Settings.View.Componets {
    public class StylesDropdown : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        [Header("Basic object references")]
        [SerializeField] private TextMeshProUGUI inputField;
        [SerializeField] private Button dropdownButton;
        [SerializeField] private ScrollRect optionsScrollRect;

        private List<StyleCheckbox> stylesCheckboxes = new List<StyleCheckbox>();
        private bool[] stylesList = new bool[Enum.GetValues(typeof(Styles)).Length];

        bool isPointerOver = false;

        #region Mono
        private void Awake() {
            foreach (Transform styleItem in optionsScrollRect.content) {
                stylesCheckboxes.Add(styleItem.GetComponent<StyleCheckbox>());
            }
        }

        private void Update() {
            if (optionsScrollRect.gameObject.activeSelf) {
                if (Input.GetMouseButtonDown(0) && !isPointerOver) {
                    ToggleDropdown();
                }
            }
        }

        private void OnEnable() {
            dropdownButton.onClick.AddListener(ToggleDropdown);

            foreach (StyleCheckbox styleCheckbox in stylesCheckboxes) {
                styleCheckbox.OnCheckboxClicked += AddMarkedStyle;
            }
        }

        private void OnDisable() {
            dropdownButton.onClick.RemoveAllListeners();

            foreach (StyleCheckbox styleCheckbox in stylesCheckboxes) {
                styleCheckbox.OnCheckboxClicked -= AddMarkedStyle;
            }
        }
        #endregion

        #region Implmented interfaces methods
        public void OnPointerEnter(PointerEventData eventData) {
            isPointerOver = true;
        }

        public void OnPointerExit(PointerEventData eventData) {
            isPointerOver = false;
        }
        #endregion

        #region Public methods
        public void SetInitValues(List<Styles> styles) {
            foreach (Styles style in styles) {
                AddMarkedStyle(style, true);
                stylesCheckboxes[(int)style].SetInitValue(true);
            }
        }

        public List<Styles> GetValues() {
            List<Styles> res = new List<Styles>();
            Array stylesValues = Enum.GetValues(typeof(Styles));
            for (int i = 0; i < stylesList.Length; ++i) {
                if (stylesList[i]) {
                    res.Add((Styles)stylesValues.GetValue(i));
                }
            }

            return res;
        }
        #endregion

        private void ToggleDropdown() {
            if (!optionsScrollRect.gameObject.activeSelf) {
                optionsScrollRect.gameObject.SetActive(true);
            } else {
                optionsScrollRect.gameObject.SetActive(false);
            }
        }

        private void AddMarkedStyle(Styles styleCheckbox, bool checkboxValue) {
            stylesList[(int)styleCheckbox] = checkboxValue;

            int stylesCount = stylesList.Count(x => x);
            if (stylesCount != 1) {
                inputField.text = stylesCount + " Styles";
            } else {
                inputField.text = Enum.GetName(typeof(Styles), Array.IndexOf(stylesList, true));
            }
        }
    }
}
