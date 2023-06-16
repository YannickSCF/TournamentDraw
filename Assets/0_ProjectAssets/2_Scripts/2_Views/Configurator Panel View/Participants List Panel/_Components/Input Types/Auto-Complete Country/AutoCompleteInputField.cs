using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YannickSCF.CountriesData;
using static TMPro.TMP_Dropdown;

namespace YannickSCF.TournamentDraw.Views.Configurator.ParticipantList.Components.InputType.AutoCompleteCountry {
    public class AutoCompleteInputField : MonoBehaviour {

        public delegate void FinalValue(string finalValue);
        public event FinalValue OnFinalValueSetted;

        [Header("Basic object references")]
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Image captionImage;
        [SerializeField] private ScrollRect optionsScrollRect;

        [Header("Options")]
        [SerializeField] private OptionDataList options;

        private string lastText = "";

        #region Mono
        public void Awake() {
            SetFinalValue(null, true);
        }

        private void OnEnable() {
            inputField.onValueChanged.AddListener(OnInputFieldChanged);
        }

        private void OnDisable() {
            inputField.onValueChanged.RemoveAllListeners();
        }
        #endregion

        public void SetInitValue(string countryCode, bool withoutNotify = false) {
            OptionData initOption = new OptionData(countryCode, CountriesDataUtils.GetFlagByCode(countryCode));
            SetFinalValue(initOption, withoutNotify);
        }

        public string GetValue() {
            return inputField.text;
        }

        private void OnInputFieldChanged(string text) {
            SetFinalValue(null);

            if (string.IsNullOrEmpty(text)) {
                optionsScrollRect.gameObject.SetActive(false);
                options.options.Clear();
                ClearObjectList();
                lastText = string.Empty;
                return;
            }

            if (text.All(char.IsLetter) && text.Length <= 2) {
                lastText = text.ToUpper();
                inputField.SetTextWithoutNotify(lastText);
                SetValidOptions(lastText);
                ShowValidOptions();
            } else {
                inputField.SetTextWithoutNotify(lastText);
            }
        }

        private void SetValidOptions(string text) {
            options.options.Clear();

            Dictionary<string, Sprite> namesOptions = CountriesDataUtils.SearchCountriesByCode(text);
            foreach (KeyValuePair<string, Sprite> nameOption in namesOptions) {
                options.options.Add(new OptionData(nameOption.Key, nameOption.Value));
            }
        }

        private void ShowValidOptions() {
            if (options.options.Count == 0) {
                optionsScrollRect.gameObject.SetActive(false);
            } else if (options.options.Count == 1) {
                SetFinalValue(options.options[0]);
            } else {
                GameObject templateItem = optionsScrollRect.content.GetChild(0).gameObject;
                for (int i = 0; i < options.options.Count; ++i) {
                    GameObject listItem;
                    if (i < optionsScrollRect.content.childCount) {
                        listItem = optionsScrollRect.content.GetChild(i).gameObject;
                    } else {
                        listItem = Instantiate(templateItem.gameObject, optionsScrollRect.content.transform);
                    }

                    listItem.GetComponentInChildren<TextMeshProUGUI>().text = options.options[i].text;
                    if (options.options[i].image != null) {
                        listItem.GetComponentsInChildren<Image>()[1].sprite = options.options[i].image;
                    }
                }

                optionsScrollRect.gameObject.SetActive(true);
            }

            ClearObjectList();
        }

        private void ClearObjectList() {
            int index = 0;
            foreach (Transform child in optionsScrollRect.content) {
                if (index != 0) {
                    if (index >= options.options.Count) {
                        Destroy(child.gameObject);
                    }
                }

                index++;
            }
        }

        private void SetFinalValue(OptionData optionData, bool withoutNotify = false) {
            if (optionData != null && optionData.image != null) {
                captionImage.sprite = optionData.image;
                captionImage.color = Color.white;
                inputField.SetTextWithoutNotify(optionData.text);
                optionsScrollRect.gameObject.SetActive(false);

                if (!withoutNotify) {
                    OnFinalValueSetted?.Invoke(CountriesDataUtils.GetNameByCode(optionData.text, null));
                }
            } else {
                captionImage.sprite = null;
                captionImage.color = new Color(1, 1, 1, 0);

                if (!withoutNotify) {
                    OnFinalValueSetted?.Invoke("");
                }
            }
        }

        public void SetClickFinalValue(TextMeshProUGUI itemLabel) {
            SetFinalValue(options.options.Where(x => x.text.Equals(itemLabel.text)).FirstOrDefault());
        }
    }
}
