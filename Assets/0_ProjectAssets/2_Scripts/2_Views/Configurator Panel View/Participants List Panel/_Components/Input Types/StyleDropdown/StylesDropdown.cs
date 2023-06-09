using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YannickSCF.TournamentDraw.Views.Configurator.ParticipantList.Components.InputType.StyleDropdown {
    public class StylesDropdown : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        public delegate void StylesSelected(List<Styles> styles);
        public event StylesSelected OnStylesSetted;

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
        public void SetInitValues(List<Styles> styles, bool withoutNotify = false) {
            foreach (Styles style in styles) {
                AddMarkedStyle(style, true, withoutNotify);
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

        private void AddMarkedStyle(Styles styleCheckbox, bool checkboxValue, bool withoutNotify = false) {
            stylesList[(int)styleCheckbox] = checkboxValue;

            int stylesCount = stylesList.Count(x => x);
            if (stylesCount != 1) {
                inputField.text = stylesCount + " Styles";
            } else {
                inputField.text = Enum.GetName(typeof(Styles), Array.IndexOf(stylesList, true));
            }

            if (!withoutNotify) {
                List<Styles> styles = new List<Styles>();
                for (int i = 0; i < stylesList.Length; ++i) {
                    if (stylesList[i]) {
                        styles.Add((Styles)i);
                    }
                }

                OnStylesSetted?.Invoke(styles);
            }
        }
    }
}
